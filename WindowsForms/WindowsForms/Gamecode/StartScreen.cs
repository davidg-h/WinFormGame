using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    public partial class StartScreen : Form
    {

        AxWMPLib.AxWindowsMediaPlayer gameMusicPlayer;
        Random r = new Random();
        SoundPlayer clickSound;
        public StartScreen()
        {
            InitializeComponent();
            this.FormClosed += closeGame;
            initializeGameMusicPlayer();
            clickSound = new SoundPlayer(Properties.Resources.MenueKlick);
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {
            gameMusicPlayer.Ctlcontrols.play();

        }

        /// <summary>
        /// terminates the app if player quits game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void closeGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openEndless(object sender, EventArgs e)
        {
            // opens the Endless-Mode and shows the screen
            clickSound.Play();
            gameMusicPlayer.Ctlcontrols.stop();
            EndlessMode endless = new EndlessMode();
            endless.Show();
            this.Visible = false;
        }

        private void openStory(object sender, EventArgs e)
        {
            clickSound.Play();
            gameMusicPlayer.Ctlcontrols.stop();
            StoryMode1 story = new StoryMode1();
            story.Show();
            this.Visible = false;
        }

        private void quitGame(object sender, EventArgs e)
        {
            clickSound.Play();
            gameMusicPlayer.Ctlcontrols.stop();
            closeGame(sender, e);
        }

        private void Instructions(object sender, EventArgs e)
        {
            clickSound.Play();
            Instructions instructions = new Instructions();
            instructions.Show();
        }

        private void initializeGameMusicPlayer()
        {
            //newgrounds , author: Hyenaedon https://www.newgrounds.com/audio/listen/987597
            gameMusicPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            gameMusicPlayer.BeginInit();
            gameMusicPlayer.Enabled = true;
            gameMusicPlayer.Location = new System.Drawing.Point(1102, 21);
            gameMusicPlayer.Name = "gameMusicPlayer";
            gameMusicPlayer.Size = new System.Drawing.Size(75, 98);
            gameMusicPlayer.TabIndex = 71;
            gameMusicPlayer.Visible = false;
            string url;
            if (System.Diagnostics.Debugger.IsAttached)
                url = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + @"\resources\";
            else
                url = Application.StartupPath + @"\resources\";
            url += "987597_woodland-sprites.mp3";
            this.Controls.Add(gameMusicPlayer);
            gameMusicPlayer.EndInit();
            gameMusicPlayer.URL = url;
            gameMusicPlayer.settings.setMode("loop", true);
            gameMusicPlayer.settings.volume = 7;    //changes Volume 
        }

        void Credits()
        {
            CreditsLabel.Text =
                "Created by: \n" +
                "David Nguyen, \n" +
                "Elisabeth Volkinshtein, \n" +
                "Vincent Neumann, \n" +
                "Patrick Huber \n" +
                "\n" +
                "Music by: \n" +
                "Hyenaedon \n" +
                "defensem3ch";
            CreditsLabel.Location = new Point(30, 150);
        }

        private void CreditsBox_Click(object sender, EventArgs e)
        {
            int x = r.Next(0, Width - 30);
            int y = r.Next(0, Height - 50);
            if (x > Width / 3 && x < Width * 2 / 3 + 50 && y > Height / 4 )
                CreditsBox_Click(sender, e);
            else
            {
                Color c = CreditsBox.BackColor;
                if (c.G < 20)
                    Credits();
                else
                    CreditsBox.BackColor = Color.FromArgb(c.R, c.G - 20, c.B - 20);
                CreditsBox.Location = new Point(x, y);
            }
        }
    }
}
