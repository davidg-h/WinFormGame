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
    public partial class EndlessMode : Form
    {
        internal Player player;
        bool gameOver;

        public EndlessMode()
        {
            InitializeComponent();
            this.FormClosed += StartScreen.closeGame;
        }
    }
}
