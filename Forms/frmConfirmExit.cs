using System;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Hộp thoại xác nhận thoát game đẹp mắt, phong cách GUNA2 hiện đại.
    /// </summary>
    public partial class frmConfirmExit : Form
    {
        public frmConfirmExit()
        {
            InitializeComponent();
            try { Icon = ImageLoader.GetAppIcon(); } catch { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
