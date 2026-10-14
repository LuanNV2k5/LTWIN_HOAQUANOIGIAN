using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Hộp thoại Tạm dừng trò chơi (frmPause): Tiếp tục, Chơi lại, Về menu, Bật/tắt âm thanh (FR-08).
    /// </summary>
    public partial class frmPause : Form
    {
        public frmPause()
        {
            InitializeComponent();
            btnMute.Text = SettingsManager.Instance.SoundEnabled ? "🔊 Âm thanh: BẬT" : "🔇 Âm thanh: TẮT";
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            SoundManager.ToggleSound();
            SoundManager.ToggleMusic();
            btnMute.Text = SettingsManager.Instance.SoundEnabled ? "🔊 Âm thanh: BẬT" : "🔇 Âm thanh: TẮT";
        }
    }
}
