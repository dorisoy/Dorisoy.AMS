using Dorisoy.AMS.models;
using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.view
{
    public partial class UserManagementForm : Form
    {
        private SqlSugarClient db;

        public UserManagementForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            db = SqliteHelper.GetDb();
            LoadUsers();
        }



        private void LoadUsers()
        {
            var users = db.Queryable<User>().ToList();
            dataGridView1.DataSource = users;

            // 设置中文列标题
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                var property = typeof(User).GetProperty(column.DataPropertyName);
                if (property != null)
                {
                    var displayName = property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                        .OfType<DisplayNameAttribute>()
                        .FirstOrDefault()?.DisplayName;

                    if (!string.IsNullOrEmpty(displayName))
                    {
                        column.HeaderText = displayName;
                    }
                }
            }

            // 隐藏密码列（可选）
            if (dataGridView1.Columns["Password"] != null)
            {
                dataGridView1.Columns["Password"].Visible = false;
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var form = new UserEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var user = (User)dataGridView1.CurrentRow.DataBoundItem;
            var form = new UserEditForm(user);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请先选择要删除的用户");
                return;
            }
            var activeUser = AppContext.CurrentUser;
            var user = (User)dataGridView1.CurrentRow.DataBoundItem;

            // 新增：检查是否为当前登录用户
            if (user.Username.Equals(activeUser.Username, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("不能删除当前登录的用户！",
                              "提示",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Stop);
                return;
            }

            // 确认对话框
            if (MessageBox.Show($"确定要删除用户 [{user.Username}] 吗？",
                              "删除确认",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // 执行删除
                    db.Deleteable(user).ExecuteCommand();

                    // 刷新列表
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}",
                                  "错误",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
        }


    }
}
