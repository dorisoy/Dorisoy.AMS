using Dorisoy.AMS.models;
using QuestPDF;
using QuestPDF.Fluent;

namespace Dorisoy.AMS.Reports.QuestPDF
{
    /// <summary>
    /// PDF 报表服务 - 提供各类报表的生成功能
    /// </summary>
    public static class PdfReportService
    {
        /// <summary>
        /// 静态构造函数 - 配置 QuestPDF 许可证
        /// </summary>
        static PdfReportService()
        {
            // QuestPDF 社区版许可证（免费使用）
            Settings.License = LicenseType.Community;
        }

        /// <summary>
        /// 生成库存报表 PDF
        /// </summary>
        /// <param name="data">库存数据</param>
        /// <param name="warehouseName">仓库名称</param>
        /// <param name="filePath">保存路径</param>
        public static void GenerateStockReport(List<StockReportItem> data, string warehouseName, string filePath)
        {
            var document = new StockReportDocument(data, warehouseName);
            document.GeneratePdf(filePath);
        }

        /// <summary>
        /// 生成库存报表 PDF 并返回字节数组
        /// </summary>
        public static byte[] GenerateStockReportBytes(List<StockReportItem> data, string warehouseName)
        {
            var document = new StockReportDocument(data, warehouseName);
            return document.GeneratePdf();
        }

        /// <summary>
        /// 生成库存报表 PDF 并直接打开
        /// </summary>
        public static void GenerateStockReportAndShow(List<StockReportItem> data, string warehouseName)
        {
            var document = new StockReportDocument(data, warehouseName);
            document.GeneratePdfAndShow();
        }

        /// <summary>
        /// 生成资产清单 PDF
        /// </summary>
        /// <param name="assets">资产列表</param>
        /// <param name="filterInfo">筛选条件信息</param>
        /// <param name="filePath">保存路径</param>
        public static void GenerateAssetList(List<Asset> assets, string filterInfo, string filePath)
        {
            var document = new AssetListDocument(assets, filterInfo);
            document.GeneratePdf(filePath);
        }

        /// <summary>
        /// 生成资产清单 PDF 并返回字节数组
        /// </summary>
        public static byte[] GenerateAssetListBytes(List<Asset> assets, string filterInfo)
        {
            var document = new AssetListDocument(assets, filterInfo);
            return document.GeneratePdf();
        }

        /// <summary>
        /// 生成资产清单 PDF 并直接打开
        /// </summary>
        public static void GenerateAssetListAndShow(List<Asset> assets, string filterInfo)
        {
            var document = new AssetListDocument(assets, filterInfo);
            document.GeneratePdfAndShow();
        }

        /// <summary>
        /// 从数据库加载并生成库存报表
        /// </summary>
        /// <param name="warehouseId">仓库ID（0表示全部）</param>
        /// <param name="filePath">保存路径</param>
        public static void GenerateStockReportFromDb(int warehouseId, string filePath)
        {
            using (var db = SqliteHelper.GetDb())
            {
                // 获取仓库名称
                var warehouseName = "全部仓库";
                if (warehouseId > 0)
                {
                    var warehouse = db.Queryable<Warehouse>().First(w => w.Id == warehouseId);
                    warehouseName = warehouse?.Name ?? "未知仓库";
                }

                // 查询资产
                var assets = db.Queryable<Asset>()
                    .Where(a => a.Status != 1)
                    .WhereIF(warehouseId > 0, a => a.WarehouseId == warehouseId)
                    .OrderBy(a => a.Location)
                    .OrderBy(a => a.AssetID)
                    .ToList();

                // 转换为报表数据
                var reportData = assets.Select(a =>
                {
                    var borrowedQty = db.Queryable<BorrowRecord>()
                        .Where(r => r.AssetID == a.AssetID && r.Status == 0)
                        .Sum(r => r.BorrowedQuantity);

                    return new StockReportItem
                    {
                        AssetID = a.AssetID ?? "",
                        Category = a.Category ?? "",
                        Name = a.Name ?? "",
                        Model = a.Model ?? "",
                        Location = a.Location ?? "",
                        Quantity = a.Quantity,
                        MinQuantity = a.MinQuantity,
                        AvailableQuantity = a.Quantity - borrowedQty,
                        BorrowedQuantity = borrowedQty,
                        Unit = a.Unit ?? "",
                        StatusName = StatusConfig.GetStatusName(a.Status),
                        IsWarning = a.MinQuantity > 0 && a.Quantity <= a.MinQuantity
                    };
                }).ToList();

                GenerateStockReport(reportData, warehouseName, filePath);
            }
        }

        /// <summary>
        /// 从数据库加载并生成资产清单
        /// </summary>
        /// <param name="department">部门（空表示全部）</param>
        /// <param name="status">状态（-1表示全部）</param>
        /// <param name="filePath">保存路径</param>
        public static void GenerateAssetListFromDb(string? department, int status, string filePath)
        {
            using (var db = SqliteHelper.GetDb())
            {
                var query = db.Queryable<Asset>()
                    .Where(a => a.Status != 1);  // 排除已删除

                if (!string.IsNullOrEmpty(department) && department != "全部部门")
                {
                    query = query.Where(a => a.Department == department);
                }

                if (status >= 0)
                {
                    query = query.Where(a => a.Status == status);
                }

                var assets = query.OrderBy(a => a.AssetID).ToList();

                // 构建筛选信息
                var filterParts = new List<string>();
                if (!string.IsNullOrEmpty(department) && department != "全部部门")
                {
                    filterParts.Add($"部门：{department}");
                }
                if (status >= 0)
                {
                    filterParts.Add($"状态：{StatusConfig.GetStatusName(status)}");
                }
                var filterInfo = filterParts.Count > 0 ? $"筛选条件：{string.Join("，", filterParts)}" : "";

                GenerateAssetList(assets, filterInfo, filePath);
            }
        }
    }
}
