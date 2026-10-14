using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Nho Bắn Hạt (GrapeShooter): Giá 100 Nắng, HP 300, Hồi 7s.
    /// Bắn 1 hạt thẳng (20 sát thương) mỗi 1.5s khi có zombie ở cùng hàng phía trước (FR-6.2).
    /// </summary>
    public class GrapeShooter : Fruit
    {
        public System.Func<Projectile, bool>? OnFireProjectile;
        public System.Func<int, float, bool>? HasZombieInRow;

        public GrapeShooter(int row, int col)
            : base(row, col, maxHp: 300, cost: 100, cooldown: 7.0f, FruitType.GrapeShooter)
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            ActionTimer += dt;
            if (ActionTimer >= 1.5f)
            {
                ActionTimer = 0;
                // Chỉ bắn khi có Zombie cùng hàng ở bên phải
                if (HasZombieInRow != null && HasZombieInRow(Row, X))
                {
                    var seed = new Seed(X + Width, Y + Height / 4, Row, damage: 20);
                    OnFireProjectile?.Invoke(seed);
                    Managers.SoundManager.PlaySound("sfx_shoot");
                }
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
            var img = Managers.ImageLoader.GetImage("fruit_grape", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
