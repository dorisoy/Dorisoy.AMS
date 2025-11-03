using Dorisoy.AMS.models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Dorisoy.AMS.services.ExcelService
{
    public class ExcelExporter
    {
        public void ExportToExcel(List<Asset> assets, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("资产清单");

                // 创建标题行
                var headerRow = sheet.CreateRow(0);
                string[] headers = {
                "序号","资产编号", "资产类别", "资产名称", "规格型号",
                "数量", "单位", "存放地点", "责任部门",
                "使用人", "状态", "备注"
            };

                // 设置标题样式
                ICellStyle headerStyle = CreateHeaderStyle(workbook);
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = headerRow.CreateCell(i);
                    cell.SetCellValue(headers[i]);
                    cell.CellStyle = headerStyle;
                    sheet.AutoSizeColumn(i);
                }

                // 填充数据
                int rowIndex = 1;
                foreach (var asset in assets)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(rowIndex);
                    row.CreateCell(1).SetCellValue(asset.AssetID);
                    row.CreateCell(2).SetCellValue(asset.Category);
                    row.CreateCell(3).SetCellValue(asset.Name);
                    row.CreateCell(4).SetCellValue(asset.Model);
                    row.CreateCell(5).SetCellValue((double)asset.Quantity);
                    row.CreateCell(6).SetCellValue(asset.Unit);
                    row.CreateCell(7).SetCellValue(asset.Location);
                    row.CreateCell(8).SetCellValue(asset.Department);
                    row.CreateCell(9).SetCellValue(asset.User);
                    row.CreateCell(10).SetCellValue(GetStatusText(asset.Status));
                    row.CreateCell(11).SetCellValue(asset.Remark);
                    rowIndex++;
                }

                // 自动调整列宽（第二次调整更准确）
                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.AutoSizeColumn(i);
                    sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1024); // 增加额外宽度
                }

                workbook.Write(fs);
            }
        }

        private ICellStyle CreateHeaderStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.IsBold = true;
            style.SetFont(font);
            style.FillForegroundColor = IndexedColors.Yellow.Index;
            style.FillPattern = FillPattern.SolidForeground;
            return style;
        }

        private string GetStatusText(int status)
        {
            // 直接调用配置类的标准获取方法
            return StatusConfig.GetStatusName(status);
        }
    }
}
