using System;
using System.Collections.Generic;

namespace HoaQuaNoiGian.Models
{
    /// <summary>
    /// Lưu thông tin tiến trình của từng người chơi: Tên, Màn đang chơi, Màn đã mở khóa, Điểm, Sao, Ngày giờ.
    /// </summary>
    public class PlayerProfile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "Người chơi";
        public int CurrentLevel { get; set; } = 1;     // Màn đang chơi dở (1 - 5)
        public int UnlockedLevel { get; set; } = 1;    // Màn cao nhất đã mở khóa (1 - 5)
        public int TotalScore { get; set; } = 0;       // Tổng điểm tích lũy
        private Dictionary<string, int>? _levelStars;
        public Dictionary<string, int> LevelStars
        {
            get => _levelStars ??= new Dictionary<string, int>();
            set => _levelStars = value ?? new Dictionary<string, int>();
        }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastPlayed { get; set; } = DateTime.Now;

        public int GetStars(int level)
        {
            return LevelStars.TryGetValue(level.ToString(), out int stars) ? stars : 0;
        }

        public void SetStars(int level, int stars)
        {
            string key = level.ToString();
            if (LevelStars.ContainsKey(key))
            {
                LevelStars[key] = Math.Max(LevelStars[key], stars);
            }
            else
            {
                LevelStars[key] = stars;
            }
        }

        public int TotalStars
        {
            get
            {
                int sum = 0;
                foreach (var stars in LevelStars.Values)
                {
                    sum += stars;
                }
                return sum;
            }
        }

        public string GetLevelTitle()
        {
            return CurrentLevel switch
            {
                1 => "Màn 1: Vườn Sau Nhà",
                2 => "Màn 2: Vườn Trước Nhà",
                3 => "Màn 3: Vườn Cây Ăn Trái",
                4 => "Màn 4: Khu Vườn Đêm",
                5 => "Màn 5: Trận Cuối Cùng",
                _ => $"Màn {CurrentLevel}"
            };
        }
    }
}
