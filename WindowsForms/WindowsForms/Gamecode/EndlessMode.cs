using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace WindowsForms.Gamecode
{
    public partial class EndlessMode : Form
    {
        #region Game(EndlessMode) variables
        Random rand = new Random();
        bool gameOver = false;
        int obstacleSpeed = 10;
        int inventoryChestCoins;
        internal Player player;
        #endregion

        public EndlessMode()
        {
            InitializeComponent();
            player = new Player(playerBox, 100);
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += loadInventory;
            this.FormClosing += saveInventory;
            GameReset();
        }

        #region Esc Menu (with safe/load)
        /// <summary>
        /// funcionality explained in StoryMode1
        /// </summary>

        private void formKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                MainGameTick.Stop();
                escMenu.BringToFront();
                escMenu.Visible = true;
            }
        }

        private void resumeClick(object sender, EventArgs e)
        {
            escMenu.Visible = false;
            MainGameTick.Start();
        }

        private void startScreenClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Gameplay will not be saved. Would you like to continue?", "", MessageBoxButtons.YesNo);
            saveInventory(this, new EventArgs());
            if (result == DialogResult.Yes)
            {
                StartScreen start = new StartScreen();
                start.Show();
                this.Visible = false;
            }
        }

        // Opens Coin Shop
        private void shopClick(object sender, EventArgs e)
        {
            shopMenu.BringToFront();
            shopMenu.Visible = true;
        }

        private void saveInventory(object sender, EventArgs e)
        {
            SystemSave.saveCoins(player.coins + inventoryChestCoins);
        }
        private void loadInventory(object sender, EventArgs e)
        {
            inventoryChestCoins = SystemSave.loadCoins();
        }
        #endregion

        #region Shop Menu
        private void superJumpBuy(object sender, EventArgs e)
        {
            const int cost = 100;
            if (buySomethingValid(cost))
            {
                player.jumpSpeed = player.jumpSpeed * 2;
            }
        }

        private void healthBuy(object sender, EventArgs e)
        {
            const int cost = 150;
            if (buySomethingValid(cost))
            {
                player.Hp = player.Hp * 2;
                healthBar.Maximum = player.Hp;
            }
        }

        private void dmgBuy(object sender, EventArgs e)
        {
            const int cost = 50;
            if (buySomethingValid(cost))
            {
                player.Dmg += 10;
            }
        }

        private void doneClick(object sender, EventArgs e)
        {
            MessageBox.Show("Power-Ups only active on current live! Your Death resets your Abilities!!!!");
            shopMenu.SendToBack();
            shopMenu.Visible = false;
        }

        private bool buySomethingValid(int cost)
        {
            if (inventoryChestCoins - cost >= 0)
            {
                inventoryChestCoins -= cost;
                MessageBox.Show("Thanks for the purchase!", "MONEY!!!!!", MessageBoxButtons.OK);
                return true;
            }
            else
            {
                MessageBox.Show("You dont have enough MONEY! Get more Money!", "MONEY!!!!!", MessageBoxButtons.OK);
                return false;
            }
        }
        #endregion

        #region EndlessMode Gameloop
        private void endlessTickTimer(object sender, EventArgs e)
        {
            background_move();
            scoreLabel.Text = "Score: " + player.score;
            coinCounter.Text = $": {player.coins}";
            inventoryCoins.Text = $"Tresure Chest: {inventoryChestCoins}";

            player.move(this);
            player.isOnGround = false;

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
                    inventoryChestCoins += player.coins;
                    GameReset();
                }
                else if (dialogresult == DialogResult.No)
                {
                    // player shall enter his name for highscore entry
                    NameInput nameInput = new NameInput();
                    dialogresult = nameInput.ShowDialog();

                    if (dialogresult == DialogResult.OK)
                    {
                        string name = nameInput.playerName.Text;
                        // processes the name and score and displays them
                        HighscoreList highscoreList = new HighscoreList(name, scoreLabel.Text);
                        highscoreList.Show();
                        Visible = false;
                    }
                    else { Application.Exit(); }
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "obstacleTree")
                    {
                        EnemySmall small = new EnemySmall((PictureBox)x);

                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            player.Hp -= small.Dmg;
                        }

                        //TODO spawn other types of enemys (use the enemy classes)
                        small.characterSpeed = obstacleSpeed;
                        // moves the enemy to the player
                        small.box.Left -= small.characterSpeed;

                        //if (small.box.Left < -50)
                        //{
                        //    small.box.Left = this.ClientSize.Width + rand.Next(100, 300) + (x.Width * 15);
                        //    // increment score for longer survival time
                        //}
                        randomPlacement(small.box, true);
                    }
                    if ((string)x.Tag == "plattform")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            player.IsOnGround = true;
                            player.MoveToTopOfPlatform(x.Top);
                        }
                    }
                }

                if (x is PictureBox && (string)x.Tag == "coins")
                {
                    if (playerBox.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        player.coins += 1;
                    }
                }
            }

            if (player.Hp < 20)
            {
                healthBar.ForeColor = System.Drawing.Color.Red;
            }

            if (player.score > 5) obstacleSpeed = 20;
            if (player.score > 15) obstacleSpeed = 30;
            if (player.score > 30) obstacleSpeed = 50;
        }

        private void GameReset()
        {
            player.Hp = 100;
            player.force = 12;
            player.jumpSpeed = 12;
            player.jumps = false;
            player.score = 0;
            player.coins = 0;
            obstacleSpeed = 10;
            scoreLabel.Text = "Score: " + player.score;
            playerBox.Image = Properties.Resources.idle;
            gameOver = false;
            playerBox.Location = player.defaultLocation;

            foreach (Control x in this.Controls)
            {
                // takes all pictureBoxes with the tag == "obstacleTree" and places them further to the right (outside the viewing screen)
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "obstacleTree")
                    {
                        x.Left = this.ClientSize.Width + rand.Next(450, 800) + (x.Width * 10);
                    }

                }
            }
            MainGameTick.Start();
        }
        #endregion

        #region Key Inputs
        bool holdDirection = true;
        private void keyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.jump();
                    //different sprites for holding a 'move' button
                    if (holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.walking;
                    }
                    break;
                case Keys.A:
                    player.Left(true);
                    if (holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.walkingLeft;
                    }
                    break;
                case Keys.S:
                    player.Down();
                    if (holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.walking;
                    }
                    break;
                case Keys.D:
                    player.Right(true);
                    if (holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.walking;
                    }
                    break;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    if (gameOver == true)
                        GameReset();
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

        #region endless backgroundScrolling

        void background_move()
        {

            if (background1.Left <= -1200)
                background1.Left = 1198;

            if (background2.Left <= -1200)
                background2.Left = 1198;

            background1.Left -= 2;
            background2.Left -= 2;

            Invalidate();
        }
        #endregion

        private void randomPlacement(PictureBox box, bool upDownPlacement)
        {
            if (box.Left < -50)
            {
                box.Left = this.ClientSize.Width + rand.Next(100, 300) + (box.Width * 15);
                if (upDownPlacement)
                {
                    box.Top = rand.Next(37, 367);
                }
                player.score++;
            }
        }
    }
}
