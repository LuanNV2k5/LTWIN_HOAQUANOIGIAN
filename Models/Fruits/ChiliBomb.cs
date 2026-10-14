using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Ớt Bom (ChiliBomb): Giá 150 Nắng, Hồi 30s.
    /// Sau 1 giây nổ tiêu diệt zombie trong cả hàng (sát thương lớn), rồi biến mất (FR-6.5).
    /// </summary>
    public class ChiliBomb : Fruit
    {
        public System.Action<ChiliBomb>? OnExplode;
        public float FuseTimer { get; private set; } = 1.0f; // 1s ngòi nổ
        public bool HasExploded { get; private set; } = false;

        public ChiliBomb(int row, int col)
            : base(row, col, maxHp: 300, cost: 150, cooldown: 30.0f, FruitType.ChiliBomb)
        {
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!HasExploded)
            {
                FuseTimer -= dt;
                if (FuseTimer <= 0)
                {
                    HasExploded = true;
                    IsAlive = false;
                    OnExplode?.Invoke(this);
                    Managers.SoundManager.PlaySound("sfx_explode");
                }
            }
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("fruit_chili", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);

            // Vẽ hoạt ảnh ngòi nổ
            using (var b = new SolidBrush(Color.Yellow))
                g.FillEllipse(b, X + Width / 2 - 5, Y - 5, 10, 10);
        }
    }
}
