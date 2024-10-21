using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgileStructure
{
    public partial class ComplexRearrangement : Form
    {
        string primaryReference = "";

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

        public ComplexRearrangement(Form1 parentForm, string PrimaryReference)
        {
            InitializeComponent();
            form = parentForm;
            this.primaryReference = PrimaryReference;
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
                form.deleteSelectedList();
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
                form.deleteSelectedList();
            }
            catch
            {
                lblPrimary2.Text = "Error";
                lblSecondary2.Text = "Error";
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                txtAnswer.Clear();
                int[] places = getUniquePlaces();
                float[] alignment = setPrimaryAlignmentLocation(places);

                if (places.Length == 3)
                {
                    System.Diagnostics.Debug.WriteLine(average11.ToString() + "\t" + average12.ToString() + "\t" + average21.ToString() + "\t" + average22.ToString() + "\t" + alignment[0].ToString() + "\t" + alignment[1].ToString() + "\t" + alignment[2].ToString() + "\t-" );
                    inversionWithCommonBraelPoint(alignment, places);                    
                }
                else if (places.Length == 4)
                {
                 System.Diagnostics.Debug.WriteLine(average11.ToString() + "\t" + average12.ToString() + "\t" + average21.ToString() + "\t" + average22.ToString() + "\t" + alignment[0].ToString() + "\t" + alignment[1].ToString() + "\t" + alignment[2].ToString() + "\t" + alignment[3].ToString());
                   
                    if (alignment[1] < 0.2f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2]; }
                        else
                        { txtAnswer.Text = items1[0] + "." + items2[2] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[1]; }
                    }
                    else if (alignment[1] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }
                        else
                        { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }
                    }
                    else if (annotations1[3].StartsWith("o") == true && annotations2[4].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[3]);
                        string[] items2 = processIAnnotationString(annotations2[4]);
                        if (alignment[1] > 0.8f)
                        { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }//154
                        else if (alignment[2] > 0.8f)
                        { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }//156
                        
                    }
                    else if (alignment[2] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                        else
                        { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }
                    }
                    else if (alignment[3] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { txtAnswer.Text = items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[1]; }
                        else
                        { txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }
                    }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[3].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[3]);
                        if (alignment[3] > 0.8f)
                        { txtAnswer.Text = items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items1[1] + "_" + items2[1]; }
                        else
                        { txtAnswer.Text = items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items2[1] + "_" + items1[1]; }
                    }
                    else if (annotations1[1].StartsWith("o") == true && annotations2[1].StartsWith('o') == true)
                    {
                        insertionOnOtherChromosome(alignment, places);                        
                    }
                }
            }
            catch(Exception ex)
            { txtAnswer.Text = " Error"; }
        }

        private string[] processTranslocationString(string annotation)
        {
            string modified = annotation.Substring(3, annotation.Length - 4).Replace(") (", ";").Replace("g.","");

            string[] items = modified.Split(';');
            string[] answer = { items[0], items[2], items[1], items[3] };

            return answer;
        }

        private string[] processIAnnotationString(string annotation)
        {
            string[] answer = new string[4];
            string[] items = annotation.Split('_');
            answer[2] = items[1].Substring(0, items[1].Length - 3);
            answer[0] = items[0].Substring(1, items[0].IndexOf(".") - 1);
            answer[1] = items[0].Substring(items[0].IndexOf(".") + 1);
            answer[3] = annotation.Substring(annotation.Length - 3);
            return answer;
        }

        private void inversionWithCommonBraelPoint(float[] alignment, int[] places)
        {
            if (alignment[0] > 0.4f && alignment[0] < 0.6f)
            {
                string[] items1 = processIAnnotationString(annotations1[0]);
                string[] items2 = processIAnnotationString(annotations2[0]);
                if (places[1] == average11 || places[1] == average12)
                {
                    if (alignment[1] > 0.8f)
                    {//inversion
                        txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] < 0.2f)
                    {//inversion
                        txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                }
            }
            else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
            {
                string[] items1 = processIAnnotationString(annotations1[0]);
                string[] items2 = processIAnnotationString(annotations2[0]);
                if (alignment[1] < 0.2f)
                {//inversion
                    txtAnswer.Text = items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                }
                else if (alignment[1] > 0.8f)
                {//inversion
                    txtAnswer.Text = items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                }
            }
        }

        private void insertionOnOtherChromosome(float[] alignment, int[] places)
        {
            List<string> items1 = new List<string>(processTranslocationString(annotations1[1]));
            List<string> items2 = new List<string>(processTranslocationString(annotations2[1]));
            List<int> lPlaces = new List<int>();
            lPlaces.AddRange(places);
            int index = lPlaces.IndexOf(average11);
            if (alignment[index] < 0.2f)
            {
                index = items1.IndexOf(average11.ToString("N0"));
                string[] fragments = new string[6];
                if (index > -1)
                {
                    fragments[0] = items1[index - 1];
                    fragments[1] = items1[index];
                    fragments[2] = average21.ToString("N0");
                    index = items1.IndexOf(average12.ToString("N0"));
                    fragments[3] = items1[index - 1];
                    fragments[4] = items1[index];
                    fragments[5] = average22.ToString("N0");
                    if (average11 > average12)
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)
                            { txtAnswer.Text = fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)//Translocation
                            { txtAnswer.Text = fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)//Translocation
                            { txtAnswer.Text = fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)
                            { txtAnswer.Text = fragments[0] + ":" + fragments[1] + "_" + fragments[2] + " has been inserted in to " + fragments[3] + ":" + fragments[4] + "_" + fragments[5]; }
                            else
                            { txtAnswer.Text = "The reverse complement of " + fragments[0] + ":" + fragments[1] + "_" + fragments[2] + " has been inserted in to " + fragments[3] + ":" + fragments[5] + "_" + fragments[4]; }
                        }
                    }
                }
            }
            else if (alignment[index] > 0.8f)
            {
                index = items1.IndexOf(average11.ToString("N0"));
                string[] fragments = new string[6];
                if (index > -1)
                {
                    fragments[3] = items1[index - 1];
                    fragments[4] = items1[index];
                    fragments[5] = average21.ToString("N0");
                    index = items1.IndexOf(average12.ToString("N0"));
                    fragments[0] = items1[index - 1];
                    fragments[1] = items1[index];
                    fragments[2] = average22.ToString("N0");

                    if (average11 > average12)
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)//Translocation
                            { txtAnswer.Text = fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { txtAnswer.Text = fragments[0] + ":" + fragments[2] + "_" + fragments[1] + " has been inserted in to " + fragments[3] + ":" + fragments[4] + "_" + fragments[5]; }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)
                            { txtAnswer.Text = fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)//Translocation
                            { txtAnswer.Text = fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { txtAnswer.Text = "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[2] + "_" + fragments[1]; }
                        }
                    }
                }
            }
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

    }
}
