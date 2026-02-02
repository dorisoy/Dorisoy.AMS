using Dorisoy.AMS.models;
using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.view
{
    public partial class BorrowForm : Form
    {
        private Asset _asset;
        private decimal _availableQuantity;

        public BorrowForm(Asset asset, decimal availableQuantity = 0)
        {
            InitializeComponent();
            _asset = asset ?? throw new ArgumentNullException(nameof(asset));
            _availableQuantity = availableQuantity;
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadAssetData();
        }

        private void LoadAssetData()
        {
            // 绑定资产信息
            lblAssetID.Text = _asset.AssetID;
            lblAssetName.Text = _asset.Name;
            lblCategory.Text = _asset.Category;
            lblModel.Text = _asset.Model;
            lblLocation.Text = _asset.Location;
            lblDepartment.Text = _asset.Department;
            lblCurrentUser.Text = _asset.User;

            // 加载用户列表到下拉框
            LoadBorrowerList();

            // 设置默认值
            dtBorrowDate.Value = DateTime.Now;
            dtExpectedReturnDate.Value = DateTime.Now.AddDays(7); // 默认借用7天
            numBorrowQuantity.Maximum = _availableQuantity > 0 ? _availableQuantity : 1; // 设置最大借用数
            numBorrowQuantity.Value = 1; // 默认借用量为1
        }

        private void LoadBorrowerList()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var users = db.Queryable<User>()
                        .Select(u => u.Username)
                        .ToList();

                    cmbBorrowedBy.DataSource = users;
                    cmbBorrowedBy.SelectedIndex = -1; // 默认不选中
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载用户列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbBorrowedBy.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cmbBorrowedBy.Text))
            {
                MessageBox.Show("请选择借用人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBorrowedBy.Focus();
                return false;
            }

            if (numBorrowQuantity.Value <= 0)
            {
                MessageBox.Show("借用数量必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numBorrowQuantity.Focus();
                return false;
            }

            if (numBorrowQuantity.Value > _availableQuantity)
            {
                MessageBox.Show($"借用数量不能超过可用库存！当前可用库存：{_availableQuantity}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numBorrowQuantity.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("借用原因不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return false;
            }

            if (dtExpectedReturnDate.Value <= dtBorrowDate.Value)
            {
                MessageBox.Show("预期归还日期必须晚于借用日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 使用事务保证数据一致性
                    db.Ado.BeginTran();
                    try
                    {
                        // 初始化借用记录表（只在表不存在时创建）
                        if (!db.DbMaintenance.IsAnyTable("BorrowRecords"))
                        {
                            db.CodeFirst.InitTables(typeof(BorrowRecord));
                        }

                        // 实时检查可用库存（防止并发超借）
                        var asset = db.Queryable<Asset>().First(a => a.AssetID == _asset.AssetID);
                        if (asset == null)
                        {
                            throw new Exception("资产不存在或已被删除");
                        }

                        var borrowedQty = db.Queryable<BorrowRecord>()
                            .Where(r => r.AssetID == _asset.AssetID && r.Status == 0)
                            .Sum(r => r.BorrowedQuantity);
                        var realAvailable = asset.Quantity - borrowedQty;

                        if ((decimal)numBorrowQuantity.Value > realAvailable)
                        {
                            throw new Exception($"可用库存不足！当前可用库存：{realAvailable}，您要借用：{numBorrowQuantity.Value}");
                        }

                        // 创建借用记录
                        var borrowRecord = new BorrowRecord
                        {
                            AssetID = _asset.AssetID,
                            BorrowedBy = cmbBorrowedBy.Text.Trim(),
                            BorrowedQuantity = (decimal)numBorrowQuantity.Value,
                            BorrowedDate = dtBorrowDate.Value,
                            ExpectedReturnDate = dtExpectedReturnDate.Value,
                            BorrowReason = txtReason.Text.Trim(),
                            Status = 0 // 借用中
                        };

                        var result = db.Insertable(borrowRecord).ExecuteReturnIdentity();

                        if (result > 0)
                        {
                            // 生成出库记录
                            CreateStockOutRecord(db, asset, (decimal)numBorrowQuantity.Value, 
                                cmbBorrowedBy.Text.Trim(), result.ToString(), txtReason.Text.Trim());

                            db.Ado.CommitTran();
                            MessageBox.Show("资产借用成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            throw new Exception("借用记录创建失败");
                        }
                    }
                    catch
                    {
                        db.Ado.RollbackTran();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"借用失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 创建借用出库记录
        /// </summary>
        private void CreateStockOutRecord(SqlSugarClient db, Asset asset, decimal quantity, 
            string handler, string relatedId, string remark)
        {
            try
            {
                // 初始化库存记录表
                if (!db.DbMaintenance.IsAnyTable("StockRecords"))
                {
                    db.CodeFirst.InitTables(typeof(StockRecord));
                }

                // 计算当前可用库存
                var borrowedQty = db.Queryable<BorrowRecord>()
                    .Where(r => r.AssetID == asset.AssetID && r.Status == 0)
                    .Sum(r => r.BorrowedQuantity);
                var beforeQty = asset.Quantity - borrowedQty + quantity; // 加回刚借的
                var afterQty = beforeQty - quantity;

                var stockRecord = new StockRecord
                {
                    AssetID = asset.AssetID,
                    AssetName = asset.Name,
                    WarehouseId = asset.WarehouseId,
                    WarehouseName = asset.Location,
                    RecordType = StockRecordType.Out,
                    BusinessType = StockBusinessType.BorrowOut,
                    Quantity = -quantity, // 出库为负数
                    BeforeQuantity = beforeQty,
                    AfterQuantity = afterQty,
                    RelatedId = relatedId,
                    Operator = AppContext.CurrentUser?.Username ?? "",
                    Handler = handler,
                    RecordTime = DateTime.Now,
                    Remark = $"借用出库: {remark}"
                };

                db.Insertable(stockRecord).ExecuteCommand();
            }
            catch (Exception ex)
            {
                // 出库记录失败不影响主流程，只记录日志
                System.Diagnostics.Debug.WriteLine($"创建出库记录失败：{ex.Message}");
            }
        }
    }
}
