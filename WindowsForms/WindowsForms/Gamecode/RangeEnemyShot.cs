using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace WindowsForms.Gamecode
{
    class RangeEnemyShot
    {
        public string direction;
        public int shotLeft;
        public int shotTop;

        private int speed = 30;
        private PictureBox shot = new PictureBox();
        private Timer shotTimer = new Timer();
        public static int ShotDmg = 10;
       
        public void CreateShot(Form form)
        {
            shot.Image = Properties.Resources.Piranha_ball;
            shot.Size = new Size(26, 16);
            shot.Tag = "shot";
            shot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            shot.Left = shotLeft;
            shot.Top = shotTop;
            shot.BringToFront();
            form.Controls.Add(shot);

            shotTimer.Interval = 100;
            shotTimer.Tick += new EventHandler(ShotTimerEvent);
            shotTimer.Start();
        }
        private void ShotTimerEvent(object sender, EventArgs e)
        {

            if (direction == "left")
            {
                shot.Left -= speed;
            }

            if (direction == "right")
            {
                shot.Left += speed;
            }

            if (shot.Left < 10 || shot.Left > 860 || shot.Top < 10 || shot.Top > 600)
            {
                shotTimer.Stop();
                shotTimer.Dispose();
                shot.Dispose();
                shotTimer = null;
                shot = null;
            }
        }
        public void DeleteShot()
        {
            if(shotTimer!= null)
            {
                shotTimer.Stop();
                shotTimer.Dispose();
                shot.Dispose();
                shotTimer = null;
                shot = null;
            }
        }
    }
}
