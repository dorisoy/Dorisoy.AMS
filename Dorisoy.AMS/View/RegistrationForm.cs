using System;
using System.Windows.Forms;
using static Dorisoy.AMS.Utilities.LicenseManager;
namespace Dorisoy.AMS.view
{
    public partial class RegistrationForm : Form
    {
        public string MachineCode { get; private set; }
        public string RegistrationCode { get; private set; }
        private LicenseInfo _license;

        public RegistrationForm(string machineCode)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            MachineCode = Utilities.LicenseManager.GenerateMachineCode();
            txtMachineCode.Text = MachineCode;
            //如果不要直接生成注册码，可以直接注释下面代码
            txtRegCode.Text = Utilities.LicenseManager.GenerateRegistrationCode(MachineCode);

            _license = Utilities.LicenseManager.LoadLicense();

            if (_license.IsRegistered)
            {
                txtRegCode.Text = _license.RegistrationCode;
                lblStatus.Text = "✔已激活";
                lblRegDate.Text = $"注册日期：{_license.RegistrationDate?.ToShortDateString() ?? "N/A"}";

                txtRegCode.ReadOnly = true;
                btnRegister.Text = "重新注册";
            }
            else
            {
                // 计算剩余试用天数
                var elapsedDays = (DateTime.Today - _license.FirstRunDate.Date).TotalDays;
                int remainingDays = Math.Max(0, Utilities.LicenseManager.TrialDays - (int)elapsedDays);
                lblStatus.Text = $"未激活（剩余{remainingDays}天试用）";
                lblRegDate.Text = string.Empty;
                txtRegCode.ReadOnly = false;
                btnRegister.Text = "注册";
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMachineCode.Text))
            {
                Clipboard.SetText(txtMachineCode.Text);
                MessageBox.Show("已复制到剪贴板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("文本框内容为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (_license.IsRegistered && btnRegister.Text == "重新注册")
            {
                txtRegCode.ReadOnly = false;
                btnRegister.Text = "注册";
                MessageBox.Show("请输入新的注册码进行重新注册。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 获取用户输入的注册码
            string inputRegistrationCode = txtRegCode.Text;

            // 验证注册码
            if (Utilities.LicenseManager.ValidateRegistrationCode(this.MachineCode, inputRegistrationCode))
            {
                // 注册码有效，更新许可证信息
                _license.IsRegistered = true;
                _license.RegistrationDate = DateTime.Now;
                _license.RegistrationCode = inputRegistrationCode;
                Utilities.LicenseManager.SaveLicense(_license);

                // 显示注册成功消息并关闭注册窗口
                MessageBox.Show("激活成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRegCode.ReadOnly = true;
                btnRegister.Text = "重新注册";
                lblStatus.Text = "✔已激活";
                lblRegDate.Text = $"注册日期：{_license.RegistrationDate?.ToShortDateString() ?? "N/A"}";


                // 这里不关闭窗口，可以根据实际需求选择是否关闭
                // this.Close();
            }
            else
            {
                // 注册码无效，显示错误消息
                MessageBox.Show("无效的注册码，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRegCode.ReadOnly = false;
                btnRegister.Text = "重新注册";
            }
        }
    }
}
