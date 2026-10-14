using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Zombies
{
    /// <summary>
    /// Zombie Thường (NormalZombie): HP 200, Tốc độ 20 px/s (0.25 ô/s), Sát thương cắn 20/s (FR-7.1).
    /// </summary>
    public class NormalZombie : Zombie
    {
        public NormalZombie(int row)
            : base(row, maxHp: GameConfig.HP_ZOMBIE_NORMAL, speed: 20.0f, attackDmg: 20, type: ZombieType.Normal)
        {
        }

        public override void Draw(Graphics g)
        {
            if (State == ZombieState.Dying) return;

            var img = Managers.ImageLoader.GetImage("zombie_normal", (int)Width, (int)Height);

            if (FlashTimer > 0)
            {
                using (var b = new SolidBrush(Color.White))
                    g.FillRectangle(b, Bounds);
            }
            else if (SlowTimer > 0)
            {
                // Đổi tông màu xanh nhạt khi bị làm chậm
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
            DrawHealthBar(g, HP, MaxHP);
        }
    }
}
