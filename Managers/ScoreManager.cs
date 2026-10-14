using System;
using System.Collections.Generic;
using System.IO;

using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Managers
{
    public class ScoreRecord
    {
        public string PlayerName { get; set; } = "Người chơi";
        public int LevelIndex { get; set; } = 1;
        public int Score { get; set; } = 0;
        public int Stars { get; set; } = 1;
        public int TimeSeconds { get; set; } = 0;
        public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }

    /// <summary>
    /// Quản lý việc lưu, đọc, sắp xếp, lọc và xóa bảng thành tích ra file scores.csv.
    /// Bẫy lỗi không làm dừng game nếu file hỏng (FR-20 đến FR-28).
    /// </summary>
    public class ScoreManager
    {
        private static ScoreManager? _instance;
        public static ScoreManager Instance => _instance ??= new ScoreManager();

        private readonly List<ScoreRecord> _scores = new List<ScoreRecord>();

        private ScoreManager()
        {
            LoadScores();
        }

        public void LoadScores()
        {
            _scores.Clear();
            try
            {
                if (!File.Exists(GameConfig.SCORES_FILE))
                {
                    return;
                }

                string[] lines = File.ReadAllLines(GameConfig.SCORES_FILE, System.Text.Encoding.UTF8);
                var rawList = new List<ScoreRecord>();
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                    string[] parts = line.Split(';');
                    if (parts.Length < 6)
                    {
                        Logger.LogWarning($"Dòng thành tích bị lỗi định dạng (bỏ qua): {line}");
                        continue;
                    }

                    try
                    {
                        var record = new ScoreRecord
                        {
                            PlayerName = parts[0].Trim(),
                            LevelIndex = int.Parse(parts[1].Trim()),
                            Score = int.Parse(parts[2].Trim()),
                            Stars = int.Parse(parts[3].Trim()),
                            TimeSeconds = int.Parse(parts[4].Trim()),
                            Date = parts[5].Trim()
                        };
                        rawList.Add(record);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"Lỗi parse dòng score: {line}", ex);
                    }
                }

                // Mỗi người chơi chỉ giữ lại 1 kỷ lục thành tích cao nhất
                var bestDict = new Dictionary<string, ScoreRecord>();
                foreach (var r in rawList)
                {
                    string key = r.PlayerName.Trim().ToLowerInvariant();
                    if (!bestDict.ContainsKey(key))
                    {
                        bestDict[key] = r;
                    }
                    else
                    {
                        var cur = bestDict[key];
                        if (r.Score > cur.Score || (r.Score == cur.Score && r.TimeSeconds < cur.TimeSeconds))
                        {
                            bestDict[key] = r;
                        }
                    }
                }

                var bestScores = new List<ScoreRecord>(bestDict.Values);
                bestScores.Sort((a, b) =>
                {
                    int cmp = b.Score.CompareTo(a.Score);
                    if (cmp == 0) return a.TimeSeconds.CompareTo(b.TimeSeconds);
                    return cmp;
                });

                _scores.AddRange(bestScores);

                // Chuẩn hóa ghi lại file nếu có bản ghi trùng người chơi
                if (rawList.Count != _scores.Count)
                {
                    SaveAllScores();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi đọc file scores.csv", ex);
            }
        }

        public void AddScore(ScoreRecord record)
        {
            // Clean player name
            string cleanName = record.PlayerName.Replace(";", "").Trim();
            if (string.IsNullOrEmpty(cleanName)) cleanName = "Người chơi";
            if (cleanName.Length > 15) cleanName = cleanName.Substring(0, 15);
            record.PlayerName = cleanName;

            // Kiểm tra xem người chơi này đã có thành tích nào chưa
            ScoreRecord? existing = null;
            foreach (var s in _scores)
            {
                if (string.Equals(s.PlayerName, cleanName, StringComparison.OrdinalIgnoreCase))
                {
                    existing = s;
                    break;
                }
            }

            if (existing != null)
            {
                // Chỉ cập nhật nếu thành tích mới cao hơn kỷ lục cũ (hoặc bằng điểm nhưng tốn ít thời gian hơn)
                if (record.Score > existing.Score || (record.Score == existing.Score && record.TimeSeconds < existing.TimeSeconds))
                {
                    existing.Score = record.Score;
                    existing.LevelIndex = record.LevelIndex;
                    existing.Stars = record.Stars;
                    existing.TimeSeconds = record.TimeSeconds;
                    existing.Date = record.Date;
                    SaveAllScores();
                }
            }
            else
            {
                // Người chơi mới -> thêm mới vào bảng kỷ lục
                _scores.Add(record);
                SaveAllScores();
            }
        }

        public void SaveAllScores()
        {
            try
            {
                if (!Directory.Exists(GameConfig.DATA_DIR))
                {
                    Directory.CreateDirectory(GameConfig.DATA_DIR);
                }

                _scores.Sort((a, b) =>
                {
                    int cmp = b.Score.CompareTo(a.Score);
                    if (cmp == 0) return a.TimeSeconds.CompareTo(b.TimeSeconds);
                    return cmp;
                });

                var lines = new List<string>();
                foreach (var s in _scores)
                {
                    lines.Add($"{s.PlayerName};{s.LevelIndex};{s.Score};{s.Stars};{s.TimeSeconds};{s.Date}");
                }

                File.WriteAllLines(GameConfig.SCORES_FILE, lines, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi ghi scores.csv", ex);
            }
        }

        public List<ScoreRecord> GetTopScores(int count = 10, int? levelFilter = null)
        {
            var filtered = new List<ScoreRecord>();
            foreach (var s in _scores)
            {
                if (!levelFilter.HasValue || levelFilter.Value <= 0 || s.LevelIndex == levelFilter.Value)
                {
                    filtered.Add(s);
                }
            }

            filtered.Sort((a, b) =>
            {
                int cmp = b.Score.CompareTo(a.Score);
                if (cmp == 0) return a.TimeSeconds.CompareTo(b.TimeSeconds);
                return cmp;
            });

            var result = new List<ScoreRecord>();
            for (int i = 0; i < filtered.Count && i < count; i++)
            {
                result.Add(filtered[i]);
            }
            return result;
        }

        public void ClearScores()
        {
            _scores.Clear();
            try
            {
                if (File.Exists(GameConfig.SCORES_FILE))
                {
                    File.Delete(GameConfig.SCORES_FILE);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Lỗi khi xóa bảng điểm", ex);
            }
        }
    }
}
