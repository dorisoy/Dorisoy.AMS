using Dorisoy.AMS.models;

namespace Dorisoy.AMS.view
{
    public partial class WarehouseForm : Form
    {
        public WarehouseForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeDataGridView();
            LoadData();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIndex",
                HeaderText = "序号",
                Width = 50,
                ReadOnly = true
            });

            var columns = new[]
            {
                new { Name = "colId", Prop = "Id", Header = "ID", Width = 50 },
                new { Name = "colCode", Prop = "Code", Header = "仓库编号", Width = 100 },
                new { Name = "colName", Prop = "Name", Header = "仓库名称", Width = 150 },
                new { Name = "colAddress", Prop = "Address", Header = "仓库地址", Width = 200 },
                new { Name = "colContact", Prop = "Contact", Header = "联系人", Width = 100 },
                new { Name = "colPhone", Prop = "Phone", Header = "联系电话", Width = 120 },
                new { Name = "colRemark", Prop = "Remark", Header = "备注", Width = 150 },
                new { Name = "colStatus", Prop = "Status", Header = "状态", Width = 80 }
            };

            dataGridView1.Columns.AddRange(columns.Select(c => new DataGridViewTextBoxColumn
            {
                Name = c.Name,
                DataPropertyName = c.Prop,
                HeaderText = c.Header,
                Width = c.Width
            }).ToArray());

            // 隐藏ID列
            dataGridView1.Columns["colId"].Visible = false;

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

            // 状态列格式化
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.Value is int statusCode)
                {
                    e.Value = statusCode == 0 ? "正常" : "停用";
                    e.FormattingApplied = true;
                }
            }
        }

        private void LoadData(string search = "")
        {
            using (var db = SqliteHelper.GetDb())
            {
                var warehouses = db.Queryable<Warehouse>()
                    .WhereIF(!string.IsNullOrWhiteSpace(search),
                        w => (w.Code ?? "").Contains(search) ||
                             (w.Name ?? "").Contains(search) ||
                             (w.Address ?? "").Contains(search))
                    .OrderBy(w => w.Code)
                    .ToList();

                dataGridView1.DataSource = warehouses;
                lblTotal.Text = $"共 {warehouses.Count} 条记录";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new WarehouseEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(txtSearch.Text.Trim());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("请选择要编辑的仓库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var warehouse = dataGridView1.CurrentRow.DataBoundItem as Warehouse;
            if (warehouse == null) return;

            var form = new WarehouseEditForm(warehouse);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData(txtSearch.Text.Trim());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("请选择要删除的仓库", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var warehouse = dataGridView1.CurrentRow.DataBoundItem as Warehouse;
            if (warehouse == null) return;

            // 检查是否有资产使用该仓库
            using (var db = SqliteHelper.GetDb())
            {
                var assetCount = db.Queryable<Asset>()
                    .Where(a => a.WarehouseId == warehouse.Id)
                    .Count();

                if (assetCount > 0)
                {
                    MessageBox.Show($"该仓库下有 {assetCount} 个资产，无法删除！\n请先将资产转移到其他仓库。",
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var result = MessageBox.Show($"确定要删除仓库【{warehouse.Name}】吗？",
                "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var db = SqliteHelper.GetDb())
                    {
                        db.Deleteable<Warehouse>().Where(w => w.Id == warehouse.Id).ExecuteCommand();
                    }
                    MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(txtSearch.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData(txtSearch.Text.Trim());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
