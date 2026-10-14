using System;
using System.IO;
using HoaQuaNoiGian.Engine;

namespace HoaQuaNoiGian.Managers
{
    /// <summary>
    /// Bẫy lỗi và ghi nhật ký hoạt động/lỗi của game ra file Data/error.log.
    /// </summary>
    public static class Logger
    {
        private static readonly object _lock = new object();

        static Logger()
        {
            try
            {
                if (!Directory.Exists(GameConfig.DATA_DIR))
                {
                    Directory.CreateDirectory(GameConfig.DATA_DIR);
                }
            }
            catch
            {
                // Bỏ qua lỗi nếu không tạo được thư mục
            }
        }

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message, null);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARN", message, null);
        }

        public static void LogError(string message, Exception? ex = null)
        {
            WriteLog("ERROR", message, ex);
        }

        private static void WriteLog(string level, string message, Exception? ex)
        {
            lock (_lock)
            {
                try
                {
                    if (!Directory.Exists(GameConfig.DATA_DIR))
                    {
                        Directory.CreateDirectory(GameConfig.DATA_DIR);
                    }

                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                    if (ex != null)
                    {
                        logEntry += $"\nException: {ex.GetType().Name} - {ex.Message}\nStackTrace:\n{ex.StackTrace}";
                    }
                    logEntry += "\n" + new string('-', 60) + "\n";

                    File.AppendAllText(GameConfig.LOG_FILE, logEntry);
                }
                catch
                {
                    // Đảm bảo bẫy lỗi không làm sập ứng dụng
                }
            }
        }
    }
}
