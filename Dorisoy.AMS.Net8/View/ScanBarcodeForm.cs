using Dorisoy.AMS.models;
using Dorisoy.AMS.services;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace Dorisoy.AMS.view
{
    /// <summary>
    /// 扫码借还窗口 - 使用摄像头扫描条码自动借还资产
    /// </summary>
    public partial class ScanBarcodeForm : Form
    {
        private VideoCapture? _capture;
        private Thread? _cameraThread;
        private bool _isRunning = false;
        private Asset? _scannedAsset;
        private BorrowRecord? _borrowRecord;
        private string _lastScannedCode = string.Empty;
        private DateTime _lastScanTime = DateTime.MinValue;

        /// <summary>
        /// 扫描完成后是否有操作（用于通知父窗口刷新）
        /// </summary>
        public bool HasOperation { get; private set; } = false;

        public ScanBarcodeForm()
        {
            InitializeComponent();
        }

        private void ScanBarcodeForm_Load(object sender, EventArgs e)
        {
            StartCamera();
        }

        /// <summary>
        /// 启动摄像头
        /// </summary>
        private void StartCamera()
        {
            try
            {
                _capture = new VideoCapture(0);  // 使用默认摄像头

                if (!_capture.IsOpened())
                {
                    lblStatus.Text = "无法打开摄像头！";
                    lblStatus.ForeColor = Color.Red;
                    MessageBox.Show("无法打开摄像头，请检查摄像头是否正常连接！", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 设置摄像头分辨率
                _capture.Set(VideoCaptureProperties.FrameWidth, 640);
                _capture.Set(VideoCaptureProperties.FrameHeight, 480);

                _isRunning = true;
                _cameraThread = new Thread(CameraLoop)
                {
                    IsBackground = true
                };
                _cameraThread.Start();

                lblStatus.Text = "摄像头已启动，请对准条码";
                lblStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"摄像头启动失败：{ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 摄像头循环线程
        /// </summary>
        private void CameraLoop()
        {
            var barcodeReader = new BarcodeReader<Bitmap>(null, bitmap =>
            {
                return new BitmapLuminanceSource(bitmap);
            }, null)
            {
                AutoRotate = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new List<BarcodeFormat>
                    {
                        BarcodeFormat.CODE_128,
                        BarcodeFormat.QR_CODE,
                        BarcodeFormat.CODE_39,
                        BarcodeFormat.EAN_13
                    }
                }
            };

            using var frame = new Mat();

            while (_isRunning && _capture != null && _capture.IsOpened())
            {
                try
                {
                    _capture.Read(frame);

                    if (frame.Empty())
                    {
                        Thread.Sleep(30);
                        continue;
                    }

                    // 转换为 Bitmap 用于显示和扫描
                    var bitmap = frame.ToBitmap();

                    // 在 UI 线程更新图像
                    picCamera.Invoke(() =>
                    {
                        var oldImage = picCamera.Image;
                        picCamera.Image = bitmap;
                        oldImage?.Dispose();
                    });

                    // 尝试识别条码（每100ms识别一次）
                    if ((DateTime.Now - _lastScanTime).TotalMilliseconds > 100)
                    {
                        _lastScanTime = DateTime.Now;
                        
                        var result = barcodeReader.Decode(bitmap);
                        if (result != null && !string.IsNullOrEmpty(result.Text))
                        {
                            var code = result.Text;

                            // 防止重复扫描同一条码
                            if (code != _lastScannedCode || (DateTime.Now - _lastScanTime).TotalSeconds > 3)
                            {
                                _lastScannedCode = code;
                                
                                // 在 UI 线程处理扫描结果
                                this.Invoke(() => ProcessScannedCode(code));
                            }
                        }
                    }

                    Thread.Sleep(30);  // ~30 FPS
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"摄像头读取错误: {ex.Message}");
                    Thread.Sleep(100);
                }
            }
        }

        /// <summary>
        /// 处理扫描到的条码
        /// </summary>
        private void ProcessScannedCode(string assetId)
        {
            try
            {
                lblStatus.Text = $"扫描到条码: {assetId}";
                lblStatus.ForeColor = Color.Blue;

                using (var db = SqliteHelper.GetDb())
                {
                    // 查找资产
                    var asset = db.Queryable<Asset>().First(a => a.AssetID == assetId);

                    if (asset == null)
                    {
                        lblAssetInfo.Text = $"未找到资产！\n\n条码: {assetId}";
                        lblAssetInfo.ForeColor = Color.Red;
                        btnBorrow.Enabled = false;
                        btnReturn.Enabled = false;
                        _scannedAsset = null;
                        _borrowRecord = null;
                        return;
                    }

                    _scannedAsset = asset;

                    // 查找该资产是否有借用记录
                    _borrowRecord = db.Queryable<BorrowRecord>()
                        .First(r => r.AssetID == assetId && r.Status == 0);

                    // 计算可用库存
                    var borrowedQty = db.Queryable<BorrowRecord>()
                        .Where(r => r.AssetID == assetId && r.Status == 0)
                        .Sum(r => r.BorrowedQuantity);
                    var availableQty = asset.Quantity - borrowedQty;

                    // 显示资产信息
                    lblAssetInfo.Text = $"资产编号: {asset.AssetID}\n\n" +
                                        $"资产名称: {asset.Name}\n\n" +
                                        $"类别: {asset.Category}\n\n" +
                                        $"规格型号: {asset.Model}\n\n" +
                                        $"存放地点: {asset.Location}\n\n" +
                                        $"总库存: {asset.Quantity} {asset.Unit}\n\n" +
                                        $"可用库存: {availableQty} {asset.Unit}\n\n" +
                                        $"状态: {StatusConfig.GetStatusName(asset.Status)}";
                    lblAssetInfo.ForeColor = Color.Black;

                    // 根据库存和借用状态启用按钮
                    btnBorrow.Enabled = availableQty > 0;
                    btnReturn.Enabled = _borrowRecord != null;

                    // 播放提示音
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            catch (Exception ex)
            {
                lblAssetInfo.Text = $"处理失败：{ex.Message}";
                lblAssetInfo.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 借出按钮点击
        /// </summary>
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (_scannedAsset == null)
            {
                MessageBox.Show("请先扫描资产条码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var db = SqliteHelper.GetDb())
                {
                    // 计算可用库存
                    var borrowedQty = db.Queryable<BorrowRecord>()
                        .Where(r => r.AssetID == _scannedAsset.AssetID && r.Status == 0)
                        .Sum(r => r.BorrowedQuantity);
                    var availableQty = _scannedAsset.Quantity - borrowedQty;

                    // 弹出借用窗口
                    var form = new BorrowForm(_scannedAsset, availableQty);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        HasOperation = true;
                        lblStatus.Text = "借出成功！请继续扫描下一个";
                        lblStatus.ForeColor = Color.Green;

                        // 清空当前扫描状态，等待下次扫描
                        _lastScannedCode = string.Empty;
                        lblAssetInfo.Text = "借出成功！\n\n请将下一个条码对准摄像头...";
                        lblAssetInfo.ForeColor = Color.Green;
                        btnBorrow.Enabled = false;
                        btnReturn.Enabled = false;
                        _scannedAsset = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"借出失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 归还按钮点击
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (_borrowRecord == null)
            {
                MessageBox.Show("该资产没有借用记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 弹出归还窗口
                var form = new ReturnForm(_borrowRecord);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    HasOperation = true;
                    lblStatus.Text = "归还成功！请继续扫描下一个";
                    lblStatus.ForeColor = Color.Green;

                    // 清空当前扫描状态
                    _lastScannedCode = string.Empty;
                    lblAssetInfo.Text = "归还成功！\n\n请将下一个条码对准摄像头...";
                    lblAssetInfo.ForeColor = Color.Green;
                    btnBorrow.Enabled = false;
                    btnReturn.Enabled = false;
                    _scannedAsset = null;
                    _borrowRecord = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"归还失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 停止摄像头
        /// </summary>
        private void StopCamera()
        {
            _isRunning = false;

            // 等待线程结束
            if (_cameraThread != null && _cameraThread.IsAlive)
            {
                _cameraThread.Join(1000);
            }

            // 释放摄像头
            if (_capture != null)
            {
                _capture.Release();
                _capture.Dispose();
                _capture = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = HasOperation ? DialogResult.OK : DialogResult.Cancel;
            this.Close();
        }

        private void ScanBarcodeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();

            // 清理 PictureBox 图像
            if (picCamera.Image != null)
            {
                picCamera.Image.Dispose();
                picCamera.Image = null;
            }
        }
    }
}
