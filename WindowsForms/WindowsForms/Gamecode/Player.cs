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
        #region class variables
        internal Point defaultLocation;
        internal int score = 0;
        internal int coins = 0;
        internal System.Windows.Vector moveVector;
        internal Image[] spritesIdle;
        internal Image[] spritesWalkLeft;
        internal Image[] spritesWalkRight;
        internal Image spritesAttackLeft;
        internal Image spritesAttackRight;
        internal Image currentImage = null;
        int currentImageIndex = 0;
        int animationUpdate = 0;
        EnumPlayerAnimation currentAnimation = 0; //TODO: create enum animations
        public bool obstacleRight = false;
        public bool obstacleLeft = false;
        bool attacking = false;
        public bool isAttacking { get => attacking; set => attacking =  value; }
        internal override int Hp { get => hp; set => hp = value; }
        internal override int Dmg { get => dmg; set => dmg = value; }

        // move pattern for WASD - controls
        internal void Left(bool go) { goLeft = go; }
        internal void Right(bool go) { goRight = go; }
        internal void jump() { jumps = true; }
        internal void Down() { goDown = true; }
        internal bool IsOnGround { get => isOnGround; set => isOnGround = value; }

#endregion

        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg) { 
            defaultLocation = new Point(34,31);
            moveVector = new System.Windows.Vector(0, 0);
            isOnGround = false;
            spritesIdle = SpriteHandler.getFrames(playerBox.Image);
            spritesWalkLeft = SpriteHandler.getFrames(Properties.Resources.walkingLeft);
            spritesWalkRight = SpriteHandler.getFrames(Properties.Resources.walking);
            spritesAttackLeft = Properties.Resources.attackingLeft;
            spritesAttackRight = Properties.Resources.attackingRight;
            //spritesAttackLeft = SpriteHandler.getFrames(Properties.Resources.attackingLeft); 
            //spritesAttackRight = SpriteHandler.getFrames(Properties.Resources.attackingRight); 

            currentImage = spritesIdle[0];
        }

        public override void move(Form f)
        {
            animatePlayer();

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

        }
        public void moveEndlessmode(Form f) // player is able to move in window
        {
            animatePlayer();
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

        }

        private void animatePlayer()
        {
            if (attacking && goLeft)
            {
                animationUpdate = 0;
                currentAnimation = EnumPlayerAnimation.attackLeft;
            }
            else if(attacking)
            {
                animationUpdate = 0;
                currentAnimation = EnumPlayerAnimation.attackRight;
            }
            else if (goLeft && !obstacleLeft)
            {
                if (currentAnimation != EnumPlayerAnimation.moveLeft)
                {
                    animationUpdate = 0;
                    currentAnimation = EnumPlayerAnimation.moveLeft;
                }
            }
            else if (goRight && !obstacleRight)
            {
                if (currentAnimation != EnumPlayerAnimation.moveRight)
                {
                    animationUpdate = 0;
                    currentAnimation = EnumPlayerAnimation.moveRight;
                }
            }
            else
            {
                if (currentAnimation != EnumPlayerAnimation.idle)
                {
                    animationUpdate = 0;
                    currentAnimation = EnumPlayerAnimation.idle;
                }
            }

            updateAnimation();
        }

        private void updateAnimation()
        {
            if (animationUpdate % 3 == 0) //delays animations speed
            {
                switch (currentAnimation)
                {
                    case EnumPlayerAnimation.idle: 
                            currentImage = spritesIdle[ (currentImageIndex + 1) % spritesIdle.Length];
                        break;
                    case EnumPlayerAnimation.moveLeft:
                            currentImage = spritesWalkLeft[ (currentImageIndex + 1) % spritesIdle.Length];
                        break;
                    case EnumPlayerAnimation.moveRight:
                            currentImage = spritesWalkRight[ (currentImageIndex + 1) % spritesIdle.Length];
                        break;
                    case EnumPlayerAnimation.attackLeft:
                        currentImage = spritesAttackLeft;
                        break;
                    case EnumPlayerAnimation.attackRight:
                        currentImage = spritesAttackRight;
                        break;
                }
                currentImageIndex++;
                animationUpdate = 0;
            }
            animationUpdate++;
        }

        public override void attack()
        {
            attacking = true;
        }

        internal void MoveToTopOfPlatform(int topOfPlatform)
        {
            box.Location = new Point(box.Location.X, topOfPlatform - box.Height + 1);
        }
    }
}
