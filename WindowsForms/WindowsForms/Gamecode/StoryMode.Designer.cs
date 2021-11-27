namespace WindowsForms.Gamecode
{
    partial class StoryMode
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.escMenu = new System.Windows.Forms.MenuStrip();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.escMenuM = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.destinyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.escMenu.SuspendLayout();
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
            this.healthLabel.Location = new System.Drawing.Point(11, 16);
            this.healthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(65, 17);
            this.healthLabel.TabIndex = 5;
            this.healthLabel.Text = "Health: ";
            // 
            // healthBar
            // 
            this.healthBar.BackColor = System.Drawing.SystemColors.GrayText;
            this.healthBar.Location = new System.Drawing.Point(80, 10);
            this.healthBar.Margin = new System.Windows.Forms.Padding(2);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(83, 23);
            this.healthBar.TabIndex = 4;
            this.healthBar.Value = 100;
            // 
            // destinyBox
            // 
            this.destinyBox.Image = global::WindowsForms.Properties.Resources.images;
            this.destinyBox.Location = new System.Drawing.Point(993, 323);
            this.destinyBox.Margin = new System.Windows.Forms.Padding(2);
            this.destinyBox.Name = "destinyBox";
            this.destinyBox.Size = new System.Drawing.Size(54, 72);
            this.destinyBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.destinyBox.TabIndex = 6;
            this.destinyBox.TabStop = false;
            // 
            // playerBox
            // 
            this.playerBox.Image = global::WindowsForms.Properties.Resources.Player;
            this.playerBox.Location = new System.Drawing.Point(143, 365);
            this.playerBox.Margin = new System.Windows.Forms.Padding(2);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(20, 30);
            this.playerBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.playerBox.TabIndex = 1;
            this.playerBox.TabStop = false;
            // 
            // obstacleTree
            // 
            this.obstacleTree.BackColor = System.Drawing.Color.Transparent;
            this.obstacleTree.Image = global::WindowsForms.Properties.Resources.Enemy_Klein;
            this.obstacleTree.Location = new System.Drawing.Point(664, 326);
            this.obstacleTree.Margin = new System.Windows.Forms.Padding(2);
            this.obstacleTree.Name = "obstacleTree";
            this.obstacleTree.Size = new System.Drawing.Size(64, 64);
            this.obstacleTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.obstacleTree.TabIndex = 0;
            this.obstacleTree.TabStop = false;
            this.obstacleTree.Tag = "obstacleTree";
            this.obstacleTree.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-16, 394);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1079, 86);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // escMenu
            // 
            this.escMenu.BackColor = System.Drawing.Color.Transparent;
            this.escMenu.BackgroundImage = global::WindowsForms.Properties.Resources.TitleScreen1;
            this.escMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.escMenu.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.escMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.escMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.escMenuM,
            this.resumeToolStripMenuItem,
            this.startScreenToolStripMenuItem,
            this.saveGameToolStripMenuItem,
            this.loadGameToolStripMenuItem});
            this.escMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.escMenu.Location = new System.Drawing.Point(0, 0);
            this.escMenu.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.escMenu.Name = "escMenu";
            this.escMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.escMenu.Size = new System.Drawing.Size(1047, 448);
            this.escMenu.TabIndex = 8;
            this.escMenu.Text = "Menu";
            this.escMenu.Visible = false;
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resumeToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(1043, 37);
            this.resumeToolStripMenuItem.Text = "Resume";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeClick);
            // 
            // startScreenToolStripMenuItem
            // 
            this.startScreenToolStripMenuItem.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startScreenToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.startScreenToolStripMenuItem.Name = "startScreenToolStripMenuItem";
            this.startScreenToolStripMenuItem.Size = new System.Drawing.Size(1043, 37);
            this.startScreenToolStripMenuItem.Text = "Start-Screen";
            this.startScreenToolStripMenuItem.Click += new System.EventHandler(this.startScreenClick);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveGameToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(1043, 37);
            this.saveGameToolStripMenuItem.Text = "Save Game";
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Font = new System.Drawing.Font("Unispace", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadGameToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(1043, 37);
            this.loadGameToolStripMenuItem.Text = "Load Game";
            // 
            // escMenuM
            // 
            this.escMenuM.BackColor = System.Drawing.SystemColors.Window;
            this.escMenuM.Font = new System.Drawing.Font("Unispace", 39.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.escMenuM.Margin = new System.Windows.Forms.Padding(0, 20, 0, 60);
            this.escMenuM.Name = "escMenuM";
            this.escMenuM.Size = new System.Drawing.Size(1043, 68);
            this.escMenuM.Text = "Menu";
            this.escMenuM.Click += new System.EventHandler(this.menuEastereggClick);
            // 
            // StoryMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 448);
            this.Controls.Add(this.escMenu);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.obstacleTree);
            this.Controls.Add(this.destinyBox);
            this.Controls.Add(this.healthLabel);
            this.Controls.Add(this.healthBar);
            this.Controls.Add(this.playerBox);
            this.MainMenuStrip = this.escMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StoryMode";
            this.Text = "Story-Mode";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.destinyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.escMenu.ResumeLayout(false);
            this.escMenu.PerformLayout();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip escMenu;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem escMenuM;
    }
}

