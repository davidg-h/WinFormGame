﻿using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    /// <summary>
    /// super class for all type of enemys
    /// </summary>
    internal abstract class Enemy : Entity
    {
        protected Enemy(PictureBox box, int hp, int dmg) : base(box, hp, dmg) { }
    }
}
