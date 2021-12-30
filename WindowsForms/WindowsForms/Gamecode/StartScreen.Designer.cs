
namespace WindowsForms.Gamecode
{
    partial class StartScreen
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
            this.gameTitel = new System.Windows.Forms.Label();
            this.storyModeButton = new System.Windows.Forms.Button();
            this.endlessModeButton = new System.Windows.Forms.Button();
            this.quitBotton = new System.Windows.Forms.Button();
            this.InstructionsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameTitel
            // 
            this.gameTitel.BackColor = System.Drawing.Color.Transparent;
            this.gameTitel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameTitel.ForeColor = System.Drawing.Color.Black;
            this.gameTitel.Location = new System.Drawing.Point(259, 43);
            this.gameTitel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gameTitel.Name = "gameTitel";
            this.gameTitel.Size = new System.Drawing.Size(516, 59);
            this.gameTitel.TabIndex = 0;
            this.gameTitel.Text = "Graphic-Knight\r\n";
            this.gameTitel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storyModeButton
            // 
            this.storyModeButton.BackColor = System.Drawing.SystemColors.Control;
            this.storyModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storyModeButton.Location = new System.Drawing.Point(363, 170);
            this.storyModeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.storyModeButton.Name = "storyModeButton";
            this.storyModeButton.Size = new System.Drawing.Size(309, 55);
            this.storyModeButton.TabIndex = 1;
            this.storyModeButton.Text = "Story-Mode";
            this.storyModeButton.UseVisualStyleBackColor = false;
            this.storyModeButton.Click += new System.EventHandler(this.openStory);
            // 
            // endlessModeButton
            // 
            this.endlessModeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endlessModeButton.Location = new System.Drawing.Point(363, 280);
            this.endlessModeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.endlessModeButton.Name = "endlessModeButton";
            this.endlessModeButton.Size = new System.Drawing.Size(309, 55);
            this.endlessModeButton.TabIndex = 2;
            this.endlessModeButton.Text = "Endless-Mode";
            this.endlessModeButton.UseVisualStyleBackColor = true;
            this.endlessModeButton.Click += new System.EventHandler(this.openEndless);
            // 
            // quitBotton
            // 
            this.quitBotton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitBotton.Location = new System.Drawing.Point(363, 485);
            this.quitBotton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.quitBotton.Name = "quitBotton";
            this.quitBotton.Size = new System.Drawing.Size(309, 55);
            this.quitBotton.TabIndex = 3;
            this.quitBotton.Text = "Quit";
            this.quitBotton.UseVisualStyleBackColor = true;
            this.quitBotton.Click += new System.EventHandler(this.quitGame);
            // 
            // InstructionsButton
            // 
            this.InstructionsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionsButton.Location = new System.Drawing.Point(363, 388);
            this.InstructionsButton.Margin = new System.Windows.Forms.Padding(4);
            this.InstructionsButton.Name = "InstructionsButton";
            this.InstructionsButton.Size = new System.Drawing.Size(309, 55);
            this.InstructionsButton.TabIndex = 4;
            this.InstructionsButton.Text = "Instructions";
            this.InstructionsButton.UseVisualStyleBackColor = true;
            this.InstructionsButton.Click += new System.EventHandler(this.Instructions);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsForms.Properties.Resources.TitleScreen1;
            this.ClientSize = new System.Drawing.Size(1053, 678);
            this.Controls.Add(this.InstructionsButton);
            this.Controls.Add(this.quitBotton);
            this.Controls.Add(this.endlessModeButton);
            this.Controls.Add(this.storyModeButton);
            this.Controls.Add(this.gameTitel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "StartScreen";
            this.Text = "StartScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label gameTitel;
        private System.Windows.Forms.Button storyModeButton;
        private System.Windows.Forms.Button endlessModeButton;
        private System.Windows.Forms.Button quitBotton;
        private System.Windows.Forms.Button InstructionsButton;
    }
}