using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.models
{
    /// <summary>
    /// 仓库实体
    /// </summary>
    [SugarTable("Warehouse")]
    public class Warehouse
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [DisplayName("ID")]
        public int Id { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [DisplayName("仓库编号")]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 仓库名称
        /// </summary>
        [DisplayName("仓库名称")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 仓库地址
        /// </summary>
        [DisplayName("仓库地址")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        public string Contact { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [DisplayName("状态")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
