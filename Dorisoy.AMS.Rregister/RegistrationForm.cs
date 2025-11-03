using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace AssetManageRregister
{
    public partial class RegistrationForm : Form
    {
        public string MachineCode { get; private set; }
        public string RegistrationCode { get; private set; }

        public RegistrationForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //MachineCode = Dorisoy.AMS.Utilities.LicenseManager.GenerateMachineCode();
            //txtMachineCode.Text = MachineCode;

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtMachineCode.Text))
            {
                string registrationCode = LicenseManager.GenerateRegistrationCode(txtMachineCode.Text);
                txtRegCode.Text = registrationCode;
                Clipboard.SetText(txtRegCode.Text); // 复制文本到剪贴板1
                MessageBox.Show("已复制到剪贴板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("机器码文本框不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }
    }

}
