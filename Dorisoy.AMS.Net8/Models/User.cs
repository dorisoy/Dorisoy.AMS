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
        public string Username { get; set; } = string.Empty;

        [DisplayName("密码")]
        public string Password { get; set; } = string.Empty;

        // ==================== 基础操作权限 ====================
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

        // ==================== 业务功能权限 ====================
        [DisplayName("借还登记")]
        public bool CanBorrow { get; set; }

        [DisplayName("库存记录")]
        public bool CanViewStockRecords { get; set; }

        [DisplayName("报损登记")]
        public bool CanScrap { get; set; }

        [DisplayName("盘点管理")]
        public bool CanInventoryCheck { get; set; }

        [DisplayName("库存报表")]
        public bool CanViewStockReport { get; set; }

        // ==================== 系统管理权限 ====================
        [DisplayName("编号设置")]
        public bool CanManageNumber { get; set; }

        [DisplayName("管理日志")]
        public bool CanManageLog { get; set; }

        [DisplayName("管理数据库")]
        public bool CanManageData { get; set; }

        [DisplayName("仓库管理")]
        public bool CanManageWarehouse { get; set; }

        [DisplayName("用户管理")]
        public bool CanManageUsers { get; set; }

        [DisplayName("管理员")]
        public bool IsAdmin { get; set; }
    }
}