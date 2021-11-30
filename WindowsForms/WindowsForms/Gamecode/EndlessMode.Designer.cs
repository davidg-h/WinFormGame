
namespace WindowsForms.Gamecode
{
    partial class EndlessMode
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
            this.healthLabel = new System.Windows.Forms.Label();
            this.healthBar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.playerBox = new System.Windows.Forms.PictureBox();
            this.obstacleTree = new System.Windows.Forms.PictureBox();
            this.MainGameTick = new System.Windows.Forms.Timer(this.components);
            this.escMenu = new System.Windows.Forms.MenuStrip();
            this.escMenuM = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scoreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleTree)).BeginInit();
            this.escMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // healthLabel
            // 
            this.healthLabel.AutoSize = true;
            this.healthLabel.BackColor = System.Drawing.Color.Transparent;
            this.healthLabel.Font = new System.Drawing.Font("Unispace", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.healthLabel.Location = new System.Drawing.Point(11, 9);
            this.healthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.healthLabel.Name = "healthLabel";
            this.healthLabel.Size = new System.Drawing.Size(106, 23);
            this.healthLabel.TabIndex = 6;
            this.healthLabel.Text = "Health: ";
            // 
            // healthBar
            // 
            this.healthBar.BackColor = System.Drawing.SystemColors.GrayText;
            this.healthBar.Location = new System.Drawing.Point(102, 9);
            this.healthBar.Margin = new System.Windows.Forms.Padding(2);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(83, 23);
            this.healthBar.TabIndex = 7;
            this.healthBar.Value = 100;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-16, 394);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1079, 86);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // playerBox
            // 
            this.playerBox.BackColor = System.Drawing.Color.Transparent;
            this.playerBox.Image = global::WindowsForms.Properties.Resources.idle;
            this.playerBox.Location = new System.Drawing.Point(34, 331);
            this.playerBox.Margin = new System.Windows.Forms.Padding(2);
            this.playerBox.Name = "playerBox";
            this.playerBox.Size = new System.Drawing.Size(68, 64);
            this.playerBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.playerBox.TabIndex = 9;
            this.playerBox.TabStop = false;
            // 
            // obstacleTree
            // 
            this.obstacleTree.BackColor = System.Drawing.Color.Transparent;
            this.obstacleTree.Image = global::WindowsForms.Properties.Resources.shroomIdle;
            this.obstacleTree.Location = new System.Drawing.Point(578, 367);
            this.obstacleTree.Margin = new System.Windows.Forms.Padding(2);
            this.obstacleTree.Name = "obstacleTree";
            this.obstacleTree.Size = new System.Drawing.Size(32, 28);
            this.obstacleTree.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.obstacleTree.TabIndex = 10;
            this.obstacleTree.TabStop = false;
            this.obstacleTree.Tag = "obstacleTree";
            // 
            // MainGameTick
            // 
            this.MainGameTick.Enabled = true;
            this.MainGameTick.Interval = 20;
            this.MainGameTick.Tick += new System.EventHandler(this.endlessTickTimer);
            // 
            // escMenu
            // 
            this.escMenu.BackColor = System.Drawing.Color.Transparent;
            this.escMenu.BackgroundImage = global::WindowsForms.Properties.Resources.TitleScreen1;
            this.escMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.escMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.escMenu.Size = new System.Drawing.Size(1052, 499);
            this.escMenu.TabIndex = 11;
            this.escMenu.Text = "Menu";
            this.escMenu.Visible = false;
            // 
            // escMenuM
            // 
            this.escMenuM.BackColor = System.Drawing.SystemColors.Window;
            this.escMenuM.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.escMenuM.Margin = new System.Windows.Forms.Padding(0, 20, 0, 60);
            this.escMenuM.Name = "escMenuM";
            this.escMenuM.Size = new System.Drawing.Size(1048, 65);
            this.escMenuM.Text = "Menu";
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resumeToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(1048, 35);
            this.resumeToolStripMenuItem.Text = "Resume";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeClick);
            // 
            // startScreenToolStripMenuItem
            // 
            this.startScreenToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startScreenToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.startScreenToolStripMenuItem.Name = "startScreenToolStripMenuItem";
            this.startScreenToolStripMenuItem.Size = new System.Drawing.Size(1048, 35);
            this.startScreenToolStripMenuItem.Text = "Start-Screen";
            this.startScreenToolStripMenuItem.Click += new System.EventHandler(this.startScreenClick);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveGameToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(1048, 35);
            this.saveGameToolStripMenuItem.Text = "Save Game";
            // 
            // loadGameToolStripMenuItem
            // 
            this.loadGameToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadGameToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 35);
            this.loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            this.loadGameToolStripMenuItem.Size = new System.Drawing.Size(1048, 35);
            this.loadGameToolStripMenuItem.Text = "Load Game";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Unispace", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.Location = new System.Drawing.Point(907, 9);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(116, 25);
            this.scoreLabel.TabIndex = 12;
            this.scoreLabel.Text = "Score: 0\r\n";
            // 
            // EndlessMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1047, 448);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.escMenu);
            this.Controls.Add(this.obstacleTree);
            this.Controls.Add(this.playerBox);
            this.Controls.Add(this.healthBar);
            this.Controls.Add(this.healthLabel);
            this.Name = "EndlessMode";
            this.Text = "Endless-Mode";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacleTree)).EndInit();
            this.escMenu.ResumeLayout(false);
            this.escMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label healthLabel;
        private System.Windows.Forms.ProgressBar healthBar;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.PictureBox playerBox;
        internal System.Windows.Forms.PictureBox obstacleTree;
        private System.Windows.Forms.Timer MainGameTick;
        private System.Windows.Forms.MenuStrip escMenu;
        private System.Windows.Forms.ToolStripMenuItem escMenuM;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGameToolStripMenuItem;
        private System.Windows.Forms.Label scoreLabel;
    }
}