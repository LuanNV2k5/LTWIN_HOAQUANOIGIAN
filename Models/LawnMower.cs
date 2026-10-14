using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Xe cắt cỏ đặt ở cột 0 cho từng hàng.
    /// Kích hoạt khi zombie chạm tới vạch xuất phát hàng, lao nhanh cán toàn bộ zombie (FR-5.6).
    /// </summary>
    public class LawnMower : GameObject
    {
        public int Row { get; }
        public bool IsTriggered { get; private set; } = false;

        public LawnMower(int row)
            : base(
                GameConfig.GRID_OFFSET_X - 75,
                GameConfig.GRID_OFFSET_Y + row * GameConfig.CELL_HEIGHT + 15,
                65,
                70
            )
        {
            Row = row;
        }

        public void Trigger()
        {
            if (IsTriggered) return;
            IsTriggered = true;
            Managers.SoundManager.PlaySound("sfx_shoot");
        }

        public override void Update(float dt)
        {
            if (IsTriggered)
            {
                X += GameConfig.LAWNMOWER_SPEED * dt;
                if (X > GameConfig.WINDOW_WIDTH + 50)
                {
                    IsAlive = false;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("mower", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
