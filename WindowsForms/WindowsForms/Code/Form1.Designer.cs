namespace WindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.EnemyBox = new System.Windows.Forms.PictureBox();
            this.playerBox = new System.Windows.Forms.PictureBox();
            this.MainGameTick = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.EnemyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EnemyBox
            // 
            this.EnemyBox.Image = global::WindowsForms.Properties.Resources.Enemy_Klein1;
            this.EnemyBox.Location = new System.Drawing.Point(324, 219);
            this.EnemyBox.Name = "EnemyBox";
            this.EnemyBox.Size = new System.Drawing.Size(113, 114);
            this.EnemyBox.TabIndex = 0;
            this.EnemyBox.TabStop = false;
            this.EnemyBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // playerBox
            // 
            this.playerBox.Image = global::WindowsForms.Properties.Resources.Player1;
            this.playerBox.Location = new System.Drawing.Point(150, 219);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(32, 52);
            this.playerBox.TabIndex = 1;
            this.playerBox.TabStop = false;
            // 
            // MainGameTick
            // 
            this.MainGameTick.Enabled = true;
            this.MainGameTick.Interval = 20;
            this.MainGameTick.Tick += new System.EventHandler(this.MainGameTick_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 461);
            this.Controls.Add(this.playerBox);
            this.Controls.Add(this.EnemyBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.EnemyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox EnemyBox;
        private System.Windows.Forms.PictureBox playerBox;
        private System.Windows.Forms.Timer MainGameTick;
    }
}

