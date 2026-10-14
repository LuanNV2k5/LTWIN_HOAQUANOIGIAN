using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Dưa Hấu Ném (WatermelonLobber): Giá 300 Nắng, HP 300, Hồi 10s.
    /// Mỗi 3 giây ném quả dưa hấu theo đường parabol, gây 80 sát thương trực tiếp + 20 sát thương lan (FR-6.6).
    /// </summary>
    public class WatermelonLobber : Fruit
    {
        public System.Func<Projectile, bool>? OnFireProjectile;
        public System.Func<int, float, Zombie?>? GetTargetZombieInRow;

        public WatermelonLobber(int row, int col)
            : base(row, col, maxHp: 300, cost: 300, cooldown: 10.0f, FruitType.WatermelonLobber)
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            ActionTimer += dt;
            if (ActionTimer >= 3.0f)
            {
                ActionTimer = 0;
                var targetZombie = GetTargetZombieInRow?.Invoke(Row, X);
                if (targetZombie != null)
                {
                    var melon = new MelonShot(X + Width / 2, Y, targetZombie.X, targetZombie.Y + targetZombie.Height / 2, Row, damage: 80);
                    OnFireProjectile?.Invoke(melon);
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
            var img = Managers.ImageLoader.GetImage("fruit_watermelon", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
