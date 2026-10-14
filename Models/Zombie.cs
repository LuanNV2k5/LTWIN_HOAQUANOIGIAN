using System;
using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Lớp cha của 6 loại zombie.
    /// Quản lý di chuyển, cắn cây, nhận sát thương, làm chậm, hoạt ảnh chết (Mục 7).
    /// </summary>
    public abstract class Zombie : GameObject
    {
        public int Row { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; protected set; }
        public float Speed { get; set; }
        public float BaseSpeed { get; protected set; }
        public int AttackDamage { get; protected set; }
        public ZombieState State { get; set; } = ZombieState.Walking;
        public ZombieType Type { get; protected set; }

        public float SlowTimer { get; protected set; } = 0;
        public float DeathTimer { get; protected set; } = 0.5f; // Hoạt ảnh ngã khi chết
        protected float FlashTimer { get; set; } = 0;

        protected Zombie(int row, int maxHp, float speed, int attackDmg, ZombieType type)
            : base(
                GameConfig.WINDOW_WIDTH + 10,
                GameConfig.GRID_OFFSET_Y + row * GameConfig.CELL_HEIGHT + 5,
                GameConfig.CELL_WIDTH - 15,
                GameConfig.CELL_HEIGHT - 10
            )
        {
            Row = row;
            HP = maxHp;
            MaxHP = maxHp;
            BaseSpeed = speed;
            Speed = speed;
            AttackDamage = attackDmg;
            Type = type;
        }

        public virtual void TakeDamage(int damage)
        {
            if (State == ZombieState.Dying || !IsAlive) return;

            HP -= damage;
            FlashTimer = 0.1f;
            if (HP <= 0)
            {
                HP = 0;
                State = ZombieState.Dying;
            }
        }

        public void ApplySlow(float duration, float factor = 0.5f)
        {
            SlowTimer = duration;
            Speed = BaseSpeed * factor;
        }

        public override void Update(float dt)
        {
            if (FlashTimer > 0) FlashTimer -= dt;

            // Xử lý đếm ngược làm chậm
            if (SlowTimer > 0)
            {
                SlowTimer -= dt;
                if (SlowTimer <= 0)
                {
                    SlowTimer = 0;
                    Speed = BaseSpeed;
                }
            }

            // Xử lý hoạt ảnh chết
            if (State == ZombieState.Dying)
            {
                DeathTimer -= dt;
                if (DeathTimer <= 0)
                {
                    IsAlive = false;
                }
                return;
            }

            // Di chuyển sang trái nếu đang đi
            if (State == ZombieState.Walking)
            {
                X -= Speed * dt;
            }
        }

        /// <summary>
        /// Vẽ thanh máu trên đầu Zombie.
        /// Nền tối, thanh màu ĐỎ TƯƠI ngắn dần theo lượng máu còn lại khi bị bắn.
        /// </summary>
        public virtual void DrawHealthBar(Graphics g, float currentHp, float maxHp)
        {
            if (currentHp >= maxHp || State == ZombieState.Dying || !IsAlive) return;

            float hpRatio = Math.Max(0f, Math.Min(1f, currentHp / maxHp));
            float barW = Width;
            float barH = 6f;
            float barX = X;
            float barY = Y - 12f;

            using (Brush bgBrush = new SolidBrush(Color.FromArgb(190, 30, 30, 30)))
            using (Brush redHpBrush = new SolidBrush(Color.FromArgb(235, 50, 50))) // Thanh ĐỎ ngắn dần
            using (Pen borderPen = new Pen(Color.FromArgb(220, 20, 20, 20), 1f))
            {
                // Nền đen/xám tối
                g.FillRectangle(bgBrush, barX, barY, barW, barH);

                // Thanh máu ĐỎ ngắn dần theo máu hiện tại
                if (hpRatio > 0)
                {
                    g.FillRectangle(redHpBrush, barX, barY, barW * hpRatio, barH);
                }

                // Viền khung
                g.DrawRectangle(borderPen, barX, barY, barW, barH);
            }
        }
    }
}
