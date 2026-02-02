using Dorisoy.AMS.models;

namespace Dorisoy.AMS.view
{
    public partial class StockRecordsForm : Form
    {
        public StockRecordsForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeControls();
            InitializeDataGridView();
            LoadData();
        }

        private void InitializeControls()
        {
            // 初始化日期范围
            dtStart.Value = DateTime.Now.AddMonths(-1);
            dtEnd.Value = DateTime.Now;

            // 初始化操作类型下拉框
            cmbRecordType.Items.Clear();
            cmbRecordType.Items.Add(new KeyValuePair<int, string>(-1, "全部"));
            cmbRecordType.Items.Add(new KeyValuePair<int, string>(StockRecordType.In, "入库"));
            cmbRecordType.Items.Add(new KeyValuePair<int, string>(StockRecordType.Out, "出库"));
            cmbRecordType.DisplayMember = "Value";
            cmbRecordType.ValueMember = "Key";
            cmbRecordType.SelectedIndex = 0;

            // 初始化仓库下拉框
            LoadWarehouseList();
        }

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

                    cmbWarehouse.Items.Clear();
                    cmbWarehouse.Items.Add(new KeyValuePair<int, string>(-1, "全部仓库"));
                    foreach (var wh in warehouses)
                    {
                        cmbWarehouse.Items.Add(new KeyValuePair<int, string>(wh.Id, wh.Name));
                    }
                    cmbWarehouse.DisplayMember = "Value";
                    cmbWarehouse.ValueMember = "Key";
                    cmbWarehouse.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载仓库列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIndex",
                HeaderText = "序号",
                Width = 50,
                ReadOnly = true
            });

            var columns = new[]
            {
                new { Name = "colRecordTime", Prop = "RecordTime", Header = "操作时间", Width = 140 },
                new { Name = "colRecordTypeName", Prop = "RecordTypeName", Header = "类型", Width = 60 },
                new { Name = "colBusinessTypeName", Prop = "BusinessTypeName", Header = "业务类型", Width = 80 },
                new { Name = "colAssetID", Prop = "AssetID", Header = "资产编号", Width = 100 },
                new { Name = "colAssetName", Prop = "AssetName", Header = "资产名称", Width = 120 },
                new { Name = "colWarehouseName", Prop = "WarehouseName", Header = "仓库", Width = 100 },
                new { Name = "colQuantity", Prop = "Quantity", Header = "数量", Width = 70 },
                new { Name = "colBeforeQuantity", Prop = "BeforeQuantity", Header = "操作前", Width = 70 },
                new { Name = "colAfterQuantity", Prop = "AfterQuantity", Header = "操作后", Width = 70 },
                new { Name = "colHandler", Prop = "Handler", Header = "经手人", Width = 80 },
                new { Name = "colOperator", Prop = "Operator", Header = "操作人", Width = 80 },
                new { Name = "colRemark", Prop = "Remark", Header = "备注", Width = 150 }
            };

            dataGridView1.Columns.AddRange(columns.Select(c => new DataGridViewTextBoxColumn
            {
                Name = c.Name,
                DataPropertyName = c.Prop,
                HeaderText = c.Header,
                Width = c.Width
            }).ToArray());

            dataGridView1.Font = new System.Drawing.Font("微软雅黑", 9);

            // 生成序号
            dataGridView1.DataBindingComplete += (sender, e) =>
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells["colIndex"].Value = row.Index + 1;
                    }
                }
            };

            // 格式化显示
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            
            // 防止数据错误弹窗
            dataGridView1.DataError += (sender, e) => { e.ThrowException = false; };
        }

        private void DataGridView1_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;
                
                var row = dataGridView1.Rows[e.RowIndex];
                if (row.IsNewRow || row.DataBoundItem == null) return;

                if (dataGridView1.Columns[e.ColumnIndex].Name == "colRecordTypeName")
                {
                    dynamic item = row.DataBoundItem;
                    string typeName = item.RecordTypeName ?? "";
                    if (typeName == "入库")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                    else if (typeName == "出库")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "colQuantity")
                {
                    // 从数据源获取数量值
                    dynamic item = row.DataBoundItem;
                    decimal qty = Convert.ToDecimal(item.Quantity);
                    
                    if (qty > 0)
                    {
                        e.Value = $"+{qty}";
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else if (qty < 0)
                    {
                        e.Value = qty.ToString();  // 负数已经带符号
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else
                    {
                        e.Value = "0";
                    }
                    e.FormattingApplied = true;
                }
            }
            catch
            {
                // 忽略格式化错误
            }
        }

        private void LoadData()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 初始化库存记录表
                    if (!db.DbMaintenance.IsAnyTable("StockRecords"))
                    {
                        db.CodeFirst.InitTables(typeof(StockRecord));
                    }

                    var startDate = dtStart.Value.Date;
                    var endDate = dtEnd.Value.Date.AddDays(1);
                    var searchKey = txtSearch.Text.Trim();

                    // 获取筛选条件
                    int recordType = -1;
                    if (cmbRecordType.SelectedItem is KeyValuePair<int, string> selectedType)
                    {
                        recordType = selectedType.Key;
                    }

                    int warehouseId = -1;
                    if (cmbWarehouse.SelectedItem is KeyValuePair<int, string> selectedWarehouse)
                    {
                        warehouseId = selectedWarehouse.Key;
                    }

                    var records = db.Queryable<StockRecord>()
                        .Where(r => r.RecordTime >= startDate && r.RecordTime < endDate)
                        .WhereIF(recordType != -1, r => r.RecordType == recordType)
                        .WhereIF(warehouseId != -1, r => r.WarehouseId == warehouseId)
                        .WhereIF(!string.IsNullOrWhiteSpace(searchKey),
                            r => (r.AssetID ?? "").Contains(searchKey) ||
                                 (r.AssetName ?? "").Contains(searchKey) ||
                                 (r.Handler ?? "").Contains(searchKey) ||
                                 (r.Operator ?? "").Contains(searchKey))
                        .OrderBy(r => r.RecordTime, SqlSugar.OrderByType.Desc)
                        .ToList();

                    // 转换为显示用的数据
                    var displayData = records.Select(r => new
                    {
                        r.Id,
                        RecordTime = r.RecordTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        RecordTypeName = r.RecordType == StockRecordType.In ? "入库" : "出库",
                        BusinessTypeName = StockBusinessType.GetName(r.BusinessType),
                        r.AssetID,
                        r.AssetName,
                        r.WarehouseName,
                        r.Quantity,
                        r.BeforeQuantity,
                        r.AfterQuantity,
                        r.Handler,
                        r.Operator,
                        r.Remark
                    }).ToList();

                    dataGridView1.DataSource = displayData;

                    // 统计信息
                    var totalIn = records.Where(r => r.RecordType == StockRecordType.In).Sum(r => r.Quantity);
                    var totalOut = records.Where(r => r.RecordType == StockRecordType.Out).Sum(r => Math.Abs(r.Quantity));
                    lblTotal.Text = $"共 {records.Count} 条记录 | 入库合计：{totalIn} | 出库合计：{totalOut}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }

        private void cmbRecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtStart_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel文件|*.xlsx";
                sfd.FileName = $"库存记录_{DateTime.Now:yyyyMMddHHmm}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 这里可以添加导出逻辑
                        MessageBox.Show("导出功能待实现", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
