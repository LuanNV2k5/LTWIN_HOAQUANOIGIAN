using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models
{
    public abstract class Projectile : GameObject
    {
        public int Row { get; protected set; }
        public int Damage { get; protected set; }
        public float Speed { get; protected set; }
        public bool IsSlowEffect { get; protected set; } = false;

        protected Projectile(float x, float y, int row, int damage, float speed, float width = 20, float height = 20)
            : base(x, y, width, height)
        {
            Row = row;
            Damage = damage;
            Speed = speed;
        }

        public override void Update(float dt)
        {
            X += Speed * dt;
            if (X > GameConfig.WINDOW_WIDTH + 50)
            {
                IsAlive = false;
            }
        }
    }

    /// <summary>
    /// Đạn hạt nho bắn thẳng (GrapeShooter)
    /// </summary>
    public class Seed : Projectile
    {
        public Seed(float x, float y, int row, int damage = 20)
            : base(x, y, row, damage, 350.0f, 18, 18)
        {
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("seed", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }

    /// <summary>
    /// Đạn băng (IceStrawberry) gây làm chậm
    /// </summary>
    public class IceSeed : Projectile
    {
        public IceSeed(float x, float y, int row, int damage = 20)
            : base(x, y, row, damage, 350.0f, 18, 18)
        {
            IsSlowEffect = true;
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("seed_ice", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }

    /// <summary>
    /// Đạn dưa hấu bay theo quỹ đạo parabol (WatermelonLobber)
    /// </summary>
    public class MelonShot : Projectile
    {
        public float StartX { get; private set; }
        public float TargetX { get; private set; }
        public float TargetY { get; private set; }
        public float ArcHeight { get; private set; } = 80.0f;
        public float Progress { get; private set; } = 0.0f;

        public MelonShot(float startX, float startY, float targetX, float targetY, int row, int damage = 80)
            : base(startX, startY, row, damage, 250.0f, 28, 28)
        {
            StartX = startX;
            TargetX = targetX;
            TargetY = targetY;
        }

        public override void Update(float dt)
        {
            float totalDistance = Math.Max(50, TargetX - StartX);
            Progress += (Speed * dt) / totalDistance;

            if (Progress >= 1.0f)
            {
                Progress = 1.0f;
                X = TargetX;
                Y = TargetY;
                IsAlive = false;
            }
            else
            {
                X = StartX + Progress * (TargetX - StartX);
                // Parabola: y = -4 * h * p * (1 - p)
                float arc = 4 * ArcHeight * Progress * (1 - Progress);
                Y = TargetY - arc;
            }
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("fruit_watermelon", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
