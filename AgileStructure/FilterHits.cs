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
    public partial class FilterHits : Form
    {
        public FilterHits(string[] referenceSequences, int hitCount)
        {
            InitializeComponent();
            cboRef.Items.Add("Select All");
            cboRef.Items.AddRange(referenceSequences);
            cboRef.SelectedIndex = 0;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FilterHits_Load(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public int getHitCount
        { get { return (int)nudHit.Value; } }

        public string getHitSequence
        { get { return cboRef.Text; } }

    }
}
