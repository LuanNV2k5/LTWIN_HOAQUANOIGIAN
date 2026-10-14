using System;
using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Lớp cha của 8 loại hoa quả phòng thủ.
    /// Kế thừa GameObject, mở rộng thêm các thuộc tính Row, Col, HP, Cooldown, Sun Cost.
    /// </summary>
    public abstract class Fruit : GameObject
    {
        public int Row { get; protected set; }
        public int Col { get; protected set; }
        public int HP { get; set; }
        public int MaxHP { get; protected set; }
        public int Cost { get; protected set; }
        public float CooldownTime { get; protected set; }
        public FruitType Type { get; protected set; }

        protected float ActionTimer { get; set; } = 0;
        protected float FlashTimer { get; set; } = 0; // Hiệu ứng nhấp nháy khi trúng đòn

        protected Fruit(int row, int col, int maxHp, int cost, float cooldown, FruitType type)
            : base(
                GameConfig.GRID_OFFSET_X + col * GameConfig.CELL_WIDTH + 5,
                GameConfig.GRID_OFFSET_Y + row * GameConfig.CELL_HEIGHT + 5,
                GameConfig.CELL_WIDTH - 10,
                GameConfig.CELL_HEIGHT - 10
            )
        {
            Row = row;
            Col = col;
            HP = maxHp;
            MaxHP = maxHp;
            Cost = cost;
            CooldownTime = cooldown;
            Type = type;
        }

        public virtual void TakeDamage(int damage)
        {
            HP -= damage;
            FlashTimer = 0.1f; // Nhấp nháy 100 ms
            if (HP <= 0)
            {
                HP = 0;
                IsAlive = false;
            }
        }

        public virtual void OnPlaced()
        {
            // Được gọi khi cây trồng thành công
        }

        public override void Update(float dt)
        {
            if (FlashTimer > 0)
            {
                FlashTimer -= dt;
            }
        }
    }
}
