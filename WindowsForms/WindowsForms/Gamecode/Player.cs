using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Media3D;
//using System.Windows;

namespace WindowsForms.Gamecode
{
    internal class Player : Entity
    {
        internal Point defaultLocation;
        internal int score = 0;
        internal int coins = 0;
        internal System.Windows.Vector moveVector;
        internal bool isOnGround;
        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg) { 
            defaultLocation = new Point(34,331);
            moveVector = new System.Windows.Vector(0, 0);
        }

        internal override int Hp { get => hp; set => hp = value; }

        internal override int Dmg { get => dmg; set => dmg = value; }

        // move pattern for WASD - controls

        public void Left(bool go) { this.goLeft = go; }
        public void Right(bool go) { goRight = go; }
        public void jump() { jumps = true; }
        public void Down() { goDown = true; }
        public override void move(Form f)
        {

            if (goLeft && box.Left > 30)
            {
                moveVector.X = -characterSpeed;
            }
            else if (goRight && box.Left + (box.Width + 30) < f.ClientSize.Width)
            {
                moveVector.X = characterSpeed;
            }
            else
            {
                moveVector.X = 0;
            }

            #region jumping mechanics
            // moves the box up or down depending on the threshold 'force'
            if (box.Top > defaultLocation.Y - 1 && jumps == false)
            {
                isOnGround = true;
            }

            if (jumps && isOnGround)
            {
                jumps = false;
                isOnGround = false;
                moveVector.Y = -jumpSpeed*5;
            }
            if (!isOnGround)
            {
                if (box.Top + moveVector.Y > defaultLocation.Y)
                    moveVector.Y = defaultLocation.Y - 1 - box.Top;
                else
                    moveVector.Y += force;
            }
            else
            {
                moveVector.Y = 0;
            }

            
            //TODO: type konversion from Point3d to Point
            box.Location = new Point( box.Location.X + (int)moveVector.X, box.Location.Y + (int)moveVector.Y);
            #endregion
        }

        public override void attack()
        {
            //TODO player attack
        }
    }
}
