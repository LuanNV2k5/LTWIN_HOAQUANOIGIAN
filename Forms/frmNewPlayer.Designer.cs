namespace HoaQuaNoiGian.Forms
{
    partial class frmNewPlayer
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Label();
            lblSub = new Label();
            txtPlayerName = new Guna.UI2.WinForms.Guna2TextBox();
            btnStart = new Guna.UI2.WinForms.Guna2GradientButton();
            btnCancel = new Guna.UI2.WinForms.Guna2GradientButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Gold;
            lblTitle.Location = new Point(121, 42);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(464, 43);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "👤 TẠO NGƯỜI CHƠI MỚI";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Arial", 10.5F);
            lblSub.ForeColor = Color.FromArgb(200, 230, 201);
            lblSub.Location = new Point(86, 113);
            lblSub.Margin = new Padding(4, 0, 4, 0);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(179, 24);
            lblSub.TabIndex = 1;
            lblSub.Text = "Nhập tên của bạn:";
            lblSub.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPlayerName
            // 
            txtPlayerName.BorderColor = Color.FromArgb(255, 179, 0);
            txtPlayerName.BorderRadius = 10;
            txtPlayerName.BorderThickness = 2;
            txtPlayerName.Cursor = Cursors.IBeam;
            txtPlayerName.CustomizableEdges = customizableEdges1;
            txtPlayerName.DefaultText = "";
            txtPlayerName.FillColor = Color.FromArgb(24, 36, 24);
            txtPlayerName.Font = new Font("Arial", 12F, FontStyle.Bold);
            txtPlayerName.ForeColor = Color.White;
            txtPlayerName.Location = new Point(86, 175);
            txtPlayerName.Margin = new Padding(6, 8, 6, 8);
            txtPlayerName.MaxLength = 25;
            txtPlayerName.Name = "txtPlayerName";
            txtPlayerName.PlaceholderForeColor = Color.DarkGray;
            txtPlayerName.PlaceholderText = "Ví dụ: Minh Đăng, Bảo...";
            txtPlayerName.SelectedText = "";
            txtPlayerName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtPlayerName.Size = new Size(521, 75);
            txtPlayerName.TabIndex = 2;
            txtPlayerName.KeyDown += txtPlayerName_KeyDown;
            // 
            // btnStart
            // 
            btnStart.BorderRadius = 12;
            btnStart.Cursor = Cursors.Hand;
            btnStart.CustomizableEdges = customizableEdges3;
            btnStart.FillColor = Color.FromArgb(46, 125, 50);
            btnStart.FillColor2 = Color.FromArgb(76, 175, 80);
            btnStart.Font = new Font("Arial", 11.5F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.HoverState.FillColor = Color.FromArgb(255, 179, 0);
            btnStart.HoverState.FillColor2 = Color.FromArgb(255, 143, 0);
            btnStart.HoverState.ForeColor = Color.Black;
            btnStart.Location = new Point(86, 292);
            btnStart.Margin = new Padding(4, 5, 4, 5);
            btnStart.Name = "btnStart";
            btnStart.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnStart.Size = new Size(279, 75);
            btnStart.TabIndex = 3;
            btnStart.Text = "BẮT ĐẦU CHƠI ➔";
            btnStart.Click += btnStart_Click;
            // 
            // btnCancel
            // 
            btnCancel.BorderRadius = 12;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.CustomizableEdges = customizableEdges5;
            btnCancel.FillColor = Color.FromArgb(69, 90, 100);
            btnCancel.FillColor2 = Color.FromArgb(55, 71, 79);
            btnCancel.Font = new Font("Arial", 11.5F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverState.FillColor = Color.FromArgb(120, 144, 156);
            btnCancel.Location = new Point(386, 292);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCancel.Size = new Size(221, 75);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "HỦY BỎ";
            btnCancel.Click += btnCancel_Click;
            // 
            // frmNewPlayer
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(27, 50, 32);
            ClientSize = new Size(693, 417);
            Controls.Add(btnCancel);
            Controls.Add(btnStart);
            Controls.Add(txtPlayerName);
            Controls.Add(lblSub);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmNewPlayer";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nhập tên người chơi mới";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;
        private Guna.UI2.WinForms.Guna2TextBox txtPlayerName;
        private Guna.UI2.WinForms.Guna2GradientButton btnStart;
        private Guna.UI2.WinForms.Guna2GradientButton btnCancel;
    }
}
