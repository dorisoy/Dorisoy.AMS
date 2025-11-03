using Dorisoy.AMS.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace Dorisoy.AMS.view
{
    public partial class BackupForm : Form
    {


        public BackupForm()
        {
            InitializeComponent();
            InitializeUI();
            InitializeBackupDirectory();
            LoadBackupList();
        }

        private void InitializeUI()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            dgvBackupList.AutoGenerateColumns = false;
            ConfigureGridColumns();
        }

        private void InitializeBackupDirectory()
        {
            try { Directory.CreateDirectory(BackupManager.BackupPath); }
            catch (Exception ex) { MessageBox.Show($"目录创建失败：{ex.Message}"); }
        }

        private void ConfigureGridColumns()
        {
            dgvBackupList.Columns.Clear();
            AddColumn("FileName", "文件名", 200);
            AddColumn("CreateTime", "创建时间", 150);
            AddColumn("FileSizeKB", "文件大小(KB)", 100);
            AddColumn("FullPath", "文件路径", 300);
        }

        private void AddColumn(string name, string header, int width)
        {
            dgvBackupList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = header,
                Width = width
            });
        }

        private void LoadBackupList() =>
            dgvBackupList.DataSource = BackupManager.GetBackupList();

        // 事件处理程序
        private void BtnBackup_Click(object sender, EventArgs e)
        {
            BackupManager.PerformBackup();
            LoadBackupList();
            MessageBox.Show("数据库备份成功！");
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (dgvBackupList.SelectedRows.Count == 0)
            {
                return;
            }

            var backupInfo = dgvBackupList.SelectedRows[0].DataBoundItem
                as BackupManager.BackupFileInfo;

            if (ValidatePassword())
            {
                BackupManager.RestoreBackup(backupInfo.FullPath);
                LoadBackupList();
                MessageBox.Show("数据库还原成功！");
            }
            else
            {
                MessageBox.Show("管理员密码错误,操作终止");
            }
        }

        private bool ValidatePassword()
        {
            using (var passwordForm = new PasswordInputForm())
            {
                if (passwordForm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                return SqliteHelper.ValidateAdminPassword(passwordForm.Password);
            }
        }

        private static void ClearDatabase()
        {
            string dbPath = "AssetDB.sqlite";

            try
            {
                // 关闭所有数据库连接
                SqliteHelper.GetDb().Dispose();

                // 删除数据库文件（如果存在）
                if (File.Exists(dbPath))
                {
                    File.Delete(dbPath);
                }

                // 重新初始化数据库
                SqliteHelper.InitDb();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("清空数据库失败：" + ex.Message, ex);
            }
        }



        private void btnClearDatabase_Click(object sender, EventArgs e)
        {

            // 弹出密码输入窗口
            using (var form = new PasswordInputForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 验证密码
                    if (SqliteHelper.ValidateAdminPassword(form.Password))
                    {
                        // 执行清空操作
                        try
                        {
                            ClearDatabase();
                            MessageBox.Show("数据库已重置为初始状态!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"重置失败：{ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("管理员密码错误,操作终止");
                    }
                }
            }
        }
    }
}
