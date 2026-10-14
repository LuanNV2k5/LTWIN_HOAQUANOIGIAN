using System;
using System.Drawing;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Form Cài đặt (frmSettings): Bật/tắt nhạc nền, hiệu ứng âm thanh và chỉnh âm lượng (FR-07, FR-33, FR-34).
    /// Thiết kế chuẩn GUNA2 Windows Forms Designer.
    /// </summary>
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            try { Icon = ImageLoader.GetAppIcon(); } catch { }

            swSound.Checked = SettingsManager.Instance.SoundEnabled;
            swMusic.Checked = SettingsManager.Instance.MusicEnabled;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SettingsManager.Instance.SoundEnabled = swSound.Checked;
            SettingsManager.Instance.MusicEnabled = swMusic.Checked;
            SettingsManager.Instance.SaveSettings();

            if (!swMusic.Checked)
            {
                SoundManager.StopMusic();
            }
            else
            {
                SoundManager.PlayMusic("bgm_menu");
            }

            SoundManager.PlaySound("sfx_click");
            Close();
        }
    }
}
