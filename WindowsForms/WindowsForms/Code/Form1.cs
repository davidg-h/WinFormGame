using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace WindowsForms
{
    public partial class Form1 : Form
    {

        private PictureBox BulletBox;


        private Player player;
        private EnemySmall enemy1;
        private Bullet bullet;
        public Form1()
        {
            InitializeComponent();

            player = new Player(playerBox);
            enemy1 = new EnemySmall(EnemyBox);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTick_Tick(object sender, EventArgs e)
        {

            shoot();
            player.updateMovement();
        }
        private void shoot()
        {
            if (bullet == null)
            {
                this.BulletBox = new System.Windows.Forms.PictureBox();

                this.BulletBox.Image = global::WindowsForms.Properties.Resources.Bullet;
                this.BulletBox.Name = "BulletBox";
                this.BulletBox.Size = new System.Drawing.Size(32, 52);
                this.BulletBox.TabIndex = 1;
                this.BulletBox.TabStop = false;
                bullet = new Bullet(BulletBox, EnemyBox.Location, new Point(1, 0));
            }
            bullet.move();

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                player.jump();
            }
            if (e.KeyCode == Keys.A)
            {
                player.move(new Vector3D(-2, 0, 0));
            }
            if (e.KeyCode == Keys.S)
            {
                //player.move(new Vector3D(0, 2));
            }
            if (e.KeyCode == Keys.D)
            {
                player.move(new Vector3D(2, 0,0));
            }
            //switch (e.KeyCode)
            //{
            //    case Keys.W: 
            //        player.move(new Point(0, -2));
            //        break;
            //    case Keys.A:
            //        player.move(new Point(-2, 0));
            //        break;
            //    case Keys.S:
            //        player.move(new Point(0, 2));
            //        break;
            //    case Keys.D:
            //        player.move(new Point(2, 0));
            //        break;
            //}
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

        }
    }
}
