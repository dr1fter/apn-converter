using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace apnConverter.gui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //hook event-handlers..
            menuCompo.ToApnProjectStarted += new Action<EventArgs>(menuCompo_ToApnProjectStarted);
            menuCompo.ToBmpProjectStarted += new Action<EventArgs>(menuCompo_ToBmpProjectStarted);
        }

        void menuCompo_ToBmpProjectStarted(EventArgs obj)
        {
            this.contentPanel.Controls.Clear();
            this.contentPanel.Controls.Add(bmpCompo);
        }

        void menuCompo_ToApnProjectStarted(EventArgs obj)
        {
            this.contentPanel.Controls.Clear();
            this.contentPanel.Controls.Add(apnCompo);
        }
        private MainMenuCompo menuCompo = new MainMenuCompo();
        private ToApnCompo apnCompo = new ToApnCompo();
        private ToBmpCompo bmpCompo = new ToBmpCompo();

        private void MainForm2_Load(object sender, EventArgs e)
        {
            this.contentPanel.Controls.Add(menuCompo);
        } 
    }
}
