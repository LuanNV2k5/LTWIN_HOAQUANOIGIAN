namespace HoaQuaNoiGian.Forms
{
    partial class frmProgress
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.dgvProfiles = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrentLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnlocked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastPlayed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPlayContinue = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnSelectLevel = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnNewProfile = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnDeleteProfile = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnBack = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfiles)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblTitle.Location = new System.Drawing.Point(240, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TIẾN TRÌNH NGƯỜI CHƠI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            this.lblSubtitle.Location = new System.Drawing.Point(220, 75);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(520, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Chọn người chơi của bạn để tiếp tục màn chơi dở hoặc chọn màn đã mở khóa:";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvProfiles
            // 
            this.dgvProfiles.AllowUserToAddRows = false;
            this.dgvProfiles.AllowUserToDeleteRows = false;
            this.dgvProfiles.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(36)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Gold;
            this.dgvProfiles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProfiles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProfiles.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(35)))), ((int)(((byte)(24)))));
            this.dgvProfiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProfiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProfiles.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProfiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProfiles.ColumnHeadersHeight = 42;
            this.dgvProfiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colName,
            this.colCurrentLevel,
            this.colUnlocked,
            this.colScore,
            this.colStars,
            this.colLastPlayed});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(42)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Gold;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProfiles.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvProfiles.EnableHeadersVisualStyles = false;
            this.dgvProfiles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(70)))), ((int)(((byte)(45)))));
            this.dgvProfiles.Location = new System.Drawing.Point(40, 115);
            this.dgvProfiles.MultiSelect = false;
            this.dgvProfiles.Name = "dgvProfiles";
            this.dgvProfiles.ReadOnly = true;
            this.dgvProfiles.RowHeadersVisible = false;
            this.dgvProfiles.RowTemplate.Height = 36;
            this.dgvProfiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProfiles.Size = new System.Drawing.Size(880, 310);
            this.dgvProfiles.TabIndex = 2;
            this.dgvProfiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProfiles_CellDoubleClick);
            // 
            // colIndex
            // 
            this.colIndex.FillWeight = 35F;
            this.colIndex.HeaderText = "STT";
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.FillWeight = 110F;
            this.colName.HeaderText = "Tên Người Chơi";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colCurrentLevel
            // 
            this.colCurrentLevel.FillWeight = 120F;
            this.colCurrentLevel.HeaderText = "Màn Chơi Hiện Tại";
            this.colCurrentLevel.Name = "colCurrentLevel";
            this.colCurrentLevel.ReadOnly = true;
            // 
            // colUnlocked
            // 
            this.colUnlocked.FillWeight = 80F;
            this.colUnlocked.HeaderText = "Mở Khóa";
            this.colUnlocked.Name = "colUnlocked";
            this.colUnlocked.ReadOnly = true;
            // 
            // colScore
            // 
            this.colScore.FillWeight = 75F;
            this.colScore.HeaderText = "Tổng Điểm";
            this.colScore.Name = "colScore";
            this.colScore.ReadOnly = true;
            // 
            // colStars
            // 
            this.colStars.FillWeight = 65F;
            this.colStars.HeaderText = "Số Sao";
            this.colStars.Name = "colStars";
            this.colStars.ReadOnly = true;
            // 
            // colLastPlayed
            // 
            this.colLastPlayed.FillWeight = 115F;
            this.colLastPlayed.HeaderText = "Chơi Lần Cuối";
            this.colLastPlayed.Name = "colLastPlayed";
            this.colLastPlayed.ReadOnly = true;
            // 
            // btnPlayContinue
            // 
            this.btnPlayContinue.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPlayContinue.BorderRadius = 14;
            this.btnPlayContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayContinue.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnPlayContinue.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnPlayContinue.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPlayContinue.ForeColor = System.Drawing.Color.White;
            this.btnPlayContinue.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(0)))));
            this.btnPlayContinue.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(143)))), ((int)(((byte)(0)))));
            this.btnPlayContinue.HoverState.ForeColor = System.Drawing.Color.Black;
            this.btnPlayContinue.Location = new System.Drawing.Point(40, 445);
            this.btnPlayContinue.Name = "btnPlayContinue";
            this.btnPlayContinue.Size = new System.Drawing.Size(185, 48);
            this.btnPlayContinue.TabIndex = 3;
            this.btnPlayContinue.Text = "▶ TIẾP TỤC CHƠI";
            this.btnPlayContinue.Click += new System.EventHandler(this.btnPlayContinue_Click);
            // 
            // btnSelectLevel
            // 
            this.btnSelectLevel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSelectLevel.BorderRadius = 14;
            this.btnSelectLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectLevel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(143)))), ((int)(((byte)(0)))));
            this.btnSelectLevel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(0)))));
            this.btnSelectLevel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSelectLevel.ForeColor = System.Drawing.Color.Black;
            this.btnSelectLevel.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSelectLevel.Location = new System.Drawing.Point(235, 445);
            this.btnSelectLevel.Name = "btnSelectLevel";
            this.btnSelectLevel.Size = new System.Drawing.Size(165, 48);
            this.btnSelectLevel.TabIndex = 4;
            this.btnSelectLevel.Text = "🎯 CHỌN MÀN";
            this.btnSelectLevel.Click += new System.EventHandler(this.btnSelectLevel_Click);
            // 
            // btnNewProfile
            // 
            this.btnNewProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNewProfile.BorderRadius = 14;
            this.btnNewProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewProfile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btnNewProfile.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(165)))), ((int)(((byte)(245)))));
            this.btnNewProfile.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNewProfile.ForeColor = System.Drawing.Color.White;
            this.btnNewProfile.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnNewProfile.Location = new System.Drawing.Point(410, 445);
            this.btnNewProfile.Name = "btnNewProfile";
            this.btnNewProfile.Size = new System.Drawing.Size(175, 48);
            this.btnNewProfile.TabIndex = 5;
            this.btnNewProfile.Text = "➕ CHƠI MỚI";
            this.btnNewProfile.Click += new System.EventHandler(this.btnNewProfile_Click);
            // 
            // btnDeleteProfile
            // 
            this.btnDeleteProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeleteProfile.BorderRadius = 14;
            this.btnDeleteProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteProfile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnDeleteProfile.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this.btnDeleteProfile.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteProfile.ForeColor = System.Drawing.Color.White;
            this.btnDeleteProfile.HoverState.FillColor = System.Drawing.Color.DarkRed;
            this.btnDeleteProfile.Location = new System.Drawing.Point(595, 445);
            this.btnDeleteProfile.Name = "btnDeleteProfile";
            this.btnDeleteProfile.Size = new System.Drawing.Size(160, 48);
            this.btnDeleteProfile.TabIndex = 6;
            this.btnDeleteProfile.Text = "🗑️ XÓA TIẾN TRÌNH";
            this.btnDeleteProfile.Click += new System.EventHandler(this.btnDeleteProfile_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.BorderRadius = 14;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.btnBack.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(90)))), ((int)(((byte)(100)))));
            this.btnBack.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.btnBack.Location = new System.Drawing.Point(765, 445);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(155, 48);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "QUAY LẠI";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(960, 520);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDeleteProfile);
            this.Controls.Add(this.btnNewProfile);
            this.Controls.Add(this.btnSelectLevel);
            this.Controls.Add(this.btnPlayContinue);
            this.Controls.Add(this.dgvProfiles);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoa Quả Nổi Giận - Tiến Trình Các Người Chơi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProfiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2DataGridView dgvProfiles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrentLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnlocked;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStars;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastPlayed;
        private Guna.UI2.WinForms.Guna2GradientButton btnPlayContinue;
        private Guna.UI2.WinForms.Guna2GradientButton btnSelectLevel;
        private Guna.UI2.WinForms.Guna2GradientButton btnNewProfile;
        private Guna.UI2.WinForms.Guna2GradientButton btnDeleteProfile;
        private Guna.UI2.WinForms.Guna2GradientButton btnBack;
    }
}
