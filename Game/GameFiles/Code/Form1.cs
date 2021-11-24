using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        Player player;
        public Form1()
        {
            InitializeComponent();

            player = new Player(playerBox);
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTick_Tick(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                player.move(new Point(0, -2));
            }
            if (e.KeyCode == Keys.A)
            {
                player.move(new Point(-2, 0));
            }
            if (e.KeyCode == Keys.S)
            {
                player.move(new Point(0, 2));
            }
            if (e.KeyCode == Keys.D)
            {
                player.move(new Point(2, 0));
            }
            //switch (e.KeyCode)
            //{
            //    case Keys.W: 
            //        player.move(new Point(0, -2));
            //        break;
            //    case Keys.A:
            //        player.move(new Point(-2, 0));
            //        break;
            //    case Keys.S:
            //        player.move(new Point(0, 2));
            //        break;
            //    case Keys.D:
            //        player.move(new Point(2, 0));
            //        break;
            //}
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

        }
    }
}
