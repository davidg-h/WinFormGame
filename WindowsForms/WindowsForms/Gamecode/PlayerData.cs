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
    internal class PlayerData
    {
        internal GameLvl lvl;
        internal int hp;
        internal int coins;
        internal int dmg;
        internal int relativePlayerCoord;
        internal (int, int) timer;
        internal bool amor1, amor2, potion, invulnerable;

        internal Point location;

        internal PlayerData(GameLvl lvl, Point location, Player player, (int, int) timer, int relativePlayerCoord)
        {
            this.lvl = lvl;
            this.timer = timer;
            this.location = location;
            this.relativePlayerCoord = relativePlayerCoord;
            coins = player.coins;
            hp = player.Hp;
            dmg = player.Dmg;
            amor1 = player.armor1;
            amor2 = player.armor2;
            potion = player.potion;
            invulnerable = player.invulnerable;
        }
    }
}
