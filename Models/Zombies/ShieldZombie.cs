using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Zombies
{
    /// <summary>
    /// Zombie Khiên (ShieldZombie): HP 600 (Khiên 400 + Thân 200), Tốc độ 25 px/s (FR-7.5).
    /// Đạn thẳng trừ vào khiên trước, khiên vỡ mới trừ HP thân.
    /// </summary>
    public class ShieldZombie : Zombie
    {
        public bool HasShield { get; private set; } = true;
        public int ShieldHP { get; private set; } = GameConfig.HP_ZOMBIE_SHIELD_GATE;

        public ShieldZombie(int row)
            : base(row, maxHp: GameConfig.HP_ZOMBIE_SHIELD_GATE + GameConfig.HP_ZOMBIE_SHIELD_BODY, speed: 25.0f, attackDmg: 25, type: ZombieType.Shield)
        {
            HP = GameConfig.HP_ZOMBIE_SHIELD_BODY;
        }

        public override void TakeDamage(int damage)
        {
            if (HasShield)
            {
                ShieldHP -= damage;
                FlashTimer = 0.1f;
                if (ShieldHP <= 0)
                {
                    int overflow = -ShieldHP;
                    ShieldHP = 0;
                    HasShield = false;
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

            string imageName = HasShield ? "zombie_shield" : "zombie_normal";
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
            DrawHealthBar(g, ShieldHP + HP, MaxHP);
        }
    }
}
