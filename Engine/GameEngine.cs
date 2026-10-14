using System;
using System.Collections.Generic;
using System.Drawing;

using HoaQuaNoiGian.Models;
using HoaQuaNoiGian.Models.Fruits;
using HoaQuaNoiGian.Models.Zombies;

namespace HoaQuaNoiGian.Engine
{
    /// <summary>
    /// Engine điều khiển toàn bộ logic game "Hoa Quả Nổi Giận".
    /// Quản lý danh sách đối tượng, va chạm, tính điểm, Nắng, Cooldown, thắng/thua.
    /// Tách rời logic game hoàn toàn khỏi code-behind của WinForms (Mục 14 & 15).
    /// </summary>
    public class GameEngine
    {
        // thuộc tính trạng thái thông số của ván đấu
        public Level CurrentLevel { get; private set; }
        public Grid FieldGrid { get; private set; }
        public WaveManager Waves { get; private set; }
        public GameState State { get; private set; } = GameState.Playing;

        public int SunEnergy { get; private set; }
        public int Score { get; private set; } = 0;
        public int ZombiesKilled { get; private set; } = 0;
        public float GameTime { get; private set; } = 0;
        public int StarsCount { get; private set; } = 1;

        // Quản lý Cooldown cho các thẻ hạt giống
        public Dictionary<FruitType, float> CardCooldowns { get; } = new Dictionary<FruitType, float>();

        // Thao tác người chơi
        public FruitType? SelectedCard { get; set; } = null;
        public bool IsShovelSelected { get; set; } = false;

        // Các danh sách thực thể game
        public List<Fruit> Fruits { get; } = new List<Fruit>();
        public List<Zombie> Zombies { get; } = new List<Zombie>();
        public List<Projectile> Projectiles { get; } = new List<Projectile>();
        public List<SunToken> SunTokens { get; } = new List<SunToken>();
        public List<LawnMower> Mowers { get; } = new List<LawnMower>();
        public List<Effect> Effects { get; } = new List<Effect>();

        private float _skySunTimer = 0;
        private string? _bigWaveAlertText = null;
        private float _bigWaveAlertTimer = 0;

        public event Action? OnStateChanged;
        public event Action<int>? OnSunChanged;
        public event Action<int>? OnScoreChanged;

        public GameEngine(int levelNum)
        {
            CurrentLevel = Level.GetLevelConfig(levelNum);
            FieldGrid = new Grid();
            Waves = new WaveManager(CurrentLevel);
            SunEnergy = CurrentLevel.StartingSun;

            // Khởi tạo Cooldown
            foreach (FruitType ft in Enum.GetValues(typeof(FruitType)))
            {
                CardCooldowns[ft] = 0;
            }

            // Khởi tạo Xe cắt cỏ ở cột 0 cho các hàng active
            for (int r = 0; r < GameConfig.GRID_ROWS; r++)
            {
                if (FieldGrid.IsRowActive(r, CurrentLevel.ActiveRowsCount))
                {
                    Mowers.Add(new LawnMower(r));
                }
            }

            // Đăng ký event sinh zombie
            Waves.OnSpawnZombie += (zombie) =>
            {
                if (zombie is BossZombie boss)
                {
                    boss.OnSpawnMinion += (row) =>
                    {
                        var minion = new ConeZombie(row) { X = boss.X - 50 };
                        Zombies.Add(minion);
                    };
                }
                Zombies.Add(zombie);
            };

            Waves.OnBigWaveAlert += (msg) =>
            {
                _bigWaveAlertText = msg;
                _bigWaveAlertTimer = 3.0f;
            };

            Managers.SoundManager.PlayMusic("bgm_game");
        }

        public void PauseGame()
        {
            if (State == GameState.Playing)
            {
                State = GameState.Paused;
                OnStateChanged?.Invoke();
            }
        }

        public void ResumeGame()
        {
            if (State == GameState.Paused)
            {
                State = GameState.Playing;
                OnStateChanged?.Invoke();
            }
        }

        public void Update(float dt)
        {
            if (State != GameState.Playing) return;

            try
            {
                GameTime += dt;

                // 1. Cập nhật Cooldown thẻ hạt giống
                List<FruitType> cooldownKeys = new List<FruitType>(CardCooldowns.Keys);
                foreach (var ft in cooldownKeys)
                {
                    if (CardCooldowns[ft] > 0)
                    {
                        CardCooldowns[ft] -= dt;
                        if (CardCooldowns[ft] < 0) CardCooldowns[ft] = 0;
                    }
                }

                // 2. Cập nhật đợt zombie (WaveManager)
                Waves.Update(dt);

                // 3. Sinh Nắng từ trời (nếu màn chơi cho phép)
                if (CurrentLevel.SkySunEnabled)
                {
                    _skySunTimer += dt;
                    if (_skySunTimer >= GameConfig.SKY_SUN_INTERVAL)
                    {
                        _skySunTimer = 0;
                        SpawnSkySun();
                    }
                }

                // 4. Cập nhật thông báo Wave lớn
                if (_bigWaveAlertTimer > 0)
                {
                    _bigWaveAlertTimer -= dt;
                    if (_bigWaveAlertTimer <= 0) _bigWaveAlertText = null;
                }

                // 5. Cập nhật cây phòng thủ (Shoot / Produce Sun / Bomb)
                for (int i = Fruits.Count - 1; i >= 0; i--)
                {
                    var f = Fruits[i];
                    f.Update(dt);
                }

                // 6. Cập nhật zombie (Walk / Eat)
                for (int i = Zombies.Count - 1; i >= 0; i--)
                {
                    var z = Zombies[i];
                    z.Update(dt);

                    // Tìm cây cùng hàng va chạm
                    Fruit? targetPlant = null;
                    foreach (var p in Fruits)
                    {
                        if (p.Row == z.Row && p.IntersectsWith(z) && p.IsAlive)
                        {
                            targetPlant = p;
                            break;
                        }
                    }

                    if (z.State == ZombieState.Walking)
                    {
                        if (targetPlant != null)
                        {
                            z.State = ZombieState.Eating;
                        }
                    }
                    else if (z.State == ZombieState.Eating)
                    {
                        if (targetPlant != null)
                        {
                            // Trừ máu cây
                            int damageDealt = (int)(z.AttackDamage * dt);
                            if (damageDealt < 1) damageDealt = 1;
                            targetPlant.TakeDamage(damageDealt);

                            // Sầu riêng gai phản sát thương
                            if (targetPlant is DurianThorn durian)
                            {
                                z.TakeDamage((int)(durian.ReflectDamage * dt));
                            }
                        }
                        else
                        {
                            z.State = ZombieState.Walking;
                        }
                    }

                    // Kiểm tra zombie chạm vạch cắt cỏ / vạch thua (X <= offset - 40)
                    if (z.X <= GameConfig.GRID_OFFSET_X - 40 && z.State != ZombieState.Dying)
                    {
                        LawnMower? mower = null;
                        foreach (var m in Mowers)
                        {
                            if (m.Row == z.Row && m.IsAlive)
                            {
                                mower = m;
                                break;
                            }
                        }

                        if (mower != null && !mower.IsTriggered)
                        {
                            mower.Trigger();
                        }
                        else if (mower == null || mower.X > GameConfig.GRID_OFFSET_X + 100)
                        {
                            // Zombie vượt qua phòng tuyến -> THUA!
                            TriggerGameOver(false);
                            return;
                        }
                    }
                }

                // 7. Cập nhật Xe cắt cỏ & diệt zombie
                for (int i = Mowers.Count - 1; i >= 0; i--)
                {
                    var m = Mowers[i];
                    m.Update(dt);
                    if (m.IsTriggered)
                    {
                        foreach (var z in Zombies)
                        {
                            if (z.Row == m.Row && z.IntersectsWith(m))
                            {
                                z.TakeDamage(9999); // Tiêu diệt ngay lập tức
                            }
                        }
                    }
                }

                // 8. Cập nhật đạn & Kiểm tra va chạm đạn - zombie
                for (int i = Projectiles.Count - 1; i >= 0; i--)
                {
                    var p = Projectiles[i];
                    p.Update(dt);

                    Zombie? hitZombie = null;
                    foreach (var z in Zombies)
                    {
                        if (z.Row == p.Row && z.State != ZombieState.Dying && z.IntersectsWith(p))
                        {
                            hitZombie = z;
                            break;
                        }
                    }

                    if (hitZombie != null)
                    {
                        hitZombie.TakeDamage(p.Damage);
                        if (p.IsSlowEffect)
                        {
                            hitZombie.ApplySlow(5.0f);
                        }

                        // Đạn dưa hấu sát thương lan
                        if (p is MelonShot)
                        {
                            Effects.Add(new Effect(p.X, p.Y, "", Color.Red, 0.5f, EffectType.Explosion));
                            foreach (var zNear in Zombies)
                            {
                                if (Math.Abs(zNear.X - p.X) <= 80 && Math.Abs(zNear.Y - p.Y) <= 80 && zNear != hitZombie)
                                {
                                    zNear.TakeDamage(20);
                                }
                            }
                        }

                        p.IsAlive = false;
                        Managers.SoundManager.PlaySound("sfx_hit");
                    }
                }

                // 9. Cập nhật Cà chua mìn kích nổ khi zombie dẫm lên
                for (int j = 0; j < Fruits.Count; j++)
                {
                    if (Fruits[j] is TomatoMine tomato && tomato.IsArmed)
                    {
                        Zombie? zStep = null;
                        foreach (var z in Zombies)
                        {
                            if (z.Row == tomato.Row && z.State != ZombieState.Dying && z.IntersectsWith(tomato))
                            {
                                zStep = z;
                                break;
                            }
                        }

                        if (zStep != null)
                        {
                            zStep.TakeDamage(1000);
                            tomato.Detonate();
                            Effects.Add(new Effect(tomato.X, tomato.Y, "", Color.OrangeRed, 0.6f, EffectType.Explosion));
                        }
                    }
                }

                // 10. Cập nhật Nắng & Hiệu ứng
                for (int i = SunTokens.Count - 1; i >= 0; i--)
                {
                    var sun = SunTokens[i];
                    sun.Update(dt);
                    if (sun.IsCollected && !sun.IsAlive)
                    {
                        AddSun(sun.Value);
                    }
                }

                for (int i = Effects.Count - 1; i >= 0; i--)
                {
                    Effects[i].Update(dt);
                }

                // 11. Xóa các đối tượng đã IsAlive == false (An toàn thread)
                int deadZombies = 0;
                foreach (var z in Zombies)
                {
                    if (z.State == ZombieState.Dying && !z.IsAlive)
                    {
                        deadZombies++;
                    }
                }
                if (deadZombies > 0)
                {
                    ZombiesKilled += deadZombies;
                }

                for (int i = Fruits.Count - 1; i >= 0; i--) if (!Fruits[i].IsAlive) Fruits.RemoveAt(i);
                for (int i = Zombies.Count - 1; i >= 0; i--) if (!Zombies[i].IsAlive) Zombies.RemoveAt(i);
                for (int i = Projectiles.Count - 1; i >= 0; i--) if (!Projectiles[i].IsAlive) Projectiles.RemoveAt(i);
                for (int i = SunTokens.Count - 1; i >= 0; i--) if (!SunTokens[i].IsAlive) SunTokens.RemoveAt(i);
                for (int i = Mowers.Count - 1; i >= 0; i--) if (!Mowers[i].IsAlive) Mowers.RemoveAt(i);
                for (int i = Effects.Count - 1; i >= 0; i--) if (!Effects[i].IsAlive) Effects.RemoveAt(i);

                // Đảm bảo mọi zombie hết máu đều chuyển sang trạng thái Dying
                foreach (var z in Zombies)
                {
                    if (z.HP <= 0 && z.State != ZombieState.Dying)
                    {
                        z.State = ZombieState.Dying;
                    }
                }

                // 12. Kiểm tra điều kiện THẮNG MÀN (Đã sinh hết quái và không còn zombie sống nào)
                bool allZombiesCleared = true;
                foreach (var z in Zombies)
                {
                    if (z.IsAlive && z.State != ZombieState.Dying)
                    {
                        allZombiesCleared = false;
                        break;
                    }
                }

                if (Waves.HasSpawnedAll && allZombiesCleared)
                {
                    TriggerGameOver(true);
                }
            }
            catch (Exception ex)
            {
                Managers.Logger.LogError("Lỗi trong vòng lặp GameEngine.Update", ex);
            }
        }

        public void SpawnSkySun()
        {
            var rand = new Random();
            int col = rand.Next(0, GameConfig.GRID_COLS);
            int row = rand.Next(0, CurrentLevel.ActiveRowsCount);
            if (CurrentLevel.ActiveRowsCount == 1) row = 2;
            else if (CurrentLevel.ActiveRowsCount == 3) row = rand.Next(1, 4);

            float x = GameConfig.GRID_OFFSET_X + col * GameConfig.CELL_WIDTH + 15;
            float targetY = GameConfig.GRID_OFFSET_Y + row * GameConfig.CELL_HEIGHT + 15;

            SunTokens.Add(new SunToken(x, 0, targetY, isSkySun: true));
        }

        public bool TryPlacePlant(int row, int col, FruitType type)
        {
            if (!FieldGrid.IsRowActive(row, CurrentLevel.ActiveRowsCount)) return false;
            
            bool cellOccupied = false;
            foreach (var f in Fruits)
            {
                if (f.Row == row && f.Col == col)
                {
                    cellOccupied = true;
                    break;
                }
            }
            if (cellOccupied) return false; // Ô đã có cây

            int cost = GetPlantCost(type);
            if (SunEnergy < cost) return false; // Không đủ Nắng
            if (CardCooldowns[type] > 0) return false; // Chưa hết hồi chiêu

            Fruit? newFruit = CreateFruitInstance(type, row, col);
            if (newFruit == null) return false;

            // Đăng ký event từ cây
            if (newFruit is BananaSun banana)
            {
                banana.OnSunProduced = (sun) => { SunTokens.Add(sun); return true; };
            }
            else if (newFruit is GrapeShooter grape)
            {
                grape.OnFireProjectile = (p) => { Projectiles.Add(p); return true; };
                grape.HasZombieInRow = (r, x) =>
                {
                    foreach (var z in Zombies)
                    {
                        if (z.Row == r && z.X > x && z.State != ZombieState.Dying) return true;
                    }
                    return false;
                };
            }
            else if (newFruit is IceStrawberry ice)
            {
                ice.OnFireProjectile = (p) => { Projectiles.Add(p); return true; };
                ice.HasZombieInRow = (r, x) =>
                {
                    foreach (var z in Zombies)
                    {
                        if (z.Row == r && z.X > x && z.State != ZombieState.Dying) return true;
                    }
                    return false;
                };
            }
            else if (newFruit is WatermelonLobber melon)
            {
                melon.OnFireProjectile = (p) => { Projectiles.Add(p); return true; };
                melon.GetTargetZombieInRow = (r, x) =>
                {
                    Zombie? target = null;
                    foreach (var z in Zombies)
                    {
                        if (z.Row == r && z.X > x && z.State != ZombieState.Dying)
                        {
                            if (target == null || z.X < target.X)
                            {
                                target = z;
                            }
                        }
                    }
                    return target;
                };
            }
            else if (newFruit is ChiliBomb chili)
            {
                chili.OnExplode = (bomb) =>
                {
                    Effects.Add(new Effect(bomb.X + bomb.Width / 2, bomb.Y + bomb.Height / 2, "BOOM!", Color.Red, 1.0f, EffectType.Explosion));
                    foreach (var z in Zombies)
                    {
                        if (z.Row == bomb.Row)
                        {
                            z.TakeDamage(1500);
                        }
                    }
                };
            }

            Fruits.Add(newFruit);
            SunEnergy -= cost;
            CardCooldowns[type] = newFruit.CooldownTime;

            OnSunChanged?.Invoke(SunEnergy);
            Managers.SoundManager.PlaySound("sfx_plant");
            return true;
        }

        public bool TryRemovePlant(int row, int col)
        {
            Fruit? plant = null;
            foreach (var f in Fruits)
            {
                if (f.Row == row && f.Col == col)
                {
                    plant = f;
                    break;
                }
            }

            if (plant != null)
            {
                plant.IsAlive = false;
                Managers.SoundManager.PlaySound("sfx_plant");
                return true;
            }
            return false;
        }

        public void AddSun(int amount)
        {
            SunEnergy = Math.Min(GameConfig.MAX_SUN, SunEnergy + amount);
            OnSunChanged?.Invoke(SunEnergy);
        }

        private void TriggerGameOver(bool isWin)
        {
            if (State != GameState.Playing) return;
            State = isWin ? GameState.Won : GameState.Lost;

            if (isWin)
            {
                // Tính điểm cộng thêm khi thắng
                int sunBonus = (SunEnergy / 25) * GameConfig.SCORE_PER_25_SUN;
                int plantBonus = Fruits.Count * GameConfig.SCORE_PER_SURVIVING_PLANT;
                
                int activeMowersCount = 0;
                foreach (var m in Mowers)
                {
                    if (!m.IsTriggered) activeMowersCount++;
                }
                int mowerBonus = activeMowersCount * GameConfig.SCORE_NO_MOWER_BONUS;

                Score += (ZombiesKilled * 15) + sunBonus + plantBonus + mowerBonus;
                OnScoreChanged?.Invoke(Score);

                // Tính Sao (1 sao = thắng, 2 sao = mất <= 3 cây, 3 sao = không mất cây nào)
                int lostPlantsCount = 0; // Đếm số cây đã mất
                if (lostPlantsCount == 0 && Mowers.Count == CurrentLevel.ActiveRowsCount) StarsCount = 3;
                else if (lostPlantsCount <= 3) StarsCount = 2;
                else StarsCount = 1;

                Managers.SoundManager.PlaySound("sfx_win");
            }
            else
            {
                Managers.SoundManager.PlaySound("sfx_lose");
            }

            OnStateChanged?.Invoke();
        }

        public static int GetPlantCost(FruitType type)
        {
            switch (type)
            {
                case FruitType.BananaSun: return 50;
                case FruitType.GrapeShooter: return 100;
                case FruitType.IceStrawberry: return 175;
                case FruitType.CoconutWall: return 50;
                case FruitType.ChiliBomb: return 150;
                case FruitType.WatermelonLobber: return 300;
                case FruitType.TomatoMine: return 25;
                case FruitType.DurianThorn: return 125;
                default: return 100;
            }
        }

        public static float GetPlantCooldown(FruitType type)
        {
            switch (type)
            {
                case FruitType.BananaSun: return 7.0f;
                case FruitType.GrapeShooter: return 7.0f;
                case FruitType.IceStrawberry: return 7.0f;
                case FruitType.CoconutWall: return 20.0f;
                case FruitType.ChiliBomb: return 30.0f;
                case FruitType.WatermelonLobber: return 10.0f;
                case FruitType.TomatoMine: return 25.0f;
                case FruitType.DurianThorn: return 15.0f;
                default: return 7.0f;
            }
        }

        private Fruit? CreateFruitInstance(FruitType type, int row, int col)
        {
            switch (type)
            {
                case FruitType.BananaSun: return new BananaSun(row, col);
                case FruitType.GrapeShooter: return new GrapeShooter(row, col);
                case FruitType.IceStrawberry: return new IceStrawberry(row, col);
                case FruitType.CoconutWall: return new CoconutWall(row, col);
                case FruitType.ChiliBomb: return new ChiliBomb(row, col);
                case FruitType.WatermelonLobber: return new WatermelonLobber(row, col);
                case FruitType.TomatoMine: return new TomatoMine(row, col);
                case FruitType.DurianThorn: return new DurianThorn(row, col);
                default: return null;
            }
        }

        public string? GetBigWaveAlertText() => _bigWaveAlertText;
    }
}
