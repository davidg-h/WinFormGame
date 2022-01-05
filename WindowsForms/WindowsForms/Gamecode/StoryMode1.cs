using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace WindowsForms.Gamecode
{
    public partial class StoryMode1 : Level
    {
        public StoryMode1()
        {
            InitializeComponent();
            initializeParent(this);

            this.FormClosed += StartScreen.closeGame;
            this.KeyDown += formKeyDown;
            this.Load += startTimer;
        }
    }
}
