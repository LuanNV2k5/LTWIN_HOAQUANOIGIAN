using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Engine;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Menu nhỏ kết quả màn chơi: Chơi tiếp, Chơi lại, Trở về menu.
    /// Thiết kế chuẩn GUNA2 Windows Forms Designer.
    /// </summary>
    public partial class frmResult : Form
    {
        private int _levelNum;
        private bool _isWin;
        private int _score;
        private int _stars;
        private int _zombiesKilled;
        private int _timeSec;

        public frmResult(int levelNum, bool isWin, int score, int stars, int zombiesKilled, int timeSec)
        {
            InitializeComponent();

            _levelNum = levelNum;
            _isWin = isWin;
            _score = score;
            _stars = stars;
            _zombiesKilled = zombiesKilled;
            _timeSec = timeSec;

            DisplayResultDetails();
        }

        private void DisplayResultDetails()
        {
            var levelConfig = Level.GetLevelConfig(_levelNum);
            lblLevelName.Text = levelConfig.Name;

            if (_isWin)
            {
                Text = "Chiến Thắng Màn Chơi!";
                lblTitle.ForeColor = Color.Gold;

                int starCount = Math.Max(1, _stars);
                string starDisplay = "";
                for (int i = 0; i < starCount; i++)
                {
                    starDisplay += (i == 0 ? "⭐" : " ⭐");
                }
                lblStars.Text = starDisplay;
                lblStars.ForeColor = Color.Orange;

                lblStats.Text = $"Điểm số: {_score:N0} đ   |   Diệt: {_zombiesKilled} Zombie   |   Thời gian: {_timeSec}s";

                if (_levelNum < 5)
                {
                    lblTitle.Font = new Font("Arial", 18F, FontStyle.Bold);
                    lblTitle.Text = "🎉 CHIẾN THẮNG! 🎉";
                    btnPlayNext.Text = "CHƠI TIẾP (MÀN TIẾP THEO) ➔";
                    btnPlayNext.Visible = true;
                }
                else
                {
                    // Đã thắng Màn 5 (Phá đảo toàn bộ game)
                    btnPlayNext.Visible = false;
                    lblTitle.Font = new Font("Arial", 15.5F, FontStyle.Bold);
                    lblTitle.Text = "🏆 PHÁ ĐẢO TOÀN BỘ GAME! 🏆";
                }
            }
            else
            {
                Text = "Thua Cuộc!";
                lblTitle.Font = new Font("Arial", 18F, FontStyle.Bold);
                lblTitle.Text = "💀 THUA CUỘC! 💀";
                lblTitle.ForeColor = Color.Crimson;

                lblStars.Text = " Zombie đã xâm chiếm sân vườn";
                lblStars.ForeColor = Color.FromArgb(239, 83, 80);

                lblStats.Text = $"Điểm số: {_score:N0} đ   |   Thời gian sống sót: {_timeSec}s";

                btnPlayNext.Visible = false;
            }
        }

        private void btnPlayNext_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.OK; // Bấm chữ chơi tiếp thì màn tiếp theo mới hiện lên
            Close();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.Retry; // Chơi lại màn hiện tại
            Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.Abort; // Trở về Menu chính
            Close();
        }
    }
}
