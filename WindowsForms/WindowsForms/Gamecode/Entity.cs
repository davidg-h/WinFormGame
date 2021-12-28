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
        protected int hp;
        protected int dmg;
        // connection to the GUI
        internal PictureBox box;
        internal int characterSpeed = 10;
        internal int jumpSpeed = 40;
        internal int force = 5;
        internal bool goRight, goLeft, goDown, jumps;
        internal bool isOnGround;

        internal abstract int Hp { get; set; }
        internal abstract int Dmg { get; set; }

        protected Entity(PictureBox box, int hp, int dmg)
        {
            this.hp = hp;
            this.dmg = dmg;
            this.box = box;
        }

        // move pattern 
        public virtual void move(Form f) { }
        // attack pattern
        public virtual void attack() { }
    }
}
