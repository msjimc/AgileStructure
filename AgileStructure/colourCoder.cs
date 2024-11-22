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
    public partial class colourCoder : Form
    {
        private Color selectionColour = Color.Blue;
        private Form1 form1;
        public colourCoder(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Green;
            changeColour();
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Red;
            changeColour();
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Blue;
            changeColour();
        }

        private void btnGrey_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Gray;
            changeColour();
        }

        private void btnOrange_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Orange;
            changeColour();
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Yellow;
            changeColour();
        }

        private void btnOrchid_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Purple;
            changeColour();
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Black;
            changeColour();
        }

        private void btnViolet_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Violet;
            changeColour();
        }

        private void btnIndigo_Click(object sender, EventArgs e)
        {
            selectionColour = Color.Indigo;
            changeColour();
        }

        private void btnDarkgrey_Click(object sender, EventArgs e)
        {
            selectionColour = Color.DarkGray;
            changeColour();
        }

        private void btnPaleGrey_Click(object sender, EventArgs e)
        {
            selectionColour = Color.LightGray;
            changeColour();
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            ColorDialog scheme = new ColorDialog();
            scheme.FullOpen = true;
            if (scheme.ShowDialog() == DialogResult.OK)
            {
                selectionColour = scheme.Color;
                changeColour();
            }
        }

        private void changeColour()
        {
            form1.SetSelectionColour(selectionColour);
        }

        public void clean()
        {
            selectionColour = Color.Blue;
        }

        private void colourCoder_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.cC_Closing();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            form1.deleteSelectedList();
        }

    }
}
