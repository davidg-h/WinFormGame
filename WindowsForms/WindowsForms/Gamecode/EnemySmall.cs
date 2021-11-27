using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    class EnemySmall : Enemy
    {
        //String name = "Blutsauger";

        public EnemySmall(PictureBox eBox, int hp = 10, int dmg = 1) : base(eBox, hp, dmg) { }

        internal override int Hp { get => hp; set => hp = value; }

        internal override int Dmg { get => dmg; set => dmg = value; }

        public override void move(Form f)
        {
            //TODO move pattern of enemy
        }

        public override void attack()
        {
            //TODO attack pattern enemy small
        }
    }
}
