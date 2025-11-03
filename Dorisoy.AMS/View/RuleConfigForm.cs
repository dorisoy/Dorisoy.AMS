using Dorisoy.AMS.models;
using SqlSugar;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dorisoy.AMS.view
{
    public partial class RuleConfigForm : Form
    {
        private readonly SqlSugarClient _db;
        private BindingList<AssetRule> _rules;
        public bool UseDefaultRule { get; set; }

        public RuleConfigForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _db = SqliteHelper.GetDb();
            InitializeDataGridView();
            LoadData();         // 第一步：加载数据
            InitializeControls(); // 第二步：初始化控件和数据绑定


        }

        private void InitializeDataGridView()
        {
            // 配置DataGridView列
            dgvRules.AutoGenerateColumns = false;
            dgvRules.Columns.AddRange(
                new DataGridViewCheckBoxColumn
                {
                    Name = "colIsDefault",
                    DataPropertyName = "IsDefault",
                    HeaderText = "默认规则",
                    ReadOnly = true,
                    Width = 80
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colKeywords",
                    DataPropertyName = "Keywords",
                    HeaderText = "关键词（逗号分隔）",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colPrefix",
                    DataPropertyName = "Prefix",
                    HeaderText = "前缀",
                    Width = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "colNumberLength",
                    DataPropertyName = "NumberLength",
                    HeaderText = "数字位数",
                    Width = 80
                }
            );

            // 设置中文字体
            dgvRules.Font = new Font("微软雅黑", 9);
        }

        private void InitializeControls()
        {
            // 绑定到公共属性 UseDefaultRule
            chkUseDefault.DataBindings.Add("Checked", this, nameof(UseDefaultRule));
            chkUseDefault.CheckedChanged += (s, e) => SaveSettings();


        }

        private void SaveSettings()
        {
            using (var db = SqliteHelper.GetDb())
            {
                db.Storageable(new GlobalSetting
                {
                    Key = "UseDefaultRule",
                    Value = UseDefaultRule ? "1" : "0"
                }).ExecuteCommand();
            }
        }

        private void LoadData()
        {
            using (var db = SqliteHelper.GetDb())
            {
                var setting = db.Queryable<GlobalSetting>()
                    .First(s => s.Key == "UseDefaultRule");
                // 赋值给公共属性，而非私有字段
                UseDefaultRule = (setting?.Value ?? "1") == "1"; // 默认启用
            }

            // 加载规则并标记默认规则
            var rules = _db.Queryable<AssetRule>().ToList();
            var defaultRuleId = _db.Queryable<GlobalSetting>()
                .Where(s => s.Key == "DefaultRuleId")
                .Select(s => s.Value)
                .First();

            if (int.TryParse(defaultRuleId, out int id))
            {
                rules.ForEach(r => r.IsDefault = r.Id == id);
            }

            _rules = new BindingList<AssetRule>(rules);
            dgvRules.DataSource = _rules;

            UpdateControls();

        }


        private void UpdateControls()
        {
            bool enableEdit = !UseDefaultRule;
            dgvRules.Enabled = enableEdit;
            btnSetDefault.Enabled = enableEdit;
            btnAdd.Enabled = enableEdit;
            btnDelete.Enabled = enableEdit;
        }


        // 添加规则
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var editForm = new RuleEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        // 编辑规则
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRules.CurrentRow?.DataBoundItem is AssetRule selectedRule)
            {
                var editForm = new RuleEditForm(selectedRule);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        // 删除规则
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRules.CurrentRow?.DataBoundItem is AssetRule selectedRule)
            {
                if (MessageBox.Show("确定删除此规则？", "确认",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _db.Deleteable<AssetRule>(selectedRule.Id).ExecuteCommand();
                    LoadData();
                }
            }
        }

        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            if (dgvRules.CurrentRow?.DataBoundItem is AssetRule selectedRule)
            {
                // 清除其他默认标记
                _rules.ToList().ForEach(r => r.IsDefault = false);
                selectedRule.IsDefault = true;
                dgvRules.Refresh();

                // 保存到数据库
                _db.Storageable(new GlobalSetting
                {
                    Key = "DefaultRuleId",
                    Value = selectedRule.Id.ToString()
                }).ExecuteCommand();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 保存规则启用状态
            _db.Storageable(new GlobalSetting
            {
                Key = "UseDefaultRule",
                Value = UseDefaultRule ? "1" : "0"
            }).ExecuteCommand();

            // 保存规则数据
            _db.Storageable(_rules.ToList()).ExecuteCommand();
            UpdateControls();

        }
    }
}
