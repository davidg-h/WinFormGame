using System;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    public partial class WinnerScreen : Form
    {
        public WinnerScreen()
        {
            InitializeComponent();
            this.FormClosed += closeGame;
        }

        internal static void closeGame(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Quit(object sender, EventArgs e)
        {
            closeGame(sender, e);
        }

        private void PlayAgain(object sender, EventArgs e)
        {
            StartScreen startScreen = new StartScreen();
            startScreen.Show();
            this.Visible = false;
        }
    }
}
