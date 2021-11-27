using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsForms
{
    class EnemySmall
    {
        String name = "Blutsauger";
        int health = 10;
        int damage = 1;
        PictureBox enemyBox;

        public EnemySmall(PictureBox enemyBox)
        {
            this.enemyBox = enemyBox;
        }

        public void shoot(Point target)
        {

            
        }
    }
}
