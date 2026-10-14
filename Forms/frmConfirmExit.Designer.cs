namespace HoaQuaNoiGian.Forms
{
    partial class frmConfirmExit
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2BorderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            guna2ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            lblMessage = new Label();
            btnExit = new Guna.UI2.WinForms.Guna2GradientButton();
            btnCancel = new Guna.UI2.WinForms.Guna2GradientButton();
            pnlCard.SuspendLayout();
            SuspendLayout();
            // 
            // guna2BorderlessForm
            // 
            guna2BorderlessForm.BorderRadius = 18;
            guna2BorderlessForm.ContainerControl = this;
            guna2BorderlessForm.DockIndicatorTransparencyValue = 0.6D;
            guna2BorderlessForm.TransparentWhileDrag = true;
            // 
            // guna2ShadowForm
            // 
            guna2ShadowForm.BorderRadius = 18;
            guna2ShadowForm.TargetForm = this;
            // 
            // pnlCard
            // 
            pnlCard.BackColor = Color.Transparent;
            pnlCard.BorderColor = Color.FromArgb(255, 193, 7);
            pnlCard.BorderRadius = 18;
            pnlCard.BorderThickness = 2;
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblMessage);
            pnlCard.Controls.Add(btnExit);
            pnlCard.Controls.Add(btnCancel);
            pnlCard.CustomizableEdges = customizableEdges5;
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.FillColor = Color.FromArgb(24, 44, 30);
            pnlCard.Location = new Point(0, 0);
            pnlCard.Margin = new Padding(4, 5, 4, 5);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlCard.Size = new Size(614, 417);
            pnlCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Gold;
            lblTitle.Location = new Point(29, 84);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(557, 47);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "XÁC NHẬN THOÁT GAME";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Arial", 10.5F);
            lblMessage.ForeColor = Color.FromArgb(220, 237, 200);
            lblMessage.Location = new Point(29, 158);
            lblMessage.Margin = new Padding(4, 0, 4, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(557, 80);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "Bạn có chắc chắn muốn rời khỏi trò chơi không?\nTiến trình chơi của bạn đã được tự động lưu lại.";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.Transparent;
            btnExit.BorderRadius = 10;
            btnExit.Cursor = Cursors.Hand;
            btnExit.CustomizableEdges = customizableEdges1;
            btnExit.FillColor = Color.FromArgb(198, 40, 40);
            btnExit.FillColor2 = Color.FromArgb(239, 83, 80);
            btnExit.Font = new Font("Arial", 11F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.HoverState.FillColor = Color.FromArgb(183, 28, 28);
            btnExit.HoverState.FillColor2 = Color.FromArgb(211, 47, 47);
            btnExit.Location = new Point(64, 292);
            btnExit.Margin = new Padding(4, 5, 4, 5);
            btnExit.Name = "btnExit";
            btnExit.ShadowDecoration.BorderRadius = 10;
            btnExit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnExit.ShadowDecoration.Enabled = true;
            btnExit.Size = new Size(229, 73);
            btnExit.TabIndex = 3;
            btnExit.Text = "THOÁT GAME";
            btnExit.Click += btnExit_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.BorderRadius = 10;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.CustomizableEdges = customizableEdges3;
            btnCancel.FillColor = Color.FromArgb(46, 125, 50);
            btnCancel.FillColor2 = Color.FromArgb(76, 175, 80);
            btnCancel.Font = new Font("Arial", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.HoverState.FillColor = Color.FromArgb(27, 94, 32);
            btnCancel.HoverState.FillColor2 = Color.FromArgb(56, 142, 60);
            btnCancel.Location = new Point(321, 292);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.BorderRadius = 10;
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.ShadowDecoration.Enabled = true;
            btnCancel.Size = new Size(229, 73);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "CHƠI TIẾP";
            btnCancel.Click += btnCancel_Click;
            // 
            // frmConfirmExit
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 44, 30);
            ClientSize = new Size(614, 417);
            Controls.Add(pnlCard);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "frmConfirmExit";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Xác nhận thoát";
            pnlCard.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm;
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMessage;
        private Guna.UI2.WinForms.Guna2GradientButton btnExit;
        private Guna.UI2.WinForms.Guna2GradientButton btnCancel;
    }
}
