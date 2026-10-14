using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Zombies
{
    /// <summary>
    /// Zombie Chạy Nhanh (RunnerZombie): HP 200, Tốc độ 48 px/s (0.6 ô/s) (FR-7.4).
    /// </summary>
    public class RunnerZombie : Zombie
    {
        public RunnerZombie(int row)
            : base(row, maxHp: GameConfig.HP_ZOMBIE_RUNNER, speed: 48.0f, attackDmg: 20, type: ZombieType.Runner)
        {
        }

        public override void Draw(Graphics g)
        {
            if (State == ZombieState.Dying) return;

            var img = Managers.ImageLoader.GetImage("zombie_runner", (int)Width, (int)Height);

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
            DrawHealthBar(g, HP, MaxHP);
        }
    }
}
