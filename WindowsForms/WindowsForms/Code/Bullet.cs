using System.Windows.Forms;
using System.Drawing;

namespace WindowsForms
{
    internal class Bullet
    {
        Point moveVector;
        PictureBox bulletBox;
        public Bullet(PictureBox bulletBox, Point Location, Point moveVector)
        {
            bulletBox.Location = new Point(Location.X, Location.Y);
            this.bulletBox = bulletBox;
            this.moveVector = moveVector;
        }
        public void move()
        {
            bulletBox.Location = new Point(bulletBox.Location.X + moveVector.X, bulletBox.Location.Y + moveVector.Y);
        }
    }
}