using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgileStructure
{
    public partial class ComplexRearrangement : Form
    {
        private Form1 form = null;
        private int average11;
        private int average12;
        private float primary5primeOfPlace11;
        private float primary5primeOfPlace12;
        private List<int> selectedindexes1;
        string[] annotations1;

        private int average21;
        private int average22;
        private float primary5primeOfPlace21;
        private float primary5primeOfPlace22;
        private List<int> selectedindexes2;
        string[] annotations2;

        public ComplexRearrangement(Form1 parentForm)
        {
            InitializeComponent();
            form = parentForm;
        }

        private void ComplexRearrangement_Load(object sender, EventArgs e)
        {

        }

        private void btnAccept1_Click(object sender, EventArgs e)
        {
            try
            {
                if (form == null) { return; }
                txtAnswer.Clear();

                annotations1 = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();

                selectedindexes1 = form.getSelected;

                average11 = first[0].getAveragePlace;
                average12 = first[1].getAveragePlace;
                primary5primeOfPlace11 = form.PrimaryAlignment5PrimeOfbreakPoint(average11, first[0].getReferenceName);
                primary5primeOfPlace12 = form.PrimaryAlignment5PrimeOfbreakPoint(average12, first[1].getReferenceName);

                lblPrimary1.Text = "Break point 1: " + first[0].getReferenceName + ":" + average11.ToString("N0");
                lblSecondary1.Text = "Break point 2: " + first[1].getReferenceName + ":" + average12.ToString("N0");
            }
            catch
            {
                lblPrimary1.Text = "Error";
                lblSecondary1.Text = "Error";
            }
        }

        private void btnAccept2_Click(object sender, EventArgs e)
        {
            try
            {
                if (form == null) { return; }
                txtAnswer.Clear();

                annotations2 = form.externalannotations();

                BreakPointData[] second = form.getTwobreakPoints();

                selectedindexes2 = form.getSelected;

                average21 = second[0].getAveragePlace;
                average22 = second[1].getAveragePlace;
                primary5primeOfPlace21 = form.PrimaryAlignment5PrimeOfbreakPoint(average21, second[0].getReferenceName);
                primary5primeOfPlace22 = form.PrimaryAlignment5PrimeOfbreakPoint(average22, second[1].getReferenceName);

                lblPrimary2.Text = "Break point 1: " + second[0].getReferenceName + ":" + average21.ToString("N0");
                lblSecondary2.Text = "Break point 2: " + second[1].getReferenceName + ":" + average22.ToString("N0");
            }
            catch
            {
                lblPrimary2.Text = "Error";
                lblSecondary2.Text = "Error";
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //inversion = 0
            txtAnswer.Clear();
            int[] places = getUniquePlaces();
            float[] alignment = setPrimaryAlignmentLocation(places);

            if (places.Length == 3)
            {

                if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processInversionString(annotations1[0]);
                    string[] items2 = processInversionString(annotations2[0]);
                    if (places[1] == average11 || places[1] == average12)
                    {
                        if (alignment[1] > 0.8f)
                        {
                            txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                        else if (alignment[1] < 0.2f)
                        {
                            txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processInversionString(annotations1[0]);
                    string[] items2 = processInversionString(annotations2[0]);
                    if (alignment[1] < 0.2f)
                    {
                        txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                }
            }
            else if (places.Length ==4)
            {

                string isOverlapping = IsOverlapping();

                if (alignment[1] < 0.2f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                {
                    string[] items1 = processInversionString(annotations1[0]);
                    string[] items2 = processInversionString(annotations2[0]);
                    txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                }
                else if (alignment[1] > 0.8f && annotations1[0].StartsWith("o")==true && annotations2[0].StartsWith("o") == true && isOverlapping == "overlapping")
                {
                    string[] items1 = processInversionString(annotations1[0]);
                    string[] items2 = processInversionString(annotations2[0]);
                    txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                }
                else if (annotations1[3].StartsWith("o") == true && annotations2[4].StartsWith('o') == true) 
                {
                    string[] items1 = processInversionString(annotations1[3]);
                    string[] items2 = processInversionString(annotations2[4]);
                    if (isOverlapping == "overlapping")
                    { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                    else if (isOverlapping == "threeprime")
                    { txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by " + items1[0] + "." + items2[2] + "_" + items1[2]; }
                    else if (isOverlapping == "fiveprime")
                    { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                }
                else if (annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                {
                    string[] items1 = processInversionString(annotations1[0]);
                    string[] items2 = processInversionString(annotations2[0]);
                    if (Convert.ToInt32(items2[1].Replace(",","")) < Convert.ToInt32(items1[1].Replace(",", "")))
                   { txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                    else
                    { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }
                }
                else if (annotations1[1].StartsWith("o") == true && annotations2[1].StartsWith('o') == true)
                {

                }

            }
        }


        private string[] processInversionString(string annotation)
        {
            string[] answer = new string[3];
            string[] items = annotation.Split('_');
            answer[2] = items[1].Substring(0, items[1].Length - 3);
            answer[0] = items[0].Substring(1, items[0].IndexOf(".") - 1);
            answer[1] = items[0].Substring(items[0].IndexOf(".") + 1);
            return answer;
        }

        private int[] getUniquePlaces()
        {
            int[] uniquePlaces = { average11, average21, average12, average22 };

            Array.Sort(uniquePlaces);
            List<int> result = new List<int>();

            result.Add(uniquePlaces[0]);

            for (int index = 1; index < uniquePlaces.Length; index++)
            {
                if (uniquePlaces[index] > uniquePlaces[index - 1] - 50 && uniquePlaces[index] > uniquePlaces[index - 1] + 50)
                {
                    result.Add(uniquePlaces[index]);
                }
            }
            return result.ToArray();
        }
        private float[] setPrimaryAlignmentLocation(int[] places)
        {
            float[] ratio = new float[places.Length];

            for (int index = 0; index < places.Length; index++)
            {
                if (places[index] - 50 < average11 && places[index] + 50 > average11)
                { ratio[index] = primary5primeOfPlace11; }
                else if (places[index] - 50 < average12 && places[index] + 50 > average12)
                { ratio[index] = primary5primeOfPlace12; }
                else if (places[index] - 50 < average21 && places[index] + 50 > average21)
                { ratio[index] = primary5primeOfPlace21; }
                else if (places[index] - 50 < average22 && places[index] + 50 > average22)
                { ratio[index] = primary5primeOfPlace22; }
            }

            return ratio;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ComplexRearrangement_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.cR_Closing();
        }

        private string IsOverlapping()
        {

            if (average11 - 50 < average22 && average21 + 50 > average22)
            { return "overLapping"; }
            else if (average11 - 50 < average12 && average21 + 50 > average12)
            { return "overLapping"; }
            else if (average11 - 50 < average22 && average21 + 50 > average22)
            { return "overLapping"; }
            else if (average11 < average12)
            { return "fiveprime"; }
            else if (average11 > average12)
            { return "threeprime"; }
            else { return "same"; }           

        }
    }
}
