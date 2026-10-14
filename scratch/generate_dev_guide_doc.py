import docx
from docx import Document
from docx.shared import Inches, Pt, RGBColor
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.enum.table import WD_TABLE_ALIGNMENT
from docx.oxml import parse_xml
from docx.oxml.ns import nsdecls

def set_cell_background(cell, color_hex):
    shading_elm = parse_xml(f'<w:shd {nsdecls("w")} w:fill="{color_hex}"/>')
    cell._tc.get_or_add_tcPr().append(shading_elm)

def set_cell_margins(cell, top=100, bottom=100, left=150, right=150):
    tcPr = cell._tc.get_or_add_tcPr()
    tcMar = parse_xml(f'<w:tcMar {nsdecls("w")}><w:top w:w="{top}" w:type="dxa"/><w:bottom w:w="{bottom}" w:type="dxa"/><w:left w:w="{left}" w:type="dxa"/><w:right w:w="{right}" w:type="dxa"/></w:tcMar>')
    tcPr.append(tcMar)

def create_dev_guide_doc():
    doc = Document()

    for section in doc.sections:
        section.top_margin = Inches(0.8)
        section.bottom_margin = Inches(0.8)
        section.left_margin = Inches(0.8)
        section.right_margin = Inches(0.8)

    COLOR_PRIMARY = RGBColor(27, 94, 32)      # #1B5E20
    COLOR_SECONDARY = RGBColor(46, 125, 50)  # #2E7D32
    COLOR_ACCENT = RGBColor(230, 81, 0)      # #E65100
    COLOR_TEXT = RGBColor(33, 33, 33)        # #212121
    COLOR_CODE_BG = "263238"                 # Nền code xám đậm
    COLOR_CODE_TXT = RGBColor(238, 255, 65)  # Chữ code vàng chanh

    # Tiêu đề
    title_p = doc.add_paragraph()
    title_p.alignment = WD_ALIGN_PARAGRAPH.CENTER
    title_p.paragraph_format.space_before = Pt(14)
    title_p.paragraph_format.space_after = Pt(4)
    r_title = title_p.add_run("HƯỚNG DẪN CHI TIẾT TỪ A ĐẾN Z\nCÁCH LẬP TRÌNH & NGUYÊN LÝ CÁC HÀM TRONG GAME\n\"HOA QUẢ NỔI GIẬN\"")
    r_title.font.name = "Arial"
    r_title.font.size = Pt(20)
    r_title.font.bold = True
    r_title.font.color.rgb = COLOR_PRIMARY

    sub_p = doc.add_paragraph()
    sub_p.alignment = WD_ALIGN_PARAGRAPH.CENTER
    sub_p.paragraph_format.space_after = Pt(16)
    r_sub = sub_p.add_run("TÀI LIỆU HƯỚNG DẪN PHÁT TRIỂN GAME CHIẾN THUẬT PHÒNG THỦ - C# WINFORMS .NET 8")
    r_sub.font.name = "Arial"
    r_sub.font.size = Pt(11.5)
    r_sub.font.bold = True
    r_sub.font.color.rgb = COLOR_ACCENT

    # Bảng thông tin tổng quan
    info_table = doc.add_table(rows=5, cols=2)
    info_table.alignment = WD_TABLE_ALIGNMENT.CENTER
    info_data = [
        ("Mục đích tài liệu:", "Hướng dẫn toàn bộ quy trình xây dựng game và giải thích chi tiết hoạt động của từng hàm"),
        ("Ngôn ngữ & Framework:", "C# .NET 8 (Windows Forms), Guna.UI2 WinForms Framework"),
        ("Đồ họa & Âm thanh:", "GDI+ Drawing 60 FPS, Double Buffered Canvas, SoundPlayer/WAV"),
        ("Cơ sở dữ liệu:", "JSON (Tiến trình người chơi), CSV (Điểm cao), INI (Cấu hình)"),
        ("Phiên bản tài liệu:", "Phiên bản Hoàn Chỉnh - Đầy Đủ Mã Nguồn & Giải Thuật")
    ]
    for row_idx, (label, val) in enumerate(info_data):
        row = info_table.rows[row_idx]
        cell_lbl, cell_val = row.cells[0], row.cells[1]
        cell_lbl.width = Inches(2.2)
        cell_val.width = Inches(4.6)
        set_cell_background(cell_lbl, "E8F5E9")
        set_cell_background(cell_val, "FAFAFA")
        set_cell_margins(cell_lbl, 70, 70, 110, 110)
        set_cell_margins(cell_val, 70, 70, 110, 110)
        
        p1 = cell_lbl.paragraphs[0]
        r1 = p1.add_run(label)
        r1.font.name = "Arial"
        r1.font.bold = True
        r1.font.size = Pt(9.5)
        r1.font.color.rgb = COLOR_PRIMARY
        
        p2 = cell_val.paragraphs[0]
        r2 = p2.add_run(val)
        r2.font.name = "Arial"
        r2.font.size = Pt(9.5)

    doc.add_paragraph().paragraph_format.space_after = Pt(8)

    def add_heading_1(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(16)
        p.paragraph_format.space_after = Pt(6)
        p.paragraph_format.keep_with_next = True
        r = p.add_run(text)
        r.font.name = "Arial"
        r.font.size = Pt(14)
        r.font.bold = True
        r.font.color.rgb = COLOR_PRIMARY
        return p

    def add_heading_2(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(12)
        p.paragraph_format.space_after = Pt(4)
        p.paragraph_format.keep_with_next = True
        r = p.add_run(text)
        r.font.name = "Arial"
        r.font.size = Pt(12)
        r.font.bold = True
        r.font.color.rgb = COLOR_SECONDARY
        return p

    def add_heading_3(text):
        p = doc.add_paragraph()
        p.paragraph_format.space_before = Pt(8)
        p.paragraph_format.space_after = Pt(2)
        p.paragraph_format.keep_with_next = True
        r = p.add_run(text)
        r.font.name = "Arial"
        r.font.size = Pt(10.5)
        r.font.bold = True
        r.font.color.rgb = COLOR_ACCENT
        return p

    def add_body(text, bold_prefix=None, italic=False):
        p = doc.add_paragraph()
        p.paragraph_format.space_after = Pt(4)
        p.paragraph_format.line_spacing = 1.15
        if bold_prefix:
            r_pre = p.add_run(bold_prefix)
            r_pre.font.name = "Arial"
            r_pre.font.size = Pt(9.5)
            r_pre.font.bold = True
            r_pre.font.color.rgb = COLOR_PRIMARY
        r = p.add_run(text)
        r.font.name = "Arial"
        r.font.size = Pt(9.5)
        r.font.italic = italic
        r.font.color.rgb = COLOR_TEXT
        return p

    def add_bullet(text, bold_prefix=None):
        p = doc.add_paragraph(style='List Bullet')
        p.paragraph_format.space_after = Pt(3)
        p.paragraph_format.line_spacing = 1.15
        if bold_prefix:
            r_pre = p.add_run(bold_prefix)
            r_pre.font.name = "Arial"
            r_pre.font.size = Pt(9.5)
            r_pre.font.bold = True
            r_pre.font.color.rgb = COLOR_PRIMARY
        r = p.add_run(text)
        r.font.name = "Arial"
        r.font.size = Pt(9.5)
        r.font.color.rgb = COLOR_TEXT
        return p

    def add_code_block(code_text):
        tbl = doc.add_table(rows=1, cols=1)
        tbl.alignment = WD_TABLE_ALIGNMENT.CENTER
        cell = tbl.rows[0].cells[0]
        cell.width = Inches(6.8)
        set_cell_background(cell, "ECEFF1")
        set_cell_margins(cell, 80, 80, 120, 120)
        p = cell.paragraphs[0]
        p.paragraph_format.line_spacing = 1.05
        p.paragraph_format.space_after = Pt(0)
        r = p.add_run(code_text)
        r.font.name = "Consolas"
        r.font.size = Pt(8.5)
        r.font.color.rgb = RGBColor(38, 50, 56)
        doc.add_paragraph().paragraph_format.space_after = Pt(4)

    # =========================================================================
    # CHƯƠNG 1
    # =========================================================================
    add_heading_1("CHƯƠNG 1: KHỞI TẠO DỰ ÁN & THIẾT KẾ CẤU HÌNH TỔNG THỂ")
    add_body("Để xây dựng tựa game một cách bài bản và không bị lỗi thời gian chạy, bước đầu tiên là thiết lập dự án chuẩn và xây dựng lớp hằng số GameConfig.")

    add_heading_2("1.1. Tạo Project và Cài đặt Thư viện")
    add_bullet(" Mở Visual Studio -> Create a new project -> Chọn 'Windows Forms App' -> Target Framework chọn '.NET 8.0 (Long Term Support)'.", "Bước 1:")
    add_bullet(" Mở NuGet Package Manager -> Tìm và cài đặt package 'Guna.UI2.WinForms' (bộ điều khiển giao diện hiện đại bo góc, gradient).", "Bước 2:")
    add_bullet(" Tạo cấu trúc thư mục phân tầng gồm 5 thư mục cốt lõi: Assets/ (chứa ảnh và âm thanh), Data/ (chứa dữ liệu lưu trữ), Engine/ (chứa logic và bảng vẽ), Forms/ (chứa các màn hình), Managers/ (chứa bộ điều khiển dịch vụ), Models/ (chứa thực thể).", "Bước 3:")

    add_heading_2("1.2. Lớp Hằng Số Hệ Thống GameConfig.cs")
    add_body("Lớp GameConfig (nằm trong thư mục Engine/) là nơi quy định toàn bộ 'luật chơi' và kích thước của thế giới game. Nhờ lớp này, khi muốn cân chỉnh game (ví dụ: làm quái nhanh chết hơn, hoặc tăng năng lượng nhặt được), lập trình viên chỉ cần sửa 1 con số tại đây mà không cần đụng vào code logic.")
    
    add_code_block(
"""public static class GameConfig
{
    // Kích thước sân đấu chuẩn HD và tốc độ khung hình
    public const int WINDOW_WIDTH = 1200;
    public const int WINDOW_HEIGHT = 700;
    public const int FPS = 60;
    public const float TARGET_DELTA_TIME = 1.0f / FPS; // ~0.0167 giây mỗi khung hình

    // Cấu hình Lưới sân cỏ 5 hàng x 9 cột chuẩn
    public const int GRID_ROWS = 5;
    public const int GRID_COLS = 9;
    public const int CELL_WIDTH = 96;
    public const int CELL_HEIGHT = 100;
    public const int GRID_OFFSET_X = 135;
    public const int GRID_OFFSET_Y = 105;

    // Cấu hình Năng lượng Nắng (Energy)
    public const int INITIAL_SUN = 150;     // Nắng khởi đầu
    public const int SUN_VALUE = 50;        // Mỗi bông nắng nhặt được +50 Nắng
    public const float SKY_SUN_INTERVAL = 9.0f; // Bầu trời rơi nắng mỗi 9 giây

    // Máu của các loại Zombie (cân bằng để bắn nhanh chết, đã tay)
    public const int HP_ZOMBIE_NORMAL = 80;       // ~4 phát đạn
    public const int HP_ZOMBIE_CONE_HAT = 120;    // Giáp nón lá
    public const int HP_ZOMBIE_BUCKET_HAT = 240;  // Giáp xô sắt
    public const int HP_ZOMBIE_BOSS = 1800;       // Boss khổng lồ
}"""
    )

    # =========================================================================
    # CHƯƠNG 2
    # =========================================================================
    add_heading_1("CHƯƠNG 2: XÂY DỰNG MÔ HÌNH THỰC THỂ (MODELS)")
    add_body("Hệ thống Models áp dụng nguyên lý Lập trình hướng đối tượng (OOP), kế thừa và đa hình để định nghĩa mọi vật thể xuất hiện trên sân.")

    add_heading_2("2.1. Lớp Trừu Tượng Hoa Quả (Fruit.cs)")
    add_body("Là lớp cha của tất cả các loài cây chiến binh. Quản lý tọa độ lưới, máu, giá nắng và trạng thái sống/chết.")
    add_bullet(" int Row, Col (vị trí trên ô lưới); float X, Y (tọa độ pixel thực tế); int Health, MaxHealth; int Cost (giá nắng); float CooldownTime; bool IsAlive.", "Thuộc tính cơ bản:")
    add_bullet(" Hàm nhận sát thương khi bị Zombie cắn: public virtual void TakeDamage(int damage). Khi Health <= 0, gán IsAlive = false để Engine xóa cây khỏi sân.", "Hàm TakeDamage():")
    add_bullet(" Hàm cập nhật theo thời gian: public virtual void Update(float dt). Mỗi loài cây con sẽ ghi đè (override) hàm này để thực hiện hành động riêng.", "Hàm Update():")

    add_heading_3("Cách hoạt động của các lớp cây con:")
    add_bullet(" Ghi đè hàm Update(dt): Mỗi khi bộ đếm _produceTimer >= 10 giây, hàm sẽ gọi sự kiện OnProduceSun(this) để sinh ra bông nắng ngay cạnh cây.", "1. Chuối Nắng (BananaSun):")
    add_bullet(" Ghi đè hàm Update(dt): Đếm thời gian _shootTimer >= 1.4 giây. Nếu phát hiện có Zombie ở cùng hàng (Row) phía trước mặt, hàm sẽ gọi OnShoot(this) tạo một viên đạn Projectile bay thẳng.", "2. Nho Bắn Súng (GrapeShooter):")
    add_bullet(" Tương tự Nho Bắn Súng nhưng gắn cờ IsSlowEffect = true cho viên đạn để làm chậm 50% tốc độ Zombie trúng đạn.", "3. Dâu Tuyết (IceStrawberry):")
    add_bullet(" Không bắn đạn, nhưng đặt Health = 4000 (gấp hơn 13 lần cây thường) làm bức tường cản bước Zombie.", "4. Dừa Hộ Vệ (CoconutWall):")
    add_bullet(" Sau khi trồng 0.8 giây, phát nổ thiêu rụi toàn bộ Zombie trên cùng hàng với sát thương 1800 HP.", "5. Ớt Lửa (ChiliBomb):")
    add_bullet(" Bắn đạn cầu vồng bay theo quỹ đạo parabol, phát nổ gây 80 sát thương đơn mục tiêu và 26 sát thương diện rộng (AoE).", "6. Dưa Hấu Đại Bác (WatermelonLobber):")

    add_heading_2("2.2. Lớp Trừu Tượng Zombie (Zombie.cs)")
    add_body("Quản lý trạng thái, tốc độ di chuyển và hoạt ảnh tấn công của Zombie.")
    add_bullet(" Row (hàng đang đi); float X, Y; int Health, ArmorHealth, MaxHealth; float Speed; int AttackDamage; ZombieState State (Walking, Eating, Dying).", "Thuộc tính cơ bản:")
    add_bullet(" Hàm cập nhật trạng thái di chuyển: Nếu State == Walking thì X -= Speed * dt (tiến về phía nhà người chơi). Nếu State == Eating thì dừng lại và cắn cây.", "Hàm Update(float dt):")
    add_bullet(" Hàm trừ máu thông minh: Trừ vào ArmorHealth (mũ nón/xô/khiên) trước, khi giáp vỡ thì trừ tiếp vào Health của thân.", "Hàm TakeDamage(int damage):")

    add_heading_2("2.3. Lớp Đạn (Projectile.cs), Bông Nắng (SunToken.cs), Xe Cắt Cỏ (LawnMower.cs)")
    add_bullet(" Hàm Update(dt): X += Speed * dt (bay sang phải với vận tốc 450 px/s). Có hàm IntersectsWith(Zombie) kiểm tra va chạm hình học.", "Projectile (Đạn):")
    add_bullet(" Rơi từ trên trời xuống điểm đích Y, lơ lửng chờ người chơi bấm chuột vào để cộng +50 Nắng.", "SunToken (Bông Nắng):")
    add_bullet(" Nằm ở đầu hàng (X = 55). Khi có Zombie lọt tới gần, gọi hàm Trigger() để phóng xe với tốc độ 400 px/s, gọi hàm va chạm diệt sạch mọi Zombie trên hàng đó.", "LawnMower (Xe Cắt Cỏ):")

    # =========================================================================
    # CHƯƠNG 3
    # =========================================================================
    add_heading_1("CHƯƠNG 3: ĐỘNG CƠ TRÒ CHƠI & QUẢN LÝ ĐỢT QUÁI (GAME ENGINE)")
    add_body("Động cơ GameEngine.cs là bộ não trung tâm vận hành toàn bộ thế giới trò chơi. Tách rời hoàn toàn khỏi giao diện WinForms để đảm bảo hiệu năng và dễ bảo trì.")

    add_heading_2("3.1. Vòng Lặp Trò Chơi (Game Loop) 60 FPS")
    add_body("Được kích hoạt bởi Timer trong frmGame mỗi 16.6 mili-giây, gọi vào hàm GameEngine.Update(float dt):")

    add_code_block(
"""public void Update(float dt)
{
    if (State != GameState.Playing) return;

    GameTime += dt;

    // 1. Giảm Cooldown của các thẻ bài hạt giống hoa quả
    foreach (var ft in CardCooldowns.Keys.ToList())
    {
        if (CardCooldowns[ft] > 0)
        {
            CardCooldowns[ft] -= dt;
            if (CardCooldowns[ft] < 0) CardCooldowns[ft] = 0;
        }
    }

    // 2. Cập nhật WaveManager (đếm thời gian sinh quái các đợt)
    Waves.Update(dt);

    // 3. Tự động rơi Nắng từ trời mỗi 9 giây
    if (CurrentLevel.SkySunEnabled)
    {
        _skySunTimer += dt;
        if (_skySunTimer >= GameConfig.SKY_SUN_INTERVAL)
        {
            _skySunTimer = 0;
            SpawnSkySun();
        }
    }

    // 4. Cập nhật các cây hoa quả (bắn đạn hoặc đếm nhịp sinh nắng)
    for (int i = Fruits.Count - 1; i >= 0; i--) Fruits[i].Update(dt);

    // 5. Cập nhật Zombie: di chuyển, cắn cây, kiểm tra vạch phòng thủ
    UpdateZombies(dt);

    // 6. Cập nhật Xe cắt cỏ: lao đi tiêu diệt zombie nếu kích hoạt
    UpdateMowers(dt);

    // 7. Cập nhật Đạn & Kiểm tra va chạm Đạn với Zombie
    UpdateProjectilesAndCollisions(dt);

    // 8. Dọn dẹp thực thể chết (RemoveAll IsAlive == false)
    CleanupDeadEntities();

    // 9. Kiểm tra Thắng/Thua
    CheckGameStatus();
}"""
    )

    add_heading_2("3.2. Chi Tiết Các Hàm Xử Lý Nghiệp Vụ Cốt Lõi")

    add_heading_3("1. Hàm Trồng Cây: public bool PlantFruit(FruitType type, int row, int col)")
    add_bullet(" Bước 1: Kiểm tra xem ô (row, col) trên Grid đã có cây chưa. Nếu đã có -> return false.", "Thuật toán:")
    add_bullet(" Bước 2: Kiểm tra người chơi có đủ Nắng không (SunEnergy >= cost) và thẻ bài đã hết hồi chiêu chưa (CardCooldowns[type] == 0).", "")
    add_bullet(" Bước 3: Trừ Nắng: SunEnergy -= cost; Đặt lại hồi chiêu: CardCooldowns[type] = plant.CooldownTime.", "")
    add_bullet(" Bước 4: Khởi tạo thực thể cây con tương ứng, tính tọa độ X, Y = Grid.GetCellPosition(row, col), đăng ký sự kiện bắn đạn OnShoot và sinh nắng OnProduceSun.", "")
    add_bullet(" Bước 5: Thêm cây vào danh sách Fruits.Add(plant), phát âm thanh sfx_plant và return true.", "")

    add_heading_3("2. Hàm Nhổ Cây Bằng Xẻng: public bool RemoveFruit(int row, int col)")
    add_bullet(" Tìm cây đang nằm ở ô (row, col) trong danh sách Fruits. Nếu tìm thấy, gán IsAlive = false và xóa khỏi Grid. Phát âm thanh đào đất.", "Nguyên lý:")

    add_heading_3("3. Hàm Thu Hoạch Nắng: public void CollectSun(SunToken token)")
    add_bullet(" Khi người chơi nhấp chuột vào bông mặt trời, gọi hàm này: SunEnergy += GameConfig.SUN_VALUE (+50 Nắng). Gán token.IsAlive = false và phát âm thanh sfx_sun.", "Nguyên lý:")

    add_heading_3("4. Hàm Kiểm Tra Va Chạm & Diệt Quái:")
    add_bullet(" Duyệt qua từng viên đạn trong Projectiles. Tìm con Zombie đầu tiên cùng hàng (p.Row == z.Row) có hộp va chạm giao nhau (z.IntersectsWith(p)).", "Đạn va chạm Zombie:")
    add_bullet(" Gọi z.TakeDamage(p.Damage). Nếu đạn băng (IsSlowEffect) thì gọi z.ApplySlow(4.0f). Viên đạn phát nổ và gán p.IsAlive = false.", "")
    add_bullet(" Khi Zombie chạm mép trái (X <= GRID_OFFSET_X - 40), kích hoạt Xe Cắt Cỏ hàng đó. Xe phóng đi và diệt mọi quái z.TakeDamage(9999). Nếu xe đã dùng rồi mà Zombie vẫn vượt qua -> gọi TriggerGameOver(false) (Thua cuộc).", "Zombie vượt vạch phòng ngự:")

    add_heading_2("3.3. Quản Lý Đợt Sóng Quái (WaveManager.cs)")
    add_bullet(" Quản lý danh sách các đợt Zombie theo cấu hình từng màn chơi.", "Vai trò:")
    add_bullet(" Hàm Update(dt): Đếm ngược thời gian giữa các đợt. Khi đến thời điểm, gọi StartNextWave().", "Hàm Update(dt):")
    add_bullet(" Hàm SpawnZombie(): Chọn ngẫu nhiên loại Zombie được phép xuất hiện trong màn và chọn ngẫu nhiên hàng xuất hiện từ 0 đến 4 (Row). Thêm Zombie vào danh sách quản lý của GameEngine.", "Hàm SpawnZombie():")
    add_bullet(" Khi đến đợt sóng cuối cùng, kích hoạt sự kiện OnBigWaveAlert(\"ĐỢT TẤN CÔNG CUỐI CÙNG!\") để GamePanel hiển thị thông báo rực lửa.", "Đợt Final Wave:")

    # =========================================================================
    # CHƯƠNG 4
    # =========================================================================
    add_heading_1("CHƯƠNG 4: LẬP TRÌNH BẢNG VẼ ĐỒ HỌA THỜI GIAN THỰC (GAMEPANEL)")
    add_body("GamePanel.cs là UserControl trực tiếp hiển thị toàn bộ hình ảnh sân đấu lên màn hình với tốc độ 60 khung hình/giây bằng công nghệ GDI+.")

    add_heading_2("4.1. Khử Nháy Tuyệt Đối Với DoubleBuffering")
    add_body("Trong hàm khởi tạo GamePanel(), bắt buộc kích hoạt thuộc tính DoubleBuffered:")
    add_code_block(
"""public GamePanel()
{
    DoubleBuffered = true;
    SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    UpdateStyles();
}"""
    )
    add_body("Kỹ thuật này vẽ toàn bộ hình ảnh vào một bộ nhớ đệm (Back Buffer) trước rồi mới đẩy lên màn hình trong một tích tắc, loại bỏ hoàn toàn hiện tượng chớp nháy hình ảnh.")

    add_heading_2("4.2. Trình Tự Vẽ 9 Lớp (Render Layers) Trong OnPaint")
    add_body("Hàm Render(Graphics g) chia việc vẽ thành 9 lớp tuần tự từ sâu nhất đến nổi nhất:")
    add_bullet(" Vẽ thảm cỏ xanh hoặc bóng đêm huyền ảo.", "Lớp 1 - Background:")
    add_bullet(" Vẽ lưới 5 hàng x 9 cột, ô đất sáng lên khi di chuột tới.", "Lớp 2 - Grid:")
    add_bullet(" Vẽ 5 chiếc máy cắt cỏ màu đỏ bóng bẩy ở đầu mỗi hàng.", "Lớp 3 - LawnMowers:")
    add_bullet(" Vẽ từng cây hoa quả, thanh máu mini nếu cây đang bị cắn.", "Lớp 4 - Plants:")
    add_bullet(" Vẽ Zombie, mũ nón, và vẽ thanh máu đỏ ngắn dần tỉ lệ theo HP còn lại.", "Lớp 5 - Zombies:")
    add_bullet(" Vẽ các hạt nho, đạn băng, quả dưa hấu bay trên không.", "Lớp 6 - Projectiles:")
    add_bullet(" Vẽ các bông mặt trời vàng cam xoay tròn lơ lửng.", "Lớp 7 - SunTokens:")
    add_bullet(" Vẽ khay chọn hạt giống trên cùng, số Nắng hiện có, hiệu ứng mờ dần Cooldown và icon Xẻng đào.", "Lớp 8 - Top Seed Bar:")
    add_bullet(" Vẽ thanh tiến độ sóng quái đáy màn, cắm cờ đỏ 🚩 tại vị trí đợt tấn công lớn (đã loại bỏ icon cảnh báo ⚠️ theo yêu cầu).", "Lớp 9 - Bottom Wave Bar:")

    add_heading_2("4.3. Bắt Tương Tác Chuột (OnMouseDown & OnMouseMove)")
    add_bullet(" Nhấp vào khay thẻ bài -> Gán SelectedCard = FruitType tương ứng.", "Chọn hạt giống:")
    add_bullet(" Nhấp vào ô đất trống trên sân cỏ -> Tính toán hàng và cột: row = (e.Y - OFFSET_Y) / CELL_H; col = (e.X - OFFSET_X) / CELL_W -> Gọi engine.PlantFruit(SelectedCard, row, col).", "Gieo trồng:")
    add_bullet(" Nhấp vào bông nắng -> Gọi engine.CollectSun(token).", "Nhặt nắng:")
    add_bullet(" Nhấp vào biểu tượng xẻng -> Gán IsShovelSelected = true -> Nhấp vào cây cần nhổ -> Gọi engine.RemoveFruit(row, col).", "Dùng xẻng:")

    # =========================================================================
    # CHƯƠNG 5
    # =========================================================================
    add_heading_1("CHƯƠNG 5: LẬP TRÌNH CÁC BỘ QUẢN LÝ DỊCH VỤ (MANAGERS)")

    add_heading_2("5.1. Bộ Nạp Ảnh Không Khóa File (ImageLoader.cs)")
    add_body("Để người chơi có thể thay thế hình ảnh tùy thích bằng file PNG bên ngoài (ví dụ đổi quả chuối thành hoa hướng dương) mà không bị lỗi 'File is being used by another process', ImageLoader sử dụng giải thuật nạp qua MemoryStream:")

    add_code_block(
"""public static Image GetImage(string imageName, int width = 80, int height = 80)
{
    string key = $"{imageName}_{width}_{height}";
    if (_cache.TryGetValue(key, out var cachedImg)) return cachedImg;

    string fileName = imageName.EndsWith(".png") ? imageName : imageName + ".png";
    string binPath = Path.Combine(GameConfig.IMAGES_DIR, fileName);
    string srcPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\..\\..\\Assets\\Images", fileName));

    // Kiểm tra đồng thời cả 2 nơi, ưu tiên file có thời gian chỉnh sửa mới hơn
    string? filePath = null;
    if (File.Exists(srcPath) && File.Exists(binPath))
        filePath = File.GetLastWriteTimeUtc(srcPath) >= File.GetLastWriteTimeUtc(binPath) ? srcPath : binPath;
    else if (File.Exists(srcPath)) filePath = srcPath;
    else if (File.Exists(binPath)) filePath = binPath;

    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
    {
        // Đọc qua byte[] và MemoryStream để Windows KHÔNG KHÓA FILE
        byte[] bytes = File.ReadAllBytes(filePath);
        using (var ms = new MemoryStream(bytes))
        using (var src = Image.FromStream(ms))
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, 0, 0, width, height);
            }
            _cache[key] = bmp;
            return bmp;
        }
    }
    // Nếu chưa có file PNG, tự động vẽ ảnh vector chất lượng cao dự phòng
    Image fallback = GenerateFallbackImage(imageName, width, height);
    _cache[key] = fallback;
    return fallback;
}"""
    )

    add_heading_2("5.2. Quản Lý Tiến Trình Người Chơi Đa Hồ Sơ (ProfileManager.cs)")
    add_bullet(" Sử dụng Newtonsoft.Json / System.Text.Json để lưu trữ danh sách Profile vào file Data/profiles.json.", "Cơ chế JSON:")
    add_bullet(" Hàm CreateNewProfile(name): Tạo hồ sơ mới, gán UnlockedLevel = 1, TotalScore = 0, lưu file và đặt làm CurrentPlayer.", "Tạo người chơi:")
    add_bullet(" Hàm UpdateProgress(level, stars, score): Khi thắng màn, nếu level == UnlockedLevel và level < 5 thì tăng UnlockedLevel++, cập nhật sao ⭐ và điểm tích lũy, tự động lưu file.", "Cập nhật tiến trình:")

    add_heading_2("5.3. Bảng Vàng Vinh Danh Kỷ Lục (ScoreManager.cs)")
    add_bullet(" Đọc ghi file Data/scores.csv. Mỗi dòng gồm: Thứ hạng, Tên người chơi, Màn đạt được, Điểm số, Số sao, Thời gian sống sót, Ngày giờ.", "Cơ chế CSV:")
    add_bullet(" Hàm GetTopScores(10): Đọc toàn bộ danh sách, sắp xếp giảm dần theo Score (OrderByDescending) và lấy ra 10 bản ghi điểm cao nhất.", "Lọc Top 10:")

    add_heading_2("5.4. Âm Thanh Tập Trung (SoundManager.cs)")
    add_bullet(" Dùng System.Media.SoundPlayer để phát các hiệu ứng âm thanh SFX và nhạc nền WAV.", "Cơ chế:")
    add_bullet(" Hàm PlaySound(name): Kiểm tra cờ SettingsManager.Instance.SoundEnabled, nếu true thì phát âm thanh.", "PlaySound():")
    add_bullet(" Hàm PlayMusic(name): Kiểm tra cờ MusicEnabled, nếu true thì phát lặp vô tận.", "PlayMusic():")

    # =========================================================================
    # CHƯƠNG 6
    # =========================================================================
    add_heading_1("CHƯƠNG 6: THIẾT KẾ GIAO DIỆN FORMS & LUỒNG ĐIỀU HƯỚNG")
    add_body("Game bao gồm 12 Form giao diện kết nối nhịp nhàng qua luồng sự kiện FormClosed:")

    add_heading_2("6.1. Luồng Chuyển Giao Diện Mẫu (Menu -> Trận Đấu -> Kết Quả)")
    add_code_block(
"""// Khi người chơi bấm nút CHƠI MỚI trên frmMenu:
private void btnPlay_Click(object sender, EventArgs e)
{
    using (var newPlayerForm = new frmNewPlayer())
    {
        if (newPlayerForm.ShowDialog(this) == DialogResult.OK)
        {
            ProfileManager.Instance.CreateNewProfile(newPlayerForm.PlayerName);
            var gameForm = new frmGame(1); // Bắt đầu Màn 1
            gameForm.FormClosed += (s, ev) =>
            {
                UpdatePlayerInfoLabel(); // Cập nhật lại dải thông tin người chơi
                Show();                  // Hiển thị lại Menu
            };
            gameForm.Show();
            Hide(); // Ẩn Menu đi trong lúc chơi
        }
    }
}"""
    )

    add_heading_2("6.2. Danh Sách Các Form Trong Dự Án")
    add_bullet(" frmSplash: Màn hình chờ, nạp tài nguyên, thanh tải Guna2, dàn trận Hoa Quả vs Zombie kéo thả bằng chuột.", "1. frmSplash:")
    add_bullet(" frmMenu: Menu chính điều hướng 7 nút bấm đồng bộ màu xanh lá.", "2. frmMenu:")
    add_bullet(" frmNewPlayer: Hộp thoại tạo nhân vật mới, bắt phím Enter.", "3. frmNewPlayer:")
    add_bullet(" frmProgress: Bảng DataGridView quản lý danh sách hồ sơ người chơi, chọn chơi tiếp phần của mình.", "4. frmProgress:")
    add_bullet(" frmLevelSelect: Bản đồ 5 màn chiến dịch, hiển thị sao ⭐ và trạng thái khóa 🔒.", "5. frmLevelSelect:")
    add_bullet(" frmGame: Cửa sổ trận đấu chính chứa GamePanel.", "6. frmGame:")
    add_bullet(" frmPause: Hộp thoại tạm dừng trận đấu (Tiếp tục, Chơi lại, Về menu).", "7. frmPause:")
    add_bullet(" frmResult: Bảng thông báo Chiến Thắng / Thất Bại kèm tính sao ⭐.", "8. frmResult:")
    add_bullet(" frmScoreboard: Bảng vinh danh Top 10 huy chương Vàng, Bạc, Đồng.", "9. frmScoreboard:")
    add_bullet(" frmHelp: Cẩm nang 4 tab thông tin cách chơi, hoa quả, zombie, đồ án.", "10. frmHelp:")
    add_bullet(" frmSettings: Công tắc gạt bật tắt hiệu ứng âm thanh và nhạc nền.", "11. frmSettings:")
    add_bullet(" frmConfirmExit: Hộp thoại xác nhận thoát game an toàn.", "12. frmConfirmExit:")

    # =========================================================================
    # CHƯƠNG 7
    # =========================================================================
    add_heading_1("CHƯƠNG 7: KÉO THẢ TRỰC QUAN TRÊN VISUAL STUDIO DESIGNER")
    add_body("Toàn bộ các Form trong dự án đều được thiết kế chuẩn Drag-and-Drop Designer. Bạn có thể mở trực tiếp trong Visual Studio để tùy biến:")
    add_bullet(" Mở Visual Studio -> Solution Explorer -> Nhấp đúp vào file Form cần sửa (ví dụ: frmSplash.cs hoặc frmMenu.cs).", "Bước 1: Mở Design:")
    add_bullet(" Nhấp chuột vào bất kỳ con Hoa Quả, Zombie hoặc Nút bấm nào -> Dùng chuột kéo di chuyển đến vị trí mới tùy thích.", "Bước 2: Kéo thả vị trí:")
    add_bullet(" Nhấp vào 4 nút vuông ở góc để kéo to/nhỏ kích thước (hình ảnh có thuộc tính SizeMode = Zoom nên luôn giữ nguyên tỷ lệ sắc nét).", "Bước 3: Chỉnh kích thước:")
    add_bullet(" Bấm phím F4 để mở bảng Properties -> chỉnh tọa độ Location (X, Y) và kích thước Size (Width, Height) chính xác từng pixel.", "Bước 4: Bảng Properties:")
    add_bullet(" Toàn bộ các hàm sự kiện Click dự phòng (_Click stubs) đã được khai báo sẵn trong code-behind, đảm bảo dù bạn có vô tình nhấp đúp vào bất kỳ control nào trong màn hình Design cũng không bao giờ bị lỗi Designer CS0103.", "Lưu ý chống lỗi:")

    # Lưu file Word
    out_path = r"G:\hoa qua\HoaQuaNoiGian\Huong_Dan_Lap_Trinh_Game_Hoa_Qua_Noi_Gian_Tu_A_Den_Z.docx"
    doc.save(out_path)
    print(f"SUCCESS: Saved dev guide to {out_path}")

if __name__ == "__main__":
    create_dev_guide_doc()
