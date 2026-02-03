using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace Dorisoy.AMS.services
{
    /// <summary>
    /// 条码生成服务（线程安全）
    /// </summary>
    public static class BarcodeService
    {
        private static readonly object _lockObj = new object();

        /// <summary>
        /// 根据资产编号生成条码图片
        /// </summary>
        /// <param name="assetId">资产编号</param>
        /// <param name="width">条码宽度</param>
        /// <param name="height">条码高度</param>
        /// <returns>条码图片（每次返回新实例，调用方负责释放）</returns>
        public static Image GenerateBarcode(string assetId, int width = 150, int height = 50)
        {
            if (string.IsNullOrWhiteSpace(assetId))
            {
                return CreatePlaceholderImage(width, height);
            }

            try
            {
                lock (_lockObj)
                {
                    var writer = new BarcodeWriter<Bitmap>
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Width = width,
                            Height = height,
                            Margin = 2,
                            PureBarcode = false
                        },
                        Renderer = new BitmapRenderer()
                    };

                    var barcode = writer.Write(assetId);
                    
                    // 创建副本并返回，避免 ZXing 内部锁定问题
                    var copy = new Bitmap(barcode.Width, barcode.Height);
                    using (var g = Graphics.FromImage(copy))
                    {
                        g.DrawImage(barcode, 0, 0, barcode.Width, barcode.Height);
                    }
                    barcode.Dispose();
                    
                    return copy;
                }
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
        /// <returns>缩略图（调用方负责释放）</returns>
        public static Image GenerateBarcodeThumbnail(string assetId, int thumbnailWidth = 80, int thumbnailHeight = 30)
        {
            if (string.IsNullOrWhiteSpace(assetId))
            {
                return CreatePlaceholderImage(thumbnailWidth, thumbnailHeight);
            }

            try
            {
                lock (_lockObj)
                {
                    var writer = new BarcodeWriter<Bitmap>
                    {
                        Format = BarcodeFormat.CODE_128,
                        Options = new EncodingOptions
                        {
                            Width = 200,
                            Height = 60,
                            Margin = 2,
                            PureBarcode = false
                        },
                        Renderer = new BitmapRenderer()
                    };

                    using (var fullBarcode = writer.Write(assetId))
                    {
                        // 创建缩略图
                        var thumbnail = new Bitmap(thumbnailWidth, thumbnailHeight);
                        using (var g = Graphics.FromImage(thumbnail))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.DrawImage(fullBarcode, 0, 0, thumbnailWidth, thumbnailHeight);
                        }
                        return thumbnail;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"生成条码缩略图失败: {ex.Message}");
                return CreatePlaceholderImage(thumbnailWidth, thumbnailHeight);
            }
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        public static Image GenerateQRCode(string content, int size = 150)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return CreatePlaceholderImage(size, size);
            }

            try
            {
                lock (_lockObj)
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

                    var qrcode = writer.Write(content);
                    
                    // 创建副本
                    var copy = new Bitmap(qrcode.Width, qrcode.Height);
                    using (var g = Graphics.FromImage(copy))
                    {
                        g.DrawImage(qrcode, 0, 0, qrcode.Width, qrcode.Height);
                    }
                    qrcode.Dispose();
                    
                    return copy;
                }
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
        public static void SaveBarcodeToFile(string assetId, string filePath, ImageFormat? format = null)
        {
            using (var barcode = GenerateBarcode(assetId, 300, 100))
            {
                barcode.Save(filePath, format ?? ImageFormat.Png);
            }
        }

        /// <summary>
        /// 创建占位符图片
        /// </summary>
        private static Image CreatePlaceholderImage(int width, int height)
        {
            var placeholder = new Bitmap(width, height);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.LightGray, 0, 0, width - 1, height - 1);
                
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
