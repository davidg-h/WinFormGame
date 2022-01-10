using System;
using System.Collections;
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
    public partial class EndlessMode : Level
    {
        #region Game(EndlessMode) variables
        Random rand = new Random();
        int obstacleSpeed = 10;
        int inventoryChestCoins;
        int largestXKoord;
        List<List<PictureBox>> chapterList;
        List<PictureBox> pictureBoxList;

        PictureBox backgroundBox;
        #endregion

        public EndlessMode()
        {
            InitializeComponent();
            initializeLevel(this);
            pictureBoxList = getPictureBoxes();
            chapterList = chapters();

            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += loadInventory;
            this.FormClosing += saveInventory;
            backgroundBox = (PictureBox)Controls.Find("background1", false)[0];

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
                ScoreTimer.Stop();
                CoinSpawnTimer.Stop();
                ChapterSpawnTimer.Stop();
                escMenu.BringToFront();
                escMenu.Visible = true;
            }
        }

        private void resumeClick(object sender, EventArgs e)
        {
            escMenu.Visible = false;
            MainGameTick.Start();
            ScoreTimer.Start();
            CoinSpawnTimer.Start();
            ChapterSpawnTimer.Start();
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
            //this.Location = playerBox.Location;    //adds some fun
            coinHandler.updateSpriteEveryTimeCalled();
            mushroomHandler.updateSpriteEvery3thTimeCalled();

            background_move();
            scoreLabel.Text = "Score: " + player.score;
            coinCounter.Text = $": {player.coins}";
            inventoryCoins.Text = $"Tresure Chest: {inventoryChestCoins}";

            player.moveEndlessmode(this);
            player.isOnGround = false;

            //fallPutOfTheWorld();

            // "invinceble frames" as long as invulnerable is on true: no dmg can be taken (as to see in player.Hp property)
            invulnerableFrames();

            //keeps health status up to date 
            Healthbar();
            ContactWithAnyObject();
            MoveGameElements(-obstacleSpeed);


            if (player.score > 5) obstacleSpeed = 20;
            if (player.score > 15) obstacleSpeed = 30;
            if (player.score > 30) obstacleSpeed = 50;
            if (player.score > 60) obstacleSpeed = 80;
            if (player.score > 100) obstacleSpeed = 120;

            Draw();

            destroyPB();
            largestXKoord = getLargestXKoord();
        }

        //resets the game and gives the last coin count to the player bank
        override internal void GameOver()
        {
            MainGameTick.Stop();
            ScoreTimer.Stop();
            CoinSpawnTimer.Stop();
            ChapterSpawnTimer.Stop();
            gameOver = true;

            DialogResult dialogresult = MessageBox.Show("You Died!!!" + Environment.NewLine + "Press Yes to play again", "", MessageBoxButtons.YesNo);

            if (dialogresult == DialogResult.Yes)
            {
                DeathGameReset(player.coins);
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

        private void DeathGameReset(int coinCount)
        {
            EndlessMode endless = new EndlessMode();
            endless.Show();
            endless.player.coins += coinCount;
            this.Visible = false;
        }

        private void GameReset()
        {
            player.Hp += 100;
            player.score = 0;
            player.coins = 0;
            scoreLabel.Text = "Score: " + player.score;
            playerBox.Image = Properties.Resources.idle;
            gameOver = false;
            playerBox.Location = player.defaultLocation;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "health")
                    {
                        ((PictureBox)x).Image = WindowsForms.Properties.Resources.Heart;
                    }
                }
            }
            MainGameTick.Start();
            ScoreTimer.Start();
            CoinSpawnTimer.Start();
            ChapterSpawnTimer.Start();
        }
        #endregion

        #region Key Inputs
        private void keyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.jump();
                    //different sprites for holding a 'move' button
                    break;
                case Keys.A:
                    player.Left(true);
                    break;
                case Keys.S:
                    player.Down();
                    break;
                case Keys.D:
                    player.Right(true);
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
                    break;
                case Keys.A:
                    player.Left(false);
                    break;
                case Keys.W:
                    player.jumps = false;
                    break;
                case Keys.S:
                    break;
            }
        }
        #endregion

        #region draw
        int backGround1KoordX = 0;
        int backGround2KoordX = Properties.Resources.Background.Width - 6;
        void Draw()
        {
            Bitmap bufl = new Bitmap(pf.Width, pf.Height);
            using (Graphics g = Graphics.FromImage(bufl))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, pf.Width, pf.Height));
                g.DrawImage(backgroundBox.Image, new Rectangle(new Point(0, 0), this.Size), new Rectangle(new Point(-backGround1KoordX, 0), new Size(backgroundBox.Width, backgroundBox.Height)), GraphicsUnit.Pixel);
                g.DrawImage(backgroundBox.Image, new Rectangle(new Point(0, 0), this.Size), new Rectangle(new Point(-backGround2KoordX, 0), new Size(backgroundBox.Width, backgroundBox.Height)), GraphicsUnit.Pixel);
                g.DrawImage(player.currentImage, playerBox.Location);
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        string tag = (string)x.Tag;
                        Rectangle destRect = new Rectangle(x.Location, x.Size);

                        if (((PictureBox)x).Image == null)
                        {
                            g.FillRectangle(new SolidBrush(x.BackColor), destRect);
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
            backGround1KoordX -= 2;
            backGround2KoordX -= 2;
            //resets the background if not in ClientSize
            if (backGround1KoordX == -backgroundBox.Width)
            {
                backGround1KoordX = backgroundBox.Width - 6;
            }
            if (backGround2KoordX == -backgroundBox.Width)
            {
                backGround2KoordX = backgroundBox.Width - 6;
            }
        }
        #endregion

        #region random Placement of objects
        internal void chapterSpawnTick(object sender, EventArgs e)
        {
            if (player.score > 10) ChapterSpawnTimer.Interval = 1000;
            if (player.score > 25) ChapterSpawnTimer.Interval = 750;
            if (player.score > 60) ChapterSpawnTimer.Interval = 500;
            randomPlacement();
        }


        private void randomPlacement()
        {
            int x = largestXKoord;

            int chapter = rand.Next(1, 5);
            switch (chapter)
            {
                case 1:
                    createChapter(chapterList[0], x, 470, rand.Next(50, 150));
                    break;
                case 2:
                    createChapter(chapterList[1], x, 1185, rand.Next(45, 125));
                    break;
                case 3:
                    createChapter(chapterList[2], x, 1639, rand.Next(60, 180));
                    break;
                case 4:
                    createChapter(chapterList[3], x, 2131, rand.Next(30, 100));
                    break;
                default:
                    break;
            }
        }

        private void createChapter(List<PictureBox> chapter, int x, int boxLeft, int randomNumber)
        {
            foreach (PictureBox box in chapter)
            {
                // create box
                PictureBox boxNew = createPB(box);
                // new Location is after the last object with a random buffer(=randomNumber)
                // box.Location.X - boxLeft: to keep the ratio of the objects to each other the same and position it to the left edge of the client area
                if (x <= ClientSize.Width)
                {
                    boxNew.Location = new Point(ClientSize.Width + (box.Location.X - boxLeft) + randomNumber, box.Location.Y);
                }
                else
                {
                    boxNew.Location = new Point(x + (box.Location.X - boxLeft) + randomNumber, box.Location.Y);
                }
                // adds box to the window
                this.Controls.Add(boxNew);
            }
        }
        /// <summary>
        /// saving the chapters in a list
        /// </summary>
        /// <returns></returns>
        private List<List<PictureBox>> chapters()
        {
            List<PictureBox> chapter1 = new List<PictureBox>();
            List<PictureBox> chapter2 = new List<PictureBox>();
            List<PictureBox> chapter3 = new List<PictureBox>();
            List<PictureBox> chapter4 = new List<PictureBox>();
            foreach (PictureBox picture in pictureBoxList)
            {
                switch (picture.AccessibleName)
                {
                    case "chapter1":
                        chapter1.Add(createPB(picture));
                        break;
                    case "chapter2":
                        chapter2.Add(createPB(picture));
                        break;
                    case "chapter3":
                        chapter3.Add(createPB(picture));
                        break;
                    case "chapter4":
                        chapter4.Add(createPB(picture));
                        break;
                    default:
                        break;
                }
            }
            return new List<List<PictureBox>>() { chapter1, chapter2, chapter3, chapter4 };
        }

        private PictureBox createPB(PictureBox pictureBox)
        {
            PictureBox box = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(box)).BeginInit();
            box.Name = pictureBox.Name;
            box.AccessibleName = pictureBox.AccessibleName;
            box.AccessibleDescription = pictureBox.AccessibleDescription;
            box.Margin = pictureBox.Margin;
            box.Tag = pictureBox.Tag;
            box.Image = pictureBox.Image;
            box.Visible = true;
            box.Enabled = true;
            box.Size = pictureBox.Size;
            box.SizeMode = pictureBox.SizeMode;
            box.Location = pictureBox.Location;
            return box;
        }
        /// <summary>
        /// gets the largest x-Koord for random placement of lvl chapters
        /// </summary>
        /// <returns></returns>
        private int getLargestXKoord()
        {
            int largestXKoord = 0;
            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    if (largestXKoord < x.Location.X && (string)x.Tag != "coins" && (string)x.Tag != "background")
                    {
                        largestXKoord = x.Location.X + x.Width;
                    }
                }
            }
            return largestXKoord;
        }
        /// <summary>
        /// returns a list with all pictureBoxes in the window
        /// </summary>
        /// <returns></returns>
        private List<PictureBox> getPictureBoxes()
        {
            List<PictureBox> picBoxList = new List<PictureBox>();
            foreach (Control x in Controls)
            {
                if (x is PictureBox)
                {
                    picBoxList.Add((PictureBox)x);
                }
            }
            return picBoxList;
        }
        /// <summary>
        /// destroys pictureBoxes for performance
        /// </summary>
        private void destroyPB()
        {
            foreach (Control pb in Controls)
            {
                if (pb is PictureBox)
                {
                    if (pb.Location.X + pb.Size.Width < 0)
                    {
                        this.Controls.Remove(pb);
                    }
                }
            }
        }
        #endregion

        #region coinSpawn Timer
        internal void coinSpawnTimerTick(object sender, EventArgs e)
        {
            spawnCoins();
        }

        private void spawnCoins()
        {
            // create coin
            PictureBox coinSpawn = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(coinSpawn)).BeginInit();

            coinSpawn.Tag = "coins";
            coinSpawn.Image = Properties.Resources.coin;
            coinSpawn.Visible = true;
            coinSpawn.Size = new Size(38, 39);
            coinSpawn.SizeMode = PictureBoxSizeMode.StretchImage;
            coinSpawn.Location = new Point(ClientSize.Width + rand.Next(0, 50), rand.Next(ClientSize.Height));
            // adds coin to the window
            this.Controls.Add(coinSpawn);
        }
        #endregion

        #region ScoreTimer
        /// <summary>
        /// score linearly increase with increasing obstacleSpeed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void scoreTimerTick(object sender, EventArgs e)
        {
            player.score++;
            if (player.score > 5) ScoreTimer.Interval = 3000;
            if (player.score > 15) ScoreTimer.Interval = 2250;
            if (player.score > 30) ScoreTimer.Interval = 1500;
            if (player.score > 60) ScoreTimer.Interval = 1000;
            if (player.score > 100) ScoreTimer.Interval = 500;
        }
        #endregion
    }
}
