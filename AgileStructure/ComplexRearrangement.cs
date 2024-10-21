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
        string[] annotations1;

        private int average21;
        private int average22;
        private float primary5primeOfPlace21;
        private float primary5primeOfPlace22;
        string[] annotations2;

        PointData pd1 = null;
        PointData pd2 = null;
        PointData pd3 = null;
        PointData pd4 = null;


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

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();

                int average1 = first[0].getAveragePlace;
                int average2 = first[1].getAveragePlace;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(average11, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(average12, first[1].getReferenceName);

                if (primary5primeOfPlace1 > 0.8f || primary5primeOfPlace1 < 0.2f)
                {
                    average1 = first[0].getAveragePlace;
                    average2 = first[1].getAveragePlace;
                    lblPrimary1.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary1.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");
                }
                else if (primary5primeOfPlace2 > 0.8f || primary5primeOfPlace2 < 0.2f)
                {
                    average1 = first[1].getAveragePlace;
                    average2 = first[0].getAveragePlace;
                    float t = primary5primeOfPlace1;
                    primary5primeOfPlace1 = primary5primeOfPlace2;
                    primary5primeOfPlace2 = t;
                    lblPrimary1.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary1.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                }

                pd1 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, annotations);

                form.deleteSelectedList();

                System.Diagnostics.Debug.WriteLine("point 1\t" + average1.ToString() + "\t" + average2.ToString());
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

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();

                int average1 = first[0].getAveragePlace;
                int average2 = first[1].getAveragePlace;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(average1, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(average2, first[1].getReferenceName);

                if (primary5primeOfPlace1 > 0.8f || primary5primeOfPlace1 < 0.2f)
                {
                    average1 = first[0].getAveragePlace;
                    average2 = first[1].getAveragePlace;
                    lblPrimary2.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary2.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");
                }
                else if (primary5primeOfPlace2 > 0.8f || primary5primeOfPlace2 < 0.2f)
                {
                    average1 = first[1].getAveragePlace;
                    average2 = first[0].getAveragePlace;
                    float t = primary5primeOfPlace1;
                    primary5primeOfPlace1 = primary5primeOfPlace2;
                    primary5primeOfPlace2 = t;
                    lblPrimary2.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary2.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                }

                pd2 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, annotations);

                form.deleteSelectedList();

                System.Diagnostics.Debug.WriteLine("point 2\t" + average1.ToString() + "\t" + average2.ToString());
            }
            catch
            {
                lblPrimary2.Text = "Error";
                lblSecondary2.Text = "Error";
            }
        }

        private void btnAccept3_Click(object sender, EventArgs e)
        {
            try
            {
                if (form == null) { return; }
                txtAnswer.Clear();

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();

                int average1 = first[0].getAveragePlace;
                int average2 = first[1].getAveragePlace;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(average1, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(average2, first[1].getReferenceName);

                if (primary5primeOfPlace1 > 0.8f || primary5primeOfPlace1 < 0.2f)
                {
                    average1 = first[0].getAveragePlace;
                    average2 = first[1].getAveragePlace;
                    lblPrimary3.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary3.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");
                }
                else if (primary5primeOfPlace2 > 0.8f || primary5primeOfPlace2 < 0.2f)
                {
                    average1 = first[1].getAveragePlace;
                    average2 = first[0].getAveragePlace;
                    float t = primary5primeOfPlace1;
                    primary5primeOfPlace1 = primary5primeOfPlace2;
                    primary5primeOfPlace2 = t;
                    lblPrimary3.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary3.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                }

                pd3 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, annotations);

                form.deleteSelectedList();

                System.Diagnostics.Debug.WriteLine("point 3\t" + average1.ToString() + "\t" + average2.ToString());
            }
            catch
            {
                lblPrimary1.Text = "Error";
                lblSecondary1.Text = "Error";
            }
        }

        private void btnAccept4_Click(object sender, EventArgs e)
        {
            try
            {
                if (form == null) { return; }
                txtAnswer.Clear();

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();

                int average1 = first[0].getAveragePlace;
                int average2 = first[1].getAveragePlace;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

                if (primary5primeOfPlace1 > 0.8f || primary5primeOfPlace1 < 0.2f)
                {
                    average1 = first[0].getAveragePlace;
                    average2 = first[1].getAveragePlace;
                    lblPrimary4.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary4.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");
                }
                else if (primary5primeOfPlace2 > 0.8f || primary5primeOfPlace2 < 0.2f)
                {
                    average1 = first[1].getAveragePlace;
                    average2 = first[0].getAveragePlace;
                    float t = primary5primeOfPlace1;
                    primary5primeOfPlace1 = primary5primeOfPlace2;
                    primary5primeOfPlace2 = t;
                    lblPrimary4.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary4.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                }

               
                pd4 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, annotations);

                form.deleteSelectedList();

                System.Diagnostics.Debug.WriteLine("point 4\t" + average1.ToString() + "\t" + average2.ToString());
            }
            catch
            {
                lblPrimary1.Text = "Error";
                lblSecondary1.Text = "Error";
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            string answer12 = primFind(pd1, pd2);
            string answer13 = primFind(pd1, pd3);
            string answer14 = primFind(pd1, pd4);
            string answer21 = primFind(pd2, pd1);
            string answer23 = primFind(pd2, pd3);
            string answer24 = primFind(pd2, pd4);

            string answer31 = primFind(pd3, pd1);
            string answer32 = primFind(pd3, pd2);
            string answer34 = primFind(pd3, pd4); 
            string answer41 = primFind(pd4, pd1); 
            string answer42 = primFind(pd4, pd2);
            string answer43 = primFind(pd4, pd3);

        }

        private string primFind(PointData pda, PointData pdb)
        {
            average11 = pda.Average1;
            average12 = pda.Average2;
            primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
            primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
            annotations1 = pda.Annotations; 

            average21 = pdb.Average1; 
            average22 = pdb.Average2;
            primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
            primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
            annotations2 = pdb.Annotations;

            return Find();
        }

        private string Find()
            {
            try
            {
                txtAnswer.Clear();
                int[] places = getUniquePlaces();
                float[] alignment = setPrimaryAlignmentLocation(places);

                if (places.Length == 3)
                {
                    System.Diagnostics.Debug.WriteLine(average11.ToString() + "\t" + average12.ToString() + "\t" + average21.ToString() + "\t" + average22.ToString() + "\t" + alignment[0].ToString() + "\t" + alignment[1].ToString() + "\t" + alignment[2].ToString() + "\t-");
                   return inversionWithCommonBreaKPoint(alignment, places);
                }
                else if (places.Length == 4)
                {
                    System.Diagnostics.Debug.WriteLine(average11.ToString() + "\t" + average12.ToString() + "\t" + average21.ToString() + "\t" + average22.ToString() + "\t" + alignment[0].ToString() + "\t" + alignment[1].ToString() + "\t" + alignment[2].ToString() + "\t" + alignment[3].ToString());

                    if (alignment[1] < 0.2f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2]; }//127
                        else
                        { return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2]; }//129
                    }
                    else if (alignment[1] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }//136
                        else
                        { return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }//138
                    }
                    else if (annotations1[3].StartsWith("o") == true && annotations2[4].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[3]);
                        string[] items2 = processIAnnotationString(annotations2[4]);
                        if (alignment[1] > 0.8f)
                        { return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }//154
                        else if (alignment[2] > 0.8f)
                        { return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }//156

                    }
                    else if (alignment[2] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2]; }//155
                        else
                        { return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }//157
                    }
                    else if (alignment[3] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }//164
                        else
                        { return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }//166
                    }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[3].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[3]);
                        if (alignment[3] > 0.8f)
                        { return items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items1[1] + "_" + items2[1]; }//173
                        else
                        { return items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items2[1] + "_" + items1[1]; }//175
                    }
                    else if (annotations1[1].StartsWith("o") == true && annotations2[1].StartsWith('o') == true)
                    {
                       return insertionOnOtherChromosome(alignment, places);
                    }
                }
            }
            catch (Exception ex)
            { return " Error"; }
            return "error";
        }

        private string[] processTranslocationString(string annotation)
        {
            string modified = annotation.Substring(3, annotation.Length - 4).Replace(") (", ";").Replace("g.", "");

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

        private string inversionWithCommonBreaKPoint(float[] alignment, int[] places)
        {
            if (alignment[0] > 0.4f && alignment[0] < 0.6f)
            {
                string[] items1 = processIAnnotationString(annotations1[0]);
                string[] items2 = processIAnnotationString(annotations2[0]);
                if (places[1] == average11 || places[1] == average12)
                {
                    if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];//218
                    }
                    else if (alignment[1] < 0.2f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//222
                    }
                }
            }
            else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
            {
                string[] items1 = processIAnnotationString(annotations1[0]);
                string[] items2 = processIAnnotationString(annotations2[0]);//h
                if (alignment[1] < 0.2f)
                {//inversion
                    return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];//232
                }
                else if (alignment[1] > 0.8f)
                {//inversion
                    return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                }
            }

            return "error";
        }

        private string insertionOnOtherChromosome(float[] alignment, int[] places)
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
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)//Translocation
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)//Translocation
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)
                            { return fragments[0] + ":" + fragments[1] + "_" + fragments[2] + " has been inserted in to " + fragments[3] + ":" + fragments[4] + "_" + fragments[5]; }
                            else
                            { return "The reverse complement of " + fragments[0] + ":" + fragments[1] + "_" + fragments[2] + " has been inserted in to " + fragments[3] + ":" + fragments[5] + "_" + fragments[4]; }
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
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return fragments[0] + ":" + fragments[2] + "_" + fragments[1] + " has been inserted in to " + fragments[3] + ":" + fragments[4] + "_" + fragments[5]; }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)//Translocation
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else//Translocation
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[2] + "_" + fragments[1]; }
                        }
                    }
                }
            }
            return "error";
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
