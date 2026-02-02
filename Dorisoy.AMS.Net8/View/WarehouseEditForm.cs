using Dorisoy.AMS.models;

namespace Dorisoy.AMS.view
{
    public partial class WarehouseEditForm : Form
    {
        private Warehouse _warehouse;
        private bool _isEditMode;

        public WarehouseEditForm(Warehouse warehouse = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            _isEditMode = warehouse != null;
            _warehouse = warehouse ?? new Warehouse { Status = 0, CreateTime = DateTime.Now };

            InitializeForm();
        }

        private void InitializeForm()
        {
            Text = _isEditMode ? "编辑仓库" : "新增仓库";

            // 数据绑定
            txtCode.Text = _warehouse.Code;
            txtName.Text = _warehouse.Name;
            txtAddress.Text = _warehouse.Address;
            txtContact.Text = _warehouse.Contact;
            txtPhone.Text = _warehouse.Phone;
            txtRemark.Text = _warehouse.Remark;
            cmbStatus.SelectedIndex = _warehouse.Status;

            // 编辑模式下，仓库编号不可修改
            txtCode.Enabled = !_isEditMode;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("仓库编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("仓库名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // 检查仓库编号是否重复（新增模式下）
            if (!_isEditMode)
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var exists = db.Queryable<Warehouse>()
                        .Any(w => w.Code == txtCode.Text.Trim());
                    if (exists)
                    {
                        MessageBox.Show("仓库编号已存在，请使用其他编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        return false;
                    }
                }
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
                _warehouse.Code = txtCode.Text.Trim();
                _warehouse.Name = txtName.Text.Trim();
                _warehouse.Address = txtAddress.Text.Trim();
                _warehouse.Contact = txtContact.Text.Trim();
                _warehouse.Phone = txtPhone.Text.Trim();
                _warehouse.Remark = txtRemark.Text.Trim();
                _warehouse.Status = cmbStatus.SelectedIndex;

                using (var db = SqliteHelper.GetDb())
                {
                    if (_isEditMode)
                    {
                        db.Updateable(_warehouse).ExecuteCommand();

                        // 同步更新资产表中的存放地点名称
                        db.Updateable<Asset>()
                            .SetColumns(a => a.Location == _warehouse.Name)
                            .Where(a => a.WarehouseId == _warehouse.Id)
                            .ExecuteCommand();
                    }
                    else
                    {
                        _warehouse.CreateTime = DateTime.Now;
                        db.Insertable(_warehouse).ExecuteCommand();
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
