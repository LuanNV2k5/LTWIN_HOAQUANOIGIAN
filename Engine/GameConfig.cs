using System.Drawing;

namespace HoaQuaNoiGian.Engine
{
    /// <summary>
    /// Chứa toàn bộ hằng số và cấu hình hệ thống của game "Hoa Quả Nổi Giận".
    /// Tránh hard-code số liệu rải rác trong các file khác.
    /// </summary>
    public static class GameConfig
    {
        // Kích thước cửa sổ và Canvas chuẩn HD (1200x700)
        public const int WINDOW_WIDTH = 1200;
        public const int WINDOW_HEIGHT = 700;
        public const int VIRTUAL_WIDTH = 1200;
        public const int VIRTUAL_HEIGHT = 700;
        public const int FPS = 60;
        public const float TARGET_DELTA_TIME = 1.0f / FPS;

        // Cấu hình Lưới sân (Grid) - 5 hàng x 9 cột rộng rãi, cân đối
        public const int GRID_ROWS = 5;
        public const int GRID_COLS = 9;
        public const int CELL_WIDTH = 96;
        public const int CELL_HEIGHT = 100;
        public const int GRID_OFFSET_X = 135;
        public const int GRID_OFFSET_Y = 105;
        public const int TOP_BAR_HEIGHT = 90;

        // Cấu hình Nắng (Energy)
        public const int INITIAL_SUN = 150;
        public const int MAX_SUN = 9990;
        public const int SUN_VALUE = 50; // Mỗi lần nhặt bông mặt trời nhận được +50 Nắng
        public const float SKY_SUN_INTERVAL = 9.0f; // rớt Nắng từ trời mỗi 9s
        public const float SUN_DESPAWN_TIME = 8.0f; // Nắng biến mất sau 8s

        // Sắt / Xe cắt cỏ
        public const float LAWNMOWER_SPEED = 400.0f;

        // Máu Zombie
        public const int HP_ZOMBIE_NORMAL = 80;        // Zombie Thường: 80 HP 
        public const int HP_ZOMBIE_RUNNER = 70;        // Zombie Chạy Nhanh: 70 HP 
        public const int HP_ZOMBIE_CONE_HAT = 120;     // Nón lá: 120 HP 
        public const int HP_ZOMBIE_CONE_BODY = 80;     // Thân zombie nón lá: 80 HP
        public const int HP_ZOMBIE_SHIELD_GATE = 140;  // Khiên gỗ: 140 HP 
        public const int HP_ZOMBIE_SHIELD_BODY = 80;   // Thân zombie khiên: 80 HP
        public const int HP_ZOMBIE_BUCKET_HAT = 240;   // Xô sắt: 240 HP 
        public const int HP_ZOMBIE_BUCKET_BODY = 80;   // Thân zombie xô: 80 HP
        public const int HP_ZOMBIE_BOSS = 1800;        // Boss Khổng Lồ: 1800 HP 

        // Điểm số
        public const int SCORE_NORMAL_ZOMBIE = 10;
        public const int SCORE_CONE_ZOMBIE = 20;
        public const int SCORE_BUCKET_ZOMBIE = 30;
        public const int SCORE_RUNNER_ZOMBIE = 25;
        public const int SCORE_SHIELD_ZOMBIE = 40;
        public const int SCORE_BOSS_ZOMBIE = 500;
        public const int SCORE_PER_25_SUN = 5;
        public const int SCORE_PER_SURVIVING_PLANT = 10;
        public const int SCORE_NO_MOWER_BONUS = 100;

        // Đường dẫn thư mục dữ liệu
        public static readonly string DATA_DIR = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static readonly string ASSETS_DIR = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Assets");
        public static readonly string IMAGES_DIR = System.IO.Path.Combine(ASSETS_DIR, "Images");
        public static readonly string SOUNDS_DIR = System.IO.Path.Combine(ASSETS_DIR, "Sounds");

        public static readonly string SCORES_FILE = System.IO.Path.Combine(DATA_DIR, "scores.csv");
        public static readonly string SETTINGS_FILE = System.IO.Path.Combine(DATA_DIR, "settings.ini");
        public static readonly string PROFILES_FILE = System.IO.Path.Combine(DATA_DIR, "profiles.json");
        public static readonly string LOG_FILE = System.IO.Path.Combine(DATA_DIR, "error.log");

        // Màu sắc chủ đạo UI
        public static readonly Color COLOR_PRIMARY = Color.FromArgb(46, 125, 50); // Green
        public static readonly Color COLOR_ACCENT = Color.FromArgb(255, 179, 0); // Yellow/Orange
        public static readonly Color COLOR_BG = Color.FromArgb(33, 33, 33);
    }
}
