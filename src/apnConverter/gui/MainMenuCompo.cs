using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace apnConverter.gui
{
    public partial class MainMenuCompo : UserControl
    {
        public MainMenuCompo()
        {
            InitializeComponent();
        }
        public event Action<EventArgs> ToApnProjectStarted;
        public event Action<EventArgs> ToBmpProjectStarted;

        private void createApnButton_Click(object sender, EventArgs e)
        {
            if (ToApnProjectStarted != null)
                ToApnProjectStarted(e);
        }

        private void toBmpButton_Click(object sender, EventArgs e)
        {
            if (ToBmpProjectStarted != null)
                ToBmpProjectStarted(e);
        }
    }
}
