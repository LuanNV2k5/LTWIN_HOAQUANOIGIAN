using System.Drawing;

namespace HoaQuaNoiGian.Engine
{
    /// <summary>
    /// Quản lý lưới sân chơi 5 hàng × 9 cột.
    /// Chuyển đổi tọa độ chuột (X, Y) sang vị trí (hàng, cột) và ngược lại (Mục 14).
    /// </summary>
    public class Grid
    {
        public int Rows => GameConfig.GRID_ROWS;
        public int Cols => GameConfig.GRID_COLS;
        public int CellWidth => GameConfig.CELL_WIDTH;
        public int CellHeight => GameConfig.CELL_HEIGHT;
        public int OffsetX => GameConfig.GRID_OFFSET_X;
        public int OffsetY => GameConfig.GRID_OFFSET_Y;

        public Rectangle GetCellBounds(int row, int col)
        {
            return new Rectangle(
                OffsetX + col * CellWidth,
                OffsetY + row * CellHeight,
                CellWidth,
                CellHeight
            );
        }

        public bool GetCellFromMouse(int mouseX, int mouseY, out int row, out int col)
        {
            row = -1;
            col = -1;

            if (mouseX < OffsetX || mouseX >= OffsetX + Cols * CellWidth) return false;
            if (mouseY < OffsetY || mouseY >= OffsetY + Rows * CellHeight) return false;

            col = (mouseX - OffsetX) / CellWidth;
            row = (mouseY - OffsetY) / CellHeight;

            return true;
        }

        public void Draw(Graphics g, int activeRowsCount, int levelNum = 1)
        {
            Color lightColor, darkColor, gridLineColor, lockedColor, decoColor1, decoColor2;

            switch (levelNum)
            {
                case 2: // Màn 2: Vườn Trước Nhà - Cỏ xanh ngọc tươi tắn trước sân
                    lightColor = Color.FromArgb(72, 187, 120);
                    darkColor = Color.FromArgb(56, 161, 105);
                    gridLineColor = Color.FromArgb(60, 30, 100, 60);
                    lockedColor = Color.FromArgb(85, 115, 95);
                    decoColor1 = Color.FromArgb(255, 255, 220); // Hoa trắng nhỏ
                    decoColor2 = Color.FromArgb(255, 235, 59);
                    break;

                case 3: // Màn 3: Vườn Cây Ăn Trái - Đất vườn phù sa quả chín sum suê
                    lightColor = Color.FromArgb(124, 179, 66);
                    darkColor = Color.FromArgb(104, 159, 56);
                    gridLineColor = Color.FromArgb(70, 95, 65, 30);
                    lockedColor = Color.FromArgb(100, 95, 75);
                    decoColor1 = Color.FromArgb(255, 138, 101); // Quả rụng cam đào
                    decoColor2 = Color.FromArgb(255, 202, 40);  // Quả chín vàng
                    break;

                case 4: // Màn 4: Khu Vườn Đêm - Thảm cỏ đêm tím than huyền bí ánh trăng
                    lightColor = Color.FromArgb(28, 54, 78);
                    darkColor = Color.FromArgb(20, 40, 60);
                    gridLineColor = Color.FromArgb(70, 10, 25, 40);
                    lockedColor = Color.FromArgb(25, 35, 45);
                    decoColor1 = Color.FromArgb(190, 255, 241, 118); // Đom đóm đêm vàng dịu
                    decoColor2 = Color.FromArgb(150, 129, 212, 250); // Ánh trăng xanh mờ
                    break;

                case 5: // Màn 5: Trận Cuối Cùng - Đấu trường mái ngói đỏ gạch rực lửa Boss
                    lightColor = Color.FromArgb(168, 68, 42);
                    darkColor = Color.FromArgb(138, 52, 28);
                    gridLineColor = Color.FromArgb(80, 80, 25, 15);
                    lockedColor = Color.FromArgb(80, 45, 40);
                    decoColor1 = Color.FromArgb(240, 255, 179, 0);  // Tàn lửa vàng cam
                    decoColor2 = Color.FromArgb(220, 255, 87, 34);   // Tia lửa đỏ rực
                    break;

                default: // Màn 1: Vườn Sau Nhà - Cỏ xanh mướt ban ngày truyền thống
                    lightColor = Color.FromArgb(92, 179, 56);
                    darkColor = Color.FromArgb(76, 153, 46);
                    gridLineColor = Color.FromArgb(60, 50, 110, 30);
                    lockedColor = Color.FromArgb(85, 115, 80);
                    decoColor1 = Color.FromArgb(110, 200, 70);
                    decoColor2 = Color.FromArgb(139, 195, 74);
                    break;
            }

            using (Pen gridPen = new Pen(gridLineColor, 1))
            using (Brush lightBrush = new SolidBrush(lightColor))
            using (Brush darkBrush = new SolidBrush(darkColor))
            using (Brush lockedBrush = new SolidBrush(lockedColor))
            using (Pen lockedStripe = new Pen(Color.FromArgb(40, 0, 0, 0), 1))
            using (Brush decoBrush1 = new SolidBrush(decoColor1))
            using (Brush decoBrush2 = new SolidBrush(decoColor2))
            {
                for (int r = 0; r < Rows; r++)
                {
                    bool isActiveRow = IsRowActive(r, activeRowsCount);

                    for (int c = 0; c < Cols; c++)
                    {
                        var rect = GetCellBounds(r, c);
                        if (isActiveRow)
                        {
                            Brush cellBrush = ((r + c) % 2 == 0) ? lightBrush : darkBrush;
                            g.FillRectangle(cellBrush, rect);
                            g.DrawRectangle(gridPen, rect);

                            // Chấm họa tiết hoa văn sinh động đặc trưng từng màn
                            if ((r + c) % 2 == 0)
                            {
                                g.FillEllipse(decoBrush1, rect.X + 8, rect.Y + 8, 4, 4);
                                g.FillEllipse(decoBrush2, rect.Right - 14, rect.Bottom - 10, 3, 3);
                            }
                            else if (levelNum == 4 || levelNum == 5)
                            {
                                // Ban đêm hoặc trận Boss thêm điểm sáng lung linh
                                g.FillEllipse(decoBrush1, rect.X + rect.Width / 2, rect.Y + rect.Height / 2, 3, 3);
                            }
                        }
                        else
                        {
                            // Hàng chưa mở khóa tô màu tối có gạch chéo mờ
                            g.FillRectangle(lockedBrush, rect);
                            g.DrawRectangle(gridPen, rect);
                            g.DrawLine(lockedStripe, rect.Left, rect.Top, rect.Right, rect.Bottom);
                        }
                    }
                }
            }
        }

        public bool IsRowActive(int row, int activeRowsCount)
        {
            if (activeRowsCount == 1) return row == 2; // Màn 1: 1 hàng giữa
            if (activeRowsCount == 3) return row >= 1 && row <= 3; // Màn 2: 3 hàng
            return true; // 5 hàng
        }
    }
}
