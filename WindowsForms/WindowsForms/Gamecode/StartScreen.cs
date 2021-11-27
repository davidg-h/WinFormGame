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
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        internal static void closeGame(object sender, EventArgs e)
        {
            // terminates the app if player quits game
            Application.Exit();
        }

        private void openEndless(object sender, EventArgs e)
        {
            // opens the Endless-Mode and shows the screen
            EndlessMode endless = new EndlessMode();
            endless.Show();
            this.Visible = false;
        }

        private void openStory(object sender, EventArgs e)
        {
            StoryMode story = new StoryMode();
            story.Show();
            this.Visible = false;
        }

        private void quitGame(object sender, EventArgs e)
        {
            closeGame(sender, e);
        }
    }
}
