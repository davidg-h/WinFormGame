using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace WindowsForms.Gamecode
{
    /// <summary>
    /// save and loading functionality of the game
    /// </summary>
    public static class SystemSave
    {
        internal static void saveGame(GameLvl lvl, Form window)
        {
            PlayerData data = null;
            // depending on the lvl the player is in a cast is needed
            switch (lvl)
            {
                case GameLvl.storyLvl_1:
                    StoryMode1 m1 = window as StoryMode1;
                    data = new PlayerData(lvl, m1.playerBox.Location, m1.player);
                    break;
                case GameLvl.storyLvl_2:
                    break;
                case GameLvl.storyLvl_3:
                    break;
            }

            // formatter to format the Playerdata class in a file
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.StartupPath + "\\game.save";

            // creates the file
            FileStream fStream = new FileStream(path, FileMode.Create, FileAccess.Write);

            // stores the data in the file (serialized it)
            formatter.Serialize(fStream, data);
            fStream.Close();

            // feedback for the user/player
            MessageBox.Show("Your game has been saved!", "Game saved", MessageBoxButtons.OK);
        }

        public static PlayerData loadGame()
        {
            string path = Application.StartupPath + "\\game.save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                // gets the data back in a PlayerData class
                PlayerData loadGameData = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                if (loadGameData != null)
                {
                    MessageBox.Show("Your last game is loaded!", "Load game", MessageBoxButtons.OK);
                    return loadGameData;
                }
                else
                {
                    Debug.Fail("file has no data. please check the file or make a new one" + path);
                    return null;
                }
            }
            else
            {
                Debug.Fail("Save file not found in " + path);
                return null;
            }
        }
    }
}
