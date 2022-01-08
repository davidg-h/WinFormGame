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
    public partial class StoryMode2 : Level
    {
        public StoryMode2()
        {
            InitializeComponent();
            initializeParent(this);


            this.lvl = GameLvl.storyLvl_2;
            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += startTimer;
        }

      
        override protected void goToNextLevel()
        {
            MainGameTick.Stop();
            MessageBox.Show("Entering final Lvl", "", MessageBoxButtons.OK);
            StoryMode3 lvl3 = new StoryMode3();
            lvl3.Show();
            this.Visible = false;
        }

        private void MainGameTick_Tick_1(object sender, EventArgs e)
        {
            MainGameTick_Tick(sender, e);
        }


        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            timerTick(sender, e);
        }

        private void StoryMode2_KeyDown(object sender, KeyEventArgs e)
        {
            KeyIsDown(sender, e);
        }

        private void StoryMode2_KeyUp(object sender, KeyEventArgs e)
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
