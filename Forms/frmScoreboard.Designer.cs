namespace HoaQuaNoiGian.Forms
{
    partial class frmScoreboard
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Label();
            lblFilter = new Label();
            cboLevelFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            btnClear = new Guna.UI2.WinForms.Guna2GradientButton();
            dgvScores = new Guna.UI2.WinForms.Guna2DataGridView();
            colRank = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colLevel = new DataGridViewTextBoxColumn();
            colScore = new DataGridViewTextBoxColumn();
            colStars = new DataGridViewTextBoxColumn();
            colTime = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            lblEmpty = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2GradientButton();
            ((System.ComponentModel.ISupportInitialize)dgvScores).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Arial", 26.25F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Gold;
            lblTitle.Location = new Point(286, 42);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(831, 62);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BẢNG KỶ LỤC ĐIỂM CAO NHẤT";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.BackColor = Color.Transparent;
            lblFilter.Font = new Font("Arial", 11.25F, FontStyle.Bold);
            lblFilter.ForeColor = Color.White;
            lblFilter.Location = new Point(143, 158);
            lblFilter.Margin = new Padding(4, 0, 4, 0);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(169, 27);
            lblFilter.TabIndex = 1;
            lblFilter.Text = "Lọc theo màn:";
            lblFilter.Visible = false;
            // 
            // cboLevelFilter
            // 
            cboLevelFilter.BackColor = Color.Transparent;
            cboLevelFilter.BorderRadius = 8;
            cboLevelFilter.CustomizableEdges = customizableEdges1;
            cboLevelFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cboLevelFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLevelFilter.FillColor = Color.FromArgb(38, 50, 56);
            cboLevelFilter.FocusedColor = Color.Empty;
            cboLevelFilter.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            cboLevelFilter.ForeColor = Color.Gold;
            cboLevelFilter.ItemHeight = 30;
            cboLevelFilter.Location = new Point(314, 142);
            cboLevelFilter.Margin = new Padding(4, 5, 4, 5);
            cboLevelFilter.Name = "cboLevelFilter";
            cboLevelFilter.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cboLevelFilter.Size = new Size(241, 36);
            cboLevelFilter.TabIndex = 2;
            cboLevelFilter.Visible = false;
            cboLevelFilter.SelectedIndexChanged += cboLevelFilter_SelectedIndexChanged;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.Transparent;
            btnClear.BorderRadius = 10;
            btnClear.Cursor = Cursors.Hand;
            btnClear.CustomizableEdges = customizableEdges3;
            btnClear.FillColor = Color.FromArgb(198, 40, 40);
            btnClear.FillColor2 = Color.FromArgb(239, 83, 80);
            btnClear.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(1011, 142);
            btnClear.Margin = new Padding(4, 5, 4, 5);
            btnClear.Name = "btnClear";
            btnClear.ShadowDecoration.BorderRadius = 10;
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnClear.ShadowDecoration.Enabled = true;
            btnClear.Size = new Size(286, 60);
            btnClear.TabIndex = 3;
            btnClear.Text = "XÓA BẢNG THÀNH TÍCH";
            btnClear.Click += btnClear_Click;
            // 
            // dgvScores
            // 
            dgvScores.AllowUserToAddRows = false;
            dgvScores.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(38, 50, 56);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dgvScores.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvScores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvScores.BackgroundColor = Color.FromArgb(25, 40, 30);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(46, 125, 50);
            dataGridViewCellStyle2.Font = new Font("Arial", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.Gold;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvScores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvScores.ColumnHeadersHeight = 35;
            dgvScores.Columns.AddRange(new DataGridViewColumn[] { colRank, colName, colLevel, colScore, colStars, colTime, colDate });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(33, 33, 33);
            dataGridViewCellStyle3.Font = new Font("Arial", 9.75F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(255, 179, 0);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvScores.DefaultCellStyle = dataGridViewCellStyle3;
            dgvScores.GridColor = Color.FromArgb(50, 70, 50);
            dgvScores.Location = new Point(143, 225);
            dgvScores.Margin = new Padding(4, 5, 4, 5);
            dgvScores.MultiSelect = false;
            dgvScores.Name = "dgvScores";
            dgvScores.ReadOnly = true;
            dgvScores.RowHeadersVisible = false;
            dgvScores.RowHeadersWidth = 62;
            dgvScores.RowTemplate.Height = 25;
            dgvScores.Size = new Size(1154, 633);
            dgvScores.TabIndex = 4;
            dgvScores.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(38, 50, 56);
            dgvScores.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.White;
            dgvScores.ThemeStyle.BackColor = Color.FromArgb(25, 40, 30);
            dgvScores.ThemeStyle.GridColor = Color.FromArgb(50, 70, 50);
            dgvScores.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(46, 125, 50);
            dgvScores.ThemeStyle.HeaderStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgvScores.ThemeStyle.HeaderStyle.ForeColor = Color.Gold;
            dgvScores.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvScores.ThemeStyle.HeaderStyle.Height = 35;
            dgvScores.ThemeStyle.ReadOnly = true;
            dgvScores.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(33, 33, 33);
            dgvScores.ThemeStyle.RowsStyle.Font = new Font("Arial", 9.75F);
            dgvScores.ThemeStyle.RowsStyle.ForeColor = Color.White;
            dgvScores.ThemeStyle.RowsStyle.Height = 25;
            dgvScores.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(255, 179, 0);
            dgvScores.ThemeStyle.RowsStyle.SelectionForeColor = Color.Black;
            dgvScores.CellContentClick += dgvScores_CellContentClick;
            // 
            // colRank
            // 
            colRank.FillWeight = 50F;
            colRank.HeaderText = "Hạng";
            colRank.MinimumWidth = 8;
            colRank.Name = "colRank";
            colRank.ReadOnly = true;
            // 
            // colName
            // 
            colName.FillWeight = 120F;
            colName.HeaderText = "Tên Người Chơi";
            colName.MinimumWidth = 8;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colLevel
            // 
            colLevel.FillWeight = 80F;
            colLevel.HeaderText = "Màn Đạt Được";
            colLevel.MinimumWidth = 8;
            colLevel.Name = "colLevel";
            colLevel.ReadOnly = true;
            // 
            // colScore
            // 
            colScore.FillWeight = 80F;
            colScore.HeaderText = "Điểm Kỷ Lục";
            colScore.MinimumWidth = 8;
            colScore.Name = "colScore";
            colScore.ReadOnly = true;
            // 
            // colStars
            // 
            colStars.FillWeight = 80F;
            colStars.HeaderText = "Số Sao";
            colStars.MinimumWidth = 8;
            colStars.Name = "colStars";
            colStars.ReadOnly = true;
            // 
            // colTime
            // 
            colTime.FillWeight = 80F;
            colTime.HeaderText = "Thời Gian";
            colTime.MinimumWidth = 8;
            colTime.Name = "colTime";
            colTime.ReadOnly = true;
            // 
            // colDate
            // 
            colDate.FillWeight = 120F;
            colDate.HeaderText = "Ngày Lập Kỷ Lục";
            colDate.MinimumWidth = 8;
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            // 
            // lblEmpty
            // 
            lblEmpty.Anchor = AnchorStyles.None;
            lblEmpty.AutoSize = true;
            lblEmpty.Font = new Font("Arial", 18F, FontStyle.Bold);
            lblEmpty.ForeColor = Color.Yellow;
            lblEmpty.Location = new Point(486, 483);
            lblEmpty.Margin = new Padding(4, 0, 4, 0);
            lblEmpty.Name = "lblEmpty";
            lblEmpty.Size = new Size(511, 43);
            lblEmpty.TabIndex = 5;
            lblEmpty.Text = "CHƯA CÓ THÀNH TÍCH NÀO";
            lblEmpty.Visible = false;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom;
            btnBack.BackColor = Color.Transparent;
            btnBack.BorderRadius = 14;
            btnBack.Cursor = Cursors.Hand;
            btnBack.CustomizableEdges = customizableEdges5;
            btnBack.FillColor = Color.FromArgb(46, 125, 50);
            btnBack.FillColor2 = Color.FromArgb(76, 175, 80);
            btnBack.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(591, 883);
            btnBack.Margin = new Padding(4, 5, 4, 5);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.BorderRadius = 14;
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnBack.ShadowDecoration.Enabled = true;
            btnBack.Size = new Size(257, 80);
            btnBack.TabIndex = 6;
            btnBack.Text = "QUAY LẠI MENU";
            btnBack.Click += btnBack_Click;
            // 
            // frmScoreboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 94, 32);
            ClientSize = new Size(1440, 1002);
            Controls.Add(btnBack);
            Controls.Add(lblEmpty);
            Controls.Add(dgvScores);
            Controls.Add(btnClear);
            Controls.Add(cboLevelFilter);
            Controls.Add(lblFilter);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "frmScoreboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hoa Quả Nổi Giận - Bảng Thành Tích";
            ((System.ComponentModel.ISupportInitialize)dgvScores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFilter;
        private Guna.UI2.WinForms.Guna2ComboBox cboLevelFilter;
        private Guna.UI2.WinForms.Guna2GradientButton btnClear;
        private Guna.UI2.WinForms.Guna2DataGridView dgvScores;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStars;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.Label lblEmpty;
        private Guna.UI2.WinForms.Guna2GradientButton btnBack;
    }
}
