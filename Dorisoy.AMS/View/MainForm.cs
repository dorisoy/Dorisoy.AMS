using Dorisoy.AMS.models;
using Dorisoy.AMS.services.ExcelService;
using Dorisoy.AMS.Utilities;
using FastReport;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Dorisoy.AMS.view
{
    public partial class MainForm : Form
    {

        private string _currentPrinter;
        private BackupManager _backupManager = new BackupManager();
        public MainForm()
        {
            InitializeComponent();

            // 验证用户登录状态1
            if (AppContext.CurrentUser == null)
            {
                MessageBox.Show("未检测到登录用户，程序将退出");
                System.Windows.Forms.Application.Exit();
                return;
            }
            // 清除旧的备份数据库
            BackupManager.CleanOldBackups(retainDays: 30);  // 自定义保留天数

            // 窗体初始化时触发备份检查  
            BackupManager.CheckAndPerformStartupBackup();
            // 设置当前线程的文化信息为中文（中国）
            CultureInfo.CurrentCulture = new CultureInfo("zh-CN");
            CultureInfo.CurrentUICulture = new CultureInfo("zh-CN");
            // 设置窗口居中
            this.StartPosition = FormStartPosition.CenterScreen;

            ApplyPermissions();
            LoadPrinterSettings();
            InitializeComponents();
            LoadData();

        }




        // 应用用户权限
        private void ApplyPermissions()
        {
            var user = AppContext.CurrentUser;

            // 添加空值检查
            if (user == null)
            {
                return;
            }

            btnAdd.Enabled = user.CanAdd;
            btnEdit.Enabled = user.CanEdit;
            btnImport.Enabled = user.CanImport;
            btnPrint.Enabled = user.CanPrint;
            btnExport.Enabled = user.CanExport;
            btnManageNumber.Enabled = user.CanManageNumber;
            btnManageLog.Enabled = user.CanManageLog;
            btnManageData.Enabled = user.CanManageData;
            menuUserManagement.Visible = user.IsAdmin;

        }

        // 添加用户管理菜单项
        private void menuUserManagement_Click(object sender, EventArgs e)
        {
            var form = new UserManagementForm();
            form.ShowDialog();
        }

        // 加载打印机设置
        private void LoadPrinterSettings()
        {
            // 读取已保存的打印机设置
            _currentPrinter = Properties.Settings.Default.DefaultPrinter;

            // 验证打印机是否存在
            if (!IsPrinterValid(_currentPrinter))
            {
                // 回退到默认打印机
                _currentPrinter = new PrinterSettings().PrinterName;
                Properties.Settings.Default.DefaultPrinter = _currentPrinter;
                Properties.Settings.Default.Save();
            }
        }

        // 验证打印机是否存在
        private bool IsPrinterValid(string printerName)
        {
            return PrinterSettings.InstalledPrinters
                .Cast<string>()
                .Any(p => p.Equals(printerName, StringComparison.OrdinalIgnoreCase));
        }

        private void InitializeComponents()
        {
            LoadDepartments();
            InitializeDataGridView();
            LoadStatusOptions();
        }

        // 初始化组件
        private void InitializeDataGridView()
        {

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIndex",
                HeaderText = "序号",
                Width = 60,
                ReadOnly = true  // 设置为只读
            });

            var columns = new[]
            {
                new { Name = "colAssetID", Prop = "AssetID", Header = "资产编号", Width = 120 },
                new { Name = "colCategory", Prop = "Category", Header = "资产类别", Width = 100 },
                new { Name = "colAssetName", Prop = "Name", Header = "资产名称", Width = 150 },
                new { Name = "colModel", Prop = "Model", Header = "规格型号", Width = 120 },
                new { Name = "colQuantity", Prop = "Quantity", Header = "数量", Width = 60 },
                new { Name = "colUnit", Prop = "Unit", Header = "单位", Width = 60 },
                new { Name = "colLocation", Prop = "Location", Header = "存放地点", Width = 150 },
                new { Name = "colDepartment", Prop = "Department", Header = "责任部门", Width = 120 },
                new { Name = "colUser", Prop = "User", Header = "使用人", Width = 100 },
                new { Name = "colStatus", Prop = "Status", Header = "状态", Width = 80 },
                new { Name = "colRemark", Prop = "Remark", Header = "备注", Width = 200 }
            };

            dataGridView1.Columns.AddRange(columns.Select(c => new DataGridViewTextBoxColumn
            {
                Name = c.Name,
                DataPropertyName = c.Prop,
                HeaderText = c.Header,
                Width = c.Width
            }).ToArray());

            dataGridView1.Font = new System.Drawing.Font("微软雅黑", 9);

            //生成序号
            dataGridView1.DataBindingComplete += (sender, e) =>
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells["colIndex"].Value = row.Index + 1;
                    }
                }
            };
        }




        // 加载数据
        private void LoadData(string search = "")
        {
            var searchKey = txtSearch.Text.Trim();
            var selectedDept = cmbDepartment.Text?.Trim() ?? "全部部门";
            var selectedStatus = cmbStatus.SelectedItem as StatusConfig.StatusItem;
            var statusCode = selectedStatus?.Code ?? -1;

            using (var db = SqliteHelper.GetDb())
            {
                var query = db.Queryable<Asset>()
                    .WhereIF(!string.IsNullOrEmpty(selectedDept) && selectedDept != "全部部门",
                        a => a.Department == selectedDept)
                    .WhereIF(statusCode != -1,
                        a => a.Status == statusCode)
                    .WhereIF(!string.IsNullOrWhiteSpace(searchKey),
                        a => (a.AssetID ?? "").Contains(searchKey) ||
                       (a.Name ?? "").Contains(searchKey) ||
                       (a.Model ?? "").Contains(searchKey) ||
                       (a.Location ?? "").Contains(searchKey) ||
                       (a.Department ?? "").Contains(searchKey) ||
                       (a.User ?? "").Contains(searchKey))
                    .OrderBy(a => a.AssetID);

                dataGridView1.DataSource = query.ToList();
            }
        }





        // 初始化部门列表
        private void LoadDepartments()
        {
            using (var db = SqliteHelper.GetDb())
            {
                var depts = db.Queryable<Asset>()
                    .GroupBy(a => a.Department)
                    .Select(a => a.Department)
                    .ToList();

                cmbDepartment.Items.Clear();
                cmbDepartment.Items.Add("全部部门");
                cmbDepartment.Items.AddRange(depts.ToArray());
                cmbDepartment.SelectedIndex = 0;
            }
        }

        // 添加按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new EditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDepartments();
            }

            LoadData();
        }

        // 编辑按钮
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            var asset = (Asset)dataGridView1.CurrentRow.DataBoundItem;
            var form = new EditForm(asset);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDepartments();
            }

            LoadData();
        }




        // 导入按钮点击事件
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel文件|*.xls;*.xlsx";
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // 获取数据库已存在的资产编号
                var existingIds = new List<string>();
                using (var db = SqliteHelper.GetDb())
                {
                    existingIds = db.Queryable<Asset>()
                        .Select(a => a.AssetID)
                        .ToList();
                }

                var importer = new ExcelImporter(existingIds);
                var (validAssets, errors) = importer.Import(ofd.FileName);

                // 错误处理（显示前5条错误）
                if (errors.Any())
                {
                    var errorMsg = new StringBuilder()
                        .AppendLine($"成功导入：{validAssets.Count} 条")
                        .AppendLine($"失败：{errors.Count} 条")
                        .AppendLine("------------------------------");

                    foreach (var error in errors.Take(5))
                    {
                        errorMsg.AppendLine($"• {error}");
                    }

                    if (errors.Count > 5)
                    {
                        errorMsg.AppendLine($"......（共 {errors.Count} 条错误）");
                    }

                    MessageBox.Show(errorMsg.ToString(), "导入结果",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                }

                // 数据插入（事务处理）
                if (validAssets.Any())
                {
                    try
                    {
                        using (var db = SqliteHelper.GetDb())
                        {
                            db.Insertable(validAssets).ExecuteCommand();
                        }
                        MessageBox.Show($"成功导入 {validAssets.Count} 条数据", "导入成功");
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"保存失败：{ex.Message}", "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

            LoadDepartments();
        }



        //打印资产标签
        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    var selectedAssets = dataGridView1.SelectedRows
        //        .Cast<DataGridViewRow>()
        //        .Select(row => (Asset)row.DataBoundItem)
        //        .ToList();

        //    if (selectedAssets.Count == 0)
        //    {
        //        MessageBox.Show("请先选择要打印的资产");
        //        return;
        //    }

        //    // 创建打印机选择对话框
        //    using (PrintDialog printDialog = new PrintDialog())
        //    {
        //        // 设置默认打印机
        //        printDialog.PrinterSettings.PrinterName = _currentPrinter;

        //        // 关键修改：只有当用户点击OK时才执行打印
        //        if (printDialog.ShowDialog() != DialogResult.OK)
        //        {
        //            return; // 用户取消则直接退出
        //        }
        //        // 保存新选择的打印机
        //        _currentPrinter = printDialog.PrinterSettings.PrinterName;
        //        Properties.Settings.Default.DefaultPrinter = _currentPrinter;
        //        Properties.Settings.Default.Save();


        //        string selectedPrinter = printDialog.PrinterSettings.PrinterName;

        //        // 后续打印代码（已修改支持动态打印机设置）
        //        string appPath = System.Windows.Forms.Application.StartupPath;
        //        string templatePath = Path.Combine(appPath, "Templates", "AssetLabel.btw");

        //        if (!File.Exists(templatePath))
        //        {
        //            MessageBox.Show($"标签模板文件未找到：\n{templatePath}");
        //            return;
        //        }

        //        BarTender.Application btApp = null;
        //        try
        //        {
        //            btApp = new BarTender.Application();
        //            btApp.Visible = false;

        //            var btFormat = btApp.Formats.Open(templatePath, false, "");

        //            // 应用用户选择的打印机
        //            btFormat.Printer = selectedPrinter;
        //            btFormat.PrintSetup.IdenticalCopiesOfLabel = 1;

        //            foreach (var asset in selectedAssets)
        //            {
        //                btFormat.SetNamedSubStringValue("AssetID", asset.AssetID);
        //                btFormat.SetNamedSubStringValue("Category", asset.Category);
        //                btFormat.SetNamedSubStringValue("Name", asset.Name);
        //                btFormat.SetNamedSubStringValue("Model", asset.Model);
        //                btFormat.SetNamedSubStringValue("Location", asset.Location);
        //                btFormat.SetNamedSubStringValue("Department", asset.Department);

        //                // 使用静默打印
        //                btFormat.PrintOut(false, false);
        //            }

        //            btFormat.Close(BtSaveOptions.btDoNotSaveChanges);
        //        }
        //        catch (COMException ex)
        //        {
        //            MessageBox.Show($"打印失败：{ex.Message}\n请确认：\n1. BarTender 10.1已安装\n2. 打印机[{selectedPrinter}]状态正常");
        //        }
        //        finally
        //        {
        //            if (btApp != null)
        //            {
        //                btApp.Quit(BtSaveOptions.btDoNotSaveChanges);
        //                Marshal.ReleaseComObject(btApp);
        //            }
        //        }
        //    }
        //}
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // 先进行数据校验
            var selectedAssets = GetSelectedAssets();
            if (selectedAssets.Count == 0)
            {
                MessageBox.Show("请先选择资产！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 打印机选择流程
            string selectedPrinter;
            using (var printDialog = new PrintDialog())
            {
                // 设置默认打印机（如果存在）
                if (!string.IsNullOrEmpty(_currentPrinter) && PrinterExists(_currentPrinter))
                {
                    printDialog.PrinterSettings.PrinterName = _currentPrinter;
                }

                if (printDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                selectedPrinter = printDialog.PrinterSettings.PrinterName;
                _currentPrinter = selectedPrinter;
                Properties.Settings.Default.DefaultPrinter = _currentPrinter;
                Properties.Settings.Default.Save();
            }

            try
            {
                using (Report report = new Report())
                {
                    // 加载报表模板
                    string reportPath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Reports",
                        "AssetLabels60x30.frx");

                    if (!File.Exists(reportPath))
                    {
                        MessageBox.Show($"报表模板未找到：{reportPath}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    report.Load(reportPath);

                    // 数据绑定
                    DataTable labelData = GetSelectedAssetsAsDataTable(selectedAssets);
                    report.RegisterData(labelData, "MyData");
                    report.GetDataSource("MyData").Enabled = true;

                    // 设置打印机
                    report.PrintSettings.Printer = selectedPrinter;

                    // 打印选项
                    if (MessageBox.Show("是否预览？", "打印选项",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        report.Show();
                    }
                    else
                    {
                        report.PrintSettings.ShowDialog = false;
                        report.Print();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"打印失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        // 检查打印机是否存在
        private bool PrinterExists(string printerName)
        {
            return PrinterSettings.InstalledPrinters
                .Cast<string>()
                .Any(name => name.Equals(printerName, StringComparison.OrdinalIgnoreCase));
        }
        private List<string> GetSelectedIds()
        {
            return dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                .Select(row => ((Asset)row.DataBoundItem).AssetID)
                .ToList();
        }
        // 公共方法：更新资产状态
        private void UpdateAssetStatus(List<string> ids, int status)
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    db.Updateable<Asset>()
                        .SetColumns(a => a.Status == status)
                        .Where(a => ids.Contains(a.AssetID))
                        .ExecuteCommand();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"操作失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "colStatus")
            {
                // 安全类型转换
                if (e.Value is int statusCode)
                {
                    // 精确匹配状态码
                    var statusItem = StatusConfig.AllStatusOptions
                        .FirstOrDefault(s => s.Code == statusCode);

                    // 显示逻辑处理
                    e.Value = statusItem?.Name ?? $"未知状态({statusCode})";
                    e.FormattingApplied = true; // 标记已处理格式
                }
                else
                {
                    e.Value = "数据格式错误";
                }
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("没有可导出的数据");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel文件|*.xlsx";
                sfd.FileName = $"资产清单_{DateTime.Now:yyyyMMddHHmm}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var exporter = new ExcelExporter();
                        exporter.ExportToExcel(dataGridView1.DataSource as List<Asset>, sfd.FileName);

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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadStatusOptions()
        {
            try
            {
                cmbStatus.BeginUpdate();
                cmbStatus.DisplayMember = "Name";
                cmbStatus.ValueMember = "Code";

                // 数据源类型转换确保兼容性
                cmbStatus.DataSource = StatusConfig.AllStatusOptions.ToList();

                // 安全查找默认项索引
                var defaultItem = StatusConfig.AllStatusOptions
                    .FirstOrDefault(s => s.Code == 0);
                cmbStatus.SelectedIndex = defaultItem != null ?
                    StatusConfig.AllStatusOptions.IndexOf(defaultItem) : 0;
            }
            catch (Exception ex)
            {
                // 统一错误处理（根据实际环境选择）
                MessageBox.Show($"状态加载错误: {ex.Message}");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            finally
            {
                cmbStatus.EndUpdate();
            }
        }



        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnManageNumber_Click(object sender, EventArgs e)
        {
            using (var configForm = new RuleConfigForm())
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // 只有当点击保存时才刷新
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnManageLog_Click(object sender, EventArgs e)
        {
            new LogForm().ShowDialog();

        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            var MachineCode = Utilities.LicenseManager.GenerateMachineCode();
            new RegistrationForm(MachineCode).ShowDialog();
        }

        private void btnManageData_Click(object sender, EventArgs e)
        {
            new BackupForm().ShowDialog();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedAssets = GetSelectedAssets();
                if (selectedAssets.Count == 0)
                {
                    // 优化提示信息，并使用MessageBoxIcon.Warning图标
                    MessageBox.Show("请先选择资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // 终止后续操作
                }

                using (Report report = new Report())
                {
                    string reportPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Reports", "AssetLabels.frx");
                    report.Load(reportPath);

                    // 动态数据绑定
                    DataTable labelData = GetSelectedAssetsAsDataTable(selectedAssets);
                    report.RegisterData(labelData, "MyData");
                    report.GetDataSource("MyData").Enabled = true;

                    // 预览调试（可选）
                    if (MessageBox.Show("是否预览？", "打印选项", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        report.Show();
                    }
                    else
                    {
                        PrintDialog printDialog = new PrintDialog();
                        if (printDialog.ShowDialog() == DialogResult.OK)
                        {
                            report.PrintSettings.Printer = printDialog.PrinterSettings.PrinterName;
                            report.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打印失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Asset> GetSelectedAssets()
        {
            return dataGridView1.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow && row.DataBoundItem != null) // 过滤空行
                .Select(row => row.DataBoundItem as Asset)
                .Where(asset => asset != null) // 类型验证
                .ToList();
        }

        private DataTable GetSelectedAssetsAsDataTable(List<Asset> selectedAssets)
        {
            // 动态创建DataTable结构
            var dt = new DataTable();
            var properties = typeof(Asset).GetProperties();

            // 创建列结构（处理可空类型）
            foreach (var prop in properties)
            {
                dt.Columns.Add(
                    prop.Name,
                    Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType
                );
            }

            // 填充数据行（自动处理null值）
            selectedAssets.ForEach(asset =>
            {
                var row = dt.NewRow();
                properties.ToList().ForEach(prop =>
                {
                    var value = prop.GetValue(asset);
                    row[prop.Name] = value ?? DBNull.Value;
                });
                dt.Rows.Add(row);
            });

            return dt;
        }
    }







}


