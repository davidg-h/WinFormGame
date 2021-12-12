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
        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg) { 
            defaultLocation = new Point(34,331);
            moveVector = new System.Windows.Vector(0, 0);
            isOnGround = false;
        }

        internal override int Hp { get => hp; set => hp = value; }

        internal override int Dmg { get => dmg; set => dmg = value; }

        // move pattern for WASD - controls

        internal void Left(bool go) { goLeft = go; }
        internal void Right(bool go) { goRight = go; }
        internal void jump() { jumps = true; }
        internal void Down() { goDown = true; }
        internal bool IsOnGround { get => isOnGround; set => isOnGround = value; }
        public bool obstacleRight = false;
        public bool obstacleLeft = false;

        public override void move(Form f)
        {

            if (goLeft && box.Left > 30 && !obstacleLeft)
            {
                moveVector.X = -characterSpeed;
            }
            else if (goRight && box.Left + (box.Width + 30) < f.ClientSize.Width && !obstacleRight)
            {
                moveVector.X = characterSpeed;
            }
            else
            {
                moveVector.X = 0;
            }

            #region jumping mechanics
            //isonGround is handled in StoryMode1

            // moves the box up or down depending on the threshold 'force'
            //if (box.Top > defaultLocation.Y - 1 && jumps == false)
            //{
            //    isOnGround = true;
            //}

            if (jumps && IsOnGround)
            {
                jumps = false;
                isOnGround = false;
                moveVector.Y = -jumpSpeed*5; //add initial jumpforce
            }
            if (!IsOnGround)
            {
                //if inAir then add downforce
                moveVector.Y += force;
            }
            else
            {   //if isOnGround then stay on Ground
                moveVector.Y = 0;
            }

            
            //finaly the position gets Updated with the created moveVector
            box.Location = new Point( box.Location.X + (int)moveVector.X, box.Location.Y + (int)moveVector.Y);
            #endregion
        }

        public override void attack()
        {
            //TODO player attack
        }

        internal void MoveToTopOfPlatform(int topOfPlatform)
        {
            box.Location = new Point(box.Location.X, topOfPlatform - box.Height + 1);
        }
    }
}
