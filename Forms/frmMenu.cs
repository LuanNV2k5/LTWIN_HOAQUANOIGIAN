using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Form Menu chính với các nút: CHƠI MỚI, TIẾN TRÌNH, CHỌN MÀN CHƠI, BẢNG THÀNH TÍCH...
    /// Cho phép nhập tên người chơi mới, theo dõi tiến trình và chơi tiếp.
    /// </summary>
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            DoubleBuffered = true;

            try
            {
                Icon = ImageLoader.GetAppIcon();
                picLogo.Image = ImageLoader.GetImage("sun", 60, 60);
                picLogoRight.Image = ImageLoader.GetImage("fruit_banana", 60, 60);
            }
            catch { }

            UpdatePlayerInfoLabel();
            SoundManager.PlayMusic("bgm_menu");
        }

        private void UpdatePlayerInfoLabel()
        {
            var cur = ProfileManager.Instance.CurrentPlayer;
            if (cur != null)
            {
                lblPlayerInfo.Text = $" Người chơi: {cur.Name}  |  Màn chơi hiện tại: {cur.GetLevelTitle()}  |  Điểm: {cur.TotalScore:N0}  |  ⭐: {cur.TotalStars}";
            }
            else
            {
                lblPlayerInfo.Text = " Chưa có người chơi. Bấm 'CHƠI MỚI' để tạo nhân vật và bắt đầu!";
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");

            // Mở hộp thoại nhập tên người chơi mới
            using (var newPlayerForm = new frmNewPlayer())
            {
                if (newPlayerForm.ShowDialog(this) == DialogResult.OK)
                {
                    var newProfile = ProfileManager.Instance.CreateNewProfile(newPlayerForm.PlayerName);
                    UpdatePlayerInfoLabel();

                    // Bắt đầu ngay từ Màn 1 cho người chơi mới
                    var gameForm = new frmGame(1);
                    gameForm.FormClosed += (s, ev) =>
                    {
                        UpdatePlayerInfoLabel();
                        Show();
                    };
                    gameForm.Show();
                    Hide();
                }
            }
        }

        private void btnProgress_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            var progressForm = new frmProgress();
            progressForm.FormClosed += (s, ev) =>
            {
                UpdatePlayerInfoLabel();
                Show();
            };
            progressForm.Show();
            Hide();
        }

        private void btnLevelSelect_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");

            // Nếu chưa có profile nào, gợi ý tạo người chơi mới trước
            if (ProfileManager.Instance.CurrentPlayer == null && ProfileManager.Instance.Profiles.Count == 0)
            {
                btnPlay_Click(sender, e);
                return;
            }

            var levelForm = new frmLevelSelect();
            levelForm.FormClosed += (s, ev) =>
            {
                UpdatePlayerInfoLabel();
                Show();
            };
            levelForm.Show();
            Hide();
        }

        private void btnScoreboard_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            var scoreForm = new frmScoreboard();
            scoreForm.FormClosed += (s, ev) => Show();
            scoreForm.Show();
            Hide();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            var helpForm = new frmHelp();
            helpForm.FormClosed += (s, ev) => Show();
            helpForm.Show();
            Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            var settingsForm = new frmSettings();
            settingsForm.FormClosed += (s, ev) => Show();
            settingsForm.Show();
            Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            using (var confirmDialog = new frmConfirmExit())
            {
                if (confirmDialog.ShowDialog(this) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SoundManager.StopMusic();
        }
    }
}
