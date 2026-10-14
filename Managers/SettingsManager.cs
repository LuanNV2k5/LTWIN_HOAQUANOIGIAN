using System;
using System.IO;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Managers
{
    /// <summary>
    /// Quản lý việc lưu và đọc các cài đặt của game (âm thanh, nhạc nền, âm lượng, hướng dẫn) ra file settings.ini.
    /// Đáp ứng FR-34.
    /// </summary>
    public class SettingsManager
    {
        private static SettingsManager? _instance;
        public static SettingsManager Instance => _instance ??= new SettingsManager();

        public bool SoundEnabled { get; set; } = true;
        public bool MusicEnabled { get; set; } = true;
        public int Volume { get; set; } = 80; // 0 đến 100
        public bool ShowTutorial { get; set; } = true;
        public int UnlockedLevel { get; set; } = 1; // 1 -> 5

        private SettingsManager()
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            try
            {
                if (!File.Exists(GameConfig.SETTINGS_FILE))
                {
                    SaveSettings();
                    return;
                }

                string[] lines = File.ReadAllLines(GameConfig.SETTINGS_FILE);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                    string[] parts = line.Split('=', 2);
                    if (parts.Length != 2) continue;

                    string key = parts[0].Trim();
                    string val = parts[1].Trim();

                    switch (key.ToLower())
                    {
                        case "soundenabled":
                            if (bool.TryParse(val, out bool s)) SoundEnabled = s;
                            break;
                        case "musicenabled":
                            if (bool.TryParse(val, out bool m)) MusicEnabled = m;
                            break;
                        case "volume":
                            if (int.TryParse(val, out int v)) Volume = Math.Clamp(v, 0, 100);
                            break;
                        case "showtutorial":
                            if (bool.TryParse(val, out bool t)) ShowTutorial = t;
                            break;
                        case "unlockedlevel":
                            if (int.TryParse(val, out int lvl)) UnlockedLevel = Math.Clamp(lvl, 1, 5);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi đọc file settings.ini. Dùng giá trị mặc định.", ex);
            }
        }

        public void SaveSettings()
        {
            try
            {
                if (!Directory.Exists(GameConfig.DATA_DIR))
                {
                    Directory.CreateDirectory(GameConfig.DATA_DIR);
                }

                string content = "# Cài đặt game Hoa Quả Nổi Giận\n" +
                                 $"SoundEnabled={SoundEnabled}\n" +
                                 $"MusicEnabled={MusicEnabled}\n" +
                                 $"Volume={Volume}\n" +
                                 $"ShowTutorial={ShowTutorial}\n" +
                                 $"UnlockedLevel={UnlockedLevel}\n";

                File.WriteAllText(GameConfig.SETTINGS_FILE, content);
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi lưu file settings.ini", ex);
            }
        }
    }
}
