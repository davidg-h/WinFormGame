using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Windows;

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

        PictureBox backgroundBox;
        SpriteHandler coinHandler;
        SpriteHandler mushroomHandler;
        #endregion

        public EndlessMode()
        {
            InitializeComponent();
            player = new Player(playerBox, 100);
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += loadInventory;
            this.FormClosing += saveInventory;

            coinHandler = new SpriteHandler(Properties.Resources.coin);
            mushroomHandler = new SpriteHandler(Properties.Resources.shroomIdle);
            backgroundBox = (PictureBox)Controls.Find("background1", false)[0];
            //Creates a Panel where every item is redrawn
            pf.Location = new Point(0, 0);
            pf.Size = this.Size;
            pf.SendToBack();
            this.BackgroundImage = null;
            //makes 'normal' screen invisible 
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = false;
                }
            }
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
                //healthBar.Maximum = player.Hp;
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
            Draw();

            coinHandler.updateSpriteEveryTimeCalled();
            mushroomHandler.updateSpriteEvery3thTimeCalled();

            background_move();
            scoreLabel.Text = "Score: " + player.score;
            coinCounter.Text = $": {player.coins}";
            inventoryCoins.Text = $"Tresure Chest: {inventoryChestCoins}";

            player.moveEndlessmode(this);
            player.isOnGround = false;

            if (player.Hp > 0)
            {
                Healthbar();
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
                    if ((string)x.Tag == "platform")
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
                    if (playerBox.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Tag = "coins.collected";
                        player.coins += 1;
                    }
                }
            }

            if (player.score > 5) obstacleSpeed = 20;
            if (player.score > 15) obstacleSpeed = 30;
            if (player.score > 30) obstacleSpeed = 50;
        }

        private void GameReset()
        {
            player.Hp = 100;
            player.score = 0;
            player.coins = 0;
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

                    if ((string)x.Tag == "health")
                    {
                        ((PictureBox)x).Image = WindowsForms.Properties.Resources.Heart;
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

        #region draw
        void Draw()
        {

            Bitmap bufl = new Bitmap(pf.Width, pf.Height);
            using (Graphics g = Graphics.FromImage(bufl))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, pf.Width, pf.Height));
                //g.DrawImage(backgroundlayer, new Point(backgroundCoordX, 0));
                g.DrawImage(backgroundBox.Image, Point.Empty);
                g.DrawImage(player.currentImage, playerBox.Location);
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox )
                    {
                        string tag = (string)x.Tag;
                        Rectangle destRect = new Rectangle(x.Location, x.Size);

                        if (((PictureBox)x).Image == null)
                        {
                            g.FillRectangle(new SolidBrush(x.BackColor) , destRect);
                        }
                        else if (tag == "coins")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            g.DrawImage(coinHandler.CurrentSprite, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                        else if (tag == "obstacleTree")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            g.DrawImage(mushroomHandler.CurrentSprite, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                        else if (tag != "player" && tag != "coins.collected" && tag != "background")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            g.DrawImage(((PictureBox)x).Image, destRect, srcRect, GraphicsUnit.Pixel);
                            //g.DrawImage(((PictureBox)x).Image, x.Location);
                        }
                    }
                }
                pf.CreateGraphics().DrawImageUnscaled(bufl, 0, 0);
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

        #region Healthbar
        void Healthbar()
        {
            //if HP fall on a specific count, then change the container to empty or half empty
            if (player.Hp < 100)
            {
                ChangeHeartContainer(heart5, false);
            }
            if (player.Hp < 90)
            {
                ChangeHeartContainer(heart5, true);
            }
            if (player.Hp < 80)
            {
                ChangeHeartContainer(heart4, false);
            }
            if (player.Hp < 70)
            {
                ChangeHeartContainer(heart4, true);
            }
            if (player.Hp < 60)
            {
                ChangeHeartContainer(heart3, false);
            }
            if (player.Hp < 50)
            {
                ChangeHeartContainer(heart3, true);
            }
            if (player.Hp < 40)
            {
                ChangeHeartContainer(heart2, false);
            }
            if (player.Hp < 30)
            {
                ChangeHeartContainer(heart2, true);
            }
            if (player.Hp < 20)
            {
                ChangeHeartContainer(heart1, false);
            }
        }

        void ChangeHeartContainer(PictureBox container, bool empty)
        {
            if (empty)
            {
                container.Image = Properties.Resources.HeartEmpty;
            }
            else
                container.Image = Properties.Resources.HeartHalf;
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
