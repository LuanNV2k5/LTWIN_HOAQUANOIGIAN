using System.Drawing;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Hiệu ứng trực quan: chữ điểm số nổi (Floating text), hiệu ứng nổ bom, hiệu ứng trúng đạn.
    /// Tự hủy sau khi Lifetime hết.
    /// </summary>
    public class Effect : GameObject
    {
        public string Text { get; }
        public Color TextColor { get; }
        public float Lifetime { get; private set; }
        public float MaxLifetime { get; }
        public EffectType Type { get; }

        public Effect(float x, float y, string text, Color color, float lifetime = 1.0f, EffectType type = EffectType.Text)
            : base(x, y, 60, 40)
        {
            Text = text;
            TextColor = color;
            Lifetime = lifetime;
            MaxLifetime = lifetime;
            Type = type;
        }

        public override void Update(float dt)
        {
            Lifetime -= dt;
            if (Lifetime <= 0)
            {
                IsAlive = false;
                return;
            }

            if (Type == EffectType.Text)
            {
                Y -= 30.0f * dt; // Chữ bay từ từ lên cao
            }
        }

        public override void Draw(Graphics g)
        {
            if (Type == EffectType.Text)
            {
                float alpha = System.Math.Clamp(Lifetime / MaxLifetime, 0f, 1f);
                int a = (int)(alpha * 255);
                using (var brush = new SolidBrush(Color.FromArgb(a, TextColor)))
                using (var font = new Font("Arial", 12, FontStyle.Bold))
                {
                    g.DrawString(Text, font, brush, X, Y);
                }
            }
            else if (Type == EffectType.Explosion)
            {
                float ratio = 1.0f - (Lifetime / MaxLifetime);
                float radius = 30 + ratio * 50;
                using (var brush = new SolidBrush(Color.FromArgb(180, Color.Red)))
                {
                    g.FillEllipse(brush, X - radius / 2, Y - radius / 2, radius, radius);
                }
                using (var font = new Font("Arial", 14, FontStyle.Bold))
                {
                    g.DrawString("BOOM!", font, Brushes.Yellow, X - 25, Y - 10);
                }
            }
        }
    }
}
