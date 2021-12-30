using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Media3D;
using System.Drawing.Imaging;
//using System.Windows;

namespace WindowsForms.Gamecode
{
    internal class Player : Entity
    {
        internal Point defaultLocation;
        internal int score = 0;
        internal int coins = 0;
        internal System.Windows.Vector moveVector;
        internal Image[] spritesIdle;
        internal Image[] spritesWalkLeft;
        internal Image[] spritesWalkRight;
        internal Image currentImage = null;
        int currentImageIndex = 0;
        int animationUpdate = 0;
        int currentAnimation = 0; //TODO: create enum animations
        public bool armor1, armor2, potion;

        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg) { 
            defaultLocation = new Point(34,31);
            moveVector = new System.Windows.Vector(0, 0);
            isOnGround = false;
            spritesIdle = SpriteHandler.getFrames(playerBox.Image);
            spritesWalkLeft = SpriteHandler.getFrames(Properties.Resources.walkingLeft);
            spritesWalkRight = SpriteHandler.getFrames(Properties.Resources.walking);
            currentImage = spritesIdle[0];
            armor1 = false;
            armor2 = false;
            potion = false;
        }

        internal override int Hp
        {
            get => hp;
            set
            {
                if (value > 100)
                    hp = 100;
                else if (armor2 && value <= hp)
                {
                    armor2 = false;
                    hp += 0;
                }
                else if (armor1 && value <= hp)
                {
                    armor1 = false;
                    hp += 0;
                }
                else
                    hp = value;

            }
        }

        internal override int Dmg { get => dmg; set => dmg = value; }

        // move pattern for WASD - controls

        internal void Left(bool go) { goLeft = go; }
        internal void Right(bool go) { goRight = go; }
        internal void jump() { jumps = true; }
        internal void Down() { goDown = true; }
        internal bool IsOnGround { get => isOnGround; set => isOnGround = value; }
        public bool obstacleRight = false;
        public bool obstacleLeft = false;
        public bool attacking = false;


        public override void move(Form f)
        {
            
            if (goLeft && box.Left > 30)// && !obstacleLeft)
            {
                //moveVector.X = -characterSpeed;
                if (currentAnimation != 1)
                {
                    animationUpdate = 0;
                    currentAnimation = 1;
                }
            }
            else if (goRight && box.Left + (box.Width + 30) < f.ClientSize.Width)// && !obstacleRight)
            {
                //moveVector.X = characterSpeed;
                if(currentAnimation != 2)
                {
                    animationUpdate = 0;
                    currentAnimation = 2;
                }
            }
            else
            {
                moveVector.X = 0;

                if (currentAnimation != 0)
                {
                    animationUpdate = 0;
                    currentAnimation = 0;
                }
            }

            #region jumping mechanics
            //isonGround is handled in StoryMode1

            // moves the box up or down depending on the threshold 'force'


            if (jumps && IsOnGround)
            {
                jumps = false;
                isOnGround = false;
                moveVector.Y = -jumpSpeed; //add initial jumpforce
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
            #endregion
            //finaly the position gets Updated with the created moveVector
            box.Location = new Point(box.Location.X + (int)moveVector.X, box.Location.Y + (int)moveVector.Y);

            updateAnimation();
        }

        private void updateAnimation()
        {
            if (animationUpdate % 3 == 0)
            {
                switch (currentAnimation)
                {
                    case 0: 
                            currentImage = spritesIdle[ (currentImageIndex + 1) % spritesIdle.Length];
                        break;
                    case 1:
                            currentImage = spritesWalkLeft[ (currentImageIndex + 1) % spritesIdle.Length];
                        break;
                    case 2:
                            currentImage = spritesWalkRight[ (currentImageIndex + 1) % spritesIdle.Length];
                        break;
                }
                currentImageIndex++;
                animationUpdate = 0;
            }
            animationUpdate++;
            

        }

        private void idle() { }
        private void walkLeft() { }
        private void walkRight() { }

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
