using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Zombies
{
    /// <summary>
    /// Zombie Nón Lá (ConeZombie): HP 560 (Nón 360 + Thân 200), Tốc độ 20 px/s (FR-7.2).
    /// Đội nón lá Việt Nam, nón vỡ khi mất đủ HP rồi trở thành zombie thường.
    /// Ghi đè TakeDamage & Draw (Nạp chồng/ghi đè đa hình - Mục 14.2).
    /// </summary>
    public class ConeZombie : Zombie
    {
        public bool HasCone { get; private set; } = true;
        public int ConeHP { get; private set; } = GameConfig.HP_ZOMBIE_CONE_HAT;

        public ConeZombie(int row)
            : base(row, maxHp: GameConfig.HP_ZOMBIE_CONE_HAT + GameConfig.HP_ZOMBIE_CONE_BODY, speed: 20.0f, attackDmg: 20, type: ZombieType.Cone)
        {
            HP = GameConfig.HP_ZOMBIE_CONE_BODY;
        }

        public override void TakeDamage(int damage)
        {
            if (HasCone)
            {
                ConeHP -= damage;
                FlashTimer = 0.1f;
                if (ConeHP <= 0)
                {
                    int overflow = -ConeHP;
                    ConeHP = 0;
                    HasCone = false;
                    HP = Math.Max(0, HP - overflow);
                    if (HP <= 0)
                    {
                        HP = 0;
                        State = ZombieState.Dying;
                    }
                }
            }
            else
            {
                base.TakeDamage(damage);
            }
        }

        public override void Draw(Graphics g)
        {
            if (State == ZombieState.Dying) return;

            string imageName = HasCone ? "zombie_cone" : "zombie_normal";
            var img = Managers.ImageLoader.GetImage(imageName, (int)Width, (int)Height);

            if (FlashTimer > 0)
            {
                using (var b = new SolidBrush(Color.White))
                    g.FillRectangle(b, Bounds);
            }
            else if (SlowTimer > 0)
            {
                using (var b = new SolidBrush(Color.FromArgb(120, Color.LightBlue)))
                {
                    g.DrawImage(img, X, Y, Width, Height);
                    g.FillRectangle(b, Bounds);
                }
            }
            else
            {
                g.DrawImage(img, X, Y, Width, Height);
            }

            // Thanh máu ĐỎ ngắn dần khi bị mất máu
            DrawHealthBar(g, ConeHP + HP, MaxHP);
        }
    }
}
