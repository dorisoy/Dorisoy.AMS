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
                    // 初始化借用记录表（只在表不存在时创建）
                    if (!db.DbMaintenance.IsAnyTable("BorrowRecords"))
                    {
                        db.CodeFirst.InitTables(typeof(BorrowRecord));
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

                    var result = db.Insertable(borrowRecord).ExecuteCommand();

                    if (result > 0)
                    {
                        MessageBox.Show("资产借用成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("资产借用失败，请重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
