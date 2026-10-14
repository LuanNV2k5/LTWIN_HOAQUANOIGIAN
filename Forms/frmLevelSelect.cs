using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Form Chọn màn chơi (frmLevelSelect): Hiển thị 5 màn chơi.
    /// </summary>
    public partial class frmLevelSelect : Form
    {
        public bool ReturnedToMainMenu { get; private set; } = false;

        public frmLevelSelect()
        {
            InitializeComponent();
            DoubleBuffered = true;
            try { Icon = ImageLoader.GetAppIcon(); } catch { }

            ApplyUnlockedState();
        }

        private void ApplyUnlockedState()
        {
            var curPlayer = ProfileManager.Instance.CurrentPlayer;
            int unlockedLvl = curPlayer?.UnlockedLevel ?? SettingsManager.Instance.UnlockedLevel;

            ConfigureLevelButton(btnLevel1, 1, unlockedLvl, "Màn 1\n\nVườn Sau Nhà");
            ConfigureLevelButton(btnLevel2, 2, unlockedLvl, "Màn 2\n\nVườn Trước Nhà");
            ConfigureLevelButton(btnLevel3, 3, unlockedLvl, "Màn 3\n\nVườn Cây Ăn Trái");
            ConfigureLevelButton(btnLevel4, 4, unlockedLvl, "Màn 4\n\nKhu Vườn Đêm");
            ConfigureLevelButton(btnLevel5, 5, unlockedLvl, "Màn 5\n\nTrận Cuối Cùng");
        }

        private void ConfigureLevelButton(Guna2GradientButton btn, int level, int unlockedLvl, string title)
        {
            bool isUnlocked = level <= unlockedLvl;
            btn.Enabled = isUnlocked;

            if (isUnlocked)
            {
                int stars = ProfileManager.Instance.CurrentPlayer?.GetStars(level) ?? 0;
                string starDisplay = "Chưa hoàn thành";
                if (stars > 0)
                {
                    string s = "";
                    for (int i = 0; i < stars; i++)
                    {
                        s += (i == 0 ? "⭐" : " ⭐");
                    }
                    starDisplay = s;
                }
                btn.Text = $"{title}\n\n{starDisplay}";
                btn.FillColor = Color.FromArgb(255, 236, 179);
                btn.FillColor2 = Color.FromArgb(255, 213, 79);
                btn.ForeColor = Color.FromArgb(27, 94, 32);
                btn.Cursor = Cursors.Hand;
            }
            else
            {
                btn.Text = $"{title}\n\n🔒 ĐÃ KHÓA";
                btn.FillColor = Color.FromArgb(189, 189, 189);
                btn.FillColor2 = Color.FromArgb(158, 158, 158);
                btn.ForeColor = Color.FromArgb(97, 97, 97);
                btn.Cursor = Cursors.No;
            }
        }

        private void StartLevel(int level)
        {
            SoundManager.PlaySound("sfx_click");
            var gameForm = new frmGame(level);
            gameForm.FormClosed += (s, e) =>
            {
                if (gameForm.ReturnedToMainMenu)
                {
                    ReturnedToMainMenu = true;
                    Close(); // Quay thẳng về Menu chính
                }
                else
                {
                    ApplyUnlockedState();
                    Show();
                }
            };
            gameForm.Show();
            Hide();
        }

        private void btnLevel1_Click(object sender, EventArgs e) => StartLevel(1);
        private void btnLevel2_Click(object sender, EventArgs e) => StartLevel(2);
        private void btnLevel3_Click(object sender, EventArgs e) => StartLevel(3);
        private void btnLevel4_Click(object sender, EventArgs e) => StartLevel(4);
        private void btnLevel5_Click(object sender, EventArgs e) => StartLevel(5);

        private void btnBack_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            Close();
        }
    }
}
