using Dorisoy.AMS.models;
using Dorisoy.AMS.services.ExcelService;
using Dorisoy.AMS.Utilities;
using FastReport;
using SqlSugar;
using System.Configuration;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Text;

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
            
            // 注册快捷键
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
        }

        /// <summary>
        /// 快捷键处理：F5刷新、Delete删除、Ctrl+F搜索
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                e.SuppressKeyPress = true;
                LoadData();
            }
            else if (e.KeyCode == Keys.Delete && dataGridView1.SelectedRows.Count > 0)
            {
                e.SuppressKeyPress = true;
                DeleteSelectedAsset();
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                e.SuppressKeyPress = true;
                txtSearch.Focus();
                txtSearch.SelectAll();
            }
        }

        /// <summary>
        /// 删除选中的资产（软删除）
        /// </summary>
        private void DeleteSelectedAsset()
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            // 从投影类型中提取AssetID
            var assetIds = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.IsNewRow || row.DataBoundItem == null) continue;
                dynamic item = row.DataBoundItem;
                assetIds.Add(item.AssetID);
            }

            if (assetIds.Count == 0) return;

            var msg = assetIds.Count == 1 
                ? $"确定要删除选中的资产吗？"
                : $"确定要删除选中的 {assetIds.Count} 个资产吗？";

            if (MessageBox.Show(msg, "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    foreach (var assetId in assetIds)
                    {
                        var asset = db.Queryable<Asset>().First(a => a.AssetID == assetId);
                        if (asset == null) continue;

                        // 软删除：将状态改为1(删除)
                        asset.Status = 1;
                        db.Updateable(asset).ExecuteCommand();

                        // 记录日志
                        var log = new Log
                        {
                            OperationType = "删除",
                            OperationTime = DateTime.Now,
                            Operator = AppContext.CurrentUser?.Username ?? "系统",
                            AssetNumber = asset.AssetID,
                            Details = $"资产名称：{asset.Name}"
                        };
                        db.Insertable(log).ExecuteCommand();
                    }
                }

                MessageBox.Show($"成功删除 {assetIds.Count} 个资产", "删除成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            // 管理员拥有所有权限
            bool isAdmin = user.IsAdmin;

            // ==================== 基础操作权限 ====================
            btnAdd.Enabled = isAdmin || user.CanAdd;
            btnEdit.Enabled = isAdmin || user.CanEdit;
            btnImport.Enabled = isAdmin || user.CanImport;
            btnExport.Enabled = isAdmin || user.CanExport;
            btnPrint.Enabled = isAdmin || user.CanPrint;
            toolStripButton1.Enabled = isAdmin || user.CanPrint;  // A4打印

            // ==================== 业务功能权限 ====================
            toolStripButton2.Enabled = isAdmin || user.CanBorrow;  // 借还登记
            toolStripButton3.Enabled = isAdmin || user.CanViewStockRecords;  // 库存记录
            btnScrap.Enabled = isAdmin || user.CanScrap;  // 报损登记
            btnInventoryCheck.Enabled = isAdmin || user.CanInventoryCheck;  // 盘点管理
            btnStockReport.Enabled = isAdmin || user.CanViewStockReport;  // 库存报表

            // ==================== 系统管理权限 ====================
            btnManageNumber.Enabled = isAdmin || user.CanManageNumber;  // 编号设置
            btnManageLog.Enabled = isAdmin || user.CanManageLog;  // 日志管理
            btnManageData.Enabled = isAdmin || user.CanManageData;  // 数据库管理
            btnWarehouse.Enabled = isAdmin || user.CanManageWarehouse;  // 仓库管理
            menuUserManagement.Visible = isAdmin || user.CanManageUsers;  // 用户管理

            // 更新工具栏提示信息
            UpdateToolbarTooltips(user);
        }

        /// <summary>
        /// 更新工具栏按钮提示信息
        /// </summary>
        private void UpdateToolbarTooltips(User user)
        {
            if (user == null) return;

            // 对于禁用的按钮，显示权限不足的提示
            SetTooltipForDisabledButton(btnAdd, "添加资产", user.IsAdmin || user.CanAdd);
            SetTooltipForDisabledButton(btnEdit, "编辑资产", user.IsAdmin || user.CanEdit);
            SetTooltipForDisabledButton(btnImport, "导入资产", user.IsAdmin || user.CanImport);
            SetTooltipForDisabledButton(btnExport, "导出资产", user.IsAdmin || user.CanExport);
            SetTooltipForDisabledButton(btnPrint, "打印标签", user.IsAdmin || user.CanPrint);
            SetTooltipForDisabledButton(toolStripButton1, "A4打印", user.IsAdmin || user.CanPrint);
            SetTooltipForDisabledButton(toolStripButton2, "借还登记", user.IsAdmin || user.CanBorrow);
            SetTooltipForDisabledButton(toolStripButton3, "库存记录", user.IsAdmin || user.CanViewStockRecords);
            SetTooltipForDisabledButton(btnScrap, "报损登记", user.IsAdmin || user.CanScrap);
            SetTooltipForDisabledButton(btnInventoryCheck, "盘点管理", user.IsAdmin || user.CanInventoryCheck);
            SetTooltipForDisabledButton(btnStockReport, "库存报表", user.IsAdmin || user.CanViewStockReport);
            SetTooltipForDisabledButton(btnManageNumber, "编号设置", user.IsAdmin || user.CanManageNumber);
            SetTooltipForDisabledButton(btnManageLog, "日志管理", user.IsAdmin || user.CanManageLog);
            SetTooltipForDisabledButton(btnManageData, "数据库管理", user.IsAdmin || user.CanManageData);
            SetTooltipForDisabledButton(btnWarehouse, "仓库管理", user.IsAdmin || user.CanManageWarehouse);
        }

        /// <summary>
        /// 设置禁用按钮的提示信息
        /// </summary>
        private void SetTooltipForDisabledButton(ToolStripButton button, string functionName, bool hasPermission)
        {
            button.ToolTipText = hasPermission ? functionName : $"{functionName}（无权限）";
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
            try
            {
                // 使用AppSettings替代Properties.Settings
                // _currentPrinter = Properties.Settings.Default.DefaultPrinter;
                _currentPrinter = ConfigurationManager.AppSettings["DefaultPrinter"] ?? string.Empty;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载打印机设置失败: {ex.Message}");
                _currentPrinter = string.Empty;
            }
        }

        private void SavePrinterSettings()
        {
            try
            {
                // 使用AppSettings替代Properties.Settings
                // Properties.Settings.Default.DefaultPrinter = _currentPrinter;
                // Properties.Settings.Default.Save();

                // 注意：ConfigurationManager.AppSettings是只读的，我们需要使用其他方式保存设置
                // 这里可以考虑使用一个配置文件或者数据库来保存打印机设置
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"保存打印机设置失败: {ex.Message}");
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
                new { Name = "colQuantity", Prop = "Quantity", Header = "库存数量", Width = 70 },
                new { Name = "colMinQuantity", Prop = "MinQuantity", Header = "最低库存", Width = 70 },
                new { Name = "colUnit", Prop = "Unit", Header = "单位", Width = 60 },
                new { Name = "colAvailableQuantity", Prop = "AvailableQuantity", Header = "可用库存", Width = 80 },
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
                var assets = db.Queryable<Asset>()
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
                    .OrderBy(a => a.AssetID)
                    .ToList();

                // 计算每个资产的可用库存
                var assetsWithAvailable = assets.Select(a => new
                {
                    a.AssetID,
                    a.Category,
                    a.Name,
                    a.Model,
                    a.Quantity,
                    a.Unit,
                    AvailableQuantity = a.Quantity - db.Queryable<BorrowRecord>()
                        .Where(r => r.AssetID == a.AssetID && r.Status == 0)
                        .Sum(r => r.BorrowedQuantity),
                    a.Location,
                    a.Department,
                    a.User,
                    a.Status,
                    a.Remark
                }).ToList();

                dataGridView1.DataSource = assetsWithAvailable;
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

            try
            {
                // 介于投影类型中提取资产ID
                dynamic row = dataGridView1.CurrentRow.DataBoundItem;
                string assetID = row.AssetID;

                // 从数据库重新查询完整的Asset对象
                using (var db = SqliteHelper.GetDb())
                {
                    var asset = db.Queryable<Asset>().First(a => a.AssetID == assetID);
                    var form = new EditForm(asset);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadDepartments();
                    }
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"编辑失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedAssets = GetSelectedAssets();
                if (selectedAssets.Count == 0)
                {
                    MessageBox.Show("请先选择资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 检查用户权限
                if (!AppContext.CurrentUser.CanPrint)
                {
                    MessageBox.Show("您没有打印权限，请联系管理员", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PrintAssets(selectedAssets, "AssetLabels60x30.frx", "60x30mm标签打印");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打印失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                .Select(row => ((dynamic)row.DataBoundItem).AssetID as string)
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
            if (e.RowIndex < 0) return;

            // 状态列格式化
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

            // 库存预警高亮显示（库存数量低于最低库存时红色高亮）
            var row = dataGridView1.Rows[e.RowIndex];
            var dataBoundItem = row.DataBoundItem;
            if (dataBoundItem != null)
            {
                var asset = dataBoundItem as Asset;
                if (asset != null && asset.MinQuantity > 0 && asset.Quantity <= asset.MinQuantity)
                {
                    // 库存预警：当前库存低于或等于最低库存时，整行红色背景
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    // 正常库存，恢复默认样式
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
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

        /// <summary>
        /// 仓库管理
        /// </summary>
        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            var form = new WarehouseForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 报损登记
        /// </summary>
        private void btnScrap_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要报损的资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("请选择单个资产进行报损登记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 从投影类型中提取AssetID
            var row = dataGridView1.SelectedRows[0];
            if (row.IsNewRow || row.DataBoundItem == null) return;
            
            dynamic item = row.DataBoundItem;
            string assetId = item.AssetID;

            // 从数据库查询完整的Asset对象
            using (var db = SqliteHelper.GetDb())
            {
                var asset = db.Queryable<Asset>().First(a => a.AssetID == assetId);
                if (asset == null)
                {
                    MessageBox.Show("资产不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (asset.Quantity <= 0)
                {
                    MessageBox.Show("该资产库存为0，无法报损！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var form = new ScrapForm(asset);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        /// <summary>
        /// 盘点管理
        /// </summary>
        private void btnInventoryCheck_Click(object sender, EventArgs e)
        {
            var form = new InventoryCheckForm();
            form.ShowDialog();
            LoadData();  // 盘点后刷新数据
        }

        /// <summary>
        /// 库存报表
        /// </summary>
        private void btnStockReport_Click(object sender, EventArgs e)
        {
            var form = new StockReportForm();
            form.ShowDialog();
        }


        // A4打印
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedAssets = GetSelectedAssets();
                if (selectedAssets.Count == 0)
                {
                    MessageBox.Show("请先选择资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 检查用户权限
                if (!AppContext.CurrentUser.CanPrint)
                {
                    MessageBox.Show("您没有打印权限，请联系管理员", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PrintAssets(selectedAssets, "AssetLabels.frx", "A4标签打印");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A4打印失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// 打印资产方法
        /// </summary>
        private void PrintAssets(List<Asset> assets, string reportFileName, string printTypeName)
        {
            try
            {
                // 检查报表文件
                string reportPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Reports", reportFileName);

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show($"报表文件不存在：{reportPath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var report = new Report())
                {
                    // 加载报表
                    report.Load(reportPath);

                    // 准备数据
                    DataTable dt = GetSelectedAssetsAsDataTable(assets);
                    report.RegisterData(dt, "MyData");

                    var dataSource = report.GetDataSource("MyData");
                    if (dataSource != null)
                    {
                        dataSource.Enabled = true;
                    }

                    // 准备报表
                    if (!report.Prepare())
                    {
                        MessageBox.Show("报表准备失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 显示打印选项
                    var result = MessageBox.Show(
                        $"准备打印{assets.Count}条资产\n\n是否继续？",
                        printTypeName,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show($"{printTypeName}已提交至打印机", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{printTypeName}错误：{ex.Message}\n{ex.InnerException?.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 借用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var form = new BorrowRecordsForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        /// <summary>
        /// 库存记录（用于记录资产出入流水）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var form = new StockRecordsForm();
            form.ShowDialog();
        }
    }

}


