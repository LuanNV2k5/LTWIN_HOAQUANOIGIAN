namespace HoaQuaNoiGian.Forms
{
    partial class frmResult
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Label();
            lblLevelName = new Label();
            lblStars = new Label();
            lblStats = new Label();
            btnPlayNext = new Guna.UI2.WinForms.Guna2GradientButton();
            btnRetry = new Guna.UI2.WinForms.Guna2GradientButton();
            btnMenu = new Guna.UI2.WinForms.Guna2GradientButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Arial", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Gold;
            lblTitle.Location = new Point(14, 25);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 63);
            lblTitle.TabIndex = 0;
            lblTitle.Text = " CHIẾN THẮNG! ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLevelName
            // 
            lblLevelName.Anchor = AnchorStyles.Top;
            lblLevelName.BackColor = Color.Transparent;
            lblLevelName.Font = new Font("Arial", 12F, FontStyle.Bold);
            lblLevelName.ForeColor = Color.FromArgb(255, 236, 179);
            lblLevelName.Location = new Point(29, 97);
            lblLevelName.Margin = new Padding(4, 0, 4, 0);
            lblLevelName.Name = "lblLevelName";
            lblLevelName.Size = new Size(571, 37);
            lblLevelName.TabIndex = 1;
            lblLevelName.Text = "Màn 1: Vườn Sau Nhà";
            lblLevelName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStars
            // 
            lblStars.Anchor = AnchorStyles.Top;
            lblStars.BackColor = Color.Transparent;
            lblStars.Font = new Font("Arial", 18F, FontStyle.Bold);
            lblStars.ForeColor = Color.Orange;
            lblStars.Location = new Point(29, 142);
            lblStars.Margin = new Padding(4, 0, 4, 0);
            lblStars.Name = "lblStars";
            lblStars.Size = new Size(571, 50);
            lblStars.TabIndex = 2;
            lblStars.Text = "⭐ ⭐ ⭐";
            lblStars.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStats
            // 
            lblStats.Anchor = AnchorStyles.Top;
            lblStats.BackColor = Color.Transparent;
            lblStats.Font = new Font("Arial", 10.5F);
            lblStats.ForeColor = Color.FromArgb(220, 237, 200);
            lblStats.Location = new Point(29, 203);
            lblStats.Margin = new Padding(4, 0, 4, 0);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(571, 80);
            lblStats.TabIndex = 3;
            lblStats.Text = "Điểm số: 0 | Zombie đã diệt: 0 | Thời gian: 0s";
            lblStats.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPlayNext
            // 
            btnPlayNext.BackColor = Color.Transparent;
            btnPlayNext.BorderRadius = 12;
            btnPlayNext.Cursor = Cursors.Hand;
            btnPlayNext.CustomizableEdges = customizableEdges7;
            btnPlayNext.FillColor = Color.FromArgb(46, 125, 50);
            btnPlayNext.FillColor2 = Color.FromArgb(76, 175, 80);
            btnPlayNext.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnPlayNext.ForeColor = Color.White;
            btnPlayNext.HoverState.FillColor = Color.FromArgb(255, 179, 0);
            btnPlayNext.HoverState.FillColor2 = Color.FromArgb(255, 143, 0);
            btnPlayNext.HoverState.ForeColor = Color.Black;
            btnPlayNext.Location = new Point(100, 308);
            btnPlayNext.Margin = new Padding(4, 5, 4, 5);
            btnPlayNext.Name = "btnPlayNext";
            btnPlayNext.ShadowDecoration.BorderRadius = 12;
            btnPlayNext.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnPlayNext.ShadowDecoration.Enabled = true;
            btnPlayNext.Size = new Size(429, 77);
            btnPlayNext.TabIndex = 4;
            btnPlayNext.Text = "CHƠI TIẾP ➔";
            btnPlayNext.Click += btnPlayNext_Click;
            // 
            // btnRetry
            // 
            btnRetry.BackColor = Color.Transparent;
            btnRetry.BorderRadius = 12;
            btnRetry.Cursor = Cursors.Hand;
            btnRetry.CustomizableEdges = customizableEdges9;
            btnRetry.FillColor = Color.FromArgb(255, 143, 0);
            btnRetry.FillColor2 = Color.FromArgb(255, 179, 0);
            btnRetry.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnRetry.ForeColor = Color.Black;
            btnRetry.HoverState.FillColor = Color.FromArgb(255, 193, 7);
            btnRetry.HoverState.FillColor2 = Color.FromArgb(255, 160, 0);
            btnRetry.Location = new Point(100, 403);
            btnRetry.Margin = new Padding(4, 5, 4, 5);
            btnRetry.Name = "btnRetry";
            btnRetry.ShadowDecoration.BorderRadius = 12;
            btnRetry.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnRetry.ShadowDecoration.Enabled = true;
            btnRetry.Size = new Size(429, 77);
            btnRetry.TabIndex = 5;
            btnRetry.Text = "CHƠI LẠI";
            btnRetry.Click += btnRetry_Click;
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.Transparent;
            btnMenu.BorderRadius = 12;
            btnMenu.Cursor = Cursors.Hand;
            btnMenu.CustomizableEdges = customizableEdges11;
            btnMenu.FillColor = Color.FromArgb(55, 71, 79);
            btnMenu.FillColor2 = Color.FromArgb(69, 90, 100);
            btnMenu.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnMenu.ForeColor = Color.White;
            btnMenu.HoverState.FillColor = Color.FromArgb(90, 110, 120);
            btnMenu.Location = new Point(100, 498);
            btnMenu.Margin = new Padding(4, 5, 4, 5);
            btnMenu.Name = "btnMenu";
            btnMenu.ShadowDecoration.BorderRadius = 12;
            btnMenu.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnMenu.ShadowDecoration.Enabled = true;
            btnMenu.Size = new Size(429, 77);
            btnMenu.TabIndex = 6;
            btnMenu.Text = "TRỞ VỀ MENU";
            btnMenu.Click += btnMenu_Click;
            // 
            // frmResult
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 50, 32);
            ClientSize = new Size(629, 625);
            Controls.Add(btnMenu);
            Controls.Add(btnRetry);
            Controls.Add(btnPlayNext);
            Controls.Add(lblStats);
            Controls.Add(lblStars);
            Controls.Add(lblLevelName);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmResult";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kết Quả Màn Chơi";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLevelName;
        private System.Windows.Forms.Label lblStars;
        private System.Windows.Forms.Label lblStats;
        private Guna.UI2.WinForms.Guna2GradientButton btnPlayNext;
        private Guna.UI2.WinForms.Guna2GradientButton btnRetry;
        private Guna.UI2.WinForms.Guna2GradientButton btnMenu;
    }
}
