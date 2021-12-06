﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms.Gamecode
{
    [System.Serializable]
    // stores the highscores
    internal class HighscoresData
    {
        internal bool listIsFull = false;
        internal (int score, string name)[] list;
        public HighscoresData((int, string)[] highscores, bool full)
        {
            list = highscores;
            listIsFull = full;
        }
    }
}
