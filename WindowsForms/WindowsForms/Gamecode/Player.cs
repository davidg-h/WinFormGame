using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsForms

{
    class Player
    {
        PictureBox playerBox;
        public bool goRight, goLeft, goDown, jumping;
        int playerspeed = 10;
        int jumpspeed = 16;
        public int health = 100;
        int force = 8;
        public Player(PictureBox playerBox)
        {
            this.playerBox = playerBox;
        }

        public void move(Form form1)
        {
            if (goLeft && playerBox.Left > 30)
            {
                playerBox.Left -= playerspeed;
            }
            if (goRight && playerBox.Left + (playerBox.Width + 30) < form1.ClientSize.Width)
            {
                playerBox.Left += playerspeed;
            }
            if (jumping && playerBox.Top > 30)
            {
                playerBox.Top -= jumpspeed;
            }
            if (goDown && playerBox.Top + (playerBox.Width + 30) < form1.ClientSize.Height)
            {
                playerBox.Top += playerspeed;
            }
        }
    }
}
