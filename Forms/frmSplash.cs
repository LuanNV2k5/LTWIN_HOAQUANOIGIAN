using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Màn hình chờ (Splash Screen) mở đầu game với logo và thanh tải GUNA2 (FR-01).
    /// Toàn bộ các nhân vật Hoa Quả và Zombie được thiết kế dạng PictureBox kéo thả trực quan trên Windows Forms Designer.
    /// </summary>
    public partial class frmSplash : Form
    {
        private int _progress = 0;

        public frmSplash()
        {
            InitializeComponent();
            DoubleBuffered = true;
            try { Icon = ImageLoader.GetAppIcon(); } catch { }

            // Nạp trước và tự động xuất toàn bộ hình ảnh PNG vào Assets/Images
            ImageLoader.GetImage("sun");

            // Nạp hình ảnh cho các PictureBox để người dùng kéo thả trực quan
            LoadCharacterImages();

            timerSplash.Start();
            SoundManager.PlayMusic("bgm_menu");
        }

        private void LoadCharacterImages()
        {
            try
            {
                picGrape.Image = ImageLoader.GetImage("fruit_grape", picGrape.Width, picGrape.Height);
                picBanana.Image = ImageLoader.GetImage("fruit_banana", picBanana.Width, picBanana.Height);
                picCoconut.Image = ImageLoader.GetImage("fruit_coconut", picCoconut.Width, picCoconut.Height);
                picStrawberry.Image = ImageLoader.GetImage("fruit_ice_strawberry", picStrawberry.Width, picStrawberry.Height);

                picConeZombie.Image = ImageLoader.GetImage("zombie_cone", picConeZombie.Width, picConeZombie.Height);
                picBucketZombie.Image = ImageLoader.GetImage("zombie_bucket", picBucketZombie.Width, picBucketZombie.Height);
                picBossZombie.Image = ImageLoader.GetImage("zombie_boss", picBossZombie.Width, picBossZombie.Height);
                picNormalZombie.Image = ImageLoader.GetImage("zombie_normal", picNormalZombie.Width, picNormalZombie.Height);
            }
            catch { }
        }

        private void timerSplash_Tick(object? sender, EventArgs e)
        {
            _progress += 2;
            if (_progress > 100) _progress = 100;

            progressBar.Value = _progress;
            lblProgress.Text = $"Đang tải tài nguyên... {_progress}%";

            if (_progress >= 100)
            {
                timerSplash.Stop();

                var menuForm = new frmMenu();
                menuForm.Show();
                Hide();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 1. Nền Gradient sân vườn sâu lắng
            using (var bgBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(24, 70, 32),
                Color.FromArgb(12, 34, 18),
                90f))
            {
                g.FillRectangle(bgBrush, ClientRectangle);
            }

            // 2. Vầng hào quang ánh sáng trung tâm trên cao
            using (var haloPath = new System.Drawing.Drawing2D.GraphicsPath())
            {
                haloPath.AddEllipse(ClientRectangle.Width / 2 - 320, 10, 640, 200);
                using (var pgb = new System.Drawing.Drawing2D.PathGradientBrush(haloPath))
                {
                    pgb.CenterColor = Color.FromArgb(65, 255, 235, 59);
                    pgb.SurroundColors = new[] { Color.FromArgb(0, 76, 175, 80) };
                    g.FillPath(pgb, haloPath);
                }
            }

            // 3. Viền khung vàng kim sang trọng
            using (var borderPen = new Pen(Color.FromArgb(160, 255, 193, 7), 3f))
            {
                g.DrawRectangle(borderPen, 2, 2, ClientRectangle.Width - 4, ClientRectangle.Height - 4);
            }
        }

        // Các hàm xử lý sự kiện click dự phòng để tránh lỗi khi vô tình bấm đúp trong Visual Studio Designer
        private void lblSubtitle_Click(object? sender, EventArgs e) { }
        private void lblTitle_Click(object? sender, EventArgs e) { }
        private void lblProgress_Click(object? sender, EventArgs e) { }
        private void lblVs_Click(object? sender, EventArgs e) { }
        private void picGrape_Click(object? sender, EventArgs e) { }
        private void picBanana_Click(object? sender, EventArgs e) { }
        private void picCoconut_Click(object? sender, EventArgs e) { }
        private void picStrawberry_Click(object? sender, EventArgs e) { }
        private void picConeZombie_Click(object? sender, EventArgs e) { }
        private void picBucketZombie_Click(object? sender, EventArgs e) { }
        private void picBossZombie_Click(object? sender, EventArgs e) { }
        private void picNormalZombie_Click(object? sender, EventArgs e) { }
    }
}
