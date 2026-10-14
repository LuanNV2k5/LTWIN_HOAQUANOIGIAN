namespace HoaQuaNoiGian.Forms
{
    partial class frmGame
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
            this.components = new System.ComponentModel.Container();
            this.gamePanel = new HoaQuaNoiGian.Engine.GamePanel();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.Color.ForestGreen;
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 0);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(1200, 700);
            this.gamePanel.TabIndex = 0;
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 16;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.gamePanel);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyPreview = true;
            this.MaximizeBox = true;
            this.Name = "frmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoa Quả Nổi Giận";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGame_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGame_KeyDown);
            this.ResumeLayout(false);
        }

        #endregion

        private HoaQuaNoiGian.Engine.GamePanel gamePanel;
        private System.Windows.Forms.Timer gameTimer;
    }
}
