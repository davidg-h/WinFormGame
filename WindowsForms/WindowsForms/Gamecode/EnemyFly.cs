using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    class EnemyFly : Enemy
    {
        bool movingLeft;
        int movementCounterLeft, movementCounterRight;

        internal override int Hp { get => hp; set => hp = value; }
        internal override int Dmg { get => dmg; set => dmg = value; }

        public EnemyFly(PictureBox eBox, int hp = 1, int dmg = 20) : base(eBox, hp, dmg)
        {
            movementCounterLeft = 0;
            movementCounterRight = 0;
        }

        public override void move(Form f)
        {
            if (movingLeft)
            {
                box.Left -= 3;
                box.Top -= 3;
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
                box.Top += 3;
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
