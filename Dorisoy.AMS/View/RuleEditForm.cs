using Dorisoy.AMS.models;
using System;
using System.Windows.Forms;

namespace Dorisoy.AMS.view
{
    public partial class RuleEditForm : Form
    {
        public AssetRule _rule;
        private bool _isEditMode;

        public RuleEditForm(AssetRule rule = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            // 初始化核心字段
            _isEditMode = rule != null;
            _rule = rule ?? new AssetRule { };
            Text = _isEditMode ? "编辑资产" : "新增资产";
            InitializeData();
        }

        private void InitializeData()
        {
            txtKeywords.Text = _rule.Keywords;
            txtPrefix.Text = _rule.Prefix;
            numNumberLength.Value = _rule.NumberLength;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtKeywords.Text))
            {
                return ShowError("请输入关键词");
            }

            if (string.IsNullOrWhiteSpace(txtPrefix.Text))
            {
                return ShowError("请输入前缀");
            }

            return numNumberLength.Value > 0 || ShowError("数字位数必须大于0");
        }

        private bool ShowError(string message)
        {
            MessageBox.Show(message);
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                _rule.Keywords = txtKeywords.Text.Trim();
                _rule.Prefix = txtPrefix.Text.Trim().ToUpper();
                _rule.NumberLength = Convert.ToInt32(numNumberLength.Value);
                using (var db = SqliteHelper.GetDb())
                {
                    if (_isEditMode)
                    {
                        db.Updateable(_rule).ExecuteCommand();
                    }
                    else
                    {
                        db.Insertable(_rule).ExecuteCommand();
                    }
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}");
            }

        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
