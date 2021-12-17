using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace WindowsForms.Gamecode
{
    class RangeEnemy : Enemy
    {
        private RangeEnemy(PictureBox eBox, int hp = 10, int dmg = 1) : base(eBox, hp, dmg)
        {
            enemyBox = eBox;
        }
        public RangeEnemy(int hp = 10, int dmg = 1) : base(null, hp, dmg)
        {
             this.CreateNewRangeEnemy();
             
        }
        internal override int Hp { get => hp; set => hp = value; }
        internal override int Dmg { get => dmg; set => dmg = value; }
        public string Name { get; internal set; }
        public PictureBox enemyBox { get; private set; }
        static int CurrentPictureIndex = 0;
        Tuple<Bitmap, Point,string>[] picturesAndLocationArray =
        {
            new Tuple<Bitmap, Point, string>(Properties.Resources.zoombie, new Point(272,310),"monster"),
            new Tuple<Bitmap, Point,string>(Properties.Resources.monster, new Point(800,280),"zoombie")
        };

        public override void move(Form f)
        {
            //TODO move pattern of enemy
        }
        public override void attack()
        {
            //TODO attack pattern enemy small
        }
        public void CreateNewRangeEnemy()
        {
            if(CurrentPictureIndex>picturesAndLocationArray.Length-1)
            {
                return;
                //possible: set counter to starting point(0) to repeat enemys
            }
            Tuple<Bitmap, Point,string> currentPicture = this.picturesAndLocationArray[CurrentPictureIndex];
            this.enemyBox = new PictureBox();

            this.enemyBox.Image = currentPicture.Item1;
            this.enemyBox.Name = currentPicture.Item3;
            this.enemyBox.Location = currentPicture.Item2;
            enemyBox.TabIndex = 21;
            enemyBox.TabStop = false;
            enemyBox.Size = new System.Drawing.Size(100, 110); 
            this.enemyBox.Tag = "rangeEnemy";
            CurrentPictureIndex++;
        }
    }
}
