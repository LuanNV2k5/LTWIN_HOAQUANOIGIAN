using System;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    public partial class frmNewPlayer : Form
    {
        public string PlayerName { get; private set; } = "";

        public frmNewPlayer()
        {
            InitializeComponent();
            try { Icon = ImageLoader.GetAppIcon(); } catch { }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SubmitName();
        }

        private void txtPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SubmitName();
            }
        }

        private void SubmitName()
        {
            string name = txtPlayerName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên người chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlayerName.Focus();
                return;
            }

            PlayerName = name;
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
