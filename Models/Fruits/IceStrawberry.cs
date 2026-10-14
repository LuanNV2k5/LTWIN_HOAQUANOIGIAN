using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Dâu Tây Băng (IceStrawberry): Giá 175 Nắng, HP 300, Hồi 7s.
    /// Bắn đạn băng 20 sát thương + làm chậm 50% trong 5 giây (FR-6.3).
    /// </summary>
    public class IceStrawberry : Fruit
    {
        public System.Func<Projectile, bool>? OnFireProjectile;
        public System.Func<int, float, bool>? HasZombieInRow;

        public IceStrawberry(int row, int col)
            : base(row, col, maxHp: 300, cost: 175, cooldown: 7.0f, FruitType.IceStrawberry)
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            ActionTimer += dt;
            if (ActionTimer >= 1.5f)
            {
                ActionTimer = 0;
                if (HasZombieInRow != null && HasZombieInRow(Row, X))
                {
                    var iceSeed = new IceSeed(X + Width, Y + Height / 4, Row, damage: 20);
                    OnFireProjectile?.Invoke(iceSeed);
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
            var img = Managers.ImageLoader.GetImage("fruit_ice_strawberry", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
