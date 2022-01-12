namespace WindowsForms.Gamecode
{
    partial class GameOverScreenEndless
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
            this.PlayAgainButton = new System.Windows.Forms.Button();
            this.quitBotton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayAgainButton
            // 
            this.PlayAgainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayAgainButton.Location = new System.Drawing.Point(384, 351);
            this.PlayAgainButton.Margin = new System.Windows.Forms.Padding(4);
            this.PlayAgainButton.Name = "PlayAgainButton";
            this.PlayAgainButton.Size = new System.Drawing.Size(309, 55);
            this.PlayAgainButton.TabIndex = 7;
            this.PlayAgainButton.Tag = "PlayAgainButton";
            this.PlayAgainButton.Text = "Play again";
            this.PlayAgainButton.UseVisualStyleBackColor = true;
            this.PlayAgainButton.Click += new System.EventHandler(this.PlayAgain);
            // 
            // quitBotton
            // 
            this.quitBotton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitBotton.Location = new System.Drawing.Point(384, 450);
            this.quitBotton.Margin = new System.Windows.Forms.Padding(4);
            this.quitBotton.Name = "quitBotton";
            this.quitBotton.Size = new System.Drawing.Size(309, 55);
            this.quitBotton.TabIndex = 8;
            this.quitBotton.Text = "Quit";
            this.quitBotton.UseVisualStyleBackColor = true;
            this.quitBotton.Click += new System.EventHandler(this.Quit);
            // 
            // GameOverScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsForms.Properties.Resources.GameOver2;
            this.ClientSize = new System.Drawing.Size(1053, 678);
            this.Controls.Add(this.quitBotton);
            this.Controls.Add(this.PlayAgainButton);
            this.Name = "GameOverScreen";
            this.Text = "GameOverScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PlayAgainButton;
        private System.Windows.Forms.Button quitBotton;
    }
}