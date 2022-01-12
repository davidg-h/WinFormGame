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
    public partial class GameOverScreenEndless : Form
    {
        public GameOverScreenEndless()
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
        }
        private void Quit(object sender, EventArgs e)
        {
            //// player shall enter his name for highscore entry

            DialogResult dialogresult;
            NameInput nameInput = new NameInput();
            dialogresult = nameInput.ShowDialog();

            if (dialogresult == DialogResult.OK)
            {
                string name = nameInput.playerName.Text;
                // processes the name and score and displays them
                EndlessMode endlessMode = new EndlessMode();
                HighscoreList highscoreList = new HighscoreList(name, endlessMode.label1.Text);
                highscoreList.Show();
                Visible = false;
            }
            else { closeGame(sender, e); }
        }
    }
}
