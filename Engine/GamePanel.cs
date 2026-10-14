using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HoaQuaNoiGian.Models;

namespace HoaQuaNoiGian.Engine
{
    /// <summary>
    /// Canvas vẽ bằng GDI+ cho màn chơi game Hoa Quả Nổi Giận.
    /// Bật DoubleBuffered chống giật, hỗ trợ ScaleTransform co giãn toàn màn hình tự động,
    /// </summary>
    public class GamePanel : Panel
    {
        public GameEngine? Engine { get; private set; }

        private Point _mouseHoverPos = Point.Empty;
        private int _hoverRow = -1;
        private int _hoverCol = -1;

        public event Action? OnPauseRequested;
        public event Action? OnGameOver;

        public GamePanel()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();

            Width = GameConfig.WINDOW_WIDTH;
            Height = GameConfig.WINDOW_HEIGHT;
            BackColor = Color.FromArgb(50, 130, 45);
        }

        public void InitializeEngine(int levelNum)
        {
            Engine = new GameEngine(levelNum);
            Engine.OnStateChanged += () =>
            {
                if (Engine.State == GameState.Won || Engine.State == GameState.Lost)
                {
                    OnGameOver?.Invoke();
                }
            };
        }

        private void ToVirtualCoords(int mouseX, int mouseY, out int vx, out int vy)
        {
            float scaleX = (float)Width / GameConfig.WINDOW_WIDTH;
            float scaleY = (float)Height / GameConfig.WINDOW_HEIGHT;
            if (scaleX <= 0.0001f) scaleX = 1f;
            if (scaleY <= 0.0001f) scaleY = 1f;

            vx = (int)(mouseX / scaleX);
            vy = (int)(mouseY / scaleY);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ToVirtualCoords(e.X, e.Y, out int vx, out int vy);
            _mouseHoverPos = new Point(vx, vy);

            if (Engine != null)
            {
                Engine.FieldGrid.GetCellFromMouse(vx, vy, out _hoverRow, out _hoverCol);
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Engine == null || Engine.State != GameState.Playing) return;

            ToVirtualCoords(e.X, e.Y, out int vx, out int vy);
            Point vPos = new Point(vx, vy);

            // Chuột phải: Hủy chọn thẻ hạt giống / Hủy chế độ xẻng (FR-17)
            if (e.Button == MouseButtons.Right)
            {
                Engine.SelectedCard = null;
                Engine.IsShovelSelected = false;
                Invalidate();
                return;
            }

            if (e.Button != MouseButtons.Left) return;

            // 1. Kiểm tra click nhặt Nắng rơi (FR-12)
            for (int i = Engine.SunTokens.Count - 1; i >= 0; i--)
            {
                var sun = Engine.SunTokens[i];
                if (!sun.IsCollected && sun.Bounds.Contains(vPos))
                {
                    sun.Collect();
                    Invalidate();
                    return;
                }
            }

            // 2. Kiểm tra click các nút trên Thanh Trên (Top Bar)
            if (vy <= GameConfig.TOP_BAR_HEIGHT)
            {
                // Nút Xẻng (x: 1008, y: 10, w: 62, h: 70) (FR-13)
                Rectangle shovelRect = new Rectangle(1008, 10, 62, 70);
                if (shovelRect.Contains(vPos))
                {
                    Engine.IsShovelSelected = !Engine.IsShovelSelected;
                    Engine.SelectedCard = null;
                    Managers.SoundManager.PlaySound("sfx_click");
                    Invalidate();
                    return;
                }

                // Nút Pause (x: 1080, y: 10, w: 105, h: 70) (FR-15)
                Rectangle pauseRect = new Rectangle(1080, 10, 105, 70);
                if (pauseRect.Contains(vPos))
                {
                    OnPauseRequested?.Invoke();
                    Managers.SoundManager.PlaySound("sfx_click");
                    return;
                }

                // Click chọn Thẻ Hạt Giống (FR-10)
                int cardWidth = 66;
                int cardHeight = 70;
                int cardStartX = 130;
                var unlockedFruits = Engine.CurrentLevel.UnlockedFruits;

                for (int i = 0; i < unlockedFruits.Count; i++)
                {
                    int cardX = cardStartX + i * (cardWidth + 5);
                    Rectangle cardRect = new Rectangle(cardX, 10, cardWidth, cardHeight);

                    if (cardRect.Contains(vPos))
                    {
                        FruitType ft = unlockedFruits[i];
                        int cost = GameEngine.GetPlantCost(ft);
                        float cd = Engine.CardCooldowns[ft];

                        if (Engine.SunEnergy >= cost && cd <= 0)
                        {
                            Engine.SelectedCard = (Engine.SelectedCard == ft) ? null : ft;
                            Engine.IsShovelSelected = false;
                            Managers.SoundManager.PlaySound("sfx_click");
                        }
                        Invalidate();
                        return;
                    }
                }
            }

            // 3. Click vào Ô Lưới trên sân trồng cây hoặc nhổ cây (FR-11 & FR-13)
            if (Engine.FieldGrid.GetCellFromMouse(vx, vy, out int row, out int col))
            {
                if (Engine.IsShovelSelected)
                {
                    if (Engine.TryRemovePlant(row, col))
                    {
                        Engine.IsShovelSelected = false;
                    }
                }
                else if (Engine.SelectedCard.HasValue)
                {
                    if (Engine.TryPlacePlant(row, col, Engine.SelectedCard.Value))
                    {
                        Engine.SelectedCard = null;
                    }
                }
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (Engine == null) return;

            // Co giãn ma trận toàn màn hình theo tỷ lệ Virtual Resolution
            float scaleX = (float)Width / GameConfig.WINDOW_WIDTH;
            float scaleY = (float)Height / GameConfig.WINDOW_HEIGHT;
            g.ScaleTransform(scaleX, scaleY);

            // 1. Vẽ nền Sân Vườn (Tùy biến theo từng Màn chơi: Vườn sau, Vườn trước, Vườn trái cây, Vườn đêm, Trận cuối)
            DrawGardenBackground(g, Engine.CurrentLevel.LevelNumber);

            // 2. Vẽ Lưới sân 5x9 (Đổi màu và hoa văn đồng bộ với từng Màn chơi)
            Engine.FieldGrid.Draw(g, Engine.CurrentLevel.ActiveRowsCount, Engine.CurrentLevel.LevelNumber);

            // 3. Vẽ Xe cắt cỏ
            foreach (var mower in Engine.Mowers) mower.Draw(g);

            // 4. Vẽ Hoa quả phòng thủ
            foreach (var fruit in Engine.Fruits) fruit.Draw(g);

            // 5. Vẽ Zombie
            foreach (var zombie in Engine.Zombies) zombie.Draw(g);

            // 6. Vẽ Đạn
            foreach (var proj in Engine.Projectiles) proj.Draw(g);

            // 7. Vẽ Nắng rơi
            foreach (var sun in Engine.SunTokens) sun.Draw(g);

            // 8. Vẽ Hiệu ứng (Explosion, floating text...)
            foreach (var eff in Engine.Effects) eff.Draw(g);

            // 9. Vẽ "Bóng mờ" xem trước hoa quả khi rê chuột qua ô lưới (Hover ghost preview)
            if (Engine.SelectedCard.HasValue && _hoverRow >= 0 && _hoverCol >= 0)
            {
                if (Engine.FieldGrid.IsRowActive(_hoverRow, Engine.CurrentLevel.ActiveRowsCount))
                {
                    Rectangle cellBounds = Engine.FieldGrid.GetCellBounds(_hoverRow, _hoverCol);
                    using (Brush ghostBrush = new SolidBrush(Color.FromArgb(120, 165, 214, 167)))
                    {
                        g.FillRectangle(ghostBrush, cellBounds);
                    }
                    string ghostImg = GetFruitImageName(Engine.SelectedCard.Value);
                    var img = Managers.ImageLoader.GetImage(ghostImg, cellBounds.Width - 16, cellBounds.Height - 16);
                    g.DrawImage(img, cellBounds.X + 8, cellBounds.Y + 8);
                }
            }
            else if (Engine.IsShovelSelected && _hoverRow >= 0 && _hoverCol >= 0)
            {
                Rectangle cellBounds = Engine.FieldGrid.GetCellBounds(_hoverRow, _hoverCol);
                using (Pen p = new Pen(Color.FromArgb(200, 244, 67, 54), 3))
                {
                    g.DrawRectangle(p, cellBounds);
                }
            }

            // 10. Vẽ Thanh Trên (Top Bar) & Thẻ Hạt Giống
            DrawTopBar(g);

            // 11. Vẽ Thanh Tiến Trình Wave & Thông Báo ở đáy màn hình
            DrawBottomWaveBar(g);

            // 12. Vẽ Thông Báo Wave Khổng Lồ (Nổi bật chính giữa màn hình)
            string? alert = Engine.GetBigWaveAlertText();
            if (!string.IsNullOrEmpty(alert))
            {
                using (Brush bBg = new SolidBrush(Color.FromArgb(220, 183, 28, 28)))
                using (Font alertFont = new Font("Arial", 22, FontStyle.Bold))
                using (Pen border = new Pen(Color.Gold, 3))
                {
                    Rectangle alertRect = new Rectangle(0, GameConfig.WINDOW_HEIGHT / 2 - 45, GameConfig.WINDOW_WIDTH, 90);
                    g.FillRectangle(bBg, alertRect);
                    g.DrawRectangle(border, alertRect);

                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    using (Brush textBrush = new SolidBrush(Color.Yellow))
                    {
                        g.DrawString(alert, alertFont, textBrush, alertRect, sf);
                    }
                }
            }
        }

        private void DrawGardenBackground(Graphics g, int levelNum = 1)
        {
            Color baseColor, porchColor1, porchColor2, plankColor, roadColor1, roadColor2, roadBorderColor;

            switch (levelNum)
            {
                case 2: // Màn 2: Vườn Trước Nhà - Hiên lát gạch đá xám sáng, vỉa hè đường phố
                    baseColor = Color.FromArgb(50, 135, 60);
                    porchColor1 = Color.FromArgb(120, 144, 156);
                    porchColor2 = Color.FromArgb(84, 110, 122);
                    plankColor = Color.FromArgb(55, 71, 79);
                    roadColor1 = Color.FromArgb(125, 125, 125);
                    roadColor2 = Color.FromArgb(90, 90, 90);
                    roadBorderColor = Color.FromArgb(60, 60, 60);
                    break;

                case 3: // Màn 3: Vườn Cây Ăn Trái - Sàn tre nứa vàng rơm, đường mòn đất đỏ phù sa
                    baseColor = Color.FromArgb(100, 130, 45);
                    porchColor1 = Color.FromArgb(198, 156, 109);
                    porchColor2 = Color.FromArgb(161, 124, 79);
                    plankColor = Color.FromArgb(120, 85, 45);
                    roadColor1 = Color.FromArgb(150, 85, 50);
                    roadColor2 = Color.FromArgb(115, 60, 32);
                    roadBorderColor = Color.FromArgb(85, 40, 20);
                    break;

                case 4: // Màn 4: Khu Vườn Đêm - Không gian đêm tím than huyền ảo ánh trăng
                    baseColor = Color.FromArgb(18, 32, 48);
                    porchColor1 = Color.FromArgb(55, 71, 79);
                    porchColor2 = Color.FromArgb(38, 50, 56);
                    plankColor = Color.FromArgb(20, 30, 40);
                    roadColor1 = Color.FromArgb(35, 40, 48);
                    roadColor2 = Color.FromArgb(22, 26, 32);
                    roadBorderColor = Color.FromArgb(15, 20, 25);
                    break;

                case 5: // Màn 5: Trận Cuối Cùng - Mái ngói đỏ gạch nung nảy lửa Boss Battle
                    baseColor = Color.FromArgb(125, 42, 28);
                    porchColor1 = Color.FromArgb(85, 35, 25);
                    porchColor2 = Color.FromArgb(60, 25, 20);
                    plankColor = Color.FromArgb(45, 18, 15);
                    roadColor1 = Color.FromArgb(52, 42, 42);
                    roadColor2 = Color.FromArgb(35, 25, 25);
                    roadBorderColor = Color.FromArgb(25, 15, 15);
                    break;

                default: // Màn 1: Vườn Sau Nhà - Hiên gỗ nâu ấm, bãi cỏ xanh mướt ban ngày
                    baseColor = Color.FromArgb(70, 142, 48);
                    porchColor1 = Color.FromArgb(141, 110, 99);
                    porchColor2 = Color.FromArgb(109, 76, 65);
                    plankColor = Color.FromArgb(80, 50, 40);
                    roadColor1 = Color.FromArgb(85, 139, 47);
                    roadColor2 = Color.FromArgb(120, 110, 80);
                    roadBorderColor = Color.FromArgb(55, 90, 30);
                    break;
            }

            // Nền tổng thể sân vườn
            using (Brush baseBrush = new SolidBrush(baseColor))
            {
                g.FillRectangle(baseBrush, 0, 0, GameConfig.WINDOW_WIDTH, GameConfig.WINDOW_HEIGHT);
            }

            // Hiên nhà bên trái (House porch) đặt xe cắt cỏ
            int porchWidth = GameConfig.GRID_OFFSET_X;
            Rectangle porchRect = new Rectangle(0, GameConfig.TOP_BAR_HEIGHT, porchWidth, GameConfig.WINDOW_HEIGHT - GameConfig.TOP_BAR_HEIGHT);
            using (LinearGradientBrush porchGrad = new LinearGradientBrush(
                new PointF(0, 0), new PointF(porchWidth, 0),
                porchColor1, porchColor2))
            {
                g.FillRectangle(porchGrad, porchRect);
            }

            // Kẻ rãnh hoa văn ván gỗ hoặc ô đá hiên nhà
            using (Pen plankPen = new Pen(plankColor, 2))
            {
                for (int y = GameConfig.TOP_BAR_HEIGHT; y < GameConfig.WINDOW_HEIGHT; y += 40)
                {
                    g.DrawLine(plankPen, 0, y, porchWidth, y);
                }
                g.DrawLine(plankPen, porchWidth, GameConfig.TOP_BAR_HEIGHT, porchWidth, GameConfig.WINDOW_HEIGHT);
            }

            // Vỉa hè / Đường tiến công Zombie bên phải
            int gridRight = GameConfig.GRID_OFFSET_X + GameConfig.GRID_COLS * GameConfig.CELL_WIDTH;
            Rectangle roadRect = new Rectangle(gridRight, GameConfig.TOP_BAR_HEIGHT, GameConfig.WINDOW_WIDTH - gridRight, GameConfig.WINDOW_HEIGHT - GameConfig.TOP_BAR_HEIGHT);
            using (LinearGradientBrush roadGrad = new LinearGradientBrush(
                new PointF(gridRight, 0), new PointF(GameConfig.WINDOW_WIDTH, 0),
                roadColor1, roadColor2))
            {
                g.FillRectangle(roadGrad, roadRect);
            }
            using (Pen roadBorder = new Pen(roadBorderColor, 2))
            {
                g.DrawLine(roadBorder, gridRight, GameConfig.TOP_BAR_HEIGHT, gridRight, GameConfig.WINDOW_HEIGHT);
            }

            // Họa tiết đêm lấp lánh (Màn 4) hoặc tia lửa Boss (Màn 5)
            if (levelNum == 4)
            {
                // Ánh trăng sương mờ dịu nhẹ
                using (Brush moonGaze = new SolidBrush(Color.FromArgb(25, 179, 229, 252)))
                {
                    g.FillRectangle(moonGaze, 0, GameConfig.TOP_BAR_HEIGHT, GameConfig.WINDOW_WIDTH, GameConfig.WINDOW_HEIGHT - GameConfig.TOP_BAR_HEIGHT);
                }
            }
            else if (levelNum == 5)
            {
                // Ánh lửa hoàng hôn rực rỡ chiến trận
                using (Brush battleGaze = new SolidBrush(Color.FromArgb(30, 255, 87, 34)))
                {
                    g.FillRectangle(battleGaze, 0, GameConfig.TOP_BAR_HEIGHT, GameConfig.WINDOW_WIDTH, GameConfig.WINDOW_HEIGHT - GameConfig.TOP_BAR_HEIGHT);
                }
            }
        }

        private void DrawTopBar(Graphics g)
        {
            if (Engine == null) return;

            // Khung nền Thanh Trên (Gỗ mun cao cấp)
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                new PointF(0, 0), new PointF(0, GameConfig.TOP_BAR_HEIGHT),
                Color.FromArgb(240, 50, 50, 50), Color.FromArgb(240, 30, 30, 30)))
            {
                g.FillRectangle(bgBrush, 0, 0, GameConfig.WINDOW_WIDTH, GameConfig.TOP_BAR_HEIGHT);
            }
            using (Pen barBorder = new Pen(Color.FromArgb(70, 70, 70), 2))
            {
                g.DrawLine(barBorder, 0, GameConfig.TOP_BAR_HEIGHT, GameConfig.WINDOW_WIDTH, GameConfig.TOP_BAR_HEIGHT);
            }

            // Bộ đếm Nắng (x: 12, y: 10, w: 110, h: 70)
            using (LinearGradientBrush sunBg = new LinearGradientBrush(
                new PointF(12, 10), new PointF(122, 80),
                Color.FromArgb(255, 248, 225), Color.FromArgb(255, 236, 179)))
            using (Pen sunBorder = new Pen(Color.FromArgb(255, 179, 0), 2.5f))
            using (Font font = new Font("Arial", 16, FontStyle.Bold))
            {
                Rectangle sunBox = new Rectangle(12, 10, 110, 70);
                g.FillRectangle(sunBg, sunBox);
                g.DrawRectangle(sunBorder, sunBox);

                var sunIcon = Managers.ImageLoader.GetImage("sun", 40, 40);
                g.DrawImage(sunIcon, 15, 25);

                using (Brush textBrush = new SolidBrush(Color.FromArgb(46, 125, 50)))
                {
                    g.DrawString(Engine.SunEnergy.ToString(), font, textBrush, 58, 26);
                }
            }

            // Danh sách Thẻ Hạt Giống
            int cardWidth = 66;
            int cardHeight = 70;
            int cardStartX = 130;
            var unlockedFruits = Engine.CurrentLevel.UnlockedFruits;

            for (int i = 0; i < unlockedFruits.Count; i++)
            {
                FruitType ft = unlockedFruits[i];
                int x = cardStartX + i * (cardWidth + 5);
                int y = 10;
                Rectangle cardRect = new Rectangle(x, y, cardWidth, cardHeight);

                int cost = GameEngine.GetPlantCost(ft);
                float maxCd = GameEngine.GetPlantCooldown(ft);
                float cd = Engine.CardCooldowns[ft];
                bool canAfford = Engine.SunEnergy >= cost && cd <= 0;
                bool isSelected = Engine.SelectedCard == ft;

                // Nền thẻ bài
                Color c1 = isSelected ? Color.FromArgb(255, 241, 118) : (canAfford ? Color.FromArgb(255, 248, 225) : Color.FromArgb(189, 189, 189));
                Color c2 = isSelected ? Color.FromArgb(255, 213, 79) : (canAfford ? Color.FromArgb(255, 224, 178) : Color.FromArgb(158, 158, 158));

                using (LinearGradientBrush cardBg = new LinearGradientBrush(cardRect, c1, c2, 90f))
                using (Pen cardBorder = new Pen(isSelected ? Color.FromArgb(255, 111, 0) : Color.FromArgb(121, 85, 72), isSelected ? 3 : 1.5f))
                {
                    g.FillRectangle(cardBg, cardRect);
                    g.DrawRectangle(cardBorder, cardRect);
                }

                // Hình minh họa cây
                string imgName = GetFruitImageName(ft);
                var fruitImg = Managers.ImageLoader.GetImage(imgName, 48, 48);
                g.DrawImage(fruitImg, x + 9, y + 2);

                // Giá Nắng ở góc dưới
                using (Font costFont = new Font("Arial", 10, FontStyle.Bold))
                using (Brush costBrush = new SolidBrush(canAfford ? Color.Black : Color.FromArgb(198, 40, 40)))
                {
                    g.DrawString($"{cost}", costFont, costBrush, x + 20, y + 50);
                }

                // Lớp phủ tối đếm ngược Cooldown (FR-4.1)
                if (cd > 0)
                {
                    float cdRatio = cd / maxCd;
                    int overlayHeight = (int)(cardHeight * cdRatio);
                    using (Brush cdOverlay = new SolidBrush(Color.FromArgb(170, 0, 0, 0)))
                    {
                        g.FillRectangle(cdOverlay, x, y, cardWidth, overlayHeight);
                    }
                }
            }

            // Tên Màn chơi (x: 710, y: 12, w: 280, h: 66 - Bố trí rộng rãi, tuyệt đối không đè lên thẻ hoa quả)
            Rectangle titleRect = new Rectangle(710, 12, 280, 66);
            using (Brush badgeBg = new SolidBrush(Color.FromArgb(160, 20, 20, 20)))
            using (Pen badgeBorder = new Pen(Color.FromArgb(220, 255, 179, 0), 1.5f))
            using (Font titleFont = new Font("Arial", 11.5f, FontStyle.Bold))
            using (Brush titleBrush = new SolidBrush(Color.FromArgb(255, 236, 179)))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.FillRectangle(badgeBg, titleRect);
                g.DrawRectangle(badgeBorder, titleRect);
                g.DrawString(Engine.CurrentLevel.Name, titleFont, titleBrush, titleRect, sf);
            }

            // Nút Xẻng (x: 1008, y: 10, w: 62, h: 70) (FR-13)
            Rectangle shovelRect = new Rectangle(1008, 10, 62, 70);
            using (Brush shovelBg = new SolidBrush(Engine.IsShovelSelected ? Color.FromArgb(255, 238, 88) : Color.FromArgb(78, 52, 46)))
            using (Pen border = new Pen(Color.FromArgb(255, 179, 0), 2))
            {
                g.FillRectangle(shovelBg, shovelRect);
                g.DrawRectangle(border, shovelRect);
                var shovelImg = Managers.ImageLoader.GetImage("shovel", 44, 44);
                g.DrawImage(shovelImg, 1017, 23);
            }

            // Nút Tạm dừng [P] (x: 1080, y: 10, w: 105, h: 70)
            Rectangle pauseRect = new Rectangle(1080, 10, 105, 70);
            using (LinearGradientBrush pauseBg = new LinearGradientBrush(pauseRect, Color.FromArgb(69, 90, 100), Color.FromArgb(38, 50, 56), 90f))
            using (Pen pauseBorder = new Pen(Color.FromArgb(144, 164, 174), 1.5f))
            using (Font pFont = new Font("Arial", 11f, FontStyle.Bold))
            using (StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.FillRectangle(pauseBg, pauseRect);
                g.DrawRectangle(pauseBorder, pauseRect);
                using (Brush pText = new SolidBrush(Color.White))
                {
                    g.DrawString("Tạm dừng", pFont, pText, pauseRect, sfCenter);
                }
            }
        }

        private void DrawBottomWaveBar(Graphics g)
        {
            if (Engine == null) return;

            int barW = 540;
            int barH = 24;
            int barX = (GameConfig.WINDOW_WIDTH - barW) / 2;
            int barY = GameConfig.WINDOW_HEIGHT - 38;

            float progress = Engine.Waves.ProgressRatio;

            using (Brush bg = new SolidBrush(Color.FromArgb(220, 25, 25, 25)))
            using (LinearGradientBrush fill = new LinearGradientBrush(
                new PointF(barX, barY), new PointF(barX + barW, barY),
                Color.FromArgb(129, 199, 132), Color.FromArgb(46, 125, 50)))
            using (Pen border = new Pen(Color.FromArgb(255, 193, 7), 2))
            using (Font font = new Font("Arial", 9.5f, FontStyle.Bold))
            {
                Rectangle barRect = new Rectangle(barX, barY, barW, barH);
                g.FillRectangle(bg, barRect);
                if (progress > 0)
                {
                    g.FillRectangle(fill, barX, barY, barW * progress, barH);
                }
                g.DrawRectangle(border, barRect);

                // CẮM CÂY CỜ (🚩) tại các mốc có đợt Zombie tấn công lớn
                var bigWaveRatios = Engine.Waves.BigWaveRatios;
                foreach (float waveRatio in bigWaveRatios)
                {
                    float flagX = barX + barW * waveRatio;
                    bool isWaveReached = progress >= waveRatio;

                    // Cột cờ
                    using (Pen polePen = new Pen(Color.FromArgb(224, 224, 224), 2f))
                    {
                        g.DrawLine(polePen, flagX, barY - 16, flagX, barY + barH);
                    }

                    // Chóp đỉnh cột cờ (viên ngọc vàng)
                    using (Brush finialBrush = new SolidBrush(Color.FromArgb(255, 215, 0)))
                    {
                        g.FillEllipse(finialBrush, flagX - 3f, barY - 19, 6, 6);
                    }

                    // Lá cờ tam giác đỏ (cắm hướng sang phải)
                    Color flagColor = isWaveReached ? Color.FromArgb(255, 23, 68) : Color.FromArgb(213, 0, 0);
                    Color flagBorderColor = isWaveReached ? Color.Gold : Color.DarkRed;

                    PointF[] flagPoints = new PointF[]
                    {
                        new PointF(flagX, barY - 16),
                        new PointF(flagX + 14, barY - 9),
                        new PointF(flagX, barY - 2)
                    };

                    using (Brush flagBrush = new SolidBrush(flagColor))
                    using (Pen flagBorderPen = new Pen(flagBorderColor, 1.2f))
                    {
                        g.FillPolygon(flagBrush, flagPoints);
                        g.DrawPolygon(flagBorderPen, flagPoints);
                    }
                }

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                using (Brush textBrush = new SolidBrush(Color.White))
                {
                    g.DrawString($"Tiến trình đợt Zombie: {(int)(progress * 100)}%", font, textBrush, barRect, sf);
                }
            }
        }

        private string GetFruitImageName(FruitType ft)
        {
            switch (ft)
            {
                case FruitType.BananaSun: return "fruit_banana";
                case FruitType.GrapeShooter: return "fruit_grape";
                case FruitType.IceStrawberry: return "fruit_ice_strawberry";
                case FruitType.CoconutWall: return "fruit_coconut";
                case FruitType.ChiliBomb: return "fruit_chili";
                case FruitType.WatermelonLobber: return "fruit_watermelon";
                case FruitType.TomatoMine: return "fruit_tomato";
                case FruitType.DurianThorn: return "fruit_durian";
                default: return "fruit_grape";
            }
        }
    }
}
