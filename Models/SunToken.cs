using System;
using System.Drawing;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Cục Nắng rơi từ trên trời hoặc do Chuối Nắng sản xuất.
    /// Tự hủy sau 8s nếu không nhặt. Nhặt thì bay mượt về bộ đếm Nắng (FR-12).
    /// </summary>
    public class SunToken : GameObject
    {
        public int Value { get; } = GameConfig.SUN_VALUE;
        public float TargetY { get; set; }
        public bool IsCollected { get; private set; } = false;
        public float DespawnTimer { get; private set; } = GameConfig.SUN_DESPAWN_TIME;
        public bool IsSkySun { get; }

        private float _collectStartX;
        private float _collectStartY;
        private float _collectProgress = 0f;

        public SunToken(float x, float startY, float targetY, bool isSkySun = true)
            : base(x, startY, 45, 45)
        {
            TargetY = targetY;
            IsSkySun = isSkySun;
        }

        public void Collect()
        {
            if (IsCollected || !IsAlive) return;
            IsCollected = true;
            _collectStartX = X;
            _collectStartY = Y;
            Managers.SoundManager.PlaySound("sfx_sun");
        }

        public override void Update(float dt)
        {
            if (IsCollected)
            {
                _collectProgress += dt * 3.0f; // Bay về bộ đếm Nắng top bar (x=50, y=30)
                if (_collectProgress >= 1.0f)
                {
                    _collectProgress = 1.0f;
                    IsAlive = false;
                }
                else
                {
                    X = _collectStartX + (50 - _collectStartX) * _collectProgress;
                    Y = _collectStartY + (30 - _collectStartY) * _collectProgress;
                }
                return;
            }

            // Rơi từ từ xuống TargetY
            if (Y < TargetY)
            {
                Y += 75.0f * dt;
                if (Y > TargetY) Y = TargetY;
            }

            DespawnTimer -= dt;
            if (DespawnTimer <= 0)
            {
                IsAlive = false;
            }
        }

        public override void Draw(Graphics g)
        {
            var img = Managers.ImageLoader.GetImage("sun", (int)Width, (int)Height);
            g.DrawImage(img, X, Y, Width, Height);
        }
    }
}
