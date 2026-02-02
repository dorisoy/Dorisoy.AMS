using Dorisoy.AMS.models;

namespace Dorisoy.AMS.view
{
    public partial class UserEditForm : Form
    {
        private readonly User _user;
        private readonly bool _isEdit;

        public UserEditForm(User? user = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (user != null)
            {
                _user = user;
                _isEdit = true;
                Text = "编辑用户";
                LoadData();
            }
            else
            {
                _user = new User();
                Text = "新增用户";
            }
        }

        private void LoadData()
        {
            txtUsername.Text = _user.Username;
            
            // 基础操作权限
            chkCanAdd.Checked = _user.CanAdd;
            chkCanEdit.Checked = _user.CanEdit;
            chkCanDelete.Checked = _user.CanDelete;
            chkCanPrint.Checked = _user.CanPrint;
            chkCanExport.Checked = _user.CanExport;
            chkCanImport.Checked = _user.CanImport;
            
            // 业务功能权限
            chkCanBorrow.Checked = _user.CanBorrow;
            chkCanViewStockRecords.Checked = _user.CanViewStockRecords;
            chkCanScrap.Checked = _user.CanScrap;
            chkCanInventoryCheck.Checked = _user.CanInventoryCheck;
            chkCanViewStockReport.Checked = _user.CanViewStockReport;
            
            // 系统管理权限
            chkManageNumber.Checked = _user.CanManageNumber;
            chkManageLog.Checked = _user.CanManageLog;
            chkManageData.Checked = _user.CanManageData;
            chkManageWarehouse.Checked = _user.CanManageWarehouse;
            chkManageUsers.Checked = _user.CanManageUsers;
            
            // 管理员
            chkIsAdmin.Checked = _user.IsAdmin;
            
            // 如果是管理员，禁用其他权限复选框
            UpdatePermissionControlsState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("请输入用户名！", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            
            // 新增用户时必须输入密码
            if (!_isEdit && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("新增用户必须设置密码！", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            var db = SqliteHelper.GetDb();
            
            // 检查用户名是否已存在
            var existingUser = db.Queryable<User>().First(u => u.Username == txtUsername.Text.Trim());
            if (existingUser != null && (!_isEdit || existingUser.Id != _user.Id))
            {
                MessageBox.Show("用户名已存在！", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            
            var user = _isEdit ? _user : new User();

            user.Username = txtUsername.Text.Trim();
            
            // 基础操作权限
            user.CanAdd = chkCanAdd.Checked;
            user.CanEdit = chkCanEdit.Checked;
            user.CanDelete = chkCanDelete.Checked;
            user.CanPrint = chkCanPrint.Checked;
            user.CanExport = chkCanExport.Checked;
            user.CanImport = chkCanImport.Checked;
            
            // 业务功能权限
            user.CanBorrow = chkCanBorrow.Checked;
            user.CanViewStockRecords = chkCanViewStockRecords.Checked;
            user.CanScrap = chkCanScrap.Checked;
            user.CanInventoryCheck = chkCanInventoryCheck.Checked;
            user.CanViewStockReport = chkCanViewStockReport.Checked;
            
            // 系统管理权限
            user.CanManageNumber = chkManageNumber.Checked;
            user.CanManageLog = chkManageLog.Checked;
            user.CanManageData = chkManageData.Checked;
            user.CanManageWarehouse = chkManageWarehouse.Checked;
            user.CanManageUsers = chkManageUsers.Checked;
            
            // 管理员
            user.IsAdmin = chkIsAdmin.Checked;

            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                user.Password = SqliteHelper.HashPassword(txtPassword.Text);
            }

            if (_isEdit)
            {
                db.Updateable(user).ExecuteCommand();
            }
            else
            {
                db.Insertable(user).ExecuteCommand();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 管理员复选框状态变化时，更新其他权限控件状态
        /// </summary>
        private void chkIsAdmin_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePermissionControlsState();
        }

        /// <summary>
        /// 更新权限控件状态：管理员拥有所有权限，其他复选框禁用并勾选
        /// </summary>
        private void UpdatePermissionControlsState()
        {
            bool isAdmin = chkIsAdmin.Checked;
            
            // 基础操作权限
            SetCheckBoxState(chkCanAdd, isAdmin);
            SetCheckBoxState(chkCanEdit, isAdmin);
            SetCheckBoxState(chkCanDelete, isAdmin);
            SetCheckBoxState(chkCanPrint, isAdmin);
            SetCheckBoxState(chkCanExport, isAdmin);
            SetCheckBoxState(chkCanImport, isAdmin);
            
            // 业务功能权限
            SetCheckBoxState(chkCanBorrow, isAdmin);
            SetCheckBoxState(chkCanViewStockRecords, isAdmin);
            SetCheckBoxState(chkCanScrap, isAdmin);
            SetCheckBoxState(chkCanInventoryCheck, isAdmin);
            SetCheckBoxState(chkCanViewStockReport, isAdmin);
            
            // 系统管理权限
            SetCheckBoxState(chkManageNumber, isAdmin);
            SetCheckBoxState(chkManageLog, isAdmin);
            SetCheckBoxState(chkManageData, isAdmin);
            SetCheckBoxState(chkManageWarehouse, isAdmin);
            SetCheckBoxState(chkManageUsers, isAdmin);
        }

        /// <summary>
        /// 设置复选框状态：如果是管理员模式，勾选并禁用
        /// </summary>
        private void SetCheckBoxState(CheckBox checkBox, bool isAdmin)
        {
            if (isAdmin)
            {
                checkBox.Checked = true;
                checkBox.Enabled = false;
            }
            else
            {
                checkBox.Enabled = true;
            }
        }
    }
}
