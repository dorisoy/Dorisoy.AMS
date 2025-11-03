using SqlSugar;
using System;

namespace Dorisoy.AMS.models
{
    public class Log
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string OperationType { get; set; }  // 操作类型
        public DateTime OperationTime { get; set; }// 操作时间
        public string Operator { get; set; }       // 操作人
        public string AssetNumber { get; set; }    // 资产编号
        public string Details { get; set; }        // 操作详情
    }
}
