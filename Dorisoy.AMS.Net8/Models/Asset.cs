namespace Dorisoy.AMS.models
{
    using SqlSugar;

    [SugarTable("Assets")]
    public class Asset
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string AssetID { get; set; } = string.Empty;        // 资产编号
        public string Category { get; set; } = string.Empty;       // 资产类别
        public string Name { get; set; } = string.Empty;          // 资产名称
        public string Model { get; set; } = string.Empty;          // 规格型号
        public decimal Quantity { get; set; }      // 数量
        public string Unit { get; set; } = string.Empty;          // 单位
        public string Location { get; set; } = string.Empty;      // 存放地点
        public string Department { get; set; } = string.Empty;    // 责任部门
        public string User { get; set; } = string.Empty;          // 使用人
        public int Status { get; set; }           // 状态（0正常 1删除 2报废 3转移 4借用）
        public string Remark { get; set; } = string.Empty;        // 备注
    }
}