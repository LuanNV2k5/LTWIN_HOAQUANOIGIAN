using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Zombies
{
    /// <summary>
    /// Boss Zombie Khổng Lồ (BossZombie): HP 8000, Tốc độ 8 px/s, Sát thương cắn 120/s (FR-7.6).
    /// Hiển thị thanh máu cực lớn phía trên màn hình. Ném zombie con khi bị thương 50% HP.
    /// </summary>
    public class BossZombie : Zombie
    {
        public bool HasSpawnedMinion { get; private set; } = false;
        public System.Action<int>? OnSpawnMinion;

        public BossZombie(int row)
            : base(row, maxHp: GameConfig.HP_ZOMBIE_BOSS, speed: 8.0f, attackDmg: 120, type: ZombieType.Boss)
        {
            // Kích thước Boss: Bề ngang bự hầm hố (120px) nhưng chiều cao (92px) nằm trọn vẹn trong ô hàng (100px)
            Width = GameConfig.CELL_WIDTH * 1.25f;
            Height = GameConfig.CELL_HEIGHT - 8f;

            // Đưa Boss lên ngay ngắn giữa hàng (Y căn chính giữa hàng row), không bị thò thân/chân xuống hàng dưới
            Y = GameConfig.GRID_OFFSET_Y + row * GameConfig.CELL_HEIGHT + (GameConfig.CELL_HEIGHT - Height) / 2f;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            // Khi còn 50% HP, triệu hồi 1 zombie nón lá chạy lên phía trước
            if (!HasSpawnedMinion && HP <= MaxHP / 2)
            {
                HasSpawnedMinion = true;
                OnSpawnMinion?.Invoke(Row);
            }
        }

        public override void Draw(Graphics g)
        {
            if (State == ZombieState.Dying) return;

            // Bóng đổ dưới chân Boss nằm gọn gàng ngay trên hàng
            using (var shadowBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
            {
                g.FillEllipse(shadowBrush, X + 10, Y + Height - 12, Width - 20, 12);
            }

            var img = Managers.ImageLoader.GetImage("zombie_boss", (int)Width, (int)Height);

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

            // Thanh máu ĐỎ ngắn dần trên đầu Boss
            DrawHealthBar(g, HP, MaxHP);
        }

        public override void DrawHealthBar(Graphics g, float currentHp, float maxHp)
        {
            if (currentHp >= maxHp || State == ZombieState.Dying || !IsAlive) return;

            float hpRatio = Math.Max(0f, Math.Min(1f, currentHp / maxHp));
            float barW = Width;
            float barH = 6f;
            float barX = X;
            float barY = Y - 8f; // Gọn gàng ngay trên đầu Boss

            using (Brush bgBrush = new SolidBrush(Color.FromArgb(200, 30, 30, 30)))
            using (Brush redHpBrush = new SolidBrush(Color.FromArgb(244, 67, 54)))
            using (Pen borderPen = new Pen(Color.FromArgb(255, 215, 0), 1.2f))
            {
                g.FillRectangle(bgBrush, barX, barY, barW, barH);
                if (hpRatio > 0)
                {
                    g.FillRectangle(redHpBrush, barX, barY, barW * hpRatio, barH);
                }
                g.DrawRectangle(borderPen, barX, barY, barW, barH);
            }
        }
    }
}
