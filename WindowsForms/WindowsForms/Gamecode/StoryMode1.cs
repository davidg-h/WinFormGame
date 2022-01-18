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
    public partial class StoryMode1 : Level
    {
        public StoryMode1()
        {
            InitializeComponent();
            initializeLevel(this);

            this.lvl = GameLvl.storyLvl_1;
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += startTimer;
        }
        override protected void goToNextLevel()
        {
            MainGameTick.Stop();
            gameMusicPlayer.Ctlcontrols.stop(); // pauses game Music
            MessageBox.Show("Entering second Lvl", "", MessageBoxButtons.OK);
            StoryMode2 lvl2 = new StoryMode2();
            lvl2.player.coins = this.player.coins;
            lvl2.player.potion = this.player.potion;
            lvl2.player.armor1 = this.player.armor1;
            lvl2.player.armor2 = this.player.armor2;
            lvl2.player.Dmg = this.player.Dmg;
            lvl2.Show();
            this.Visible = false;
        }
        internal void Restart()
        {
            MainGameTick.Stop();
            CountdownTimer.Stop();
            gameMusicPlayer.Ctlcontrols.stop(); // pauses game Music
            gameOver = false;
            StoryMode1 newWindow = new StoryMode1();
            newWindow.Show();
            this.Hide();
        }

        private void MainGameTick_Tick_1(object sender, EventArgs e)
        {
            MainGameTick_Tick(sender, e);
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timerTick(sender, e);
        }

        private void StoryMode1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyIsDown(sender, e);
            switch (e.KeyCode)
            {
                case Keys.R:
                    Restart();
                    break;
            }
        }

        private void StoryMode1_KeyUp(object sender, KeyEventArgs e)
        {
            KeyIsUp(sender, e);
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resumeClick(sender, e);
        }

        private void startScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startScreenClick(sender, e);
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveGameClick(sender, e);
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadGameClick(sender, e);
        }

        private void StoryMode1_Load(object sender, EventArgs e)
        {
            LevelIsLoaded(sender, e);

        }

        private void MediaPlayer_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
