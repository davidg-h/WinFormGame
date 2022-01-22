using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    class RangeEnemy : Enemy
    {
        internal int shootingCounter = 0;

        public RangeEnemy(PictureBox eBox, int hp = 10, int dmg = 10) : base(null, hp, dmg)
        {
            this.box = eBox;
        }

        internal override int Hp { get => hp; set => hp = value; }
        internal override int Dmg { get => dmg; set => dmg = value; }
        internal RangeEnemyShot Shot { get; set; }

        public void ShootShot(Form f, string direction)
        {
            //manipulates frequency of shooting
            if (shootingCounter % 20 == 0) 
            {
                this.Shot = new RangeEnemyShot();
                Shot.direction = direction;
                Shot.shotLeft = this.box.Left + (this.box.Width / 130) ;
                Shot.shotTop = this.box.Top + (this.box.Height / 4);
                Shot.CreateShot(f);
            }
            shootingCounter++;
        }
    }
}
