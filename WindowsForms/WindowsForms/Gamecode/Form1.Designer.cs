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
            this.MainGameTick = new System.Windows.Forms.Timer(this.components);
            this.healthLabel = new System.Windows.Forms.Label();
            this.healthBar = new System.Windows.Forms.ProgressBar();
            this.destinyBox = new System.Windows.Forms.PictureBox();
            this.playerBox = new System.Windows.Forms.PictureBox();
            this.obstacleTree = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.destinyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleTree)).BeginInit();
            this.SuspendLayout();
            // 
            // MainGameTick
            // 
            this.MainGameTick.Enabled = true;
            this.MainGameTick.Interval = 20;
            this.MainGameTick.Tick += new System.EventHandler(this.MainGameTick_Tick);
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.healthLabel.Location = new System.Drawing.Point(201, 14);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(76, 20);
            this.healthLabel.TabIndex = 5;
            this.healthLabel.Text = "Health: ";
            // 
            // healthBar
            // 
            this.healthBar.BackColor = System.Drawing.SystemColors.GrayText;
            this.healthBar.Location = new System.Drawing.Point(296, 12);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(111, 28);
            this.healthBar.TabIndex = 4;
            this.healthBar.Value = 100;
            // 
            // destinyBox
            // 
            this.destinyBox.Image = global::WindowsForms.Properties.Resources.images;
            this.destinyBox.Location = new System.Drawing.Point(335, 140);
            this.destinyBox.Name = "destinyBox";
            this.destinyBox.Size = new System.Drawing.Size(72, 88);
            this.destinyBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.destinyBox.TabIndex = 6;
            this.destinyBox.TabStop = false;
            // 
            // playerBox
            // 
            this.playerBox.Image = global::WindowsForms.Properties.Resources.Player;
            this.playerBox.Location = new System.Drawing.Point(100, 140);
            this.playerBox.Margin = new System.Windows.Forms.Padding(2);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(20, 30);
            this.playerBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.playerBox.TabIndex = 1;
            this.playerBox.TabStop = false;
            // 
            // obstacleTree
            // 
            this.obstacleTree.Image = global::WindowsForms.Properties.Resources.Enemy_Klein;
            this.obstacleTree.Location = new System.Drawing.Point(216, 140);
            this.obstacleTree.Margin = new System.Windows.Forms.Padding(2);
            this.obstacleTree.Name = "obstacleTree";
            this.obstacleTree.Size = new System.Drawing.Size(75, 73);
            this.obstacleTree.TabIndex = 0;
            this.obstacleTree.TabStop = false;
            this.obstacleTree.Tag = "obstacleTree";
            this.obstacleTree.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 295);
            this.Controls.Add(this.destinyBox);
            this.Controls.Add(this.healthLabel);
            this.Controls.Add(this.healthBar);
            this.Controls.Add(this.playerBox);
            this.Controls.Add(this.obstacleTree);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.destinyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleTree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox obstacleTree;
        private System.Windows.Forms.PictureBox playerBox;
        private System.Windows.Forms.Timer MainGameTick;
        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.ProgressBar healthBar;
        private System.Windows.Forms.PictureBox destinyBox;
    }
}

