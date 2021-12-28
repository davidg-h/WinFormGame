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

        bool movingLeft;
        int movementCounterLeft, movementCounterRight;
        public EnemySmall(PictureBox eBox, int hp = 10, int dmg = 10) : base(eBox, hp, dmg)
        {
            movementCounterLeft = 0;
            movementCounterRight = 0;
        }

        internal override int Hp { get => hp; set => hp = value; }

        internal override int Dmg { get => dmg; set => dmg = value; }

        public override void move(Form f)
        {
            if (movingLeft)
            {
                box.Left -= 3;
                movementCounterLeft++;
            }
            if (movementCounterLeft == 20)
            {
                movingLeft = false;
                movementCounterLeft = 0;
            }
            if (!movingLeft)
            {
                box.Left += 3;
                movementCounterRight++;
            }
            if (movementCounterRight == 20)
            {
                movingLeft = true;
                movementCounterRight = 0;
            }
        }
    }
}
