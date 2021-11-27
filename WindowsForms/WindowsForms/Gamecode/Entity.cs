using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    /// <summary>
    /// super class for all objecs/characters in the game with default values
    /// </summary>
    internal abstract class Entity
    {
        protected PictureBox box; // connection to the GUI
        protected int characterSpeed = 10;
        protected int jumpSpeed = 16;
        protected int force = 8;
        protected int hp;
        protected int dmg;
        internal bool goRight, goLeft, goDown, jumping;

        internal abstract int Hp { get; set; }
        internal abstract int Dmg { get; set; }

        protected Entity(PictureBox box, int hp, int dmg)
        {
            this.hp = hp;
            this.dmg = dmg;
            this.box = box;
        }

        // move pattern 
        public abstract void move(Form f);
        // attack pattern
        public abstract void attack();
    }
}
