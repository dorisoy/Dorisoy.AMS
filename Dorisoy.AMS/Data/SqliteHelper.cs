using Dorisoy.AMS.models;
using SqlSugar;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Dorisoy.AMS
{

    public class SqliteHelper
    {

        public static SqlSugarClient GetDb()
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "DataSource=AssetDB.sqlite",
                DbType = SqlSugar.DbType.Sqlite,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }

        public static void InitDb()
        {
            using (var db = GetDb())
            {

                db.CodeFirst.InitTables(
                typeof(Asset),
                typeof(AssetRule),
                typeof(GlobalSetting),
                typeof(User),
                typeof(Log));



                // 初始化默认规则
                if (!db.Queryable<AssetRule>().Any())
                {
                    var defaultRule = new AssetRule
                    {
                        Keywords = "默认资产",
                        Prefix = "DNS",
                        NumberLength = 5
                    };

                    db.Insertable(defaultRule).ExecuteCommand();

                    // 设置默认规则
                    db.Storageable(new GlobalSetting
                    {
                        Key = "DefaultRuleId",
                        Value = defaultRule.Id.ToString()
                    }).ExecuteCommand();
                }

                // 初始化全局设置
                if (!db.Queryable<GlobalSetting>().Any(s => s.Key == "UseDefaultRule"))
                {
                    db.Insertable(new GlobalSetting
                    {
                        Key = "UseDefaultRule",
                        Value = "1" // 默认启用
                    }).ExecuteCommand();
                }

                // 添加用户表初始化
                if (!db.Queryable<User>().Any())
                {
                    var defaultUser = new User
                    {
                        Username = "admin",
                        Password = HashPassword("admin"),
                        CanAdd = true,
                        CanEdit = true,
                        CanDelete = true,
                        CanPrint = true,
                        CanExport = true,
                        CanImport = true,
                        CanManageNumber = true,
                        CanManageLog = true,
                        CanManageData = true,
                        IsAdmin = true
                    };
                    db.Insertable(defaultUser).ExecuteCommand();
                }



            }
        }

        public static bool ValidateAdminPassword(string inputPassword)
        {
            try
            {
                using (var db = GetDb())
                {
                    // 查询管理员用户
                    var adminUser = db.Queryable<User>()
                        .First(u => u.IsAdmin);

                    if (adminUser == null)
                    {
                        MessageBox.Show("未找到管理员账户");
                        return false;
                    }

                    // 哈希输入密码并比对
                    string inputHash = HashPassword(inputPassword);
                    return adminUser.Password == inputHash;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"验证失败：{ex.Message}");
                return false;
            }
        }


        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
