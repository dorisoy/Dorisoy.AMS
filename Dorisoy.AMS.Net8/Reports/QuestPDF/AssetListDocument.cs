using Dorisoy.AMS.models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Dorisoy.AMS.Reports.QuestPDF
{
    /// <summary>
    /// 资产清单 PDF 文档
    /// </summary>
    public class AssetListDocument : ReportBase
    {
        private readonly List<Asset> _assets;
        private readonly string _filterInfo;

        protected override string ReportTitle => "资产清单";

        public AssetListDocument(List<Asset> assets, string filterInfo = "")
        {
            _assets = assets;
            _filterInfo = filterInfo;
        }

        protected override void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(10);

                // 筛选条件信息
                if (!string.IsNullOrEmpty(_filterInfo))
                {
                    column.Item().Background(Colors.Grey.Lighten4).Padding(8).Text(_filterInfo)
                        .FontSize(10).Italic().FontColor(Colors.Grey.Darken2);
                }

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
            var totalQuantity = _assets.Sum(a => a.Quantity);
            var normalCount = _assets.Count(a => a.Status == 0);
            var borrowedCount = _assets.Count(a => a.Status == 2);

            container.Background(Colors.Blue.Lighten5).Padding(10).Row(row =>
            {
                row.RelativeItem().Text($"共 {_assets.Count} 种资产")
                    .FontSize(11).Bold();

                row.RelativeItem().Text($"总数量：{totalQuantity}")
                    .FontSize(11).Bold();

                row.RelativeItem().Text($"正常：{normalCount} 种")
                    .FontSize(11).Bold().FontColor(Colors.Green.Darken2);

                row.RelativeItem().Text($"借出：{borrowedCount} 种")
                    .FontSize(11).Bold().FontColor(Colors.Orange.Darken2);
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
                    columns.ConstantColumn(50);   // 数量
                    columns.ConstantColumn(40);   // 单位
                    columns.RelativeColumn(1.5f); // 存放地点
                    columns.RelativeColumn(1);    // 部门
                    columns.RelativeColumn(1);    // 使用人
                    columns.ConstantColumn(50);   // 状态
                });

                // 表头
                table.Header(header =>
                {
                    header.Cell().Element(HeaderCellStyle).Text("序号").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("资产编号").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("类别").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("资产名称").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("规格型号").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("数量").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("单位").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("存放地点").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("部门").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("使用人").Style(HeaderTextStyle);
                    header.Cell().Element(HeaderCellStyle).Text("状态").Style(HeaderTextStyle);
                });

                // 数据行
                for (int i = 0; i < _assets.Count; i++)
                {
                    var asset = _assets[i];
                    var isAlternate = i % 2 == 1;
                    var cellStyle = (Func<IContainer, IContainer>)(c => DataCellStyle(c, isAlternate));

                    table.Cell().Element(cellStyle).Text((i + 1).ToString()).FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.AssetID ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.Category ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.Name ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.Model ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).AlignRight().Text(asset.Quantity.ToString()).FontSize(9);
                    table.Cell().Element(cellStyle).AlignCenter().Text(asset.Unit ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.Location ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.Department ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).Text(asset.User ?? "").FontSize(9);
                    table.Cell().Element(cellStyle).AlignCenter().Text(StatusConfig.GetStatusName(asset.Status)).FontSize(9);
                }
            });
        }

        // 辅助方法：应用文本样式
        private static void HeaderTextStyle(TextSpanDescriptor text)
        {
            text.FontSize(9).Bold().FontColor(Colors.White);
        }
    }
}
