namespace HoaQuaNoiGian.Models
{
    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        Won,
        Lost
    }

    public enum ZombieState
    {
        Walking,
        Eating,
        Dying,
        Frozen
    }

    public enum FruitType
    {
        BananaSun,       // Chuối Nắng
        GrapeShooter,    // Nho Bắn Hạt
        IceStrawberry,   // Dâu Tây Băng
        CoconutWall,     // Dừa Tường
        ChiliBomb,       // Ớt Bom
        WatermelonLobber,// Dưa Hấu Ném
        TomatoMine,      // Cà Chua Mìn
        DurianThorn      // Sầu Riêng Gai
    }

    public enum ZombieType
    {
        Normal,   // Zombie Thường
        Cone,     // Zombie Nón Lá
        Bucket,   // Zombie Nón Sắt
        Runner,   // Zombie Chạy Nhanh
        Shield,   // Zombie Khiên
        Boss      // Boss Zombie Khổng Lồ
    }

    public enum EffectType
    {
        Explosion,
        Text,
        Splat,
        IceFreeze
    }
}
