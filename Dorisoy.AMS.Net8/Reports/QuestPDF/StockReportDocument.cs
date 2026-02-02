using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Dorisoy.AMS.Reports.QuestPDF
{
    /// <summary>
    /// 库存报表数据项
    /// </summary>
    public class StockReportItem
    {
        public string AssetID { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal AvailableQuantity { get; set; }
        public decimal BorrowedQuantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public bool IsWarning { get; set; }
    }

    /// <summary>
    /// 库存报表 PDF 文档
    /// </summary>
    public class StockReportDocument : ReportBase
    {
        private readonly List<StockReportItem> _data;
        private readonly string _warehouseName;

        protected override string ReportTitle => $"库存报表 - {_warehouseName}";

        public StockReportDocument(List<StockReportItem> data, string warehouseName)
        {
            _data = data;
            _warehouseName = warehouseName;
        }

        protected override void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(10);

                // 统计摘要
                column.Item().Element(ComposeSummary);

                // 数据表格
                column.Item().Element(ComposeTable);
            });
        }

        /// <summary>
        /// 构建统计摘要
        /// </summary>
        private void ComposeSummary(IContainer container)
        {
            var totalQuantity = _data.Sum(r => r.Quantity);
            var totalAvailable = _data.Sum(r => r.AvailableQuantity);
            var totalBorrowed = _data.Sum(r => r.BorrowedQuantity);
            var warningCount = _data.Count(r => r.IsWarning);

            container.Background(Colors.Grey.Lighten4).Padding(10).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"资产种类：{_data.Count} 种")
                        .FontSize(11).Bold();
                });

                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"总库存：{totalQuantity}")
                        .FontSize(11).Bold();
                });

                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"可用库存：{totalAvailable}")
                        .FontSize(11).Bold().FontColor(Colors.Green.Darken2);
                });

                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"借出数量：{totalBorrowed}")
                        .FontSize(11).Bold().FontColor(Colors.Orange.Darken2);
                });

                row.RelativeItem().Column(col =>
                {
                    var warningColor = warningCount > 0 ? Colors.Red.Darken2 : Colors.Grey.Darken1;
                    col.Item().Text($"库存预警：{warningCount} 项")
                        .FontSize(11).Bold().FontColor(warningColor);
                });
            });
        }

        /// <summary>
        /// 构建数据表格
        /// </summary>
        private void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // 定义列
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(30);   // 序号
                    columns.RelativeColumn(1.5f); // 资产编号
                    columns.RelativeColumn(1);    // 类别
                    columns.RelativeColumn(2);    // 名称
                    columns.RelativeColumn(1.5f); // 规格型号
                    columns.RelativeColumn(1.5f); // 所在仓库
                    columns.ConstantColumn(50);   // 总库存
                    columns.ConstantColumn(50);   // 最低库存
                    columns.ConstantColumn(50);   // 可用库存
                    columns.ConstantColumn(50);   // 借出数量
                    columns.ConstantColumn(40);   // 单位
                    columns.ConstantColumn(50);   // 状态
                });

                // 表头
                table.Header(header =>
                {
                    header.Cell().Element(HeaderCellStyle).Text("序号").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("资产编号").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("类别").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("资产名称").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("规格型号").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("所在仓库").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("总库存").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("最低库存").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("可用库存").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("借出").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("单位").FontSize(9).Bold().FontColor(Colors.White);
                    header.Cell().Element(HeaderCellStyle).Text("状态").FontSize(9).Bold().FontColor(Colors.White);
                });

                // 数据行
                for (int i = 0; i < _data.Count; i++)
                {
                    var item = _data[i];
                    var isAlternate = i % 2 == 1;
                    var cellStyle = item.IsWarning 
                        ? (Func<IContainer, IContainer>)WarningCellStyle 
                        : c => DataCellStyle(c, isAlternate);

                    table.Cell().Element(cellStyle).Text((i + 1).ToString()).FontSize(9);
                    table.Cell().Element(cellStyle).Text(item.AssetID).FontSize(9);
                    table.Cell().Element(cellStyle).Text(item.Category).FontSize(9);
                    table.Cell().Element(cellStyle).Text(item.Name).FontSize(9);
                    table.Cell().Element(cellStyle).Text(item.Model).FontSize(9);
                    table.Cell().Element(cellStyle).Text(item.Location).FontSize(9);
                    table.Cell().Element(cellStyle).AlignRight().Text(item.Quantity.ToString()).FontSize(9);
                    table.Cell().Element(cellStyle).AlignRight().Text(item.MinQuantity.ToString()).FontSize(9);
                    table.Cell().Element(cellStyle).AlignRight().Text(item.AvailableQuantity.ToString()).FontSize(9).Bold();
                    table.Cell().Element(cellStyle).AlignRight().Text(item.BorrowedQuantity.ToString()).FontSize(9);
                    table.Cell().Element(cellStyle).AlignCenter().Text(item.Unit).FontSize(9);
                    table.Cell().Element(cellStyle).AlignCenter().Text(item.StatusName).FontSize(9);
                }
            });
        }
    }
}
