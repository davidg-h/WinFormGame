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
        #region Game variables
        private int min = 5;
        private int sec;
        public GameLvl lvl = GameLvl.storyLvl_1;
        internal Player player;
        bool gameOver;
        #endregion

        public StoryMode1()
        {
            InitializeComponent();

            player = new Player(playerBox, 100);
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += startTimer;
        }

        #region Esc Menu (with safe/load)
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
                    // player.score = gameData.score; for endless mode
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

        #region Countdown Timer
        private void startTimer(object sender, EventArgs e)
        {
            countdownLabel.Text = $"{min}:00";
            CountdownTimer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (min == 5)
            {
                min -= 1;
                sec = 59;
            }
            else
            {
                sec -= 1;
                if (sec == 0 && !gameOver)
                {
                    if (min == 0) { gameOver = true; CountdownTimer.Stop(); }
                    else { min -= 1; sec = 59; }
                }
            }

            if (sec < 10) countdownLabel.Text = $"{min}:0{sec}";
            else countdownLabel.Text = $"{min}:{sec}";
        }
        #endregion

        #region GameLoop StoryMode
        private void MainGameTick_Tick(object sender, EventArgs e)
        {

            coinCounter.Text = $": {player.coins}";

            player.move(this);
            player.IsOnGround = false; //gets updated to correct value below



            if (player.Hp > 1 && !gameOver)
            {
                healthBar.Value = Convert.ToInt32(player.Hp);
            }
            else
            {
                MainGameTick.Stop();
                gameOver = true;
                GameOver();
            }

            ContactWithAnyObject();

            if (player.Hp < 20)
            {
                healthBar.ForeColor = System.Drawing.Color.Red;
            }

           

            background_move();

            //Move all GameElements
            if (player.goRight == true)
            {
                MoveGameElements("back");
            }
            if (player.goLeft == true && backgroundCoordX < 0)
            {
                MoveGameElements("forward");
            }
        }
        public void ContactWithAnyObject()
        {
            foreach (Control x in this.Controls)
            {
                //TODO spawn of enemys (use the enemy classes)
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "obstacleTree")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            if ((((PictureBox)x).Location.X - playerBox.Location.X) > 0)
                            {
                                player.obstacleRight = true;
                            }
                            else
                            {
                                player.obstacleLeft = true;
                            }
                            EnemySmall small = new EnemySmall((PictureBox)x);
                            player.Hp -= small.Dmg;
                        }
                    }
                    if ((string)x.Tag == "platform")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            player.IsOnGround = true;
                            player.MoveToTopOfPlatform(x.Top);
                        }
                    }
                    if ((string)x.Tag == "coins")
                    {
                        if (playerBox.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            player.coins += 1;
                        }
                    }
                }
             
            }
            if (playerBox.Bounds.IntersectsWith(destinyBox.Bounds))
            {
                MainGameTick.Stop();
                YouWon();
            }
        }
        internal void Restart()
        {
            gameOver = false;
            StoryMode1 newWindow = new StoryMode1();
            newWindow.Show();
            this.Hide();
        }

        internal void GameOver()
        {
            gameOver = false;
            GameOverScreen gameOverScreen = new GameOverScreen();
            gameOverScreen.Show();
            this.Hide();
        }
        internal void YouWon()
        {
            gameOver = true;
            WinnerScreen winnerScreen = new WinnerScreen();
            winnerScreen.Show();
            this.Hide();
        }



        private void StartGame(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Key Inputs
        bool holdDirection = true;
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.jump();
                    //different sprites for holding a 'move' button
                    if (holdDirection)
                    {
                        playerBox.Image = Properties.Resources.walking;

                    }
                    break;
                case Keys.A:
                    player.Left(true);
                    if (holdDirection)
                    {
                        playerBox.Image = Properties.Resources.walkingLeft;
                        holdDirection = false;

                    }
                    break;
                case Keys.S:
                    player.Down();
                    if (holdDirection)
                    {
                        playerBox.Image = Properties.Resources.walking;
                        holdDirection = false;

                    }
                    break;
                case Keys.D:
                    player.Right(true);
                    if (holdDirection)
                    {
                        playerBox.Image = Properties.Resources.walking;
                        holdDirection = false;

                    }
                    break;
            }
        }

        internal void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    if (gameOver == true)
                        Restart();
                    break;
                case Keys.D:
                    player.Right(false);

                    //also switch to another sprite when a key is let go of
                    if (!holdDirection)
                    {
                        holdDirection = true;
                        playerBox.Image = Properties.Resources.idle;
                    }
                    break;
                case Keys.A:
                    player.Left(false);
                    if (!holdDirection)
                    {
                        holdDirection = true;
                        playerBox.Image = Properties.Resources.idle;
                    }
                    break;
                case Keys.S:
                    player.goDown = false;
                    if (!holdDirection)
                    {
                        holdDirection = true;
                        playerBox.Image = Properties.Resources.idle;
                    }
                    break;
            }

            if (player.jumps == true)
            {
                player.jumps = false;
            }
        }
        #endregion

        #region Background

        Image backgroundlayer = Properties.Resources.Background;
        int backgroundCoordX = 0, backgroundCoordX2 = 1600;
        private void StoryMode1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(backgroundlayer, backgroundCoordX, 0);
            e.Graphics.DrawImage(backgroundlayer, backgroundCoordX2, 0);

        }


        void background_move()
        {
            if (backgroundCoordX <= -1600)
                backgroundCoordX = 1600;

            if (backgroundCoordX2 <= -1600)
                backgroundCoordX2 = 1600;


            if (player.goRight)
            {
                backgroundCoordX -= 2;
                backgroundCoordX2 -= 2;
            }
            if (player.goLeft && backgroundCoordX < 0)
            {
                backgroundCoordX += 2;
                backgroundCoordX2 += 2;
            }

            Invalidate();
        }

        #endregion

        #region Moving GameElements
        private void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                //moving the elements with the wanted Tags with the movement of the player
                //new object that need to be moved: enter "Tag" in this if statement
                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "obstacleTree" || x is PictureBox && (string)x.Tag == "coins" || x is PictureBox && (string)x.Tag == "finish" || x is PictureBox && (string)x.Tag == "......")
                {
                    if (direction == "back")
                    {
                        x.Left -= player.characterSpeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += player.characterSpeed;
                    }
                }
            }
        }

        #endregion

    }
}
