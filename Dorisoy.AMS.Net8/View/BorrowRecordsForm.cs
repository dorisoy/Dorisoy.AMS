using Dorisoy.AMS.models;
using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.view
{
    public partial class BorrowRecordsForm : Form
    {
        private List<AssetViewModel> _availableAssets = new List<AssetViewModel>();
        private List<BorrowRecord> _borrowedRecords = new List<BorrowRecord>();

        public BorrowRecordsForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = SystemIcons.Application;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 初始化借用记录表（只在表不存在时创建）
                    if (!db.DbMaintenance.IsAnyTable("BorrowRecords"))
                    {
                        db.CodeFirst.InitTables(typeof(BorrowRecord));
                    }

                    // 加载待借用资产（状态为正常的）及其库存
                    var assets = db.Queryable<Asset>()
                        .Where(a => a.Status == 0)
                        .OrderBy(a => a.AssetID)
                        .ToList();

                    _availableAssets = assets.Select(a => new AssetViewModel
                    {
                        Asset = a,
                        AvailableQuantity = GetAvailableQuantity(db, a.AssetID)
                    }).ToList();

                    // 加载已借用的记录（状态为借用中的）
                    _borrowedRecords = db.Queryable<BorrowRecord>()
                        .Where(r => r.Status == 0)
                        .OrderBy(r => r.AssetID)
                        .ToList();

                    RefreshLists();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 计算资产的可用库存（总数 - 借用数）
        private decimal GetAvailableQuantity(SqlSugarClient db, string assetID)
        {
            var asset = db.Queryable<Asset>().First(a => a.AssetID == assetID);
            var borrowedQuantity = db.Queryable<BorrowRecord>()
                .Where(r => r.AssetID == assetID && r.Status == 0)
                .Sum(r => r.BorrowedQuantity);

            return asset.Quantity - borrowedQuantity;
        }

        private void RefreshLists()
        {
            using (var db = SqliteHelper.GetDb())
            {
                // 刷新待借用列表（显示库存）
                var availableDataSource = _availableAssets.Select(av => new
                {
                    av.Asset.AssetID,
                    av.Asset.Name,
                    av.Asset.Location,
                    av.Asset.Quantity,
                    av.Asset.Unit,
                    BorrowedQuantity = _borrowedRecords.Where(r => r.AssetID == av.Asset.AssetID).Sum(r => r.BorrowedQuantity),
                    av.AvailableQuantity
                }).ToList();

                dgvAvailable.DataSource = null;
                dgvAvailable.DataSource = availableDataSource;

                // 应用行颜色格式化（库存为零时变红）
                foreach (DataGridViewRow row in dgvAvailable.Rows)
                {
                    if (row.DataBoundItem != null)
                    {
                        dynamic item = row.DataBoundItem;
                        decimal availableQuantity = (decimal)item.AvailableQuantity;
                        if (availableQuantity <= 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;  // 浅红色
                            row.DefaultCellStyle.ForeColor = Color.DarkRed;    // 深红色文字
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.White;       // 杍子色（正常）
                            row.DefaultCellStyle.ForeColor = Color.Black;      // 黑色文字
                        }
                    }
                }

                // 刷新已借用列表
                var borrowedDataSource = _borrowedRecords.Select(r => new
                {
                    r.AssetID,
                    AssetName = db.Queryable<Asset>().First(a => a.AssetID == r.AssetID).Name,
                    r.BorrowedBy,
                    r.BorrowedQuantity,
                    r.BorrowedDate,
                    r.ExpectedReturnDate,
                    r.BorrowReason
                }).ToList();

                dgvBorrowed.DataSource = null;
                dgvBorrowed.DataSource = borrowedDataSource;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvAvailable.CurrentRow == null)
            {
                MessageBox.Show("请先选择要借用的资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvAvailable.CurrentRow;
                var assetID = selectedRow.Cells["AssetID"].Value?.ToString();
                var availableQty = Convert.ToDecimal(selectedRow.Cells["AvailableQuantity"].Value ?? 0);

                if (availableQty <= 0)
                {
                    MessageBox.Show("该资产库存不足，无法借用！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = SqliteHelper.GetDb())
                {
                    var asset = db.Queryable<Asset>().First(a => a.AssetID == assetID);
                    var form = new BorrowForm(asset, availableQty);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadData(); // 刷新列表
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvBorrowed.CurrentRow == null)
            {
                MessageBox.Show("请先选择要归还的资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvBorrowed.CurrentRow;
                var assetID = selectedRow.Cells["AssetID"].Value?.ToString();
                var borrowedBy = selectedRow.Cells["BorrowedBy"].Value?.ToString();
                var borrowedQty = Convert.ToDecimal(selectedRow.Cells["BorrowedQuantity"].Value ?? 0);

                using (var db = SqliteHelper.GetDb())
                {
                    // 查找对应的借用记录
                    var borrowRecord = db.Queryable<BorrowRecord>()
                        .First(r => r.AssetID == assetID && r.BorrowedBy == borrowedBy && r.Status == 0);

                    if (borrowRecord != null)
                    {
                        var form = new ReturnForm(borrowRecord);

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            LoadData(); // 刷新列表
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
