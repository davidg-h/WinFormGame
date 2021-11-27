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
        internal int score = 0;
        internal int coins = 0;

        public Player(PictureBox playerBox, int hp, int dmg = 1) : base(playerBox, hp, dmg) { }

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
            if (jumping && box.Top > 30)
            {
                box.Top -= jumpSpeed;
            }
            if (goDown && box.Top + (box.Width + 30) < f.ClientSize.Height)
            {
                box.Top += characterSpeed;
            }
        }

        public override void attack()
        {
            //TODO player attack
        }
    }
}
