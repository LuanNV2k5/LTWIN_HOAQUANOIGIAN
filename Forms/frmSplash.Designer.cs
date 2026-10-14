namespace HoaQuaNoiGian.Forms
{
    partial class frmSplash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Label();
            lblSubtitle = new Label();
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblProgress = new Label();
            timerSplash = new System.Windows.Forms.Timer(components);
            picGrape = new PictureBox();
            picBanana = new PictureBox();
            picCoconut = new PictureBox();
            picStrawberry = new PictureBox();
            lblVs = new Guna.UI2.WinForms.Guna2Button();
            picConeZombie = new PictureBox();
            picBucketZombie = new PictureBox();
            picBossZombie = new PictureBox();
            picNormalZombie = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picGrape).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBanana).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCoconut).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picStrawberry).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picConeZombie).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBucketZombie).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBossZombie).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNormalZombie).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Arial", 42F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Gold;
            lblTitle.Location = new Point(143, 58);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1154, 113);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HOA QUẢ NỔI GIẬN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Anchor = AnchorStyles.Top;
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Arial", 13.5F, FontStyle.Bold);
            lblSubtitle.ForeColor = Color.FromArgb(255, 236, 179);
            lblSubtitle.Location = new Point(143, 180);
            lblSubtitle.Margin = new Padding(4, 0, 4, 0);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(1154, 50);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "NHÓM U30";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom;
            progressBar.BorderRadius = 12;
            progressBar.CustomizableEdges = customizableEdges1;
            progressBar.FillColor = Color.FromArgb(25, 60, 25);
            progressBar.Location = new Point(363, 867);
            progressBar.Margin = new Padding(4, 5, 4, 5);
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.FromArgb(255, 193, 7);
            progressBar.ProgressColor2 = Color.FromArgb(255, 87, 34);
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressBar.Size = new Size(714, 43);
            progressBar.TabIndex = 2;
            progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblProgress
            // 
            lblProgress.Anchor = AnchorStyles.Bottom;
            lblProgress.BackColor = Color.Transparent;
            lblProgress.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            lblProgress.ForeColor = Color.LightYellow;
            lblProgress.Location = new Point(363, 920);
            lblProgress.Margin = new Padding(4, 0, 4, 0);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(714, 42);
            lblProgress.TabIndex = 3;
            lblProgress.Text = "Đang tải tài nguyên... 0%";
            lblProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timerSplash
            // 
            timerSplash.Interval = 25;
            timerSplash.Tick += timerSplash_Tick;
            // 
            // picGrape
            // 
            picGrape.BackColor = Color.Transparent;
            picGrape.Location = new Point(343, 245);
            picGrape.Margin = new Padding(4, 5, 4, 5);
            picGrape.Name = "picGrape";
            picGrape.Size = new Size(284, 293);
            picGrape.SizeMode = PictureBoxSizeMode.Zoom;
            picGrape.TabIndex = 4;
            picGrape.TabStop = false;
            // 
            // picBanana
            // 
            picBanana.BackColor = Color.Transparent;
            picBanana.Location = new Point(66, 244);
            picBanana.Margin = new Padding(4, 5, 4, 5);
            picBanana.Name = "picBanana";
            picBanana.Size = new Size(253, 294);
            picBanana.SizeMode = PictureBoxSizeMode.Zoom;
            picBanana.TabIndex = 5;
            picBanana.TabStop = false;
            // 
            // picCoconut
            // 
            picCoconut.BackColor = Color.Transparent;
            picCoconut.Location = new Point(343, 559);
            picCoconut.Margin = new Padding(4, 5, 4, 5);
            picCoconut.Name = "picCoconut";
            picCoconut.Size = new Size(267, 280);
            picCoconut.SizeMode = PictureBoxSizeMode.Zoom;
            picCoconut.TabIndex = 6;
            picCoconut.TabStop = false;
            // 
            // picStrawberry
            // 
            picStrawberry.BackColor = Color.Transparent;
            picStrawberry.Location = new Point(66, 559);
            picStrawberry.Margin = new Padding(4, 5, 4, 5);
            picStrawberry.Name = "picStrawberry";
            picStrawberry.Size = new Size(269, 280);
            picStrawberry.SizeMode = PictureBoxSizeMode.Zoom;
            picStrawberry.TabIndex = 7;
            picStrawberry.TabStop = false;
            // 
            // lblVs
            // 
            lblVs.Anchor = AnchorStyles.None;
            lblVs.BackColor = Color.Transparent;
            lblVs.BorderColor = Color.Gold;
            lblVs.BorderRadius = 35;
            lblVs.BorderThickness = 3;
            lblVs.CustomizableEdges = customizableEdges3;
            lblVs.FillColor = Color.FromArgb(230, 74, 25);
            lblVs.Font = new Font("Arial", 22F, FontStyle.Bold);
            lblVs.ForeColor = Color.White;
            lblVs.Location = new Point(670, 475);
            lblVs.Margin = new Padding(4, 5, 4, 5);
            lblVs.Name = "lblVs";
            lblVs.ShadowDecoration.CustomizableEdges = customizableEdges4;
            lblVs.Size = new Size(100, 117);
            lblVs.TabIndex = 8;
            lblVs.Text = "VS";
            // 
            // picConeZombie
            // 
            picConeZombie.BackColor = Color.Transparent;
            picConeZombie.Location = new Point(830, 235);
            picConeZombie.Margin = new Padding(4, 5, 4, 5);
            picConeZombie.Name = "picConeZombie";
            picConeZombie.Size = new Size(279, 303);
            picConeZombie.SizeMode = PictureBoxSizeMode.Zoom;
            picConeZombie.TabIndex = 9;
            picConeZombie.TabStop = false;
            // 
            // picBucketZombie
            // 
            picBucketZombie.BackColor = Color.Transparent;
            picBucketZombie.Location = new Point(1117, 235);
            picBucketZombie.Margin = new Padding(4, 5, 4, 5);
            picBucketZombie.Name = "picBucketZombie";
            picBucketZombie.Size = new Size(281, 303);
            picBucketZombie.SizeMode = PictureBoxSizeMode.Zoom;
            picBucketZombie.TabIndex = 10;
            picBucketZombie.TabStop = false;
            // 
            // picBossZombie
            // 
            picBossZombie.BackColor = Color.Transparent;
            picBossZombie.Location = new Point(830, 548);
            picBossZombie.Margin = new Padding(4, 5, 4, 5);
            picBossZombie.Name = "picBossZombie";
            picBossZombie.Size = new Size(279, 309);
            picBossZombie.SizeMode = PictureBoxSizeMode.Zoom;
            picBossZombie.TabIndex = 11;
            picBossZombie.TabStop = false;
            // 
            // picNormalZombie
            // 
            picNormalZombie.BackColor = Color.Transparent;
            picNormalZombie.Location = new Point(1117, 548);
            picNormalZombie.Margin = new Padding(4, 5, 4, 5);
            picNormalZombie.Name = "picNormalZombie";
            picNormalZombie.Size = new Size(300, 309);
            picNormalZombie.SizeMode = PictureBoxSizeMode.Zoom;
            picNormalZombie.TabIndex = 12;
            picNormalZombie.TabStop = false;
            // 
            // frmSplash
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 100, 34);
            ClientSize = new Size(1440, 1002);
            Controls.Add(picNormalZombie);
            Controls.Add(picBossZombie);
            Controls.Add(picBucketZombie);
            Controls.Add(picConeZombie);
            Controls.Add(lblVs);
            Controls.Add(picStrawberry);
            Controls.Add(picCoconut);
            Controls.Add(picBanana);
            Controls.Add(picGrape);
            Controls.Add(lblProgress);
            Controls.Add(progressBar);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "frmSplash";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hoa Quả Nổi Giận - Đang tải...";
            ((System.ComponentModel.ISupportInitialize)picGrape).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBanana).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCoconut).EndInit();
            ((System.ComponentModel.ISupportInitialize)picStrawberry).EndInit();
            ((System.ComponentModel.ISupportInitialize)picConeZombie).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBucketZombie).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBossZombie).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNormalZombie).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.PictureBox picGrape;
        private System.Windows.Forms.PictureBox picBanana;
        private System.Windows.Forms.PictureBox picCoconut;
        private System.Windows.Forms.PictureBox picStrawberry;
        private Guna.UI2.WinForms.Guna2Button lblVs;
        private System.Windows.Forms.PictureBox picConeZombie;
        private System.Windows.Forms.PictureBox picBucketZombie;
        private System.Windows.Forms.PictureBox picBossZombie;
        private System.Windows.Forms.PictureBox picNormalZombie;
    }
}
