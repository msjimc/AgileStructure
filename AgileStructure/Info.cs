using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgileStructure
{
    public partial class Info : Form
    {
        string[] referenceSequenceNames = null;
        ToolStripMenuItem tsmi = null;
        int counter = 0;

        public Info(string[] names, ToolStripMenuItem Sender)
        {
            InitializeComponent();

            referenceSequenceNames = names;
            tsmi = Sender;
        }

        private void Info_Load(object sender, EventArgs e)
        {

        }

        public void displayData(string[] data)
        {
            txtDisplay.Text = data[0];
            txtDisplay.Text += referenceSequenceNames[Convert.ToInt32(data[1])];
            txtDisplay.Text += data[2];
        }

        private void Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (counter == 0)
            {
                counter++;
                tsmi.PerformClick();
            }
        }
    }
}
