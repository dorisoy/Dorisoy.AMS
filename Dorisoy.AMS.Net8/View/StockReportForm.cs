using Dorisoy.AMS;
using Dorisoy.AMS.models;

namespace Dorisoy.AMS.view
{
    /// <summary>
    /// 库存报表窗体
    /// </summary>
    public partial class StockReportForm : Form
    {
        public StockReportForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadWarehouseList();
            LoadData();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            
            // 防止数据错误弹窗
            dataGridView1.DataError += (sender, e) => { e.ThrowException = false; };
        }

        /// <summary>
        /// 加载仓库列表
        /// </summary>
        private void LoadWarehouseList()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var warehouses = db.Queryable<Warehouse>()
                        .Where(w => w.Status == 0)
                        .OrderBy(w => w.Code)
                        .ToList();

                    // 添加"全部仓库"选项
                    var allItem = new Warehouse { Id = 0, Name = "全部仓库" };
                    warehouses.Insert(0, allItem);

                    cmbWarehouse.DisplayMember = "Name";
                    cmbWarehouse.ValueMember = "Id";
                    cmbWarehouse.DataSource = warehouses;
                    cmbWarehouse.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载仓库列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载库存数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var warehouseId = cmbWarehouse.SelectedValue as int? ?? 0;

                    var assets = db.Queryable<Asset>()
                        .Where(a => a.Status != 1)  // 排除已删除
                        .WhereIF(warehouseId > 0, a => a.WarehouseId == warehouseId)
                        .OrderBy(a => a.Location)
                        .OrderBy(a => a.AssetID)
                        .ToList();

                    // 计算借出数量和可用库存
                    var reportData = assets.Select(a =>
                    {
                        var borrowedQty = db.Queryable<BorrowRecord>()
                            .Where(r => r.AssetID == a.AssetID && r.Status == 0)
                            .Sum(r => r.BorrowedQuantity);

                        return new
                        {
                            a.AssetID,
                            a.Category,
                            a.Name,
                            a.Model,
                            a.Location,
                            a.Quantity,
                            a.MinQuantity,
                            AvailableQuantity = a.Quantity - borrowedQty,
                            BorrowedQuantity = borrowedQty,
                            a.Unit,
                            StatusName = StatusConfig.GetStatusName(a.Status),
                            IsWarning = a.MinQuantity > 0 && a.Quantity <= a.MinQuantity
                        };
                    }).ToList();

                    dataGridView1.DataSource = reportData;

                    // 统计信息
                    var totalQuantity = reportData.Sum(r => r.Quantity);
                    var totalAvailable = reportData.Sum(r => r.AvailableQuantity);
                    var warningCount = reportData.Count(r => r.IsWarning);
                    
                    lblTotal.Text = $"共 {reportData.Count} 种资产 | 总库存：{totalQuantity} | 可用库存：{totalAvailable} | 预警：{warningCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 格式化显示（库存预警红色高亮）
        /// </summary>
        private void dataGridView1_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

                var row = dataGridView1.Rows[e.RowIndex];
                if (row.IsNewRow || row.DataBoundItem == null) return;

                dynamic item = row.DataBoundItem;
                bool isWarning = item.IsWarning;

                if (isWarning)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
            catch
            {
                // 忽略格式化错误
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel文件|*.xlsx";
                var warehouseName = cmbWarehouse.Text;
                sfd.FileName = $"库存报表_{warehouseName}_{DateTime.Now:yyyyMMdd}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportToExcel(sfd.FileName);
                        MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        private void ExportToExcel(string filePath)
        {
            using (var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook())
            {
                var sheet = workbook.CreateSheet("库存报表");

                // 创建表头样式
                var headerStyle = workbook.CreateCellStyle();
                headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                var headerFont = workbook.CreateFont();
                headerFont.IsBold = true;
                headerStyle.SetFont(headerFont);

                // 写入表头
                var headerRow = sheet.CreateRow(0);
                string[] headers = { "资产编号", "类别", "资产名称", "规格型号", "所在仓库", 
                                     "总库存", "最低库存", "可用库存", "借出数量", "单位", "状态" };
                
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = headerRow.CreateCell(i);
                    cell.SetCellValue(headers[i]);
                    cell.CellStyle = headerStyle;
                    sheet.SetColumnWidth(i, 15 * 256);
                }

                // 写入数据
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var dataRow = sheet.CreateRow(i + 1);
                    var row = dataGridView1.Rows[i];

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        var cell = dataRow.CreateCell(j);
                        var value = row.Cells[j].Value;
                        
                        if (value is decimal d)
                            cell.SetCellValue((double)d);
                        else
                            cell.SetCellValue(value?.ToString() ?? "");
                    }
                }

                // 保存文件
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
