using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    public partial class StoryMode : Form
    {
        Player player;
        bool gameOver;

        public StoryMode()
        {
            InitializeComponent();

            player = new Player(playerBox);
            this.FormClosed += StartScreen.closeGame;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTick_Tick(object sender, EventArgs e)
        {
            player.move(this);

            if (player.health > 1)
            {
                healthBar.Value = Convert.ToInt32(player.health);
            }
            else
            {
                MainGameTick.Stop();
                gameOver = true;
                //MessageBox.Show("You Died!!!"+Environment.NewLine+ "Press OK to play again");

                //Restart();

                DialogResult dialogresult = MessageBox.Show("You Died!!!" + Environment.NewLine + "Press Yes to play again", "", MessageBoxButtons.YesNo);

                if (dialogresult == DialogResult.Yes)
                {
                    Restart();
                }
                else if (dialogresult == DialogResult.No)
                {
                    Application.Exit();
                }

            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "obstacleTree")
                {


                    if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                    {
                        player.health -= 1;
                    }
                }
            }
            if (player.health < 20)
            {
                healthBar.ForeColor = System.Drawing.Color.Red;
            }
            if (playerBox.Bounds.IntersectsWith(destinyBox.Bounds))
            {
                MainGameTick.Stop();
                MessageBox.Show("Congratulations, You won!!" + Environment.NewLine + "Press OK to play again");
                Restart();

            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (!player.jumping)
                        player.jumping = true;
                    break;
                case Keys.A:
                    player.goLeft = true;
                    break;
                case Keys.S:
                    player.goDown = true;
                    break;
                case Keys.D:
                    player.goRight = true;
                    break;
                case Keys R:
                    Restart();
                    break;
            }
        }
        private void Restart()
        {
            gameOver = false;
            StoryMode newWindow = new StoryMode();
            newWindow.Show();
            this.Hide();

        }
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    if (gameOver == true)
                        Restart();
                    break;
                case Keys.D:
                    player.goRight = false;
                    break;
                case Keys.A:
                    player.goLeft = false;
                    break;
                case Keys.S:
                    player.goDown = false;
                    break;
            }

            if (player.jumping == true)
            {
                player.jumping = false;
            }
        }
        private void StartGame(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void OpenInstructions(object sender, EventArgs e)
        {

        }
    }
}
