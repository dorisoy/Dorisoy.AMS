using Dorisoy.AMS.models;
using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.view
{
    /// <summary>
    /// 库存盘点管理窗体
    /// </summary>
    public partial class InventoryCheckForm : Form
    {
        private BindingList<InventoryCheckItem> _checkItems = new BindingList<InventoryCheckItem>();

        public InventoryCheckForm()
        {
            InitializeComponent();
            InitializeControls();
            LoadWarehouseList();
            LoadData();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeControls()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _checkItems;
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
                    var warehouses = db.Queryable<Warehouse>()
                        .Where(w => w.Status == 0)
                        .OrderBy(w => w.Code)
                        .ToList();

                    // 添加"全部仓库"选项
                    var allItem = new Warehouse { Id = 0, Name = "全部仓库" };
                    warehouses.Insert(0, allItem);

                    cmbWarehouse.DisplayMember = "Name";
                    cmbWarehouse.ValueMember = "Id";
                    cmbWarehouse.DataSource = warehouses;
                    cmbWarehouse.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载仓库列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载盘点数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                _checkItems.Clear();

                using (var db = SqliteHelper.GetDb())
                {
                    var warehouseId = cmbWarehouse.SelectedValue as int? ?? 0;

                    var assets = db.Queryable<Asset>()
                        .Where(a => a.Status == 0)  // 正常状态
                        .WhereIF(warehouseId > 0, a => a.WarehouseId == warehouseId)
                        .OrderBy(a => a.Location)
                        .OrderBy(a => a.AssetID)
                        .ToList();

                    foreach (var asset in assets)
                    {
                        _checkItems.Add(new InventoryCheckItem
                        {
                            AssetID = asset.AssetID,
                            AssetName = asset.Name,
                            Warehouse = asset.Location,
                            WarehouseId = asset.WarehouseId,
                            SystemQuantity = asset.Quantity,
                            ActualQuantity = asset.Quantity,  // 默认等于系统库存
                            Difference = 0,
                            Remark = string.Empty
                        });
                    }
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 单元格编辑完成时计算差异
        /// </summary>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _checkItems.Count) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "colActualQuantity")
            {
                var item = _checkItems[e.RowIndex];
                item.Difference = item.ActualQuantity - item.SystemQuantity;
                dataGridView1.InvalidateRow(e.RowIndex);
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 自动盘点：一键将实际盘点数量填充为系统库存（即无差异）
        /// </summary>
        private void btnAutoCheck_Click(object sender, EventArgs e)
        {
            if (_checkItems.Count == 0)
            {
                MessageBox.Show("没有可盘点的资产！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "自动盘点将把所有资产的“实际盘点”数量设置为“系统库存”，\n表示实际库存与系统记录一致。\n\n是否继续？",
                "自动盘点",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            foreach (var item in _checkItems)
            {
                item.ActualQuantity = item.SystemQuantity;
                item.Difference = 0;
            }

            dataGridView1.Refresh();
            MessageBox.Show($"已自动填充 {_checkItems.Count} 条资产的盘点数量！", "自动盘点完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 仓库选择改变
        /// </summary>
        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 保存盘点结果
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 找出有差异的记录
                var changedItems = _checkItems.Where(i => i.Difference != 0).ToList();

                if (changedItems.Count == 0)
                {
                    MessageBox.Show("没有需要调整的库存记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var message = $"共有 {changedItems.Count} 条记录存在差异：\n" +
                              $"- 盘盈（实际 > 系统）：{changedItems.Count(i => i.Difference > 0)} 条\n" +
                              $"- 盘亏（实际 < 系统）：{changedItems.Count(i => i.Difference < 0)} 条\n\n" +
                              "确定要保存盘点结果吗？";

                if (MessageBox.Show(message, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var db = SqliteHelper.GetDb())
                {
                    foreach (var item in changedItems)
                    {
                        // 获取资产
                        var asset = db.Queryable<Asset>().First(a => a.AssetID == item.AssetID);
                        if (asset == null) continue;

                        // 创建库存记录
                        var stockRecord = new StockRecord
                        {
                            AssetID = asset.AssetID,
                            AssetName = asset.Name,
                            WarehouseId = asset.WarehouseId,
                            WarehouseName = asset.Location,
                            BeforeQuantity = asset.Quantity,
                            AfterQuantity = item.ActualQuantity,
                            RelatedId = $"盘点_{DateTime.Now:yyyyMMddHHmmss}",
                            Operator = AppContext.CurrentUser?.Username ?? "系统",
                            Handler = AppContext.CurrentUser?.Username ?? "系统",
                            RecordTime = DateTime.Now,
                            Remark = item.Remark
                        };

                        if (item.Difference > 0)
                        {
                            // 盘盈入库
                            stockRecord.RecordType = StockRecordType.In;
                            stockRecord.BusinessType = StockBusinessType.InventoryProfitIn;
                            stockRecord.Quantity = item.Difference;
                            stockRecord.Remark = $"盘盈入库，差异：+{item.Difference}；" + item.Remark;
                        }
                        else
                        {
                            // 盘亏出库
                            stockRecord.RecordType = StockRecordType.Out;
                            stockRecord.BusinessType = StockBusinessType.InventoryLossOut;
                            stockRecord.Quantity = item.Difference;  // 负数
                            stockRecord.Remark = $"盘亏出库，差异：{item.Difference}；" + item.Remark;
                        }

                        db.Insertable(stockRecord).ExecuteCommand();

                        // 更新资产库存
                        asset.Quantity = item.ActualQuantity;
                        db.Updateable(asset).ExecuteCommand();

                        // 记录操作日志
                        var log = new Log
                        {
                            OperationType = item.Difference > 0 ? "盘盈" : "盘亏",
                            OperationTime = DateTime.Now,
                            Operator = AppContext.CurrentUser?.Username ?? "系统",
                            AssetNumber = asset.AssetID,
                            Details = $"资产名称：{asset.Name}，系统库存：{item.SystemQuantity}，实际盘点：{item.ActualQuantity}，差异：{item.Difference}"
                        };
                        db.Insertable(log).ExecuteCommand();
                    }
                }

                MessageBox.Show($"盘点保存成功！共处理 {changedItems.Count} 条记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();  // 刷新数据
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存盘点失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    /// <summary>
    /// 盘点项数据模型
    /// </summary>
    public class InventoryCheckItem : INotifyPropertyChanged
    {
        private decimal _actualQuantity;
        private decimal _difference;
        private string _remark = string.Empty;

        public string AssetID { get; set; } = string.Empty;
        public string AssetName { get; set; } = string.Empty;
        public string Warehouse { get; set; } = string.Empty;
        public int WarehouseId { get; set; }
        public decimal SystemQuantity { get; set; }

        public decimal ActualQuantity
        {
            get => _actualQuantity;
            set
            {
                if (_actualQuantity != value)
                {
                    _actualQuantity = value;
                    Difference = value - SystemQuantity;
                    OnPropertyChanged(nameof(ActualQuantity));
                }
            }
        }

        public decimal Difference
        {
            get => _difference;
            set
            {
                if (_difference != value)
                {
                    _difference = value;
                    OnPropertyChanged(nameof(Difference));
                }
            }
        }

        public string Remark
        {
            get => _remark;
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    OnPropertyChanged(nameof(Remark));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
