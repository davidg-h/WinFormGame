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
        public Player(PictureBox playerBox)
        {
            this.playerBox = playerBox;
        }

        public void move(Point moveVector)
        {
            playerBox.Location = new Point(playerBox.Location.X + moveVector.X, playerBox.Location.Y + moveVector.Y);
        }
    }
}
