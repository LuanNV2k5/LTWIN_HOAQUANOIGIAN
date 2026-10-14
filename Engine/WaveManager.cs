using System;
using System.Collections.Generic;
using HoaQuaNoiGian.Models;
using HoaQuaNoiGian.Models.Zombies;

namespace HoaQuaNoiGian.Engine
{
    /// <summary>
    /// Quản lý việc sinh các đợt zombie (Wave) theo thời gian của Level.
    /// Tính tỷ lệ % tiến trình wave và phát thông báo khi có đợt Zombie lớn
    /// </summary>
    public class WaveManager
    {
        private Level _level;
        private float _gameTimer = 0;
        private readonly List<ZombieSpawnData> _pendingSpawns;
        private int _totalZombieCount;
        private int _spawnedZombieCount;

        public event Action<Zombie>? OnSpawnZombie;
        public event Action<string>? OnBigWaveAlert;

        public float ProgressRatio => _totalZombieCount > 0 ? (float)_spawnedZombieCount / _totalZombieCount : 0f;
        public bool HasSpawnedAll => _pendingSpawns.Count == 0;
        public List<float> BigWaveRatios { get; } = new List<float>();

        public WaveManager(Level level)
        {
            _level = level;
            _pendingSpawns = new List<ZombieSpawnData>(level.ZombieSpawns);
            _totalZombieCount = _pendingSpawns.Count;
            _spawnedZombieCount = 0;

            // Xác định các mốc phần trăm tiến trình (%) có đợt Zombie lớn để cắm cờ
            if (_totalZombieCount > 0)
            {
                for (int i = 0; i < level.ZombieSpawns.Count; i++)
                {
                    if (level.ZombieSpawns[i].IsBigWave)
                    {
                        float ratio = (float)(i + 1) / _totalZombieCount;
                        if (!BigWaveRatios.Contains(ratio))
                        {
                            BigWaveRatios.Add(ratio);
                        }
                    }
                }
            }
        }

        public void Update(float dt)
        {
            _gameTimer += dt;

            for (int i = _pendingSpawns.Count - 1; i >= 0; i--)
            {
                var spawn = _pendingSpawns[i];
                if (_gameTimer >= spawn.SpawnTime)
                {
                    if (spawn.IsBigWave)
                    {
                        OnBigWaveAlert?.Invoke("MỘT ĐỢT ZOMBIE ĐANG TIẾN TỚI!");
                        Managers.SoundManager.PlaySound("sfx_wave_alert");
                    }

                    Zombie zombie = CreateZombie(spawn.Type, spawn.Row);
                    OnSpawnZombie?.Invoke(zombie);

                    _spawnedZombieCount++;
                    _pendingSpawns.RemoveAt(i);
                }
            }
        }

        public Zombie CreateZombie(ZombieType type, int row)
        {
            switch (type)
            {
                case ZombieType.Cone: return new ConeZombie(row);
                case ZombieType.Bucket: return new BucketZombie(row);
                case ZombieType.Runner: return new RunnerZombie(row);
                case ZombieType.Shield: return new ShieldZombie(row);
                case ZombieType.Boss: return new BossZombie(row);
                default: return new NormalZombie(row);
            }
        }
    }
}
