using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace WindowsForms

{
    class Player
    {
        PictureBox playerBox;
        bool isOnGround = false;
        Vector3D moveVector = new Vector3D(0, 0, 0);


        public Player(PictureBox playerBox)
        {
            this.playerBox = playerBox;
        }

        public void move(Vector3D moveVector)
        {
            this.moveVector += moveVector;
        }

        public void jump()
        {
            if (isOnGround)
            {
                moveVector.Y = -10;
                isOnGround = false;
            }

        }

        public void updateMovement()
        {
            if (!isOnGround)
            {
                moveVector.Y += 0.4;
            }
            if (playerBox.Location.Y  + moveVector.Y > 1000)
            {
                isOnGround = true;
                moveVector.Y = 0;
                playerBox.Location = new Point(playerBox.Location.X + (int)moveVector.X, playerBox.Location.Y + (int)moveVector.Y);

            }
            else
            {
                playerBox.Location = new Point(playerBox.Location.X + (int)moveVector.X, playerBox.Location.Y + (int)moveVector.Y);

            }
         }
    }
}
