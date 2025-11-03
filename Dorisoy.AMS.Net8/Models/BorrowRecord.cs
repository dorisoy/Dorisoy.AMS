namespace Dorisoy.AMS.models
{
    using SqlSugar;

    [SugarTable("BorrowRecords")]
    public class BorrowRecord
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public string AssetID { get; set; } = string.Empty;        // 资产编号

        [SugarColumn(IsNullable = false)]
        public string BorrowedBy { get; set; } = string.Empty;     // 借用人

        [SugarColumn(IsNullable = false)]
        public decimal BorrowedQuantity { get; set; }              // 借用数量

        [SugarColumn(IsNullable = false)]
        public DateTime BorrowedDate { get; set; }                 // 借用日期

        [SugarColumn(IsNullable = true)]
        public DateTime? ExpectedReturnDate { get; set; }          // 预期归还日期

        [SugarColumn(IsNullable = true)]
        public DateTime? ActualReturnDate { get; set; }            // 实际归还日期

        [SugarColumn(IsNullable = true)]
        public string BorrowReason { get; set; } = string.Empty;   // 借用原因

        [SugarColumn(IsNullable = true)]
        public string ReturnRemark { get; set; } = string.Empty;   // 归还备注

        public int Status { get; set; }                             // 状态（0借用中 1已归还）
    }
}
