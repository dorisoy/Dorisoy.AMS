using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Dorisoy.AMS.Reports.QuestPDF
{
    /// <summary>
    /// 报表基础类 - 提供通用的页面配置、页眉页脚等
    /// </summary>
    public abstract class ReportBase : IDocument
    {
        /// <summary>
        /// 报表标题
        /// </summary>
        protected abstract string ReportTitle { get; }

        /// <summary>
        /// 公司名称
        /// </summary>
        protected virtual string CompanyName => "Dorisoy.AMS 资产管理系统";

        /// <summary>
        /// 报表生成时间
        /// </summary>
        protected DateTime GeneratedAt { get; } = DateTime.Now;

        /// <summary>
        /// 当前操作用户
        /// </summary>
        protected string CurrentUser => AppContext.CurrentUser?.Username ?? "系统";

        /// <summary>
        /// 文档元数据
        /// </summary>
        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = ReportTitle,
            Author = CurrentUser,
            Creator = CompanyName,
            Subject = ReportTitle,
            Keywords = "资产管理,报表,PDF",
            CreationDate = GeneratedAt,
            ModifiedDate = GeneratedAt
        };

        /// <summary>
        /// 文档设置
        /// </summary>
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        /// <summary>
        /// 构建文档内容
        /// </summary>
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // 页面设置
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Microsoft YaHei"));

                // 页眉
                page.Header().Element(ComposeHeader);

                // 内容
                page.Content().Element(ComposeContent);

                // 页脚
                page.Footer().Element(ComposeFooter);
            });
        }

        /// <summary>
        /// 构建页眉
        /// </summary>
        protected virtual void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(5);

                // 标题行
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text(ReportTitle)
                            .FontSize(18)
                            .Bold()
                            .FontColor(Colors.Blue.Darken2);

                        col.Item().Text(CompanyName)
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken1);
                    });

                    row.ConstantItem(150).AlignRight().Column(col =>
                    {
                        col.Item().Text($"生成时间：{GeneratedAt:yyyy-MM-dd HH:mm}")
                            .FontSize(9)
                            .FontColor(Colors.Grey.Darken1);

                        col.Item().Text($"操作人：{CurrentUser}")
                            .FontSize(9)
                            .FontColor(Colors.Grey.Darken1);
                    });
                });

                // 分隔线
                column.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Blue.Darken2);
            });
        }

        /// <summary>
        /// 构建页脚
        /// </summary>
        protected virtual void ComposeFooter(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Text(x =>
                {
                    x.Span("第 ").FontSize(9);
                    x.CurrentPageNumber().FontSize(9);
                    x.Span(" 页 / 共 ").FontSize(9);
                    x.TotalPages().FontSize(9);
                    x.Span(" 页").FontSize(9);
                });

                row.RelativeItem().AlignRight().Text($"© {DateTime.Now.Year} {CompanyName}")
                    .FontSize(8)
                    .FontColor(Colors.Grey.Medium);
            });
        }

        /// <summary>
        /// 构建报表内容 - 由子类实现
        /// </summary>
        protected abstract void ComposeContent(IContainer container);

        /// <summary>
        /// 表头单元格样式
        /// </summary>
        protected static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background(Colors.Blue.Darken2)
                .Padding(5)
                .AlignCenter()
                .AlignMiddle();
        }

        /// <summary>
        /// 表头文本样式
        /// </summary>
        protected static void HeaderTextStyle(TextSpanDescriptor text)
        {
            text.FontSize(10).Bold().FontColor(Colors.White);
        }

        /// <summary>
        /// 数据单元格样式
        /// </summary>
        protected static IContainer DataCellStyle(IContainer container, bool isAlternate = false)
        {
            var bgColor = isAlternate ? Colors.Grey.Lighten4 : Colors.White;
            return container
                .Background(bgColor)
                .BorderBottom(0.5f)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(4)
                .AlignMiddle();
        }

        /// <summary>
        /// 预警单元格样式（红色背景）
        /// </summary>
        protected static IContainer WarningCellStyle(IContainer container)
        {
            return container
                .Background(Colors.Red.Lighten3)
                .BorderBottom(0.5f)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(4)
                .AlignMiddle();
        }
    }
}
