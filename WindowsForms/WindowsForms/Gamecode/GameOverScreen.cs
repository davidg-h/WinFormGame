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
    public partial class GameOverScreen : Form
    {
        public bool playAgainClicked = false;
        public GameOverScreen()
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
