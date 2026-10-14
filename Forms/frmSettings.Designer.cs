namespace HoaQuaNoiGian.Forms
{
    partial class frmSettings
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
            this.swSound = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblSoundText = new System.Windows.Forms.Label();
            this.swMusic = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.lblMusicText = new System.Windows.Forms.Label();
            this.btnSave = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblTitle.Location = new System.Drawing.Point(115, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(273, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CÀI ĐẶT ÂM THANH";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // swSound
            // 
            this.swSound.Checked = true;
            this.swSound.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.swSound.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.swSound.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swSound.CheckedState.InnerColor = System.Drawing.Color.White;
            this.swSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.swSound.Location = new System.Drawing.Point(60, 85);
            this.swSound.Name = "swSound";
            this.swSound.Size = new System.Drawing.Size(50, 25);
            this.swSound.TabIndex = 1;
            this.swSound.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swSound.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swSound.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swSound.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblSoundText
            // 
            this.lblSoundText.AutoSize = true;
            this.lblSoundText.BackColor = System.Drawing.Color.Transparent;
            this.lblSoundText.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoundText.ForeColor = System.Drawing.Color.White;
            this.lblSoundText.Location = new System.Drawing.Point(125, 88);
            this.lblSoundText.Name = "lblSoundText";
            this.lblSoundText.Size = new System.Drawing.Size(277, 18);
            this.lblSoundText.TabIndex = 2;
            this.lblSoundText.Text = "Hiệu ứng âm thanh (Sound Effects)";
            // 
            // swMusic
            // 
            this.swMusic.Checked = true;
            this.swMusic.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.swMusic.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.swMusic.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swMusic.CheckedState.InnerColor = System.Drawing.Color.White;
            this.swMusic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.swMusic.Location = new System.Drawing.Point(60, 135);
            this.swMusic.Name = "swMusic";
            this.swMusic.Size = new System.Drawing.Size(50, 25);
            this.swMusic.TabIndex = 3;
            this.swMusic.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swMusic.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.swMusic.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.swMusic.UncheckedState.InnerColor = System.Drawing.Color.White;
            // 
            // lblMusicText
            // 
            this.lblMusicText.AutoSize = true;
            this.lblMusicText.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicText.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMusicText.ForeColor = System.Drawing.Color.White;
            this.lblMusicText.Location = new System.Drawing.Point(125, 138);
            this.lblMusicText.Name = "lblMusicText";
            this.lblMusicText.Size = new System.Drawing.Size(236, 18);
            this.lblMusicText.TabIndex = 4;
            this.lblMusicText.Text = "Nhạc nền (Background Music)";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.BorderRadius = 14;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnSave.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSave.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(155, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.ShadowDecoration.BorderRadius = 14;
            this.btnSave.ShadowDecoration.Enabled = true;
            this.btnSave.Size = new System.Drawing.Size(170, 45);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "LƯU CÀI ĐẶT";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(60)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(480, 265);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblMusicText);
            this.Controls.Add(this.swMusic);
            this.Controls.Add(this.lblSoundText);
            this.Controls.Add(this.swSound);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoa Quả Nổi Giận - Cài Đặt Âm Thanh";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2ToggleSwitch swSound;
        private System.Windows.Forms.Label lblSoundText;
        private Guna.UI2.WinForms.Guna2ToggleSwitch swMusic;
        private System.Windows.Forms.Label lblMusicText;
        private Guna.UI2.WinForms.Guna2GradientButton btnSave;
    }
}
