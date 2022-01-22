using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    class EnemyFly : Enemy
    {
        internal System.Windows.Vector startingPoint;
        int movementCounterLeft, movementCounterRight;
        public bool chase, onStart;
        bool movingLeft;

        internal override int Hp { get => hp; set => hp = value; }
        internal override int Dmg { get => dmg; set => dmg = value; }

        public EnemyFly(PictureBox eBox, int hp = 1, int dmg = 20) : base(eBox, hp, dmg)
        {
            movementCounterLeft = 0;
            movementCounterRight = 0;
            onStart = true;
        }

        /// <summary>
        /// Movement pattern of EnemyFly
        /// </summary>
        /// <param name="f"></param>
        internal override void move(Form f)
        {
            if (onStart && !chase)
            {
                //"normal" move pattern
                if (movingLeft)
                {
                    box.Left -= 4;
                    box.Top -= 4;
                    movementCounterLeft++;
                }
                if (movementCounterLeft == 20)
                {
                    movingLeft = false;
                    movementCounterLeft = 0;
                }
                if (!movingLeft)
                {
                    box.Left += 4;
                    box.Top += 4;
                    movementCounterRight++;
                }
                if (movementCounterRight == 20)
                {
                    movingLeft = true;
                    movementCounterRight = 0;
                }
            }
            else
            {
                //chase pattern is on Level.cs because it needs the Player Location
                if (chase)
                {
                    onStart = false;
                }
                else
                {
                    ReturnStartPoint();
                }
            }
        }

        //method to let the enemy return to his spawn point (not exactly correct because of the scrolling)
        private void ReturnStartPoint()
        {
            if (box.Location.X >= startingPoint.X - 20 && box.Location.X <= startingPoint.X + 20 && box.Location.Y <= startingPoint.Y + 20 && box.Location.Y >= startingPoint.Y - 20)
                onStart = true;
            if (box.Location.X > startingPoint.X && !onStart)
                box.Left -= 2;
            if (box.Location.X < startingPoint.X && !onStart)
                box.Left += 2;
            if (box.Location.Y > startingPoint.Y && !onStart)
                box.Top -= 2;
            if (box.Location.Y < startingPoint.Y && !onStart)
                box.Top += 2;
        }
    }
}
