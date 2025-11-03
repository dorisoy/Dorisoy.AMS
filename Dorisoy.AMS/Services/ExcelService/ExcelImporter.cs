using Dorisoy.AMS.models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dorisoy.AMS.services.ExcelService
{
    public class ExcelImporter
    {
        private HashSet<string> _existingAssetIds; // 存储已存在的资产编号

        // 增加构造函数传入现有资产编号
        public ExcelImporter(IEnumerable<string> existingAssetIds = null)
        {
            _existingAssetIds = existingAssetIds != null
                ? new HashSet<string>(existingAssetIds)
                : new HashSet<string>();
        }
        public (List<Asset> ValidAssets, List<string> Errors) Import(string filePath)
        {
            var assets = new List<Asset>();
            var errors = new List<string>();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                string extension = Path.GetExtension(filePath).ToLower();
                IWorkbook workbook;

                switch (extension)
                {
                    case ".xls":
                        workbook = new HSSFWorkbook(fs);
                        break;
                    case ".xlsx":
                        workbook = new XSSFWorkbook(fs);
                        break;
                    default:
                        throw new Exception("不支持的文件格式");
                }


                ISheet sheet = workbook.GetSheetAt(0); // 读取第一个工作表
                var headerRow = sheet.GetRow(0);       // 假设第一行是标题

                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null)
                    {
                        continue;
                    }

                    try
                    {
                        var asset = ParseRowToAsset(row);
                        ValidateAsset(asset, rowIndex + 1); // 传入行号
                        assets.Add(asset);

                        // 添加当前资产编号到临时集合
                        _existingAssetIds.Add(asset.AssetID);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message); // 错误信息已包含行号
                    }
                }
            }
            return (assets, errors);
        }

        private void ValidateAsset(Asset asset, int rowNumber)
        {
            var errorPrefix = $"第{rowNumber}行：";

            if (string.IsNullOrWhiteSpace(asset.AssetID))
            {
                throw new Exception($"{errorPrefix}资产编号不能为空");
            }

            // 检查是否重复（包括数据库已有和当前文件内的重复）
            if (_existingAssetIds.Contains(asset.AssetID))
            {
                throw new Exception($"{errorPrefix}资产编号 {asset.AssetID} 已存在");
            }

            if (string.IsNullOrWhiteSpace(asset.Name))
            {
                throw new Exception($"{errorPrefix}资产名称不能为空");
            }

            if (asset.Quantity <= 0)
            {
                throw new Exception($"{errorPrefix}数量必须大于0");
            }
        }



        private Asset ParseRowToAsset(IRow row)
        {
            return new Asset
            {
                AssetID = GetCellStringValue(row, 0),
                Category = GetCellStringValue(row, 1),
                Name = GetCellStringValue(row, 2),
                Model = GetCellStringValue(row, 3),
                Quantity = GetCellDecimalValue(row, 4),
                Unit = GetCellStringValue(row, 5),
                Location = GetCellStringValue(row, 6),
                Department = GetCellStringValue(row, 7),
                User = GetCellStringValue(row, 8),
                Status = ParseStatus(GetCellStringValue(row, 9)),
                Remark = GetCellStringValue(row, 10)
            };
        }

        private int ParseStatus(string statusName)
        {
            if (string.IsNullOrWhiteSpace(statusName))
            {
                throw new ArgumentException("状态不能为空");
            }

            if (StatusConfig.StatusMap.TryGetValue(statusName.Trim(), out int status))
            {
                return status;
            }

            throw new ArgumentException($"无效的状态名称: {statusName}");
        }

        private string GetCellStringValue(IRow row, int columnIndex)
        {
            return row.GetCell(columnIndex)?.ToString()?.Trim() ?? "";
        }

        private decimal GetCellDecimalValue(IRow row, int columnIndex)
        {
            var cell = row.GetCell(columnIndex);
            if (cell == null)
            {
                return 0;
            }

            // 先用 switch 语句计算结果，再返回
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    return Convert.ToDecimal(cell.NumericCellValue);
                case CellType.String:
                    // 字符串解析逻辑
                    if (decimal.TryParse(cell.StringCellValue, out var val))
                    {
                        return val;
                    }
                    return 0;
                default:
                    return 0;
            }

        }


    }
}