using Dorisoy.AMS.models;

namespace Dorisoy.AMS.view
{
    public partial class UserEditForm : Form
    {
        private readonly User _user;
        private readonly bool _isEdit;

        public UserEditForm(User user = null)
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
                _user = new User(); // 可能需要初始化
                Text = "新增用户";
            }

        }

        private void LoadData()
        {
            txtUsername.Text = _user.Username;
            chkCanAdd.Checked = _user.CanAdd;
            chkCanEdit.Checked = _user.CanEdit;
            chkCanPrint.Checked = _user.CanPrint;
            chkCanExport.Checked = _user.CanExport;
            chkCanImport.Checked = _user.CanImport;
            chkManageNumber.Checked = _user.CanManageNumber;
            chkManageLog.Checked = _user.CanManageLog;
            chkManageData.Checked = _user.CanManageData;
            chkIsAdmin.Checked = _user.IsAdmin;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var db = SqliteHelper.GetDb();
            var user = _isEdit ? _user : new User();

            user.Username = txtUsername.Text;
            user.CanAdd = chkCanAdd.Checked;
            user.CanEdit = chkCanEdit.Checked;
            user.CanPrint = chkCanPrint.Checked;
            user.CanExport = chkCanExport.Checked;
            user.CanImport = chkCanImport.Checked;
            user.CanManageNumber = chkManageNumber.Checked;
            user.CanManageLog = chkManageLog.Checked;
            user.CanManageData = chkManageData.Checked;
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


    }
}
