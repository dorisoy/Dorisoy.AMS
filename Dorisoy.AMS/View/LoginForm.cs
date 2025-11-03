using Dorisoy.AMS.models;
using System;
using System.Windows.Forms;

namespace Dorisoy.AMS.view
{
    public partial class LoginForm : Form
    {
        public User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            // 设置窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var db = SqliteHelper.GetDb();
            var user = db.Queryable<User>()
                .Where(u => u.Username == txtUsername.Text)
                .First();

            if (user != null && user.Password == SqliteHelper.HashPassword(txtPassword.Text))
            {
                CurrentUser = user;
                DialogResult = DialogResult.OK;
                Close(); // 关闭登录窗口

            }
            else
            {
                MessageBox.Show("用户名或密码错误！");
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }

        // 回车键触发登录
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
