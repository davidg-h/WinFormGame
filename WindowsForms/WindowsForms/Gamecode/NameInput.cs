using System;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    /// <summary>
    /// player input for HighScoreList
    /// </summary>
    public partial class NameInput : Form
    {
        public NameInput()
        {
            InitializeComponent();
        }

        private void okClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Visible = false;
        }

        private void cancelClick(object sender, EventArgs e)
        {
            HighscoreList highscoreList = new HighscoreList("", "0");
            Visible = false;
            highscoreList.ShowDialog();
        }
    }
}
