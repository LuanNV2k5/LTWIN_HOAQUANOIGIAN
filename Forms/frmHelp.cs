using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Form Hướng dẫn & Kho dữ liệu game (frmHelp): Giao diện GUNA2 hiện đại, tối ưu màu sắc hiển thị.
    /// </summary>
    public partial class frmHelp : Form
    {
        private int _currentTab = 0;

        public frmHelp()
        {
            InitializeComponent();
            DoubleBuffered = true;
            try { Icon = ImageLoader.GetAppIcon(); } catch { }

            SwitchTab(0);
        }

        private void SwitchTab(int tabIndex)
        {
            _currentTab = tabIndex;

            // Reset toàn bộ style nút
            ResetButtonStyle(btnTabHowToPlay);
            ResetButtonStyle(btnTabPlants);
            ResetButtonStyle(btnTabZombies);
            ResetButtonStyle(btnTabInfo);

            switch (tabIndex)
            {
                case 0:
                    SetActiveButtonStyle(btnTabHowToPlay);
                    LoadHowToPlayContent();
                    break;
                case 1:
                    SetActiveButtonStyle(btnTabPlants);
                    LoadPlantsContent();
                    break;
                case 2:
                    SetActiveButtonStyle(btnTabZombies);
                    LoadZombiesContent();
                    break;
                case 3:
                    SetActiveButtonStyle(btnTabInfo);
                    LoadInfoContent();
                    break;
            }
        }

        private void SetActiveButtonStyle(Guna2GradientButton btn)
        {
            btn.FillColor = Color.FromArgb(255, 179, 0);
            btn.FillColor2 = Color.FromArgb(255, 143, 0);
            btn.ForeColor = Color.Black;
        }

        private void ResetButtonStyle(Guna2GradientButton btn)
        {
            btn.FillColor = Color.FromArgb(38, 50, 56);
            btn.FillColor2 = Color.FromArgb(55, 71, 79);
            btn.ForeColor = Color.White;
        }

        private void LoadHowToPlayContent()
        {
            rtbHelpContent.Clear();
            AppendHeading("MỤC TIÊU TRÒ CHƠI\n", Color.Gold);
            AppendText("Bảo vệ khu vườn của bạn khỏi các đợt zombie đói khát tràn sang từ bên phải màn hình.\n" +
                       "Tuyệt đối không để bất kỳ zombie nào vượt qua vạch phòng thủ (hàng xe cắt cỏ) bên trái sân vườn!\n\n", Color.White);

            AppendHeading("HỆ THỐNG NẮNG (NĂNG LƯỢNG SẢN XUẤT)\n", Color.Gold);
            AppendText(" - Nắng rơi tự nhiên từ trên trời xuống sân chơi (mỗi lần nhặt nhận ngay +50 NẮNG).\n" +
                       " - Trồng Chuối Nắng để tự động sản xuất Nắng định kỳ cho khu vườn.\n" +
                       " - Nhấp chuột trực tiếp vào biểu tượng Nắng rơi trên sân để thu thập tích lũy.\n\n", Color.FromArgb(255, 241, 118));

            AppendHeading("🌱 QUY TẮC TRỒNG CÂY PHÒNG THỦ\n", Color.Gold);
            AppendText(" - Nhấp chuột chọn Thẻ hạt giống trên thanh công cụ phía trên (hoặc dùng phím tắt 1 - 8).\n" +
                       " - Nhấp chuột vào ô cỏ còn trống trên sân để trồng cây xuống.\n" +
                       " - Mỗi cây có thời gian hồi chiêu (Cooldown) và yêu cầu lượng Nắng khác nhau.\n" +
                       " - Dùng Xẻng (nhấp nút Xẻng góc phải) để đào nhổ cây cũ, trồng cây mới thay thế.\n\n", Color.FromArgb(200, 230, 201));

            AppendHeading("🚩 THANH TIẾN TRÌNH & CỘT CỜ ĐỢT TẤN CÔNG\n", Color.Gold);
            AppendText(" - Theo dõi thanh tiến trình ở đáy màn hình để biết số lượng Zombie còn lại.\n" +
                       " - CÂY CỜ ĐỎ (🚩) cắm trên thanh là điểm mốc cảnh báo một đợt Zombie cực lớn sắp ập tới!\n" +
                       " - Hãy nhanh chóng củng cố hàng phòng thủ trước khi vạch tiến trình chạm đến cây cờ!\n", Color.FromArgb(255, 138, 128));
        }

        private void LoadPlantsContent()
        {
            rtbHelpContent.Clear();
            AppendHeading(" HOA QUẢ - DANH SÁCH 8 LOẠI CÂY CHIẾN ĐẤU\n\n", Color.Gold);

            AppendPlantInfo("1. Chuối Nắng (BananaSun)", "50 Nắng", "Sản xuất +50 Nắng định kỳ mỗi 9 giây, cung cấp năng lượng thiết yếu.");
            AppendPlantInfo("2. Nho Bắn Hạt (GrapeShooter)", "100 Nắng", "Bắn hạt nho thẳng liên tục, gây 20 sát thương mỗi phát. Đơn vị tấn công chủ lực.");
            AppendPlantInfo("3. Dâu Tây Băng (IceStrawberry)", "175 Nắng", "Bắn đạn băng gây 20 sát thương và làm chậm 50% tốc độ di chuyển của Zombie trong 5 giây.");
            AppendPlantInfo("4. Dừa Tường (CoconutWall)", "50 Nắng", "Lớp lá chắn kiên cố với 1800 HP máu cực trâu, chặn đứng đường tiến của Zombie.");
            AppendPlantInfo("5. Ớt Bom (ChiliBomb)", "150 Nắng", "Phát nổ dữ dội thiêu rụi toàn bộ zombie trên cùng một hàng (1500 sát thương tức thì).");
            AppendPlantInfo("6. Dưa Hấu Ném (WatermelonLobber)", "300 Nắng", "Ném quả dưa hấu theo quỹ đạo parabol, gây 80 sát thương trực tiếp và 20 sát thương lan rộng.");
            AppendPlantInfo("7. Cà Chua Mìn (TomatoMine)", "25 Nắng", "Bẫy nổ dưới đất, nổ tung 1000 sát thương khi có bất kỳ Zombie nào dẫm phải.");
            AppendPlantInfo("8. Sầu Riêng Gai (DurianThorn)", "125 Nắng", "Lớp gai nhọn sắc bén, vừa chặn đường vừa phản lại sát thương mỗi khi bị Zombie cắn.");
        }

        private void LoadZombiesContent()
        {
            rtbHelpContent.Clear();
            AppendHeading(" ZOMBIE - DANH SÁCH 6 LOẠI KẺ THÙ NGUY HIỂM\n\n", Color.Gold);

            AppendZombieInfo("1. Zombie Thường (Normal Zombie)", "80 HP - Tốc độ: Chậm", "Zombie cơ bản, di chuyển chậm rãi sang trái và gặm nhấm cây cối.");
            AppendZombieInfo("2. Zombie Chạy Nhanh (Runner Zombie)", "70 HP - Tốc độ: Rất nhanh", "Chạy nước rút cực nhanh, dễ dàng đột phá nếu bạn không có Dâu Tây Băng cản bước.");
            AppendZombieInfo("3. Zombie Nón Lá (Cone Zombie)", "200 HP (Nón 120 + Thân 80)", "Đội nón lá Việt Nam che chở, cần bắn vỡ nón trước khi tiêu diệt được thân.");
            AppendZombieInfo("4. Zombie Khiên Gỗ (Shield Zombie)", "220 HP (Khiên 140 + Thân 80)", "Cầm cánh cửa khiên chắn đạn thẳng trước ngực, cần Dưa Hấu Ném hoặc Ớt Bom trị.");
            AppendZombieInfo("5. Zombie Xô Sắt (Bucket Zombie)", "320 HP (Xô 240 + Thân 80)", "Đội xô sắt dày dặn, chịu được lượng sát thương rất lớn.");
            AppendZombieInfo("6. Boss Zombie Khổng Lồ (Boss Zombie)", "1800 HP - Sát thương cắn: 120/s", "Trùm cuối Màn 5 với thân hình khổng lồ, khi mất 50% HP sẽ triệu hồi thêm zombie con!");
        }

        private void LoadInfoContent()
        {
            rtbHelpContent.Clear();
            AppendHeading("HỆ THỐNG PHÍM TẮT \n", Color.Gold);
            AppendText(" - Phím số 1 - 8: Chọn nhanh các loại hạt giống theo thứ tự trên thanh công cụ.\n" +
                       " - Phím P hoặc Esc: Tạm dừng trận đấu / Tiếp tục chơi.\n" +
                       " - Phím M: Bật / Tắt toàn bộ âm thanh và nhạc nền nhanh chóng.\n" +
                       " - Chuột phải: Hủy chọn thẻ hạt giống hoặc hủy bỏ trạng thái dùng Xẻng.\n\n", Color.FromArgb(220, 237, 200));

            AppendHeading("⭐ CƠ CHẾ ĐÁNH GIÁ SAO (STAR RATING)\n", Color.Gold);
            AppendText(" - 3 Sao: Hoàn thành màn chơi mà không để mất bất kỳ chiếc Xe Cắt Cỏ nào.\n" +
                       " - 2 Sao: Hoàn thành màn chơi khi bị mất từ 1 đến 3 xe cắt cỏ.\n" +
                       " - 1 Sao: Vượt qua màn chơi nhưng mất hết toàn bộ phòng tuyến xe cắt cỏ.\n\n", Color.FromArgb(255, 238, 88));

          
        }

        private void AppendHeading(string text, Color color)
        {
            rtbHelpContent.SelectionColor = color;
            rtbHelpContent.SelectionFont = new Font("Arial", 12.5f, FontStyle.Bold);
            rtbHelpContent.AppendText(text);
        }

        private void AppendText(string text, Color color)
        {
            rtbHelpContent.SelectionColor = color;
            rtbHelpContent.SelectionFont = new Font("Arial", 11f, FontStyle.Regular);
            rtbHelpContent.AppendText(text);
        }

        private void AppendPlantInfo(string name, string cost, string desc)
        {
            rtbHelpContent.SelectionColor = Color.Gold;
            rtbHelpContent.SelectionFont = new Font("Arial", 11.5f, FontStyle.Bold);
            rtbHelpContent.AppendText($"{name} ");

            rtbHelpContent.SelectionColor = Color.FromArgb(255, 213, 79);
            rtbHelpContent.SelectionFont = new Font("Arial", 10.5f, FontStyle.Italic);
            rtbHelpContent.AppendText($"[{cost}]\n");

            rtbHelpContent.SelectionColor = Color.FromArgb(220, 237, 200);
            rtbHelpContent.SelectionFont = new Font("Arial", 11f, FontStyle.Regular);
            rtbHelpContent.AppendText($"  • {desc}\n\n");
        }

        private void AppendZombieInfo(string name, string stats, string desc)
        {
            rtbHelpContent.SelectionColor = Color.FromArgb(255, 138, 128);
            rtbHelpContent.SelectionFont = new Font("Arial", 11.5f, FontStyle.Bold);
            rtbHelpContent.AppendText($"{name} ");

            rtbHelpContent.SelectionColor = Color.FromArgb(255, 205, 210);
            rtbHelpContent.SelectionFont = new Font("Arial", 10.5f, FontStyle.Italic);
            rtbHelpContent.AppendText($"[{stats}]\n");

            rtbHelpContent.SelectionColor = Color.FromArgb(245, 245, 245);
            rtbHelpContent.SelectionFont = new Font("Arial", 11f, FontStyle.Regular);
            rtbHelpContent.AppendText($"  • {desc}\n\n");
        }

        private void btnTabHowToPlay_Click(object sender, EventArgs e) => SwitchTab(0);
        private void btnTabPlants_Click(object sender, EventArgs e) => SwitchTab(1);
        private void btnTabZombies_Click(object sender, EventArgs e) => SwitchTab(2);
        private void btnTabInfo_Click(object sender, EventArgs e) => SwitchTab(3);

        private void btnClose_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var bgBrush = new LinearGradientBrush(ClientRectangle, Color.FromArgb(24, 60, 32), Color.FromArgb(14, 38, 20), 90f))
            {
                e.Graphics.FillRectangle(bgBrush, ClientRectangle);
            }
        }
    }
}
