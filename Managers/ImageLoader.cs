using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Managers
{
    /// <summary>
    /// Bộ nạp và quản lý bộ nhớ đệm (Cache) hình ảnh của trò chơi.
    /// Đọc trực tiếp các file ảnh PNG thực tế từ thư mục Assets/Images.
    /// Tự động co giãn kích thước chuẩn HD và lưu đệm để game chạy mượt mà 60 FPS.
    /// </summary>
    public static class ImageLoader
    {
        private static readonly Dictionary<string, Image> _cache = new Dictionary<string, Image>();
        private static Icon? _appIcon;

        /// <summary>
        /// Nạp biểu tượng ứng dụng từ file Assets/app_icon.ico hoặc dùng ảnh mặt trời.
        /// </summary>
        public static Icon GetAppIcon()
        {
            if (_appIcon != null) return _appIcon;

            try
            {
                string iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "app_icon.ico");
                if (File.Exists(iconPath))
                {
                    _appIcon = new Icon(iconPath);
                    return _appIcon;
                }
            }
            catch { }

            try
            {
                var sunImg = GetImage("sun", 32, 32);
                if (sunImg is Bitmap bmp)
                {
                    _appIcon = Icon.FromHandle(bmp.GetHicon());
                    return _appIcon;
                }
            }
            catch { }

            return SystemIcons.Application;
        }

        public static Image GetImage(string imageName, int width = 80, int height = 80)
        {
            string key = $"{imageName}_{width}_{height}";
            if (_cache.TryGetValue(key, out var cachedImg))
            {
                return cachedImg;
            }

            string fileName = imageName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ? imageName : imageName + ".png";
            string binPath = Path.Combine(GameConfig.IMAGES_DIR, fileName);
            string srcPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Assets\Images", fileName));

            string? filePath = null;
            if (File.Exists(srcPath) && File.Exists(binPath))
            {
                filePath = File.GetLastWriteTimeUtc(srcPath) >= File.GetLastWriteTimeUtc(binPath) ? srcPath : binPath;
            }
            else if (File.Exists(srcPath))
            {
                filePath = srcPath;
            }
            else if (File.Exists(binPath))
            {
                filePath = binPath;
            }

            // Nếu tìm thấy file ảnh PNG thực tế trên máy
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(filePath);
                    using (var ms = new MemoryStream(bytes))
                    using (var src = Image.FromStream(ms))
                    {
                        var bmp = new Bitmap(width, height);
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.DrawImage(src, 0, 0, width, height);
                        }
                        _cache[key] = bmp;
                        return bmp;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Lỗi đọc file ảnh {filePath}", ex);
                }
            }

            // Trường hợp file ảnh chưa được thêm vào thư mục: tạo ảnh giữ chỗ đơn giản
            Image fallback = GenerateFallbackImage(imageName, width, height);
            _cache[key] = fallback;
            return fallback;
        }

        /// <summary>
        /// Tạo ảnh giữ chỗ (Placeholder) đơn giản nếu lỡ người dùng quên copy file ảnh vào thư mục Assets.
        /// </summary>
        public static Image GenerateFallbackImage(string name, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush b = new SolidBrush(Color.ForestGreen))
                    g.FillEllipse(b, 4, 4, w - 8, h - 8);

                using (Pen p = new Pen(Color.Gold, 2))
                    g.DrawEllipse(p, 4, 4, w - 8, h - 8);

                using (Font f = new Font("Arial", 8, FontStyle.Bold))
                using (Brush tb = new SolidBrush(Color.White))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(name, f, tb, new RectangleF(0, 0, w, h), sf);
                }
            }
            return bmp;
        }


        /// Xóa bộ nhớ đệm hình ảnh khi cần giải phóng RAM.

        public static void ClearCache()
        {
            foreach (var img in _cache.Values)
            {
                img.Dispose();
            }
            _cache.Clear();
        }
    }
}
