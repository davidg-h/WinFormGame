using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace WindowsForms.Gamecode
{
    public partial class StoryMode1 : Form
    {
        #region Game variables
        private int min = 5;
        private int sec;
        public GameLvl lvl = GameLvl.storyLvl_1;
        List<RangeEnemy> rangeEnemyList;
        internal Player player;
        bool gameOver;
        DateTime lastFrameTime = DateTime.Now; // for fps calculation
        SpriteHandler coinHandler;
        SpriteHandler mushroomHandler;
        SpriteHandler eagleHandler;
        EnemySmall[] mushroomArray;
        EnemyFly[] eagleArray;
        bool debuff;
        int debuffCounter = 0;
        bool obstacleInWay;

        #endregion

        public StoryMode1()
        {
            InitializeComponent();
            CreateEnemysLIst();

            player = new Player(playerBox, 300);
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += startTimer;
            int eagleEnemyCounter = 0;
            int mushroomEnemyCounter = 0;

            coinHandler = new SpriteHandler(global::WindowsForms.Properties.Resources.coin);
            mushroomHandler = new SpriteHandler(Properties.Resources.shroomIdle);
            eagleHandler = new SpriteHandler(Properties.Resources.eagle);
            //Creates a Panel where every item is redrawn
            pf.Location = new Point(0, 0);
            pf.Size = this.Size;
            pf.SendToBack();
            this.BackgroundImage = null;
            //makes 'normal' screen invisible 
            this.BackgroundImage = null;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = false;
                }
                //counter: how many mushroom enemies
                if ((string)x.Tag == "obstacleTree")
                {
                    mushroomEnemyCounter++;
                }
                if ((string)x.Tag == "eagleEnemy")
                {
                    eagleEnemyCounter++;
                }
                if (x is Label)
                {
                    x.Visible = false;
                }
            }
            //put all mushroom enemies in array
            mushroomArray = new EnemySmall[mushroomEnemyCounter];
            mushroomEnemyCounter = 0;

            eagleArray = new EnemyFly[eagleEnemyCounter];
            eagleEnemyCounter = 0;
            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "obstacleTree")
                {
                    mushroomArray[mushroomEnemyCounter] = new EnemySmall((PictureBox)x);
                    mushroomEnemyCounter++;
                }
                if ((string)x.Tag == "eagleEnemy")
                {
                    eagleArray[eagleEnemyCounter] = new EnemyFly((PictureBox)x);
                    eagleEnemyCounter++;
                }
            }
        }

        #region performance boost / fps
        //Doublebuffer the Grafics = remove flickering
        //EDIT: dont turn that on or it will flicker
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams handleParam = base.CreateParams;
        //        handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
        //        return handleParam;
        //    }
        //}

        private string getFramesPerSecond()
        {
            string fps = Convert.ToInt32(1000.0 / (DateTime.Now - lastFrameTime).TotalMilliseconds).ToString();
            lastFrameTime = DateTime.Now;
            return fps;
        }
        #endregion

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
                MainGameTick.Stop();
                CountdownTimer.Stop();
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
            MainGameTick.Start();
            CountdownTimer.Start();
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
            MessageBox.Show("Your last game is loaded!", "Load game", MessageBoxButtons.OK);
            // passing the data back to the window and ts elements
            switch (gameData.lvl)
            {
                case GameLvl.storyLvl_1:
                    playerBox.Location = gameData.location;
                    // player.score = gameData.score; for endless mode
                    player.coins = gameData.coins;
                    player.Hp = gameData.hp;
                    player.Dmg = gameData.dmg;
                    MainGameTick.Start();
                    CountdownTimer.Start();
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
            Draw();

            coinHandler.updateSpriteEveryTimeCalled();
            mushroomHandler.updateSpriteEvery3thTimeCalled();
            eagleHandler.updateSpriteEvery3thTimeCalled();

            coinCounter.Text = $": {player.coins}";
            fpsLabel.Text = "fps: " + getFramesPerSecond();

            player.move(this);
            player.IsOnGround = false; //gets updated to correct value below

            ContactWithAnyObject();

            if (playerBox.Location.Y > 550)
            {
                MainGameTick.Stop();
                gameOver = true;
                GameOver();
            }

            //debuff check (also possible to change the debuff EFFECT here!)
            if (debuff && debuffCounter <= 20)
            {
                player.Hp -= 1;
                debuffCounter++;
            }


            if (obstacleInWay)
                player.goRight = false;

            //HP HUD
            Healthbar();

            //make the enemies move
            foreach (EnemySmall mushroom in mushroomArray)
            {
                mushroom.move(this);
            }
            foreach (EnemyFly eagle in eagleArray)
            {
                eagle.move(this);
            }

            if (player.goRight == true)
            {
                MoveGameElements("back");
            }
            if (player.goLeft == true)
            {
                MoveGameElements("forward");
            }
            ShootWhenPlayerNear();
            //Move all GameElements
            background_move();

        }
        public void ContactWithAnyObject()
        {
            player.obstacleLeft = false;
            player.obstacleRight = false;
            foreach (Control x in this.Controls)
            {
                //TODO spawn of enemys (use the enemy classes)
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "obstacleTree")
                    {
                        if (playerBox.Bounds.IntersectsWith(x.Bounds))
                        {
                            if ((((PictureBox)x).Location.X - playerBox.Location.X) > 0)
                            {
                                player.obstacleRight = true;
                            }
                            else
                            {
                                player.obstacleLeft = true;
                            }
                            player.Hp -= mushroomArray[0].Dmg;
                        }
                    }
                    if ((string)x.Tag == "eagleEnemy")
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
                            player.Hp -= eagleArray[0].Dmg;
                        }
                    }
                    if ((string)x.Tag == "shot")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            player.Hp -= RangeEnemyShot.ShotDmg;
                        }
                    }
                    if ((string)x.Tag == "platform")
                    {
                        if (playerBox.Bounds.IntersectsWith(x.Bounds))
                        {
                            if(playerBox.Top  < x.Top)
                            {
                                player.IsOnGround = true;

                                player.MoveToTopOfPlatform(x.Top);
                            }
                            else
                            {
                                if ((x.Location.X - playerBox.Location.X) > 0)
                                {
                                    player.obstacleRight = true;
                                }
                                else
                                {
                                    player.obstacleLeft = true;
                                }
                            }
                        }
                    }
                    if ((string)x.Tag == "coins")
                    {
                        if (playerBox.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Tag = "coins.collected";
                            player.coins += 1;
                        }
                    }
                    if ((string)x.Tag == "rangeEnemy")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            RangeEnemy foundRangeEnemy = rangeEnemyList.Find(rangeEnemy => rangeEnemy.box.Name == (string)x.Name);
                            player.Hp -= foundRangeEnemy.Dmg;
                            if (player.attacking)
                            {
                                foundRangeEnemy.Hp -= player.Dmg;
                                if (foundRangeEnemy.Hp < 1)
                                {
                                    this.Controls.Remove(x);

                                    rangeEnemyList.Remove(foundRangeEnemy);
                                    //AddNextEnemy();

                                }
                            }
                        }
                    }

                    if ((string)x.Tag == "thorns")
                    {
                        if (((PictureBox)x).Bounds.IntersectsWith(playerBox.Bounds))
                        {
                            debuff = true;
                            debuffCounter = 0;
                            obstacleInWay = true;
                            playerBox.Left -= 20;
                        }
                        else
                            obstacleInWay = false;
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
        string facing = "right";
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
                        facing = "left";

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
                    if (obstacleInWay)
                        break;
                    player.Right(true);
                    if (holdDirection)
                    {
                        playerBox.Image = Properties.Resources.walking;
                        holdDirection = false;
                        facing = "right";

                    }
                    break;
                case Keys.Space:
                    if (!gameOver)
                    {
                        player.attacking = true;
                        PlayerAttack(facing);

                    }
                    break;
            }
        }

        internal void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    //if (gameOver == true)
                    Restart();
                    break;
                case Keys.D:
                    player.Right(false);

                    //also switch to another sprite when a key is let go of
                    if (!holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.idle;
                    }
                    break;
                case Keys.A:
                    player.Left(false);
                    if (!holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.idle;
                    }
                    break;
                case Keys.S:
                    player.goDown = false;
                    if (!holdDirection)
                    {
                        holdDirection = false;
                        playerBox.Image = Properties.Resources.idle;
                    }
                    break;
                case Keys.Space:
                    if (!gameOver)
                    {
                        player.attacking = false;
                        playerBox.Image = Properties.Resources.walking;

                    }
                    break;
            }

            if (player.jumps == true)
            {
                player.jumps = false;
            }
        }
        #endregion

        #region PlayerAttack
        public void PlayerAttack(string direction)
        {
            if (direction == "right")
            {
                playerBox.Image = Properties.Resources.attackingRight;
                playerBox.Tag = "attackingRight";
               
            }
            else if (direction == "left") // must be improved
            {
                playerBox.Image = Properties.Resources.attackingLeft;
                playerBox.Tag = "attackingLeft";
                
            }
        }
        #endregion

        #region CreateEnemyList RangeEnemy
        public void CreateEnemysLIst()
        {
            rangeEnemyList = new List<RangeEnemy>();
            foreach (var item in RangeEnemy.picturesAndLocationArray)
            {
                RangeEnemy nextEnemy = new RangeEnemy(10, 1);
                this.rangeEnemyList.Add(nextEnemy);
                this.Controls.Add(nextEnemy.box);
            }

        }
        #endregion

        #region ShootingOfEnemy RangeEnemy
        public void ShootWhenPlayerNear()
        {
            foreach (var rangeEnemy in this.rangeEnemyList)
            {
                if (rangeEnemy.box != null && (rangeEnemy.box.Left - player.box.Right < 200 && player.box.Right < rangeEnemy.box.Left))
                {
                    rangeEnemy.ShootShot(this, "left");
                }
                else if (rangeEnemy.Shot != null)
                {
                    rangeEnemy.Shot.DeleteShot();
                }
            }
        }
        #endregion

        #region draw
        void Draw()
        {

            Bitmap bufl = new Bitmap(pf.Width, pf.Height);
            using (Graphics g = Graphics.FromImage(bufl))
            {
                //g.FillRectangle(Brushes.Black, new Rectangle(0, 0, pf.Width, pf.Height));

                g.DrawImage(backgroundlayer, new Rectangle(new Point(0,0), this.Size), new Rectangle(new Point(-backgroundCoordX, 0), new Size(backgroundlayer.Width / 2, backgroundlayer.Height)), GraphicsUnit.Pixel);
                g.DrawImage(player.currentImage, playerBox.Location);
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        string tag = (string)x.Tag;
                        if (tag == "coins")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(coinHandler.CurrentSprite, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                        else if (tag == "obstacleTree")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(mushroomHandler.CurrentSprite, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                        else if (tag == "attackingRight" || tag == "attackingLeft")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(((PictureBox)x).Image, destRect, srcRect, GraphicsUnit.Pixel);

                        }
                        else if (tag == "rangeEnemy")
                        {
                            RangeEnemy rangeEnemy = rangeEnemyList.Find(zm => zm.box.Name == (string)x.Name);
                            // rangeEnemy.box.Left -= rangeEnemy.characterSpeed;  -> moves enemy towards player
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(((PictureBox)x).Image, destRect, srcRect, GraphicsUnit.Pixel);


                        }
                        else if (tag == "eagleEnemy")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(eagleHandler.CurrentSprite, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                        else if (tag != "player" && tag != "coins.collected")
                        {
                            Rectangle srcRect = new Rectangle(new Point(0, 0), ((PictureBox)x).Image.Size);
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(((PictureBox)x).Image, destRect, srcRect, GraphicsUnit.Pixel);
                            //g.DrawImage(((PictureBox)x).Image, x.Location);
                        }
                    }
                    if (x is Label)
                    {
                        g.DrawString(x.Text, new Font("Unispace", 11), new SolidBrush(Color.Black) , x.Location ) ;
                    }
                }
                pf.CreateGraphics().DrawImageUnscaled(bufl, 0, 0);
            }
        }
        #endregion

        #region Background

        Image backgroundlayer = Properties.Resources.Background;
        int backgroundCoordX = 0, backgroundCoordX2 = 1600;




        void background_move()
        {
            //if (backgroundCoordX <= -1600)
            //    backgroundCoordX = 1600;

            //if (backgroundCoordX2 <= -1600)
            //    backgroundCoordX2 = 1600;


            if (player.goRight && !player.obstacleRight)
            {
                backgroundCoordX -= 2;
            }
            if (player.goLeft && ! player.obstacleLeft)
            {
                backgroundCoordX += 2;
            }

            //Invalidate();
        }
        #endregion

        #region Moving GameElements
        private void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                //moving the elements with the wanted Tags with the movement of the player
                //new object that need to be moved: enter "Tag" in this if statement
                if (direction == "back" && !player.obstacleRight)
                {
                    if (x is PictureBox)
                    {
                        string tag = (string)x.Tag;
                        if (tag == "platform" || tag == "obstacleTree" || tag == "coins" || tag == "finish" || tag == "......" || tag == "thorns" || tag == "eagleEnemy")
                        {

                            x.Left -= player.characterSpeed;
                        }
                    }

                }      
                if (direction == "forward" && !player.obstacleLeft)
                {
                    if (x is PictureBox)
                    {
                        string tag = (string)x.Tag;
                        if (tag == "platform" || tag == "obstacleTree" || tag == "coins" || tag == "finish" || tag == "......" || tag == "thorns" || tag == "eagleEnemy")
                        {
                            x.Left += player.characterSpeed;
                        }
                    }
                }
            }
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
            //end game if hp is zero
            if (player.Hp <= 0)
            {
                MainGameTick.Stop();
                gameOver = true;
                GameOver();
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
    }
}
