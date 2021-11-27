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
    public partial class StoryMode1 : Form
    {
        public GameLvl lvl = GameLvl.storyLvl_1;
        internal Player player;
        bool gameOver;

        public StoryMode1()
        {
            InitializeComponent();

            player = new Player(playerBox, 100);
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
        }

        #region Esc Menu
        /// <summary>
        /// escMenu shall be visible when esc is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                escMenu.BringToFront();
                escMenu.Visible = true;
            }
        }

        private void menuEastereggClick(object sender, EventArgs e)
        {
            //TODO add a textBox for commands for easteregg
        }

        private void resumeClick(object sender, EventArgs e)
        {
            escMenu.Visible = false;
        }

        /// <summary>
        /// link to Start Screen with consent of player 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startScreenClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Gameplay will not be saved. Would you like to continue?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                StartScreen start = new StartScreen();
                start.Show();
                this.Visible = false;
            }
        }

        private void saveGameClick(object sender, EventArgs e)
        {
            SystemSave.saveGame(lvl, this);
        }

        private void loadGameClick(object sender, EventArgs e)
        {
            PlayerData gameData = SystemSave.loadGame();

            // passing the data back to the window and ts elements
            switch (gameData.lvl)
            {
                case GameLvl.storyLvl_1:
                    playerBox.Location = gameData.location;
                    player.score = gameData.score;
                    player.coins = gameData.coins;
                    player.Hp = gameData.hp;
                    player.Dmg = gameData.dmg;
                    escMenu.Visible = false;
                    break;
                case GameLvl.storyLvl_2:
                    //TODO
                    //zb: 
                    // StoryMode2 mode2 = new StoryMode2();
                    // mode2.playerBox.Location = gameData.playerLocation;
                    // ...
                    // mode2.Show();
                    // this.Visible = false;
                    break;
                case GameLvl.storyLvl_3:
                    //TODO same as above
                    break;
            }
        }
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void MainGameTick_Tick(object sender, EventArgs e)
        {
            player.move(this);

            if (player.Hp > 1)
            {
                healthBar.Value = Convert.ToInt32(player.Hp);
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
                //TODO spawn of enemys (use the enemy classes)
                if (x is PictureBox && x.Tag == "obstacleTree")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                    {
                        EnemySmall small = new EnemySmall((PictureBox)x);
                        player.Hp -= small.Dmg;
                    }
                }
            }
            if (player.Hp < 20)
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
                case Keys.R:
                    Restart();
                    break;
            }
        }
        private void Restart()
        {
            gameOver = false;
            StoryMode1 newWindow = new StoryMode1();
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
