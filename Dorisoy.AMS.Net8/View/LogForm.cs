using Dorisoy.AMS.models;
using Dorisoy.AMS.Services.ExcelService;
using SqlSugar;

namespace Dorisoy.AMS.view
{
    public partial class LogForm : Form
    {

        public LogForm()
        {
            InitializeComponent();
            InitializeForm();
            LoadLogs();
        }

        private void InitializeForm()
        {

            this.StartPosition = FormStartPosition.CenterScreen;
            //自动换行
            dgvLogs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // 操作类型下拉框
            cmbOperationType.Items.AddRange(new object[] { "全部", "新增", "修改" });
            cmbOperationType.SelectedIndex = 0;

            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpEnd.Value = DateTime.Now;

            ConfigureDataGridViewColumns();
        }

        private void ConfigureDataGridViewColumns()
        {
            dgvLogs.Columns.AddRange(new DataGridViewColumn[] {
                 new DataGridViewTextBoxColumn {
                    HeaderText = "序号",
                    DataPropertyName = "Id",
                    Width = 80
                },
                new DataGridViewTextBoxColumn {
                    HeaderText = "操作类型",
                    DataPropertyName = "OperationType",
                    Width = 80
                },
                new DataGridViewTextBoxColumn {
                    HeaderText = "操作时间",
                    DataPropertyName = "OperationTime",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle {
                        Format = "yyyy-MM-dd HH:mm:ss"
                    }
                },
                new DataGridViewTextBoxColumn {
                    HeaderText = "操作人",
                    DataPropertyName = "Operator",
                    Width = 100
                },
                new DataGridViewTextBoxColumn {
                    HeaderText = "资产编号",
                    DataPropertyName = "AssetNumber",
                    Width = 120
                },
                new DataGridViewTextBoxColumn {
                    HeaderText = "操作详情",
                    DataPropertyName = "Details",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    DefaultCellStyle = new DataGridViewCellStyle {
                    WrapMode = DataGridViewTriState.True // 启用自动换行
                     }
                }
            });
        }



        private void LoadLogs()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var query = db.Queryable<Log>()
                        .Where(l => l.OperationTime >= dtpStart.Value && l.OperationTime <= dtpEnd.Value);


                    var selectedType = cmbOperationType.SelectedItem.ToString();
                    if (selectedType != "全部")
                    {
                        query = query.Where(l => l.OperationType == selectedType);
                    }

                    var logs = query.OrderBy(l => l.OperationTime, OrderByType.Desc).ToList();
                    dgvLogs.DataSource = logs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载日志失败：{ex.Message}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnLogExport_Click(object sender, EventArgs e)
        {
            if (dgvLogs.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel文件|*.xlsx";
                sfd.FileName = $"日志清单_{DateTime.Now:yyyyMMddHHmm}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exporter = new LogExcelExporter();
                        exporter.LogExporterToExcel(dgvLogs.DataSource as List<Log>, sfd.FileName);

                        if (MessageBox.Show("导出成功，是否立即打开文件？",
                            "导出完成", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导出失败：{ex.Message}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
