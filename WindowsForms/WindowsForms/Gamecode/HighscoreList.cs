using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Gamecode
{
    public partial class HighscoreList : Form
    {
        (int score, string name)[] highscoreEntries = new (int, string)[5];
        (int score, string name) entry;
        bool listIsFull = false;

        public HighscoreList(string name, string score)
        {
            InitializeComponent();
            this.FormClosed += StartScreen.closeGame;
            // automatic save when closing the window
            this.FormClosing += saveHighscoreList;

            // filtering only the int for sorting the array
            score = score.Replace("Score: ", "");

            entry = (Int32.Parse(score), name);

            // only for the first save to create the file
            if (!File.Exists(Application.StartupPath + "\\highscoreList.save"))
            {
                fillList(entry.score, entry.name);
                saveHighscoreList(this, new EventArgs());
            }
            else
            {
                // load the highscoreList to operate on list
                highscoreListLoad();

                // fill the list (array) or insert a higher score
                if (!listIsFull) fillList(entry.score, entry.name);
                else highscoreEntries = insert(entry.score, entry.name);
            }

            displayList();
        }

        private void displayList()
        {
            int i = highscoreEntries.Length - 1;
            foreach (Control x in this.Controls)
            {
                if (x is Label && (string)x.Tag == "scoreListNames")
                {
                    x.Text = highscoreEntries[i].name;
                    continue;
                }

                if (x is Label && (string)x.Tag == "scoreListScores")
                {
                    x.Text = $"{highscoreEntries[i].score}";
                }
                i--;
            }
        }

        private void fillList(int score, string name)
        {
            if (highscoreEntries[1] != default)
            {
                // last time filling the list 
                listIsFull = true;
            }
            highscoreEntries[0] = (score, name);
            Array.Sort(highscoreEntries);
            return;
        }

        private (int, string)[] insert(int score, string name)
        {
            Array.Sort(highscoreEntries);
            bool sucessfullEntry = false;
            var temp = new (int, string)[5];

            // you can only get on the list, if you have the same/higher score than the last place
            if (score >= highscoreEntries[0].score)
            {
                for (int i = highscoreEntries.Length - 1; i - 1 >= 0; i--)
                {
                    if (score >= highscoreEntries[i].score && !sucessfullEntry)
                    {
                        // insert the score in the list
                        temp[i] = (score, name);
                        sucessfullEntry = true;
                    }

                    // _= is discard notation, push the last score out of the list
                    _ = sucessfullEntry ? (temp[i - 1] = highscoreEntries[i]) : (temp[i] = highscoreEntries[i]);
                }

                if (!sucessfullEntry)
                {
                    // replacing last place
                    temp[0] = (score, name);
                }
                return temp;
            }
            else
            {
                return highscoreEntries;
            }
        }

        private void saveHighscoreList(object sender, EventArgs e)
        {
            SystemSave.saveHighscoreList(new HighscoresData(highscoreEntries, listIsFull));
        }

        /// <summary>
        /// automatic loading of highscoreList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highscoreListLoad()
        {
            highscoreEntries = SystemSave.loadHighscoreList().list;
            listIsFull = SystemSave.loadHighscoreList().listIsFull;
        }
    }
}
