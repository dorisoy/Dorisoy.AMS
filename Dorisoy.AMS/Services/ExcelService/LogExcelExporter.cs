using Dorisoy.AMS.models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;

namespace Dorisoy.AMS.Services.ExcelService
{
    public class LogExcelExporter
    {
        public void LogExporterToExcel(List<Log> Logs, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("资产清单");

                // 创建标题行
                var headerRow = sheet.CreateRow(0);
                string[] headers = {
                "序号", "操作类型","操作时间","操作人","资产编号","操作详情"
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
                foreach (var Log in Logs)
                {
                    IRow row = sheet.CreateRow(rowIndex);
                    row.CreateCell(0).SetCellValue(rowIndex);
                    row.CreateCell(1).SetCellValue(Log.OperationType);
                    row.CreateCell(2).SetCellValue(Log.OperationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    row.CreateCell(3).SetCellValue(Log.Operator);
                    row.CreateCell(4).SetCellValue(Log.AssetNumber);
                    row.CreateCell(5).SetCellValue(Log.Details);

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
