using Dorisoy.AMS.models;
using Dorisoy.AMS.Utilities;
using Newtonsoft.Json;
using SqlSugar;
using System.ComponentModel;


namespace Dorisoy.AMS.view
{
    public partial class EditForm : Form
    {
        private Asset _asset;
        private bool _isEditMode;

        public EditForm(Asset asset = null)
        {

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            // 初始化核心字段
            _isEditMode = asset != null;
            _asset = asset ?? new Asset { Status = 0 };

            InitializeControls();
            LoadData(asset);
            
            // 注册快捷键
            this.KeyPreview = true;
            this.KeyDown += EditForm_KeyDown;
        }

        /// <summary>
        /// 快捷键处理：Ctrl+S 保存，Esc 取消
        /// </summary>
        private void EditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                Close();
            }
        }



        private void InitializeControls()
        {
            txtName.Leave += TxtName_Leave;
            cmbStatus.DataSource = StatusConfig.BusinessStatusOptions;
            cmbStatus.DisplayMember = "Name";
            cmbStatus.ValueMember = "Code";
            // 加载使用人列表
            LoadUserList();
            // 加载仓库列表
            LoadWarehouseList();
        }

        private void LoadData(Asset asset)
        {

            InitializeForm();
        }

        private void InitializeForm()
        {

            // 先加载部门数据
            LoadDepartmentList();

            // 数据绑定
            txtAssetID.Text = _asset.AssetID;
            txtName.Text = _asset.Name;
            txtModel.Text = _asset.Model;
            // 设置仓库选择
            if (_asset.WarehouseId > 0)
            {
                cmbLocation.SelectedValue = _asset.WarehouseId;
            }
            else if (!string.IsNullOrEmpty(_asset.Location))
            {
                cmbLocation.Text = _asset.Location;
            }
            cmbUser.SelectedItem = _asset.User; // 改为下拉框
            txtRemark.Text = _asset.Remark;
            cmbStatus.SelectedValue = _asset.Status;
            if (!_isEditMode)
            {
                txtCategory.Text = "电子电器";
                txtUnit.Text = "台";
                numQuantity.Value = 1;
                numMinQuantity.Value = 0;
            }
            else
            {
                txtCategory.Text = _asset.Category;
                txtUnit.Text = _asset.Unit;
                numQuantity.Value = _asset.Quantity; // 编辑模式才绑定数据
                numMinQuantity.Value = _asset.MinQuantity; // 最低库存
            }
            // 模式相关设置
            Text = _isEditMode ? "编辑资产" : "新增资产";
            cmbStatus.Enabled = _isEditMode;
            txtAssetID.Enabled = true;
            
            // 编辑模式下禁用库存数量编辑（已入库后不能修改，否则库存逻辑会出错）
            numQuantity.Enabled = !_isEditMode;
        }

        private async void TxtName_Leave(object sender, EventArgs e)
        {
            if (_isEditMode || string.IsNullOrWhiteSpace(txtName.Text))
            {
                return;
            }

            try
            {
                txtAssetID.Text = "生成中...";
                var newId = await Task.Run(() => AssetNumberGenerator.Generate(txtName.Text.Trim()));
                txtAssetID.Text = newId;
            }
            catch (Exception ex)
            {
                txtAssetID.Text = $"生成失败: {ex.Message}";
            }
        }

        private void LoadDepartmentList()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 获取所有有效部门（标准化处理）
                    var departments = db.Queryable<Asset>()
                        .Where(a => a.Status == 0)
                        .Select(a => a.Department.Trim()) // 去除数据库存储的空格
                        .Distinct()
                        .ToList();

                    // 如果是编辑模式，添加当前部门（标准化检查）
                    if (_isEditMode && !string.IsNullOrEmpty(_asset.Department))
                    {
                        var currentDept = _asset.Department.Trim();
                        // 使用不区分大小写的存在性检查
                        if (!departments.Any(d =>
                            d.Equals(currentDept, StringComparison.OrdinalIgnoreCase)))
                        {
                            departments.Add(currentDept);
                        }
                    }

                    // 添加提示项
                    departments.Insert(0, "--请选择或输入有效部门--");

                    // 配置cmbDepartment支持输入
                    cmbDepartment.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbDepartment.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbDepartment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    // 绑定数据源（使用BindingList实现动态更新）
                    var bindingList = new BindingList<string>(departments);
                    cmbDepartment.DataSource = bindingList;

                    // 设置默认选中项
                    if (_isEditMode)
                    {
                        cmbDepartment.SelectedItem = _asset.Department?.Trim();
                    }
                    else
                    {
                        cmbDepartment.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取部门列表失败：{ex.Message}");
                cmbDepartment.DataSource = new BindingList<string>(
                    new List<string> { "--请选择或输入有效部门--" });
            }
        }

        private void LoadUserList()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 获取所有用户名称
                    var users = db.Queryable<User>()
                        .Select(u => u.Username)
                        .ToList()
                        .OrderBy(u => u) // 在内存中排序
                        .ToList();

                    // 配置cmbUser支持下拉选择
                    cmbUser.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbUser.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    // 绑定数据源
                    var bindingList = new BindingList<string>(users);
                    cmbUser.DataSource = bindingList;

                    // 设置默认选中项
                    if (_isEditMode && !string.IsNullOrEmpty(_asset.User))
                    {
                        cmbUser.SelectedItem = _asset.User;
                    }
                    else
                    {
                        cmbUser.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取用户列表失败：{ex.Message}");
                cmbUser.DataSource = new BindingList<string>(new List<string>());
            }
        }

        /// <summary>
        /// 加载仓库列表
        /// </summary>
        private void LoadWarehouseList()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 获取所有正常状态的仓库
                    var warehouses = db.Queryable<Warehouse>()
                        .Where(w => w.Status == 0)
                        .OrderBy(w => w.Code)
                        .ToList();

                    // 配置cmbLocation支持下拉选择
                    cmbLocation.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbLocation.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbLocation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    // 绑定数据源
                    cmbLocation.DisplayMember = "Name";
                    cmbLocation.ValueMember = "Id";
                    cmbLocation.DataSource = warehouses;

                    // 设置默认选中项
                    if (_isEditMode && _asset.WarehouseId > 0)
                    {
                        cmbLocation.SelectedValue = _asset.WarehouseId;
                    }
                    else
                    {
                        cmbLocation.SelectedIndex = warehouses.Count > 0 ? 0 : -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取仓库列表失败：{ex.Message}");
                cmbLocation.DataSource = new List<Warehouse>();
            }
        }

        private bool ValidateInput()
        {
            // 资产编号校验
            if (string.IsNullOrWhiteSpace(txtAssetID.Text))
            {
                MessageBox.Show("资产编号不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAssetID.Focus();
                return false;
            }

            // 资产编号唯一性校验（新增模式）
            if (!_isEditMode)
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var existingAsset = db.Queryable<Asset>().First(a => a.AssetID == txtAssetID.Text.Trim());
                    if (existingAsset != null)
                    {
                        MessageBox.Show($"资产编号 '{txtAssetID.Text.Trim()}' 已存在，请使用不同的编号", "编号重复", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAssetID.Focus();
                        txtAssetID.SelectAll();
                        return false;
                    }
                }
            }

            // 资产名称校验
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("资产名称不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // 数量校验
            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("库存数量必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numQuantity.Focus();
                return false;
            }

            // 最低库存校验（不能为负数）
            if (numMinQuantity.Value < 0)
            {
                MessageBox.Show("最低库存不能为负数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numMinQuantity.Focus();
                return false;
            }

            // 仓库校验
            if (cmbLocation.SelectedIndex < 0 && string.IsNullOrWhiteSpace(cmbLocation.Text))
            {
                MessageBox.Show("请选择或输入存放地点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLocation.Focus();
                return false;
            }

            // 单位校验
            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("请输入单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnit.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                // 调用方式
                var originalAsset = DeepClone(_asset);
                // 标准化部门输入（关键修改）
                var department = cmbDepartment.Text.Trim();
                if (string.IsNullOrWhiteSpace(department) || department == "--请选择或输入有效部门--")
                {
                    MessageBox.Show("请选择或输入有效部门");
                    cmbDepartment.Focus();
                    return;
                }


                _asset.AssetID = txtAssetID.Text;
                _asset.Category = txtCategory.Text.Trim();
                _asset.Name = txtName.Text.Trim();
                _asset.Model = txtModel.Text;
                _asset.Quantity = (int)numQuantity.Value;
                _asset.MinQuantity = (int)numMinQuantity.Value;
                _asset.Unit = txtUnit.Text.Trim();
                
                // 保存仓库关联
                if (cmbLocation.SelectedValue != null && cmbLocation.SelectedValue is int warehouseId)
                {
                    _asset.WarehouseId = warehouseId;
                    var selectedWarehouse = cmbLocation.SelectedItem as Warehouse;
                    _asset.Location = selectedWarehouse?.Name ?? cmbLocation.Text.Trim();
                }
                else
                {
                    _asset.WarehouseId = 0;
                    _asset.Location = cmbLocation.Text.Trim();
                }
                
                _asset.Department = department;
                _asset.User = cmbUser.Text.Trim(); // 改为下拉框中的选中值
                _asset.Remark = txtRemark.Text.Trim();
                _asset.Status = (int)cmbStatus.SelectedValue;

                using (var db = SqliteHelper.GetDb())
                {
                    if (_isEditMode)
                    {
                        db.Updateable(_asset).ExecuteCommand();
                        var changes = GetChanges(originalAsset, _asset);
                        RecordLog("修改", _asset, changes);
                    }
                    else
                    {
                        db.Insertable(_asset).ExecuteCommand();
                        RecordLog("新增", _asset);
                        
                        // 新增资产时生成采购入库记录
                        CreatePurchaseInRecord(db, _asset);
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

        // 在类中添加日志记录方法
        private void RecordLog(string operationType, Asset asset, string details = "")
        {
            using (var db = SqliteHelper.GetDb())
            {
                var log = new Log
                {
                    OperationType = operationType,
                    OperationTime = DateTime.Now,
                    Operator = AppContext.CurrentUser.Username,
                    AssetNumber = asset.AssetID,
                    Details = $"资产名称：{asset.Name}  {details}"
                };

                db.Insertable(log).ExecuteCommand();
            }
        }

        private string GetChanges(Asset original, Asset updated)
        {
            var changes = new List<string>();
            if (original.Name != updated.Name)
            {
                changes.Add($"资产名称：{original.Name}=>{updated.Name}");
            }

            if (original.Category != updated.Category)
            {
                changes.Add($"资产类别：{original.Category}=>{updated.Category}");
            }

            if (original.Model != updated.Model)
            {
                changes.Add($"规格型号：{original.Model}=>{updated.Model}");
            }

            if (original.Quantity != updated.Quantity)
            {
                changes.Add($"数量：{original.Quantity}=>{updated.Quantity}");
            }

            if (original.MinQuantity != updated.MinQuantity)
            {
                changes.Add($"最低库存：{original.MinQuantity}=>{updated.MinQuantity}");
            }

            if (original.Unit != updated.Unit)
            {
                changes.Add($"单位：{original.Unit}=>{updated.Unit}");
            }

            if (original.Location != updated.Location)
            {
                changes.Add($"存放地点：{original.Location}=>{updated.Location}");
            }

            if (original.Department != updated.Department)
            {
                changes.Add($"部门：{original.Department}=>{updated.Department}");
            }

            if (original.User != updated.User)
            {
                changes.Add($"使用人：{original.User}=>{updated.User}");
            }

            if (original.Remark != updated.Remark)
            {
                changes.Add($"备注：{original.Remark}=>{updated.Remark}");
            }

            if (original.Status != updated.Status)
            {
                changes.Add($"状态：{StatusConfig.GetStatusName(original.Status)}=>{StatusConfig.GetStatusName(updated.Status)}");
            }

            return string.Join(", ", changes);
        }
        public static T DeepClone<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }




        private void UpdateAssetFromUI()
        {
            _asset.Category = txtCategory.Text;
            _asset.Name = txtName.Text;
            _asset.Model = txtModel.Text;
            _asset.Quantity = (int)numQuantity.Value;
            _asset.MinQuantity = (int)numMinQuantity.Value;
            _asset.Unit = txtUnit.Text;
            
            // 更新仓库关联
            if (cmbLocation.SelectedValue != null && cmbLocation.SelectedValue is int warehouseId)
            {
                _asset.WarehouseId = warehouseId;
                var selectedWarehouse = cmbLocation.SelectedItem as Warehouse;
                _asset.Location = selectedWarehouse?.Name ?? cmbLocation.Text;
            }
            else
            {
                _asset.WarehouseId = 0;
                _asset.Location = cmbLocation.Text;
            }
            
            _asset.Department = cmbDepartment.Text;
            _asset.User = cmbUser.Text; // 改为下拉框
            _asset.Remark = txtRemark.Text;

            if (_isEditMode && cmbStatus.SelectedItem is StatusConfig.StatusItem status)
            {
                _asset.Status = status.Code;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// 创建采购入库记录
        /// </summary>
        private void CreatePurchaseInRecord(SqlSugarClient db, Asset asset)
        {
            try
            {
                var stockRecord = new StockRecord
                {
                    AssetID = asset.AssetID,
                    AssetName = asset.Name,
                    WarehouseId = asset.WarehouseId,
                    WarehouseName = asset.Location,
                    RecordType = StockRecordType.In,
                    BusinessType = StockBusinessType.PurchaseIn,
                    Quantity = asset.Quantity,
                    BeforeQuantity = 0,  // 新增资产，之前库存为0
                    AfterQuantity = asset.Quantity,
                    RelatedId = asset.AssetID,
                    Operator = AppContext.CurrentUser?.Username ?? "系统",
                    Handler = asset.User,
                    RecordTime = DateTime.Now,
                    Remark = $"新增资产入库，资产编号：{asset.AssetID}"
                };

                db.Insertable(stockRecord).ExecuteCommand();
            }
            catch (Exception ex)
            {
                // 入库记录创建失败不影响资产新增，仅记录日志
                System.Diagnostics.Debug.WriteLine($"创建采购入库记录失败：{ex.Message}");
            }
        }
    }
}