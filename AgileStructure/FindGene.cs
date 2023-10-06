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
    public partial class FindGene : Form
    {
        private GeneData gd;
        AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

        private string[] data = null;

        public FindGene()
        {
            InitializeComponent();

        }

        internal void setNames(GeneData Genes)
        {
            gd = Genes;
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (Gene g in gd.Genes)
            {
                names.Add(g.getName);
            }

            txtGene.AutoCompleteMode = AutoCompleteMode.Append;
            txtGene.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGene.AutoCompleteCustomSource = names;
        }

        private void FindGene_Load(object sender, EventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtGene_TextChanged(object sender, EventArgs e)
        {
            btnAccept.Enabled = false;
            data = null;
            txtCoordinates.Clear();
        }

        private void txtCoordinates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control != true)
            { e.SuppressKeyPress = true; }
        }

        public string[] getGeneData
        { get { return data; } }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            btnAccept.Enabled = false;
            data = null;
            txtCoordinates.Clear();
            foreach (Gene g in gd.Genes)
            {
                if (g.getName.ToLower() == txtGene.Text.ToLower())
                {
                    txtCoordinates.Text = g.getChromosome + ", " + g.GetLocation.GetRegionStart.ToString("N0") + " - " + g.GetLocation.GetRegionEnd.ToString("N0");
                    data = new string[] { g.getChromosome, g.GetLocation.GetRegionStart.ToString("N0"), g.GetLocation.GetRegionEnd.ToString("N0") };
                    btnAccept.Enabled = true;
                    break;
                }
            }
        }
    }
}