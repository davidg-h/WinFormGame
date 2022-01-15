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
        internal Image[] spritesIdleLeft;
        internal Image[] spritesIdleRight;
        internal Image[] spritesWalkLeft;
        internal Image[] spritesWalkRight;
        internal Image spritesAttackLeft;
        internal Image spritesAttackRight;
        internal Image currentImage = null;
        int currentImageIndex = 0;
        int animationUpdate = 0;
        EnumPlayerAnimation currentAnimation = EnumPlayerAnimation.idleRight;
        public bool obstacleRight = false;
        public bool obstacleLeft = false;
        bool attacking = false;
        internal bool facingRight = true;
        public bool isAttacking { get => attacking; set => attacking = value; }
        internal override int Dmg { get => dmg; set => dmg = value; }
        public bool armor1, armor2, potion, invulnerable, gamemodeEndless;
        int currentHealth;
        public Rectangle swordHitRange;

        // move pattern for WASD - controls
        internal void Left(bool go) { goLeft = go; if (go) facingRight = false; }
        internal void Right(bool go) { goRight = go; if (go) facingRight = true; }
        internal void jump() { jumps = true; }
        internal void Down() { goDown = true; }
        internal bool IsOnGround { get => isOnGround; set => isOnGround = value; }

        #endregion

        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg)
        {
            defaultLocation = new Point(34, 31);
            moveVector = new System.Windows.Vector(0, 0);
            isOnGround = false;
            spritesIdleLeft = SpriteHandler.getFrames(Properties.Resources.idle);
            spritesIdleRight = SpriteHandler.getFrames(Properties.Resources.idleLeft);
            spritesWalkLeft = SpriteHandler.getFrames(Properties.Resources.walkingLeft);
            spritesWalkRight = SpriteHandler.getFrames(Properties.Resources.walking);
            spritesAttackLeft = Properties.Resources.attackingLeft; //spritesAttackLeft = SpriteHandler.getFrames(Properties.Resources.attackingLeft); 
            spritesAttackRight = Properties.Resources.attackingRight; //spritesAttackRight = SpriteHandler.getFrames(Properties.Resources.attackingRight); 

            currentImage = spritesIdleLeft[0];
            armor1 = false;
            armor2 = false;
            potion = false;
            invulnerable = false;
        }

        internal override int Hp
        {
            get => hp;
            set
            {
                currentHealth = Hp;
                if (value > 100)
                    hp = 100;
                else if (invulnerable)
                {
                    hp = currentHealth;
                    currentHealth = value;
                }
                else if (armor2 && value <= hp)
                {
                    invulnerable = true;
                    armor2 = false;
                    hp += 0;
                }
                else if (armor1 && value <= hp)
                {
                    invulnerable = true;
                    armor1 = false;
                    hp += 0;
                }
                else
                {
                    invulnerable = true;
                    hp = value;
                }
            }
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
            if (attacking && !facingRight)
            {
                animationUpdate = 0;
                currentAnimation = EnumPlayerAnimation.attackLeft;
            }
            else if (attacking && facingRight)
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
            else if (facingRight)
            {
                if (currentAnimation != EnumPlayerAnimation.idleRight)
                {
                    animationUpdate = 0;
                    currentAnimation = EnumPlayerAnimation.idleRight;
                }
            }
            else
            {
                if (currentAnimation != EnumPlayerAnimation.idleLeft)
                {
                    animationUpdate = 0;
                    currentAnimation = EnumPlayerAnimation.idleLeft;
                }
            }

            updateAnimation();
        }

        private void updateAnimation()
        {
            if (animationUpdate % 3 == 0) //slows down animations speed
            {
                switch (currentAnimation)
                {
                    case EnumPlayerAnimation.idleRight:
                        currentImage = spritesIdleLeft[(currentImageIndex + 1) % spritesIdleLeft.Length]; //swows next frame od animation
                        break;
                    case EnumPlayerAnimation.idleLeft:
                        currentImage = spritesIdleRight[(currentImageIndex + 1) % spritesIdleLeft.Length];
                        break;
                    case EnumPlayerAnimation.moveLeft:
                        currentImage = spritesWalkLeft[(currentImageIndex + 1) % spritesIdleLeft.Length];
                        break;
                    case EnumPlayerAnimation.moveRight:
                        currentImage = spritesWalkRight[(currentImageIndex + 1) % spritesIdleLeft.Length];
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
            //creates attackingarea on left or right of the Player with Size of Sword
            if (facingRight)
                swordHitRange = new Rectangle(new Point(box.Location.X + box.Width, box.Location.Y + box.Height / 2), new Size(new Point(box.Size.Width, box.Size.Height / 2)));
            else
                swordHitRange = new Rectangle(new Point(box.Location.X - box.Width, box.Location.Y + box.Height / 2), new Size(new Point(box.Size.Width, box.Size.Height / 2)));
        }

        internal void MoveToTopOfPlatform(int topOfPlatform)
        {
            box.Location = new Point(box.Location.X, topOfPlatform - box.Height + 1);
        }
    }
}
