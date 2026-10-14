using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using HoaQuaNoiGian.Engine;
using HoaQuaNoiGian.Models;

namespace HoaQuaNoiGian.Managers
{
    /// <summary>
    /// Quản lý hồ sơ và tiến trình của tất cả người chơi trong game.
    /// Cho phép tạo người chơi mới, lưu tiến trình khi thoát, và chọn chơi tiếp.
    /// </summary>
    public class ProfileManager
    {
        private static ProfileManager? _instance;
        public static ProfileManager Instance => _instance ??= new ProfileManager();

        public List<PlayerProfile> Profiles { get; private set; } = new List<PlayerProfile>();
        public PlayerProfile? CurrentPlayer { get; set; }

        private ProfileManager()
        {
            LoadProfiles();
        }

        public void LoadProfiles()
        {
            try
            {
                if (!File.Exists(GameConfig.PROFILES_FILE))
                {
                    Profiles = new List<PlayerProfile>();
                    return;
                }

                string json = File.ReadAllText(GameConfig.PROFILES_FILE);
                var list = JsonSerializer.Deserialize<List<PlayerProfile>>(json);
                Profiles = list ?? new List<PlayerProfile>();

                // Nếu có danh sách, chọn người chơi vừa chơi gần nhất làm CurrentPlayer
                if (Profiles.Count > 0)
                {
                    PlayerProfile latest = Profiles[0];
                    foreach (var p in Profiles)
                    {
                        if (p.LastPlayed > latest.LastPlayed)
                        {
                            latest = p;
                        }
                    }
                    CurrentPlayer = latest;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi đọc file tiến trình profiles.json", ex);
                Profiles = new List<PlayerProfile>();
            }
        }

        public void SaveProfiles()
        {
            try
            {
                if (!Directory.Exists(GameConfig.DATA_DIR))
                {
                    Directory.CreateDirectory(GameConfig.DATA_DIR);
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Profiles, options);
                File.WriteAllText(GameConfig.PROFILES_FILE, json);
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi lưu file tiến trình profiles.json", ex);
            }
        }

        public PlayerProfile CreateNewProfile(string playerName)
        {
            string cleanName = playerName.Trim();
            if (string.IsNullOrWhiteSpace(cleanName))
            {
                cleanName = $"Người chơi {Profiles.Count + 1}";
            }

            var newProfile = new PlayerProfile
            {
                Name = cleanName,
                CurrentLevel = 1,
                UnlockedLevel = 1,
                TotalScore = 0,
                CreatedAt = DateTime.Now,
                LastPlayed = DateTime.Now
            };

            Profiles.Insert(0, newProfile);
            CurrentPlayer = newProfile;
            SaveProfiles();

            // Đồng bộ UnlockedLevel sang SettingsManager
            SettingsManager.Instance.UnlockedLevel = 1;
            SettingsManager.Instance.SaveSettings();

            return newProfile;
        }

        public void SelectProfile(string profileId)
        {
            PlayerProfile? p = null;
            foreach (var item in Profiles)
            {
                if (item.Id == profileId)
                {
                    p = item;
                    break;
                }
            }

            if (p != null)
            {
                p.LastPlayed = DateTime.Now;
                CurrentPlayer = p;
                SaveProfiles();

                SettingsManager.Instance.UnlockedLevel = p.UnlockedLevel;
                SettingsManager.Instance.SaveSettings();
            }
        }

        public bool DeleteProfile(string profileId)
        {
            PlayerProfile? p = null;
            foreach (var item in Profiles)
            {
                if (item.Id == profileId)
                {
                    p = item;
                    break;
                }
            }

            if (p != null)
            {
                Profiles.Remove(p);
                if (CurrentPlayer != null && CurrentPlayer.Id == profileId)
                {
                    PlayerProfile? latest = null;
                    foreach (var item in Profiles)
                    {
                        if (latest == null || item.LastPlayed > latest.LastPlayed)
                        {
                            latest = item;
                        }
                    }
                    CurrentPlayer = latest;
                    if (CurrentPlayer != null)
                    {
                        SettingsManager.Instance.UnlockedLevel = CurrentPlayer.UnlockedLevel;
                    }
                }
                SaveProfiles();
                return true;
            }
            return false;
        }

        public void SaveCurrentProgress(int levelNum, int scoreGained = 0, int starsGained = 0, bool isWin = false)
        {
            if (CurrentPlayer == null)
            {
                // Nếu chưa có profile nào được chọn, tạo profile mặc định
                CreateNewProfile("Người chơi");
            }

            if (CurrentPlayer == null) return;

            CurrentPlayer.LastPlayed = DateTime.Now;

            if (isWin)
            {
                CurrentPlayer.SetStars(levelNum, starsGained);
                CurrentPlayer.TotalScore += scoreGained;

                int nextLvl = Math.Min(5, levelNum + 1);
                CurrentPlayer.CurrentLevel = nextLvl;
                CurrentPlayer.UnlockedLevel = Math.Max(CurrentPlayer.UnlockedLevel, nextLvl);
            }
            else
            {
                // Khi thoát giữa chừng hoặc thua, lưu lại đúng màn hiện tại người đó đang chơi
                CurrentPlayer.CurrentLevel = levelNum;
                CurrentPlayer.UnlockedLevel = Math.Max(CurrentPlayer.UnlockedLevel, levelNum);
            }

            SaveProfiles();

            // Đồng bộ sang SettingsManager
            SettingsManager.Instance.UnlockedLevel = CurrentPlayer.UnlockedLevel;
            SettingsManager.Instance.SaveSettings();
        }
    }
}
