using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HoaQuaNoiGian.Managers;

namespace HoaQuaNoiGian.Forms
{
    /// <summary>
    /// Form Bảng thành tích (frmScoreboard): Hiển thị Top 10 điểm cao 
    /// </summary>
    public partial class frmScoreboard : Form
    {
        public frmScoreboard()
        {
            InitializeComponent();
            DoubleBuffered = true;
            try { Icon = ImageLoader.GetAppIcon(); } catch { }

            lblFilter.Visible = false;
            cboLevelFilter.Visible = false;

            LoadData();
        }

        private void InitializeFilterItems()
        {

        }

        private void LoadData()
        {
            dgvScores.Rows.Clear();
            var topList = ScoreManager.Instance.GetTopScores(10);

            if (topList.Count == 0)
            {
                lblEmpty.Visible = true;
                dgvScores.Visible = false;
                return;
            }

            lblEmpty.Visible = false;
            dgvScores.Visible = true;

            int rank = 1;
            foreach (var rec in topList)
            {
                int rowIndex = dgvScores.Rows.Add(
                    rank,
                    rec.PlayerName,
                    $"Màn {rec.LevelIndex}",
                    rec.Score,
                    new string('⭐', rec.Stars),
                    $"{rec.TimeSeconds}s",
                    rec.Date
                );

                var row = dgvScores.Rows[rowIndex];

                // Tô nổi bật Top 1, Top 2, Top 3 (Vàng, Bạc, Đồng) (FR-26)
                if (rank == 1) row.DefaultCellStyle.BackColor = Color.Gold;
                else if (rank == 2) row.DefaultCellStyle.BackColor = Color.Silver;
                else if (rank == 3) row.DefaultCellStyle.BackColor = Color.Goldenrod;

                rank++;
            }
        }

        private void cboLevelFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            var res = MessageBox.Show("Bạn có chắc chắn muốn xóa toàn bộ bảng thành tích?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                ScoreManager.Instance.ClearScores();
                LoadData();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            SoundManager.PlaySound("sfx_click");
            Close();
        }

        // Hàm xử lý sự kiện click trên DataGridView để tránh lỗi designer khi double-click
        private void dgvScores_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvScores_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (LinearGradientBrush bg = new LinearGradientBrush(ClientRectangle, Color.ForestGreen, Color.DarkGreen, 90f))
            {
                e.Graphics.FillRectangle(bg, ClientRectangle);
            }
        }
    }
}
