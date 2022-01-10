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
        #region save/load game StoryMode
        internal static void saveGame(GameLvl lvl, Level window)
        {
            PlayerData data = null;
            // depending on the lvl the player is in a cast is needed
            switch (lvl)
            {
                case GameLvl.storyLvl_1:
                    StoryMode1 m1 = window as StoryMode1;
                    data = new PlayerData(lvl, m1.playerBox.Location, m1.player, m1.timer, m1.relativeXPositionOfPlayer);
                    break;
                case GameLvl.storyLvl_2:
                    StoryMode2 m2 = window as StoryMode2;
                    data = new PlayerData(lvl, m2.playerBox.Location, m2.player, m2.timer, m2.relativeXPositionOfPlayer);
                    break;
                case GameLvl.storyLvl_3:
                    StoryMode3 m3 = window as StoryMode3;
                    data = new PlayerData(lvl, m3.playerBox.Location, m3.player, m3.timer, m3.relativeXPositionOfPlayer);
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

        internal static PlayerData loadGame()
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
        #endregion

        #region save/load HighscoreList
        internal static void saveHighscoreList(HighscoresData highscores)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.StartupPath + "\\highscoreList.save";

            FileStream fStream = new FileStream(path, FileMode.Create, FileAccess.Write);

            formatter.Serialize(fStream, highscores);
            fStream.Close();
        }

        internal static HighscoresData loadHighscoreList()
        {
            string path = Application.StartupPath + "\\highscoreList.save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                HighscoresData highscoreData = formatter.Deserialize(stream) as HighscoresData;
                stream.Close();

                if (highscoreData != null)
                {
                    return highscoreData;
                }
                else
                {
                    Debug.Fail("file has no data. please check the file or make a new one" + path);
                    return null;
                }
            }
            else
            {
                Debug.Fail("file not found in " + path);
                return null;
            }
        }
        #endregion

        #region save/load coins
        internal static void saveCoins(int coins)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.StartupPath + "\\coinsCount.save";

            FileStream fStream = new FileStream(path, FileMode.Create, FileAccess.Write);

            formatter.Serialize(fStream, coins);
            fStream.Close();
        }

        internal static int loadCoins()
        {
            string path = Application.StartupPath + "\\coinsCount.save";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                object coins = formatter.Deserialize(stream);
                stream.Close();

                if (coins != null)
                {
                    return (int)coins;
                }
                else
                {
                    Debug.Fail("file has no data. please check the file or make a new one" + path);
                    return 0;
                }
            }
            else
            {
                Debug.Fail("file not found in " + path);
                return 0;
            }
        }
        #endregion
    }
}
