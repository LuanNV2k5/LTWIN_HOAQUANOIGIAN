namespace HoaQuaNoiGian.Forms
{
    partial class frmPause
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnResume = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnRestart = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnMute = new Guna.UI2.WinForms.Guna2Button();
            this.btnMenu = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblTitle.Location = new System.Drawing.Point(75, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(225, 29);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TẠM DỪNG GAME";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResume
            // 
            this.btnResume.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnResume.BorderRadius = 14;
            this.btnResume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResume.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnResume.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnResume.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnResume.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnResume.ForeColor = System.Drawing.Color.White;
            this.btnResume.Location = new System.Drawing.Point(67, 75);
            this.btnResume.Name = "btnResume";
            this.btnResume.ShadowDecoration.BorderRadius = 14;
            this.btnResume.ShadowDecoration.Enabled = true;
            this.btnResume.Size = new System.Drawing.Size(230, 44);
            this.btnResume.TabIndex = 1;
            this.btnResume.Text = "TIẾP TỤC CHƠI";
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRestart.BorderRadius = 14;
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnRestart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnRestart.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnRestart.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.Location = new System.Drawing.Point(67, 130);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.ShadowDecoration.BorderRadius = 14;
            this.btnRestart.ShadowDecoration.Enabled = true;
            this.btnRestart.Size = new System.Drawing.Size(230, 44);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "CHƠI LẠI MÀN NÀY";
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnMute
            // 
            this.btnMute.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMute.BorderRadius = 14;
            this.btnMute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMute.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.btnMute.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMute.ForeColor = System.Drawing.Color.Gold;
            this.btnMute.Location = new System.Drawing.Point(67, 185);
            this.btnMute.Name = "btnMute";
            this.btnMute.ShadowDecoration.BorderRadius = 14;
            this.btnMute.ShadowDecoration.Enabled = true;
            this.btnMute.Size = new System.Drawing.Size(230, 44);
            this.btnMute.TabIndex = 3;
            this.btnMute.Text = "🔊 Âm thanh: BẬT";
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMenu.BorderRadius = 14;
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnMenu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnMenu.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this.btnMenu.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Location = new System.Drawing.Point(67, 240);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.ShadowDecoration.BorderRadius = 14;
            this.btnMenu.ShadowDecoration.Enabled = true;
            this.btnMenu.Size = new System.Drawing.Size(230, 44);
            this.btnMenu.TabIndex = 4;
            this.btnMenu.Text = "VỀ MENU CHÍNH";
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // frmPause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(364, 321);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPause";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạm Dừng Game";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2GradientButton btnResume;
        private Guna.UI2.WinForms.Guna2GradientButton btnRestart;
        private Guna.UI2.WinForms.Guna2Button btnMute;
        private Guna.UI2.WinForms.Guna2GradientButton btnMenu;
    }
}
