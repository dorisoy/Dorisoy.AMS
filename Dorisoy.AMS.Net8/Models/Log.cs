using SqlSugar;

namespace Dorisoy.AMS.models
{
    [SugarTable("Logs")]
    public class Log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string OperationType { get; set; } = string.Empty;  // 操作类型

        public DateTime OperationTime { get; set; } // 操作时间

        public string Operator { get; set; } = string.Empty; // 操作人

        public string AssetNumber { get; set; } = string.Empty; // 资产编号

        public string Details { get; set; } = string.Empty; // 操作详情
    }
}