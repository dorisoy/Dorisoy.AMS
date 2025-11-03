using System;
using System.Windows.Forms;

namespace Dorisoy.AMS.view
{
    public partial class PasswordInputForm : Form
    {
        public string Password { get; private set; }
        public PasswordInputForm()
        {
            InitializeComponent();
            // 设置窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            Password = txtPassword.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }



    }
}
