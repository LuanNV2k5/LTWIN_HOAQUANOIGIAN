using System.Collections.Generic;
using HoaQuaNoiGian.Models;

namespace HoaQuaNoiGian.Engine
{
    public class ZombieSpawnData
    {
        public float SpawnTime { get; set; } // Giây
        public ZombieType Type { get; set; }
        public int Row { get; set; }
        public bool IsBigWave { get; set; } = false;
    }

    /// <summary>
    /// Định nghĩa cấu hình dữ liệu cho 5 màn chơi
    /// </summary>
    public class Level
    {
        public int LevelNumber { get; set; }
        public string Name { get; set; } = "";
        public int ActiveRowsCount { get; set; } = 5;
        public int StartingSun { get; set; } = 150;
        public bool SkySunEnabled { get; set; } = true;
        public List<FruitType> UnlockedFruits { get; set; } = new List<FruitType>();
        public List<ZombieSpawnData> ZombieSpawns { get; set; } = new List<ZombieSpawnData>();

        public static Level GetLevelConfig(int levelNum)
        {
            levelNum = System.Math.Clamp(levelNum, 1, 5);
            var lvl = new Level { LevelNumber = levelNum };

            switch (levelNum)
            {
                case 1:
                    lvl.Name = "Màn 1: Vườn Sau Nhà";
                    lvl.ActiveRowsCount = 1; // Hàng 2 giữa
                    lvl.StartingSun = 150;
                    lvl.SkySunEnabled = true;
                    lvl.UnlockedFruits = new List<FruitType> { FruitType.BananaSun, FruitType.GrapeShooter };
                    // 3 Wave cơ bản
                    lvl.ZombieSpawns = new List<ZombieSpawnData>
                    {
                        new ZombieSpawnData { SpawnTime = 10, Type = ZombieType.Normal, Row = 2 },
                        new ZombieSpawnData { SpawnTime = 25, Type = ZombieType.Normal, Row = 2 },
                        new ZombieSpawnData { SpawnTime = 40, Type = ZombieType.Normal, Row = 2, IsBigWave = true },
                        new ZombieSpawnData { SpawnTime = 41, Type = ZombieType.Normal, Row = 2 }
                    };
                    break;

                case 2:
                    lvl.Name = "Màn 2: Vườn Trước Nhà";
                    lvl.ActiveRowsCount = 3; // Hàng 1, 2, 3
                    lvl.StartingSun = 150;
                    lvl.SkySunEnabled = true;
                    lvl.UnlockedFruits = new List<FruitType> { FruitType.BananaSun, FruitType.GrapeShooter, FruitType.CoconutWall };
                    // 4 Wave
                    lvl.ZombieSpawns = new List<ZombieSpawnData>
                    {
                        new ZombieSpawnData { SpawnTime = 8, Type = ZombieType.Normal, Row = 1 },
                        new ZombieSpawnData { SpawnTime = 18, Type = ZombieType.Normal, Row = 2 },
                        new ZombieSpawnData { SpawnTime = 28, Type = ZombieType.Cone, Row = 3 },
                        new ZombieSpawnData { SpawnTime = 40, Type = ZombieType.Normal, Row = 1 },
                        new ZombieSpawnData { SpawnTime = 55, Type = ZombieType.Cone, Row = 2, IsBigWave = true },
                        new ZombieSpawnData { SpawnTime = 56, Type = ZombieType.Normal, Row = 3 }
                    };
                    break;

                case 3:
                    lvl.Name = "Màn 3: Vườn Cây Ăn Trái";
                    lvl.ActiveRowsCount = 5;
                    lvl.StartingSun = 175;
                    lvl.SkySunEnabled = true;
                    lvl.UnlockedFruits = new List<FruitType> {
                        FruitType.BananaSun, FruitType.GrapeShooter, FruitType.CoconutWall,
                        FruitType.IceStrawberry, FruitType.ChiliBomb
                    };
                    // 6 Wave
                    lvl.ZombieSpawns = new List<ZombieSpawnData>
                    {
                        new ZombieSpawnData { SpawnTime = 8, Type = ZombieType.Normal, Row = 0 },
                        new ZombieSpawnData { SpawnTime = 16, Type = ZombieType.Runner, Row = 2 },
                        new ZombieSpawnData { SpawnTime = 26, Type = ZombieType.Cone, Row = 4 },
                        new ZombieSpawnData { SpawnTime = 38, Type = ZombieType.Normal, Row = 1 },
                        new ZombieSpawnData { SpawnTime = 50, Type = ZombieType.Runner, Row = 3, IsBigWave = true },
                        new ZombieSpawnData { SpawnTime = 52, Type = ZombieType.Cone, Row = 0 },
                        new ZombieSpawnData { SpawnTime = 54, Type = ZombieType.Normal, Row = 4 }
                    };
                    break;

                case 4:
                    lvl.Name = "Màn 4: Khu Vườn Đêm";
                    lvl.ActiveRowsCount = 5;
                    lvl.StartingSun = 200;
                    lvl.SkySunEnabled = false; // Đêm: Không có Nắng rơi từ trời!
                    lvl.UnlockedFruits = new List<FruitType> {
                        FruitType.BananaSun, FruitType.GrapeShooter, FruitType.IceStrawberry,
                        FruitType.CoconutWall, FruitType.ChiliBomb, FruitType.WatermelonLobber, FruitType.TomatoMine
                    };
                    // 8 Wave
                    lvl.ZombieSpawns = new List<ZombieSpawnData>
                    {
                        new ZombieSpawnData { SpawnTime = 8, Type = ZombieType.Normal, Row = 1 },
                        new ZombieSpawnData { SpawnTime = 16, Type = ZombieType.Cone, Row = 3 },
                        new ZombieSpawnData { SpawnTime = 25, Type = ZombieType.Shield, Row = 0 },
                        new ZombieSpawnData { SpawnTime = 35, Type = ZombieType.Bucket, Row = 4 },
                        new ZombieSpawnData { SpawnTime = 48, Type = ZombieType.Runner, Row = 2 },
                        new ZombieSpawnData { SpawnTime = 60, Type = ZombieType.Shield, Row = 1, IsBigWave = true },
                        new ZombieSpawnData { SpawnTime = 62, Type = ZombieType.Bucket, Row = 3 },
                        new ZombieSpawnData { SpawnTime = 64, Type = ZombieType.Cone, Row = 0 }
                    };
                    break;

                case 5:
                    lvl.Name = "Màn 5: Trận Cuối Cùng";
                    lvl.ActiveRowsCount = 5;
                    lvl.StartingSun = 250;
                    lvl.SkySunEnabled = true;
                    lvl.UnlockedFruits = new List<FruitType> {
                        FruitType.BananaSun, FruitType.GrapeShooter, FruitType.IceStrawberry, FruitType.CoconutWall,
                        FruitType.ChiliBomb, FruitType.WatermelonLobber, FruitType.TomatoMine, FruitType.DurianThorn
                    };
                    // 10 Wave + Boss
                    lvl.ZombieSpawns = new List<ZombieSpawnData>
                    {
                        new ZombieSpawnData { SpawnTime = 6, Type = ZombieType.Normal, Row = 2 },
                        new ZombieSpawnData { SpawnTime = 14, Type = ZombieType.Cone, Row = 0 },
                        new ZombieSpawnData { SpawnTime = 22, Type = ZombieType.Runner, Row = 4 },
                        new ZombieSpawnData { SpawnTime = 30, Type = ZombieType.Shield, Row = 1 },
                        new ZombieSpawnData { SpawnTime = 40, Type = ZombieType.Bucket, Row = 3 },
                        new ZombieSpawnData { SpawnTime = 52, Type = ZombieType.Cone, Row = 2, IsBigWave = true },
                        new ZombieSpawnData { SpawnTime = 65, Type = ZombieType.Runner, Row = 0 },
                        new ZombieSpawnData { SpawnTime = 75, Type = ZombieType.Shield, Row = 4 },
                        new ZombieSpawnData { SpawnTime = 85, Type = ZombieType.Bucket, Row = 1 },
                        // Boss Wave cuối cùng
                        new ZombieSpawnData { SpawnTime = 95, Type = ZombieType.Boss, Row = 2, IsBigWave = true }
                    };
                    break;
            }

            return lvl;
        }
    }
}
