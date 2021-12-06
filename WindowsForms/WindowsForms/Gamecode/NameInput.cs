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
