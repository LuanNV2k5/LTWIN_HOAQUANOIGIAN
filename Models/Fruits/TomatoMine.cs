using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Cà Chua Mìn (TomatoMine): Giá 25 Nắng, Hồi 25s.
    /// Sau 10s "lên đạn", zombie dẫm lên thì nổ diệt zombie (1000 sát thương) (FR-6.7).
    /// </summary>
    public class TomatoMine : Fruit
    {
        public bool IsArmed { get; private set; } = false;
        public float ArmTimer { get; private set; } = 10.0f;

        public TomatoMine(int row, int col)
            : base(row, col, maxHp: 300, cost: 25, cooldown: 25.0f, FruitType.TomatoMine)
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!IsArmed)
            {
                ArmTimer -= dt;
                if (ArmTimer <= 0)
                {
                    IsArmed = true;
                }
            }
        }

        public void Detonate()
        {
            IsAlive = false;
            Managers.SoundManager.PlaySound("sfx_explode");
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("fruit_tomato", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);

            if (!IsArmed)
            {
                // Vẽ hiệu ứng đang vùi dưới đất
                using (var b = new SolidBrush(Color.FromArgb(150, Color.SaddleBrown)))
                    g.FillRectangle(b, X, Y + Height * 0.6f, Width, Height * 0.4f);
            }
            else
            {
                // Đã châm ngòi (đèn đỏ sáng)
                using (var b = new SolidBrush(Color.Red))
                    g.FillEllipse(b, X + Width / 2 - 4, Y, 8, 8);
            }
        }
    }
}
