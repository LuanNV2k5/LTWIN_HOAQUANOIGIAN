using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models.Fruits
{
    /// <summary>
    /// Sầu Riêng Gai (DurianThorn): Giá 125 Nắng, HP 700, Hồi 15s.
    /// Zombie cắn bị phản lại 10 HP mỗi nhịp cắn 
    /// </summary>
    public class DurianThorn : Fruit
    {
        public int ReflectDamage { get; } = 10;

        public DurianThorn(int row, int col)
            : base(row, col, maxHp: 700, cost: 125, cooldown: 15.0f, FruitType.DurianThorn)
        {
        }

        public override void Draw(Graphics g)
        {
            if (FlashTimer > 0)
            {
                using (var b = new SolidBrush(Color.White))
                    g.FillRectangle(b, Bounds);
                return;
            }

            var img = Managers.ImageLoader.GetImage("fruit_durian", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
