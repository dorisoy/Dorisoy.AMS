using Dorisoy.AMS.models;
using SqlSugar;

namespace Dorisoy.AMS.view
{
    /// <summary>
    /// 资产报损登记窗体
    /// </summary>
    public partial class ScrapForm : Form
    {
        private readonly Asset _asset;

        public ScrapForm(Asset asset)
        {
            InitializeComponent();
            _asset = asset ?? throw new ArgumentNullException(nameof(asset));
            LoadAssetInfo();
            InitializeControls();
        }

        /// <summary>
        /// 加载资产信息
        /// </summary>
        private void LoadAssetInfo()
        {
            txtAssetID.Text = _asset.AssetID;
            txtAssetName.Text = _asset.Name;
            txtWarehouse.Text = _asset.Location;
            txtCurrentQuantity.Text = _asset.Quantity.ToString();

            // 设置报损数量最大值
            numScrapQuantity.Maximum = (decimal)_asset.Quantity;
            numScrapQuantity.Value = 1;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeControls()
        {
            cmbScrapReason.SelectedIndex = 0;
            txtResponsiblePerson.Text = _asset.User;
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (numScrapQuantity.Value <= 0)
            {
                MessageBox.Show("报损数量必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numScrapQuantity.Value > (decimal)_asset.Quantity)
            {
                MessageBox.Show("报损数量不能超过当前库存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbScrapReason.SelectedIndex < 0)
            {
                MessageBox.Show("请选择报损原因", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存报损记录
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var scrapQuantity = (decimal)numScrapQuantity.Value;
                var scrapReason = cmbScrapReason.Text;
                var responsiblePerson = txtResponsiblePerson.Text.Trim();
                var remark = txtRemark.Text.Trim();

                using (var db = SqliteHelper.GetDb())
                {
                    // 获取最新的资产信息
                    var asset = db.Queryable<Asset>().First(a => a.AssetID == _asset.AssetID);
                    if (asset == null)
                    {
                        MessageBox.Show("资产不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 检查库存是否足够
                    if (scrapQuantity > asset.Quantity)
                    {
                        MessageBox.Show("报损数量超过当前库存", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 创建报废出库记录
                    var stockRecord = new StockRecord
                    {
                        AssetID = asset.AssetID,
                        AssetName = asset.Name,
                        WarehouseId = asset.WarehouseId,
                        WarehouseName = asset.Location,
                        RecordType = StockRecordType.Out,  // 出库
                        BusinessType = StockBusinessType.ScrapOut,  // 报废出库
                        Quantity = -scrapQuantity,  // 出库为负数
                        BeforeQuantity = asset.Quantity,
                        AfterQuantity = asset.Quantity - scrapQuantity,
                        RelatedId = asset.AssetID,
                        Operator = AppContext.CurrentUser?.Username ?? "系统",
                        Handler = responsiblePerson,
                        RecordTime = DateTime.Now,
                        Remark = $"报损原因：{scrapReason}；{remark}"
                    };

                    db.Insertable(stockRecord).ExecuteCommand();

                    // 更新资产库存
                    asset.Quantity -= scrapQuantity;

                    // 如果库存为0，将状态改为报废
                    if (asset.Quantity <= 0)
                    {
                        asset.Status = 2;  // 报废状态
                        asset.Quantity = 0;
                    }

                    db.Updateable(asset).ExecuteCommand();

                    // 记录操作日志
                    RecordLog(asset, scrapQuantity, scrapReason, responsiblePerson);

                    MessageBox.Show("报损登记成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"报损登记失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 记录操作日志
        /// </summary>
        private void RecordLog(Asset asset, decimal scrapQuantity, string scrapReason, string responsiblePerson)
        {
            using (var db = SqliteHelper.GetDb())
            {
                var log = new Log
                {
                    OperationType = "报损",
                    OperationTime = DateTime.Now,
                    Operator = AppContext.CurrentUser?.Username ?? "系统",
                    AssetNumber = asset.AssetID,
                    Details = $"资产名称：{asset.Name}，报损数量：{scrapQuantity}，报损原因：{scrapReason}，责任人：{responsiblePerson}"
                };

                db.Insertable(log).ExecuteCommand();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
