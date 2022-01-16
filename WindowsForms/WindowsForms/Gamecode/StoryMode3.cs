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
    public partial class StoryMode3 : Level
    {
        public StoryMode3()
        {
            InitializeComponent();
            initializeLevel(this);
            this.lvl = GameLvl.storyLvl_3;
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += startTimer;
        }


        internal void Restart()
        {
            MainGameTick.Stop();
            CountdownTimer.Stop();
            gameMusicPlayer.Ctlcontrols.stop(); // pauses game Music
            gameOver = false;
            StoryMode3 newWindow = new StoryMode3();
            newWindow.Show();
            this.Hide();
        }
        override protected void goToNextLevel()
        {
            MainGameTick.Stop();
            gameMusicPlayer.Ctlcontrols.stop(); // pauses game Music
            YouWon();
        }

        private void MainGameTick_Tick_1(object sender, EventArgs e)
        {
            MainGameTick_Tick(sender, e);
        }


        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timerTick(sender, e);
        }

        private void StoryMode3_KeyDown(object sender, KeyEventArgs e)
        {
            KeyIsDown(sender, e);
            switch (e.KeyCode)
            {
                case Keys.R:
                    //if (gameOver == true)
                    Restart();
                    break;
            }
        }

        private void StoryMode3_KeyUp(object sender, KeyEventArgs e)
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
    }
}
