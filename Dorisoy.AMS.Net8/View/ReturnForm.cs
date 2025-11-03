using Dorisoy.AMS.models;
using SqlSugar;
using System.ComponentModel;

namespace Dorisoy.AMS.view
{
    public partial class ReturnForm : Form
    {
        private BorrowRecord _borrowRecord;

        public ReturnForm(BorrowRecord borrowRecord)
        {
            InitializeComponent();
            _borrowRecord = borrowRecord ?? throw new ArgumentNullException(nameof(borrowRecord));
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadBorrowData();
        }

        private void LoadBorrowData()
        {
            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    var asset = db.Queryable<Asset>().First(a => a.AssetID == _borrowRecord.AssetID);

                    // 绑定资产信息
                    lblAssetID.Text = _borrowRecord.AssetID;
                    lblAssetName.Text = asset.Name;
                    lblCategory.Text = asset.Category;
                    lblModel.Text = asset.Model;
                    lblLocation.Text = asset.Location;
                    lblDepartment.Text = asset.Department;

                    // 绑定借用信息
                    lblBorrowedBy.Text = _borrowRecord.BorrowedBy;
                    lblBorrowedDate.Text = _borrowRecord.BorrowedDate.ToString("yyyy-MM-dd HH:mm");
                    lblExpectedReturnDate.Text = _borrowRecord.ExpectedReturnDate?.ToString("yyyy-MM-dd") ?? "未记录";
                    lblBorrowedQuantity.Text = _borrowRecord.BorrowedQuantity.ToString();
                    lblBorrowReason.Text = _borrowRecord.BorrowReason;

                    // 设置归还数量的最大值（不能超过借用量）
                    numReturnQuantity.Maximum = _borrowRecord.BorrowedQuantity;
                    numReturnQuantity.Value = _borrowRecord.BorrowedQuantity; // 默认全部归还

                    // 设置默认归还日期
                    dtActualReturnDate.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (numReturnQuantity.Value <= 0)
            {
                MessageBox.Show("归还数量必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numReturnQuantity.Focus();
                return false;
            }

            if (numReturnQuantity.Value > _borrowRecord.BorrowedQuantity)
            {
                MessageBox.Show($"归还数量不能超过借用数量！借用数量：{_borrowRecord.BorrowedQuantity}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numReturnQuantity.Focus();
                return false;
            }

            if (dtActualReturnDate.Value < _borrowRecord.BorrowedDate)
            {
                MessageBox.Show("实际归还日期不能早于借用日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                using (var db = SqliteHelper.GetDb())
                {
                    decimal returnQuantity = (decimal)numReturnQuantity.Value;

                    // 判断是否全部归还
                    if (returnQuantity >= _borrowRecord.BorrowedQuantity)
                    {
                        // 全部归还：更新为已归还状态
                        _borrowRecord.ActualReturnDate = dtActualReturnDate.Value;
                        _borrowRecord.ReturnRemark = txtReturnRemark.Text.Trim();
                        _borrowRecord.Status = 1; // 已归还

                        var result = db.Updateable(_borrowRecord)
                            .UpdateColumns(r => new
                            {
                                r.ActualReturnDate,
                                r.ReturnRemark,
                                r.Status
                            })
                            .Where(r => r.Id == _borrowRecord.Id)
                            .ExecuteCommand();

                        if (result > 0)
                        {
                            MessageBox.Show("资产归还成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        // 部分归还：创建新记录，原记录的借用数需走减
                        // 1. 更新原借用记录，会减少借用数量
                        _borrowRecord.BorrowedQuantity -= returnQuantity;
                        db.Updateable(_borrowRecord)
                            .UpdateColumns(r => r.BorrowedQuantity)
                            .Where(r => r.Id == _borrowRecord.Id)
                            .ExecuteCommand();

                        // 2. 增加一条筑佳记录（表示已归还部分）
                        var returnRecord = new BorrowRecord
                        {
                            AssetID = _borrowRecord.AssetID,
                            BorrowedBy = _borrowRecord.BorrowedBy,
                            BorrowedQuantity = returnQuantity,
                            BorrowedDate = _borrowRecord.BorrowedDate,
                            ExpectedReturnDate = _borrowRecord.ExpectedReturnDate,
                            BorrowReason = "部分归还中",
                            ActualReturnDate = dtActualReturnDate.Value,
                            ReturnRemark = txtReturnRemark.Text.Trim(),
                            Status = 1 // 已归还
                        };

                        db.Insertable(returnRecord).ExecuteCommand();
                        MessageBox.Show("部分资产归还成功！余下数量已更新为" + _borrowRecord.BorrowedQuantity + "。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
