using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace Dorisoy.AMS.services
{
    /// <summary>
    /// 条码生成服务
    /// </summary>
    public static class BarcodeService
    {
        // 条码缓存（避免重复生成）
        private static readonly Dictionary<string, Image> _barcodeCache = new Dictionary<string, Image>();
        private static readonly object _cacheLock = new object();

        /// <summary>
        /// 根据资产编号生成条码图片
        /// </summary>
        /// <param name="assetId">资产编号</param>
        /// <param name="width">条码宽度</param>
        /// <param name="height">条码高度</param>
        /// <returns>条码图片（每次返回新副本）</returns>
        public static Image GenerateBarcode(string assetId, int width = 150, int height = 50)
        {
            if (string.IsNullOrWhiteSpace(assetId))
            {
                return CreatePlaceholderImage(width, height);
            }

            // 检查缓存
            string cacheKey = $"{assetId}_{width}_{height}";
            lock (_cacheLock)
            {
                if (_barcodeCache.TryGetValue(cacheKey, out var cachedImage))
                {
                    // 返回副本，避免缓存图像被外部释放
                    return (Image)cachedImage.Clone();
                }
            }

            try
            {
                var writer = new BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.CODE_128,  // 使用 CODE128 格式，支持字母数字
                    Options = new EncodingOptions
                    {
                        Width = width,
                        Height = height,
                        Margin = 2,
                        PureBarcode = false  // 显示文字
                    },
                    Renderer = new BitmapRenderer()
                };

                var barcode = writer.Write(assetId);
                
                // 缓存条码
                lock (_cacheLock)
                {
                    // 如果已存在，先释放旧的
                    if (_barcodeCache.TryGetValue(cacheKey, out var oldImage))
                    {
                        oldImage?.Dispose();
                    }
                    _barcodeCache[cacheKey] = barcode;
                }

                // 返回副本
                return (Image)barcode.Clone();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"生成条码失败: {ex.Message}");
                return CreatePlaceholderImage(width, height);
            }
        }

        /// <summary>
        /// 生成条码缩略图（用于列表显示）
        /// </summary>
        /// <param name="assetId">资产编号</param>
        /// <param name="thumbnailWidth">缩略图宽度</param>
        /// <param name="thumbnailHeight">缩略图高度</param>
        /// <returns>缩略图</returns>
        public static Image GenerateBarcodeThumbnail(string assetId, int thumbnailWidth = 80, int thumbnailHeight = 30)
        {
            // 先生成完整条码
            var fullBarcode = GenerateBarcode(assetId, 200, 60);
            
            // 缩放为缩略图
            var thumbnail = new Bitmap(thumbnailWidth, thumbnailHeight);
            using (var g = Graphics.FromImage(thumbnail))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(fullBarcode, 0, 0, thumbnailWidth, thumbnailHeight);
            }

            return thumbnail;
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="size">尺寸（正方形）</param>
        /// <returns>二维码图片</returns>
        public static Image GenerateQRCode(string content, int size = 150)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return CreatePlaceholderImage(size, size);
            }

            try
            {
                var writer = new BarcodeWriter<Bitmap>
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new EncodingOptions
                    {
                        Width = size,
                        Height = size,
                        Margin = 1
                    },
                    Renderer = new BitmapRenderer()
                };

                return writer.Write(content);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"生成二维码失败: {ex.Message}");
                return CreatePlaceholderImage(size, size);
            }
        }

        /// <summary>
        /// 将条码保存为文件
        /// </summary>
        /// <param name="assetId">资产编号</param>
        /// <param name="filePath">保存路径</param>
        /// <param name="format">图片格式</param>
        public static void SaveBarcodeToFile(string assetId, string filePath, ImageFormat? format = null)
        {
            var barcode = GenerateBarcode(assetId, 300, 100);
            barcode.Save(filePath, format ?? ImageFormat.Png);
        }

        /// <summary>
        /// 清除条码缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (_cacheLock)
            {
                foreach (var image in _barcodeCache.Values)
                {
                    image?.Dispose();
                }
                _barcodeCache.Clear();
            }
        }

        /// <summary>
        /// 创建占位符图片（当生成失败时使用）
        /// </summary>
        private static Image CreatePlaceholderImage(int width, int height)
        {
            var placeholder = new Bitmap(width, height);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.LightGray, 0, 0, width - 1, height - 1);
                
                // 绘制提示文字
                using (var font = new Font("微软雅黑", 8))
                {
                    var text = "无条码";
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, Brushes.Gray, 
                        (width - textSize.Width) / 2, 
                        (height - textSize.Height) / 2);
                }
            }
            return placeholder;
        }

        /// <summary>
        /// 解析条码图片
        /// </summary>
        /// <param name="barcodeImage">条码图片</param>
        /// <returns>解析结果（资产编号）</returns>
        public static string? DecodeBarcode(Bitmap barcodeImage)
        {
            try
            {
                var luminanceSource = new BitmapLuminanceSource(barcodeImage);
                var reader = new BarcodeReader<Bitmap>(null, src => luminanceSource, null)
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

                var result = reader.Decode(barcodeImage);
                return result?.Text;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"解析条码失败: {ex.Message}");
                return null;
            }
        }
    }
}
