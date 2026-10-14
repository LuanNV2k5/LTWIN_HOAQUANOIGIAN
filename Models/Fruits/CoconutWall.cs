using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Dừa Tường (CoconutWall): Giá 50 Nắng, HP 1800, Hồi 20s.
    /// Không tấn công, chịu đòn tốt. Đổi trạng thái hiển thị khi HP < 66% và < 33% (FR-6.4).
    /// </summary>
    public class CoconutWall : Fruit
    {
        public CoconutWall(int row, int col)
            : base(row, col, maxHp: 1800, cost: 50, cooldown: 20.0f, FruitType.CoconutWall)
        {
        }

        public override void Draw(Graphics g)
        {
            if (FlashTimer > 0)
            {
                using (var b = new SolidBrush(Color.White))
                    g.FillRectangle(b, Bounds);
                return;
            }

            var img = Managers.ImageLoader.GetImage("fruit_coconut", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);

            // Vẽ vết nứt biểu thị lượng máu giảm
            float hpRatio = (float)HP / MaxHP;
            if (hpRatio < 0.66f)
            {
                using (var pen = new Pen(Color.SaddleBrown, 3))
                {
                    g.DrawLine(pen, X + Width / 3, Y + Height / 4, X + Width / 2, Y + Height / 2);
                    if (hpRatio < 0.33f)
                    {
                        g.DrawLine(pen, X + Width / 2, Y + Height / 2, X + 2 * Width / 3, Y + 3 * Height / 4);
                    }
                }
            }
        }
    }
}
