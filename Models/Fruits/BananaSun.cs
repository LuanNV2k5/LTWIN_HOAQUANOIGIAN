using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Chuối Nắng (BananaSun): Giá 50 Nắng, HP 300, Hồi 7s.
    /// Sản xuất +25 Nắng mỗi 10 giây (FR-6.1).
    /// </summary>
    public class BananaSun : Fruit
    {
        public System.Func<SunToken, bool>? OnSunProduced;

        public BananaSun(int row, int col)
            : base(row, col, maxHp: 300, cost: 50, cooldown: 7.0f, FruitType.BananaSun)
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            ActionTimer += dt;
            if (ActionTimer >= 10.0f)
            {
                ActionTimer = 0;
                // Tạo Nắng ngay vị trí cây
                var sun = new SunToken(X + Width / 4, Y + Height / 4, Y + Height / 2, isSkySun: false);
                OnSunProduced?.Invoke(sun);
            }
        }

        public override void Draw(Graphics g)
        {
            if (FlashTimer > 0)
            {
                using (var b = new SolidBrush(Color.White))
                    g.FillRectangle(b, Bounds);
                return;
            }
            var img = Managers.ImageLoader.GetImage("fruit_banana", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
