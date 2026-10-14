using System.Drawing;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Lớp trừu tượng cơ sở cho mọi đối tượng trong game (cây, zombie, đạn, nắng, xe cắt cỏ...).
    /// Đáp ứng nguyên lý đóng gói, đa hình & kế thừa của OOP 
    /// </summary>
    public abstract class GameObject : IUpdatable, IDrawable
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool IsAlive { get; set; } = true;

        public RectangleF Bounds => new RectangleF(X, Y, Width, Height);

        protected GameObject(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public abstract void Update(float dt);
        public abstract void Draw(Graphics g);

        public bool IntersectsWith(GameObject other)
        {
            if (!IsAlive || !other.IsAlive) return false;
            return Bounds.IntersectsWith(other.Bounds);
        }
    }
}
