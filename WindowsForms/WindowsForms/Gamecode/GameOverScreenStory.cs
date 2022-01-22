using System;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    public partial class GameOverScreenStory : Form
    {
        public bool playAgainClicked = false;

        public GameOverScreenStory()
        {
            InitializeComponent();
            this.FormClosed += closeGame;
        }

        internal static void closeGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void PlayAgain(object sender, EventArgs e)
        {
            StartScreen startScreen = new StartScreen();
            startScreen.Show();
            this.Visible = false;
            playAgainClicked = true;
        }

        private void Quit(object sender, EventArgs e)
        {
            closeGame(sender, e);
        }
    }
}
