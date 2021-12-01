using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsForms.Gamecode

{
    internal class Player : Entity
    {
        internal Point defaultLocation;
        internal int score = 0;
        internal int coins = 0;

        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg) { defaultLocation = new Point(34,331); }

        internal override int Hp { get => hp; set => hp = value; }

        internal override int Dmg { get => dmg; set => dmg = value; }

        // move pattern for WASD - controls
        public override void move(Form f)
        {
            if (goLeft && box.Left > 30)
            {
                box.Left -= characterSpeed;
            }
            if (goRight && box.Left + (box.Width + 30) < f.ClientSize.Width)
            {
                box.Left += characterSpeed;
            }
            if (goDown && box.Top + (box.Width + 30) < f.ClientSize.Height)
            {
                box.Top += characterSpeed;
            }

            #region jumping mechanics
            // moves the box up or down depending on the threshold 'force'
            box.Top += jumpSpeed;
            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (box.Top > defaultLocation.Y - 1 && jumping == false)
            {
                force = 12;
                box.Top = defaultLocation.Y;
                jumpSpeed = 0;
            }
            #endregion
        }

        public override void attack()
        {
            //TODO player attack
        }
    }
}
