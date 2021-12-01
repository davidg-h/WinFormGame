using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsForms.Gamecode
{
    [System.Serializable]
    /// <summary>
    /// stores the data for saving and loading gameplay
    /// </summary>
    public class PlayerData
    {
        internal GameLvl lvl;
        internal int score;
        internal int coins;
        internal int hp;
        internal int dmg;
        internal Point location;

        internal PlayerData(GameLvl lvl, Point location, Player player)
        {
            this.lvl = lvl;
            this.location = location;
            score = player.score;
            coins = player.coins;
            hp = player.Hp;
            dmg = player.Dmg;
        }
    }
}
