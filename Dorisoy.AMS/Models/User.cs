using SqlSugar;
using System.ComponentModel;
namespace Dorisoy.AMS.models
{
    [SugarTable("User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("用户名")]
        public string Username { get; set; }

        [DisplayName("密码")]
        public string Password { get; set; }

        [DisplayName("允许新增")]
        public bool CanAdd { get; set; }

        [DisplayName("允许编辑")]
        public bool CanEdit { get; set; }

        [DisplayName("允许删除")]
        public bool CanDelete { get; set; }

        [DisplayName("允许打印")]
        public bool CanPrint { get; set; }

        [DisplayName("允许导出")]
        public bool CanExport { get; set; }

        [DisplayName("允许导入")]
        public bool CanImport { get; set; }

        [DisplayName("管理编号")]
        public bool CanManageNumber { get; set; }

        [DisplayName("管理日志")]
        public bool CanManageLog { get; set; }

        [DisplayName("管理数据库")]
        public bool CanManageData { get; set; }

        [DisplayName("管理员")]
        public bool IsAdmin { get; set; }
    }

}
