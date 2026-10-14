using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Zombies
{
    /// <summary>
    /// Zombie Nón Sắt (BucketZombie): HP 1100 (Xô 900 + Thân 200), Tốc độ 20 px/s (FR-7.3).
    /// Rất trâu, ghi đè TakeDamage & Draw.
    /// </summary>
    public class BucketZombie : Zombie
    {
        public bool HasBucket { get; private set; } = true;
        public int BucketHP { get; private set; } = GameConfig.HP_ZOMBIE_BUCKET_HAT;

        public BucketZombie(int row)
            : base(row, maxHp: GameConfig.HP_ZOMBIE_BUCKET_HAT + GameConfig.HP_ZOMBIE_BUCKET_BODY, speed: 20.0f, attackDmg: 20, type: ZombieType.Bucket)
        {
            HP = GameConfig.HP_ZOMBIE_BUCKET_BODY;
        }

        public override void TakeDamage(int damage)
        {
            if (HasBucket)
            {
                BucketHP -= damage;
                FlashTimer = 0.1f;
                if (BucketHP <= 0)
                {
                    int overflow = -BucketHP;
                    BucketHP = 0;
                    HasBucket = false;
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

            string imageName = HasBucket ? "zombie_bucket" : "zombie_normal";
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
            DrawHealthBar(g, BucketHP + HP, MaxHP);
        }
    }
}
