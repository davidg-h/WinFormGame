using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Media;

namespace WindowsForms.Gamecode
{
    public class Level : Form
    {
        #region Game variables
        public GameLvl lvl;

        protected bool gameOver;
        protected bool debuff;
        protected int debuffCounter = 0;
        protected int invulnerableCounter = 0;
        protected DateTime lastFrameTime = DateTime.Now; // for fps calculation
        protected PictureBox armorHeart1 = new PictureBox();
        protected PictureBox armorHeart2 = new PictureBox();
        protected Choices buyChoice = Choices.None;

        protected Panel pf;
        internal Timer MainGameTick;
        internal PictureBox playerBox;
        internal PictureBox obstacle;
        internal PictureBox heart1;
        internal PictureBox heart2;
        internal PictureBox heart3;
        internal PictureBox heart4;
        internal PictureBox heart5;
        internal PictureBox HealthPotionHUD;
        internal Label fpsLabel;
        internal MenuStrip escMenu;
        internal PictureBox destinyBox;
        internal Timer CountdownTimer;
        internal Label countdownLabel;
        internal Label coinCounter;
        //timer: min and sec
        internal (int min, int sec) timer = (5, 0);
        //internal List<RangeEnemy> rangeEnemyList;
        internal Player player;
        internal SpriteHandler coinHandler;
        internal SpriteHandler mushroomHandler;
        internal SpriteHandler eagleHandler;

        internal List<EnemySmall> mushroomList;
        internal List<EnemyFly> flyEnemyList;
        internal List<RangeEnemy> rangeEnemyList;
        internal List<PictureBox> obstacleList;

        internal int relativeXPositionOfPlayer = 0;

        Bitmap emptyHeart = new Bitmap(Properties.Resources.HeartEmpty);
        Bitmap halfHeart = new Bitmap(Properties.Resources.HeartHalf);
        Bitmap fullHeart = new Bitmap(Properties.Resources.Heart);

        Bitmap gEmptyHeart = new Bitmap(Properties.Resources.HeartEmpty);
        Bitmap gHalfHeart = new Bitmap(Properties.Resources.HeartHalf);
        Bitmap gFullHeart = new Bitmap(Properties.Resources.Heart);

        SoundPlayer coinSound;
        SoundPlayer enemyDeathSound;
        protected AxWMPLib.AxWindowsMediaPlayer gameMusicPlayer;


        protected enum Choices { Potion, Armor, Attack, None };

        #endregion

        public Level()
        {
            coinSound = new SoundPlayer(Properties.Resources.coinSound);
            enemyDeathSound = new SoundPlayer(Properties.Resources.EnemyDeath);
        }

        #region initializeLevel
        protected void LevelIsLoaded(object sender, EventArgs e)
        {
           
        }

        protected void initializeLevel(Level levelForm)
        {
            if (levelForm is StoryMode1)
            {
                StoryMode1 form = (StoryMode1)levelForm;
                MainGameTick = form.MainGameTick;
                playerBox = form.playerBox;
                obstacle = form.obstacle1;
                heart1 = form.heart1;
                heart2 = form.heart2;
                heart3 = form.heart3;
                heart4 = form.heart4;
                heart5 = form.heart5;
                fpsLabel = form.fpsLabel;
                HealthPotionHUD = form.HealthPotionHUD;
                escMenu = form.escMenu;
                destinyBox = form.destinyBox;
                CountdownTimer = form.CountdownTimer;
                countdownLabel = form.countdownLabel;
                coinCounter = form.coinCounter;
            }
            else if (levelForm is StoryMode2)
            {
                StoryMode2 form = (StoryMode2)levelForm;
                MainGameTick = form.MainGameTick;
                playerBox = form.playerBox;
                obstacle = form.obstacle;
                heart1 = form.heart1;
                heart2 = form.heart2;
                heart3 = form.heart3;
                heart4 = form.heart4;
                heart5 = form.heart5;
                fpsLabel = form.fpsLabel;
                HealthPotionHUD = form.HealthPotionHUD;
                escMenu = form.escMenu;
                destinyBox = form.destinyBox;
                CountdownTimer = form.CountdownTimer;
                countdownLabel = form.countdownLabel;
                coinCounter = form.coinCounter;
            }
            else if (levelForm is StoryMode3)
            {
                StoryMode3 form = (StoryMode3)levelForm;
                MainGameTick = form.MainGameTick;
                playerBox = form.playerBox;
                obstacle = form.obstacle;
                heart1 = form.heart1;
                heart2 = form.heart2;
                heart3 = form.heart3;
                heart4 = form.heart4;
                heart5 = form.heart5;
                fpsLabel = form.fpsLabel;
                HealthPotionHUD = form.HealthPotionHUD;
                escMenu = form.escMenu;
                destinyBox = form.destinyBox;
                CountdownTimer = form.CountdownTimer;
                countdownLabel = form.countdownLabel;
                coinCounter = form.coinCounter;
            }
            if (levelForm is EndlessMode)
            {
                EndlessMode form = (EndlessMode)levelForm;
                MainGameTick = form.MainGameTick;
                playerBox = form.playerBox;
                obstacle = form.obstacle;
                heart1 = form.heart1;
                heart2 = form.heart2;
                heart3 = form.heart3;
                heart4 = form.heart4;
                heart5 = form.heart5;
                fpsLabel = form.fpsLabel;
                HealthPotionHUD = form.HealthPotionHUD;
                escMenu = form.escMenu;
                destinyBox = form.destinyBox;
                CountdownTimer = form.CountdownTimer;
                countdownLabel = form.countdownLabel;
                coinCounter = form.coinCounter;

                player = new Player(playerBox, 100);
                player.gamemodeEndless = true;
            }
            else
            {
                player = new Player(playerBox, 100);
            }
            //creates the handler for animations (coins, enemys...)
            createAnimationHandlers();
            //Creates a Panel where every item is redrawn
            createDrawingPanel();
            //makes 'normal' screen invisible 
            makeDefaultDrawingInvisible();
            fillEnemyArrays();

            initializeGameMusicPlayer(levelForm);

            // create green types of the original hearts 2 stands for half heart, 3 for fullHearts 
            for (int y = 0; y < heart1.Height; y++)
            {
                for (int x = 0; x < heart1.Width; x++)
                {
                    Color p = emptyHeart.GetPixel(x, y);
                    Color p2 = halfHeart.GetPixel(x, y);
                    Color p3 = fullHeart.GetPixel(x, y);

                    int a = p.A;
                    int a2 = p2.A;
                    int a3 = p3.A;

                    int g = p.G;
                    int g2 = p2.G;
                    int g3 = p3.G;

                    gEmptyHeart.SetPixel(x, y, Color.FromArgb(a, 0, g, 0));
                    gHalfHeart.SetPixel(x, y, Color.FromArgb(a2, 0, g2, 0));
                    gFullHeart.SetPixel(x, y, Color.FromArgb(a3, 0, g3, 0));
                }
            }
        }

        private void initializeGameMusicPlayer(Level levelForm)
        {
            //free license: newgrounds , author: defensem3ch https://www.newgrounds.com/audio/listen/1074444
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoryMode1));

            gameMusicPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            gameMusicPlayer.BeginInit();
            gameMusicPlayer.Enabled = true;
            gameMusicPlayer.Location = new System.Drawing.Point(1102, 21);
            gameMusicPlayer.Name = "gameMusicPlayer";
            //gameMusicPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("gameMusicPlayer.OcxState")));
            gameMusicPlayer.Size = new System.Drawing.Size(75, 98);
            gameMusicPlayer.TabIndex = 71;
            gameMusicPlayer.Visible = false;
            string url;
            if (System.Diagnostics.Debugger.IsAttached)
                url = Application.StartupPath.Substring(0, Application.StartupPath.Length -10) + @"\resources\1074444_Superscience.mp3";
            else
                url = Application.StartupPath + @"\resources\1074444_Superscience.mp3";
            levelForm.Controls.Add(gameMusicPlayer);
            gameMusicPlayer.EndInit();
            gameMusicPlayer.URL = url;
            gameMusicPlayer.settings.setMode("loop", true);
            gameMusicPlayer.settings.volume = 4;    //changes Volume 
        }

        protected void createAnimationHandlers()
        {
            coinHandler = new SpriteHandler(Properties.Resources.coin);
            mushroomHandler = new SpriteHandler(Properties.Resources.shroomIdle);
            eagleHandler = new SpriteHandler(Properties.Resources.eagle);

        }
        protected void makeDefaultDrawingInvisible()
        {
            this.BackgroundImage = null;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Visible = false;
                }
                if (x is Label)
                {
                    x.Visible = false;
                }
            }
        }
        protected void createDrawingPanel()
        {
            pf = new Panel();
            pf.Location = new Point(0, 0);
            pf.Size = this.Size;
            Debug.Print(Size.ToString());
            pf.SendToBack();
            this.Controls.Add(pf);
        }
        protected void fillEnemyArrays()
        {
           
            //put all enemies in array for later uses
            mushroomList = new List<EnemySmall>();
            flyEnemyList = new List<EnemyFly>();
            rangeEnemyList = new List<RangeEnemy>();
            obstacleList = new List<PictureBox>();


            foreach (Control x in this.Controls)
            {
                if ((string)x.Tag == "obstacleTree")
                {
                    mushroomList.Add(new EnemySmall((PictureBox)x));
                }
                if ((string)x.Tag == "eagleEnemy")
                {
                    flyEnemyList.Add(new EnemyFly((PictureBox)x));
                }
                if ((string)x.Tag == "rangeEnemy")
                {
                    rangeEnemyList.Add(new RangeEnemy((PictureBox)x));
                }
                if ((string)x.Tag == "thorns")
                {
                    obstacleList.Add((PictureBox)x);
                }
            }
        }
        #endregion

        #region  fps


        protected string getFramesPerSecond()
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
        internal void formKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                MainGameTick.Stop();
                CountdownTimer.Stop();
                escMenu.BringToFront();
                escMenu.Visible = true;
            }
        }

        internal void resumeClick(object sender, EventArgs e)
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
        internal void startScreenClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Gameplay will not be saved. Would you like to continue?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                StartScreen start = new StartScreen();
                start.Show();
                this.Visible = false;
            }
        }

        internal void saveGameClick(object sender, EventArgs e)
        {
            SystemSave.saveGame(lvl, this);
        }

        internal void loadGameClick(object sender, EventArgs e)
        {
            PlayerData gameData = SystemSave.loadGame();
            MessageBox.Show("Your last game is loaded!", "Load game", MessageBoxButtons.OK);
            // passing the data back to the window and ts elements
            switch (gameData.lvl)
            {
                case GameLvl.storyLvl_1:
                    StoryMode1 lvl1 = new StoryMode1();
                    lvl1.timer = gameData.timer;
                    lvl1.playerBox.Location = gameData.location;
                    lvl1.player.coins = gameData.coins;
                    lvl1.player.Hp = gameData.hp;
                    lvl1.player.Dmg = gameData.dmg;
                    lvl1.player.armor1 = gameData.amor1;
                    lvl1.player.armor2 = gameData.amor2;
                    lvl1.player.invulnerable = gameData.invulnerable;
                    lvl1.player.potion = gameData.potion;
                    lvl1.MoveGameElements(gameData.relativePlayerCoord);
                    lvl1.Show();
                    break;
                case GameLvl.storyLvl_2:
                    StoryMode2 lvl2 = new StoryMode2();
                    lvl2.timer = gameData.timer;
                    lvl2.player.box.Location = gameData.location;
                    lvl2.player.coins = gameData.coins;
                    lvl2.player.Hp = gameData.hp;
                    lvl2.player.Dmg = gameData.dmg;
                    lvl2.player.armor1 = gameData.amor1;
                    lvl2.player.armor2 = gameData.amor2;
                    lvl2.player.invulnerable = gameData.invulnerable;
                    lvl2.player.potion = gameData.potion;
                    lvl2.MoveGameElements(gameData.relativePlayerCoord);
                    lvl2.Show();

                    break;
                case GameLvl.storyLvl_3:
                    StoryMode3 lvl3 = new StoryMode3();
                    lvl3.timer = gameData.timer;
                    lvl3.player.box.Location = gameData.location;
                    lvl3.player.coins = gameData.coins;
                    lvl3.player.Hp = gameData.hp;
                    lvl3.player.Dmg = gameData.dmg;
                    lvl3.player.armor1 = gameData.amor1;
                    lvl3.player.armor2 = gameData.amor2;
                    lvl3.player.invulnerable = gameData.invulnerable;
                    lvl3.player.potion = gameData.potion;
                    lvl3.MoveGameElements(gameData.relativePlayerCoord);
                    lvl3.Show();
                    break;
            }
            this.Visible = false;
        }
        #endregion

        #region Countdown Timer
        // initialize timer
        protected void startTimer(object sender, EventArgs e)
        {
            countdownLabel.Text = $"{timer.min}:00";
            CountdownTimer.Start();
        }

        // logic for timer
        internal void timerTick(object sender, EventArgs e)
        {
            if (timer.min == 5)
            {
                timer.min -= 1;
                timer.sec = 59;
            }
            else
            {
                timer.sec -= 1;
                if (timer.sec == 0 && !gameOver)
                {
                    if (timer.min == 0)
                    {
                        CountdownTimer.Stop();
                        GameOver();
                    }
                    else
                    {
                        timer.min -= 1;
                        timer.sec = 59;
                    }
                }
            }

            if (timer.sec < 10) countdownLabel.Text = $"{timer.min}:0{timer.sec}";
            else countdownLabel.Text = $"{timer.min}:{timer.sec}";
        }
        #endregion

        #region GameLoop StoryMode
        internal void MainGameTick_Tick(object sender, EventArgs e)
        {
            coinHandler.updateSpriteEveryTimeCalled();
            mushroomHandler.updateSpriteEvery3thTimeCalled();
            eagleHandler.updateSpriteEvery3thTimeCalled();

            if (coinCounter != null)
                coinCounter.Text = $": {player.coins}";
            if (fpsLabel != null)
                fpsLabel.Text = "fps: " + getFramesPerSecond();

            player.move(this);

            ContactWithAnyObject();

            //if he falls out of the world
            fallPutOfTheWorld();

            //debuff check (also possible to change the debuff EFFECT here!)
            playerDebuffs();

            invulnerableFrames();

            //HP HUD
            Healthbar();
            //make the enemies move
            moveEnemys();
            if (player.goLeft && !player.obstacleLeft)
                MoveGameElements(player.characterSpeed);
            if (player.goRight && !player.obstacleRight)
                MoveGameElements(-player.characterSpeed);
            ShootWhenPlayerNear();
            //Move all GameElements
            background_move();

            Draw();
        }

        protected void invulnerableFrames()
        {
            if (player.invulnerable)
            {
                invulnerableCounter++;
            }
            if (invulnerableCounter > 10)
            {
                invulnerableCounter = 0;
                player.invulnerable = false;
            }
        }

        private void playerDebuffs()
        {
            if (debuff && debuffCounter <= 60)
            {
                player.Hp -= 5;
                debuffCounter++;
                if (debuffCounter > 60)
                {
                    debuffCounter = 0;
                    debuff = false;
                }
            }
        }

        protected void fallPutOfTheWorld()
        {
            if (player.box.Location.Y > 1550)
            {
                GameOver();
            }
        }

        internal void moveEnemys()
        {
            InRangeOfEnemy(flyEnemyList);
            foreach (EnemySmall mushroom in mushroomList)
            {
                mushroom.move(this);
            }
            foreach (EnemyFly eagle in flyEnemyList)
            {
                //chasing the player around as long as he is in range (see InRangeOfEnemy(flyEnemyArray);)
                if (eagle.chase)
                {
                    if (eagle.box.Left > playerBox.Left + 2)
                        eagle.box.Left -= 4;
                    else if (eagle.box.Left < playerBox.Left - 2)
                        eagle.box.Left += 4;
                    if (eagle.box.Top > playerBox.Top)
                        eagle.box.Top -= 4;
                    else if (eagle.box.Top < playerBox.Top - 20)
                        eagle.box.Top += 4;
                }

                //move in a normal pattern without attacking the player
                eagle.move(this);
            }
        }

        protected void ContactWithAnyObject()
        {
            //get updated to correct value below
            player.IsOnGround = false;
            player.obstacleRight = false;
            player.obstacleLeft = false;
            foreach (Control x in this.Controls)
            {
                //TODO spawn of enemys (use the enemy classes)
                if (x is PictureBox)
                {
                    if (player.box.Bounds.IntersectsWith(x.Bounds))
                    {
                        if ((string)x.Tag == "obstacleTree")
                        {
                            player.Hp -= mushroomList[0].Dmg;
                            if ((((PictureBox)x).Location.X - player.box.Location.X) > 0)
                            {
                                player.obstacleRight = true;
                            }
                            else
                            {
                                player.obstacleLeft = true;
                            }

                        }
                        if ((string)x.Tag == "eagleEnemy")
                        {

                            if ((((PictureBox)x).Location.X - player.box.Location.X) > 0)
                            {
                                player.obstacleRight = true;
                            }
                            else
                            {
                                player.obstacleLeft = true;
                            }
                            player.Hp -= flyEnemyList[0].Dmg;
                        }
                        if ((string)x.Tag == "rangeEnemy")
                        {

                            if ((((PictureBox)x).Location.X - player.box.Location.X) > 0)
                            {
                                player.obstacleRight = true;
                            }
                            else
                            {
                                player.obstacleLeft = true;
                            }
                        }

                        if ((string)x.Tag == "shot")
                        {
                            player.Hp -= RangeEnemyShot.ShotDmg;
                        }

                        if ((string)x.Tag == "platform")
                        {
                            if (player.box.Top < x.Top)
                            {
                                player.IsOnGround = true;

                                player.MoveToTopOfPlatform(x.Top);
                            }
                            else
                            {
                                if ((x.Location.X - player.box.Location.X) > 0)
                                {
                                    player.obstacleRight = true;
                                }
                                else
                                {
                                    player.obstacleLeft = true;
                                }
                            }
                        }
                        if ((string)x.Tag == "coins")
                        {
                            coinSound.Play();
                            x.Tag = "coins.collected"; //coins are not drawn anymore
                            player.coins += 1;
                        }

                        if ((string)x.Tag == "thorns")
                        {
                            debuff = true;
                            debuffCounter = 0;
                            if (player.box.Location.X < x.Location.X)
                                player.obstacleRight = true;
                            else
                                player.obstacleLeft = true;
                        }
                        if ((string)x.Tag == "merchant")
                        {
                            //depends on buy choice: consumable, not healable hearts or permanent damage upgrade
                            if (buyChoice == Choices.Potion && player.coins >= 10)
                            {
                                player.coins -= 10;
                                HealthPotionHUD.Image = Properties.Resources.health_potion;
                                buyChoice = Choices.None;
                                player.potion = true;
                            }
                            if (buyChoice == Choices.Armor && player.coins >= 20)
                            {
                                player.coins -= 20;
                                player.armor1 = true;
                                player.armor2 = true;
                                // add the picture of armor hearts onto screen
                                armorHeart1.SizeMode = PictureBoxSizeMode.AutoSize;
                                armorHeart2.SizeMode = PictureBoxSizeMode.AutoSize;
                                armorHeart1.Location = new Point(210, 5);
                                armorHeart2.Location = new Point(250, 5);
                                armorHeart1.Image = Properties.Resources.Heart_Armor;
                                armorHeart2.Image = Properties.Resources.Heart_Armor;
                                buyChoice = Choices.None;
                                this.Controls.Add(armorHeart1);
                                this.Controls.Add(armorHeart2);

                            }
                            if (buyChoice == Choices.Attack && player.coins >= 20)
                            {
                                player.coins -= 20;
                                player.Dmg += 1;
                                buyChoice = Choices.None;
                            }
                        }

                        if ((string)x.Tag == "finish")
                        {
                            goToNextLevel();
                        }
                    }
                    if (player.isAttacking && player.swordHitRange.IntersectsWith(x.Bounds))
                    {
                        EnemyDamage(x);
                    }
                }
            }
        }

        virtual protected void goToNextLevel()
        {
            MainGameTick.Stop();
            gameMusicPlayer.Ctlcontrols.stop(); // pauses game Music
            MessageBox.Show("Entering Lvl 2", "", MessageBoxButtons.OK);
            StoryMode2 lvl2 = new StoryMode2();
            lvl2.Show();
            this.Visible = false;
        }

        //is overwritten in Endlessmode
        internal virtual void GameOver()
        {
            MainGameTick.Stop();
            CountdownTimer.Stop();
            gameMusicPlayer.Ctlcontrols.stop(); // pauses game Music
            gameOver = false;
            GameOverScreenStory gameOverScreen = new GameOverScreenStory();
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
        internal void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.jump();
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
                case Keys.Space:
                    player.attack();
                    break;
                case Keys.H:
                    if (player.potion)
                    {
                        HealthPotionHUD.Image = Properties.Resources.health_potion_empty;
                        player.potion = false;
                        player.Hp += 40;
                    }
                    break;
                //key inputs for buy choice with the merchant
                case Keys.Z:
                    buyChoice = Choices.Potion;
                    break;
                case Keys.U:
                    buyChoice = Choices.Armor;
                    break;
                case Keys.I:
                    buyChoice = Choices.Attack;
                    break;
            }
        }

        internal void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.R:
                    //if (gameOver == true)
                    //Restart();
                    break;
                case Keys.D:
                    player.Right(false);
                    break;
                case Keys.A:
                    player.Left(false);
                    break;
                case Keys.S:
                    player.goDown = false;
                    break;
                case Keys.W:
                    player.jumps = false;
                    break;
                case Keys.Space:
                    player.isAttacking = false;
                    break;

            }

        }
        #endregion

        #region PlayerAttack
        public void PlayerAttack(string direction)
        {
            if (direction == "right")
            {
                player.attack();
                //playerBox.Image = Properties.Resources.attackingRight;

                //playerBox.Tag = "attackingRight";

            }
            else if (direction == "left") // must be improved
            {
                player.attack();
                //playerBox.Image = Properties.Resources.attackingLeft;
                //playerBox.Tag = "attackingLeft";

            }
        }
        #endregion

        #region CreateEnemyList RangeEnemy
        //protected void CreateEnemysLIst()
        //{
        //    rangeEnemyList = new List<RangeEnemy>();
        //    foreach (var item in RangeEnemy.picturesAndLocationArray)
        //    {
        //        RangeEnemy nextEnemy = new RangeEnemy(10, 1);
        //        this.rangeEnemyList.Add(nextEnemy);
        //        this.Controls.Add(nextEnemy.box);
        //    }

        //}
        #endregion

        #region ShootingOfEnemy RangeEnemy
        protected void ShootWhenPlayerNear()
        {
            foreach (var rangeEnemy in this.rangeEnemyList)
            {
                if (rangeEnemy.box != null && (rangeEnemy.box.Left - player.box.Right < 200 && player.box.Right < rangeEnemy.box.Left)&&rangeEnemy.Hp>0)
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
        protected void Draw()
        {

            Bitmap bufl = new Bitmap(pf.Width, pf.Height);
            using (Graphics g = Graphics.FromImage(bufl))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, pf.Width, pf.Height));

                g.DrawImage(backgroundlayer, new Rectangle(new Point(backgroundCoordX, 0), new Size( new Point(backgroundlayer.Width, this.Size.Height))));
                g.DrawImage(backgroundlayer, new Rectangle(new Point(backgroundCoordX2, 0), new Size(new Point(backgroundlayer.Width, this.Size.Height))));

                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        string tag = (string)x.Tag;
                        if (((PictureBox)x).Image == null)
                        {
                            //do nothing
                            continue;
                        }
                        else if (tag == "coins")
                        {
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(coinHandler.CurrentSprite, destRect);
                        }
                        else if (tag == "obstacleTree")
                        {
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(mushroomHandler.CurrentSprite, destRect);
                        }
                        else if (tag == "rangeEnemy")
                        {
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(((PictureBox)x).Image, destRect);
                        }
                        else if (tag == "eagleEnemy")
                        {
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(eagleHandler.CurrentSprite, destRect);
                        }
                        else if (tag == "player")
                        {
                            if (player.isAttacking)
                            {
                                Rectangle destRect;
                                if (player.facingRight)
                                {
                                    destRect = new Rectangle(x.Location, player.currentImage.Size);
                                }
                                else
                                {
                                    destRect = new Rectangle(new Point(x.Location.X - 60, x.Location.Y), player.currentImage.Size);
                                }
                                g.DrawImage(player.currentImage, destRect);
                            }
                            else
                                g.DrawImage(player.currentImage, player.box.Location);
                        }
                        else if (tag != "coins.collected" && tag != "background")
                        {
                            Rectangle destRect = new Rectangle(x.Location, x.Size);
                            g.DrawImage(((PictureBox)x).Image, destRect);
                        }
                    }
                }
                //all the Laabels and Text is drawn on Top of Game Elements
                foreach (Control x in this.Controls)
                {
                    if (x is Label)
                    {
                        g.DrawString(x.Text, new Font("Unispace", 18), new SolidBrush(Color.MintCream), x.Location);
                    }
                }
                pf.CreateGraphics().DrawImageUnscaled(bufl, 0, 0);
            }
        }
        #endregion

        #region DestroyableObstacle()

        void ChangeThorns(Control x)
        {
            foreach (PictureBox obstacle in obstacleList)
            {
                if ((string)x.Tag == "destroyedThorns" && x.Name == obstacle.Name)
                    obstacle.Image = Properties.Resources.PoisountPlant_destroyed;
            }
        }
        #endregion

        #region Background

        protected Image backgroundlayer = Properties.Resources.Background;
        protected int backgroundCoordX = 0, backgroundCoordX2 = Properties.Resources.Background.Width;

        protected void background_move()
        {
            if (backgroundCoordX < -backgroundlayer.Width)
                backgroundCoordX = backgroundlayer.Width - 20;
            if (backgroundCoordX > backgroundlayer.Width)
                backgroundCoordX = -backgroundlayer.Width + 20;

            if (backgroundCoordX2 < -backgroundlayer.Width)
                backgroundCoordX2 = backgroundlayer.Width - 20;
            if (backgroundCoordX2 > backgroundlayer.Width)
                backgroundCoordX2 = -backgroundlayer.Width + 20;


            if (player.goRight && !player.obstacleRight)
            {
                backgroundCoordX -= 3;
                backgroundCoordX2 -= 3;
            }
            if (player.goLeft && !player.obstacleLeft)
            {
                backgroundCoordX += 3;
                backgroundCoordX2 += 3;
            }
        }
        #endregion

        #region Moving GameElements
        protected void MoveGameElements(int moveAmount)
        {
            relativeXPositionOfPlayer += moveAmount;
            foreach (Control x in this.Controls)
            {
                //moving the elements with the wanted Tags with the movement of the player
                //new object that need to be moved: enter "Tag" in this if statement
                if (x is PictureBox)
                {
                    string tag = (string)x.Tag;
                    if (tag == "platform" || tag == "obstacleTree" || tag == "coins" || tag == "finish" || tag == "......" || tag == "thorns" || tag == "eagleEnemy" || tag == "rangeEnemy" || tag == "shopHUD" || tag == "merchant" || tag == "destroyedThorns")
                    {
                        x.Location = new Point(x.Location.X + moveAmount, x.Location.Y);
                    }
                }
            }
            //the starting point of the flying enemy has to be scrolled too!
            foreach (var item in flyEnemyList)
            {
                item.startingPoint.X -= moveAmount;
            }
        }
        #endregion

        #region enemy Death
        protected void EnemyDamage(Control x)
        {
            //find the same Enemy in array that is interacted with, then dmg phase and remove if HP of enemy is 0
            //TODO if more enemies added: write another foreach with the enemyType array
            foreach (var enemy in mushroomList)
            {
                if (enemy.box.Name == x.Name)
                {
                    enemy.Hp -= player.Dmg;
                    if (enemy.Hp <= 0)
                    {
                        enemyDeathSound.Play();
                        debuff = false;
                        this.Controls.Remove(x);
                    }
                }
            }
            foreach (var enemy in flyEnemyList)
            {
                if (enemy.box.Name == x.Name)
                {
                    enemy.Hp -= player.Dmg;
                    if (enemy.Hp <= 0)
                    {
                        debuff = false;
                        this.Controls.Remove(x);
                    }
                }

            }
            foreach (var enemy in rangeEnemyList)
            {
                if (enemy.box.Name == x.Name)
                {
                    enemy.Hp -= player.Dmg;
                    if (enemy.Hp <= 0)
                    {
                        debuff = false;

                        this.Controls.Remove(x);
                    }
                }
            }
            if ((string)x.Tag == "thorns")
            {
                x.BackgroundImage = Properties.Resources.PoisountPlant_destroyed;
                x.Tag = "destroyedThorns";
                ChangeThorns(x);
                player.obstacleRight = true;
            }
        }

        #endregion

        #region FlyEnemy 

        //method to test if the player is in the Fly enemies range
        void InRangeOfEnemy(List<EnemyFly> flyEnemy)
        {
            foreach (EnemyFly enemy in flyEnemy)
            {
                if (enemy.onStart)
                {
                    enemy.startingPoint.X = enemy.box.Location.X;
                    enemy.startingPoint.Y = enemy.box.Location.Y;
                }

                //aggro range can be changed here                  vvv                                                   vvv
                if (enemy.box.Location.X <= playerBox.Location.X + 200 && enemy.box.Location.X >= playerBox.Location.X - 150)
                {
                    enemy.chase = true;
                }
                else
                {
                    enemy.chase = false;
                }
            }
        }
        #endregion

        #region Healthbar
        protected void Healthbar()
        {
            if (!player.armor2)
                this.Controls.Remove(armorHeart2);
            if (!player.armor1)
                this.Controls.Remove(armorHeart1);

            //if HP fall on a specific count, then change the container to empty or half empty
            if (player.Hp >= 100)
            {
                ChangeHeartContainer(heart5);
                ChangeHeartContainer(heart4);
                ChangeHeartContainer(heart3);
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);
            }
            if (player.Hp < 100 && player.Hp > 80)
            {
                ChangeHeartContainer(heart5, "half");
                ChangeHeartContainer(heart4);
                ChangeHeartContainer(heart3);
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 80 && player.Hp > 70)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4);
                ChangeHeartContainer(heart3);
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 70 && player.Hp > 60)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "half");
                ChangeHeartContainer(heart3);
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 60 && player.Hp > 50)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3);
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 50 && player.Hp > 40)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3, "half");
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 40 && player.Hp > 30)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3, "empty");
                ChangeHeartContainer(heart2);
                ChangeHeartContainer(heart1);

            }
            if (player.Hp <= 30 && player.Hp > 20)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3, "empty");
                ChangeHeartContainer(heart2, "half");
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 20 && player.Hp > 10)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3, "empty");
                ChangeHeartContainer(heart2, "empty");
                ChangeHeartContainer(heart1);
            }
            if (player.Hp <= 10 && player.Hp > 0)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3, "empty");
                ChangeHeartContainer(heart2, "empty");
                ChangeHeartContainer(heart1, "half");
            }
            //end game if hp is zero
            if (player.Hp <= 0)
            {
                ChangeHeartContainer(heart5, "empty");
                ChangeHeartContainer(heart4, "empty");
                ChangeHeartContainer(heart3, "empty");
                ChangeHeartContainer(heart2, "empty");
                ChangeHeartContainer(heart1, "empty");
                GameOver();
            }
        }

        //method for changing the Image in the specific PictureBox --> empty,half and full
        protected void ChangeHeartContainer(PictureBox container, string heart = "full")
        {
            if (heart == "empty")
            {
                //if debuff i active change color of the hearts to green
                if (debuff)
                    container.Image = gEmptyHeart;
                else
                    container.Image = Properties.Resources.HeartEmpty;
            }
            if (heart == "half")
                if (debuff)
                    container.Image = gHalfHeart;
                else
                    container.Image = Properties.Resources.HeartHalf;
            if (heart == "full")
                if (debuff)
                    container.Image = gFullHeart;
                else
                    container.Image = Properties.Resources.Heart;
        }




        #endregion
    }
}
