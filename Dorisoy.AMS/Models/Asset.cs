namespace Dorisoy.AMS.models
{
    using SqlSugar;

    [SugarTable("Assets")]
    public class Asset
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string AssetID { get; set; }        // 资产编号
        public string Category { get; set; }       // 资产类别
        public string Name { get; set; }          // 资产名称
        public string Model { get; set; }          // 规格型号
        public decimal Quantity { get; set; }      // 数量
        public string Unit { get; set; }          // 单位
        public string Location { get; set; }      // 存放地点
        public string Department { get; set; }    // 责任部门
        public string User { get; set; }          // 使用人
        public int Status { get; set; }           // 状态（0正常 1删除 2报废 3转移 4借用）
        public string Remark { get; set; }        // 备注
        
        // 借用相关字段
        public string BorrowedBy { get; set; }    // 借用人
        public System.DateTime? BorrowedDate { get; set; }                // 借用日期
        public System.DateTime? ExpectedReturnDate { get; set; }          // 预期归还日期
        public string BorrowReason { get; set; }  // 借用原因
    }
}
