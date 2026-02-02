using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.models
{
    /// <summary>
    /// 库存出入库记录
    /// </summary>
    [SugarTable("StockRecords")]
    public class StockRecord
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [DisplayName("ID")]
        public int Id { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [DisplayName("资产编号")]
        public string AssetID { get; set; } = string.Empty;

        /// <summary>
        /// 资产名称（冗余字段，方便查询显示）
        /// </summary>
        [DisplayName("资产名称")]
        public string AssetName { get; set; } = string.Empty;

        /// <summary>
        /// 仓库ID
        /// </summary>
        [DisplayName("仓库ID")]
        public int WarehouseId { get; set; }

        /// <summary>
        /// 仓库名称（冗余字段）
        /// </summary>
        [DisplayName("仓库名称")]
        public string WarehouseName { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型：1入库 2出库
        /// </summary>
        [DisplayName("操作类型")]
        public int RecordType { get; set; }

        /// <summary>
        /// 业务类型：
        /// 入库：11采购入库 12归还入库 13调拨入库 14盘盈入库 15其他入库
        /// 出库：21借用出库 22报废出库 23调拨出库 24盘亏出库 25其他出库
        /// </summary>
        [DisplayName("业务类型")]
        public int BusinessType { get; set; }

        /// <summary>
        /// 数量（正数为入库，负数为出库）
        /// </summary>
        [DisplayName("数量")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 操作前库存
        /// </summary>
        [DisplayName("操作前库存")]
        public decimal BeforeQuantity { get; set; }

        /// <summary>
        /// 操作后库存
        /// </summary>
        [DisplayName("操作后库存")]
        public decimal AfterQuantity { get; set; }

        /// <summary>
        /// 关联单号（如借用记录ID）
        /// </summary>
        [DisplayName("关联单号")]
        public string RelatedId { get; set; } = string.Empty;

        /// <summary>
        /// 操作人
        /// </summary>
        [DisplayName("操作人")]
        public string Operator { get; set; } = string.Empty;

        /// <summary>
        /// 经手人/领用人/归还人
        /// </summary>
        [DisplayName("经手人")]
        public string Handler { get; set; } = string.Empty;

        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        public DateTime RecordTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// 库存记录类型
    /// </summary>
    public static class StockRecordType
    {
        public const int In = 1;   // 入库
        public const int Out = 2;  // 出库
    }

    /// <summary>
    /// 库存业务类型
    /// </summary>
    public static class StockBusinessType
    {
        // 入库类型
        public const int PurchaseIn = 11;      // 采购入库
        public const int ReturnIn = 12;        // 归还入库
        public const int TransferIn = 13;      // 调拨入库
        public const int InventoryProfitIn = 14; // 盘盈入库
        public const int OtherIn = 15;         // 其他入库

        // 出库类型
        public const int BorrowOut = 21;       // 借用出库
        public const int ScrapOut = 22;        // 报废出库
        public const int TransferOut = 23;     // 调拨出库
        public const int InventoryLossOut = 24; // 盘亏出库
        public const int OtherOut = 25;        // 其他出库

        /// <summary>
        /// 获取业务类型名称
        /// </summary>
        public static string GetName(int businessType)
        {
            return businessType switch
            {
                PurchaseIn => "采购入库",
                ReturnIn => "归还入库",
                TransferIn => "调拨入库",
                InventoryProfitIn => "盘盈入库",
                OtherIn => "其他入库",
                BorrowOut => "借用出库",
                ScrapOut => "报废出库",
                TransferOut => "调拨出库",
                InventoryLossOut => "盘亏出库",
                OtherOut => "其他出库",
                _ => "未知类型"
            };
        }

        /// <summary>
        /// 获取所有入库类型
        /// </summary>
        public static List<KeyValuePair<int, string>> GetInTypes()
        {
            return new List<KeyValuePair<int, string>>
            {
                new(PurchaseIn, "采购入库"),
                new(ReturnIn, "归还入库"),
                new(TransferIn, "调拨入库"),
                new(InventoryProfitIn, "盘盈入库"),
                new(OtherIn, "其他入库")
            };
        }

        /// <summary>
        /// 获取所有出库类型
        /// </summary>
        public static List<KeyValuePair<int, string>> GetOutTypes()
        {
            return new List<KeyValuePair<int, string>>
            {
                new(BorrowOut, "借用出库"),
                new(ScrapOut, "报废出库"),
                new(TransferOut, "调拨出库"),
                new(InventoryLossOut, "盘亏出库"),
                new(OtherOut, "其他出库")
            };
        }
    }
}
