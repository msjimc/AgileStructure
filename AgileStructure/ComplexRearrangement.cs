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
                if (first[1] == null)
                { first[1] = first[0]; }

                int average1 = 0;
                int average2 = 0;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

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
                if (first[1] == null)
                { first[1] = first[0]; }


                int average1 = 0;
                int average2 = 0;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

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

        private void btnFind_Click(object sender, EventArgs e)
        {

            string answer12 = primFind(pd1, pd2);

            txtAnswer.Text = answer12;

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
                else if (places.Length == 2)
                {
                    return "This applears ro be a simple rearrangement, please use the basic annotation functions";
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
                        {
                            string answer =  "Insertion-deletion: " + items1[0] + "." + items2[2] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[1] + "\r\n" +
                            "Inversion: " + items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                            return answer; //129
                        }
                    }
                    else if (alignment[1] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }//136
                        else
                        {
                            string answer = "Insertion-deletion: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2] + "\r\n" +
                                "Inversion: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                            return answer;
                        
                        }//1
                    }
                    else if (annotations1[3].StartsWith("o") == true && annotations2[4].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[3]);
                        string[] items2 = processIAnnotationString(annotations2[4]);
                        if (alignment[1] > 0.8f)
                        { return "Insertion - deletion: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }//154
                        else if (alignment[2] > 0.8f)
                        { return "Insertion-deletion: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }//156

                    }
                    else if (alignment[2] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2]; }//155
                        else
                        { return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2]; }//1
                    }
                    else if (alignment[3] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }//164
                        else
                        {
                            //return items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2];
                            return "Inversion-deletion: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items2[2] + "." + items1[1] + "_" + items1[2]; 
                        }//166
                    }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[3].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[3]);
                        if (alignment[3] > 0.8f)
                        { return "Inversion-deletion: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items1[1] + "_" + items2[1]; }//173
                        else
                        { return "Inversion-deletion: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items2[1] + "_" + items1[1]; }//175
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
            if (annotations1[0].StartsWith("o") && annotations1[1].StartsWith("o"))
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
            }
            else if (annotations1[3].StartsWith("o") && annotations2[4].StartsWith("o"))
            {
                if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[3]);
                    string[] items2 = processIAnnotationString(annotations2[4]);
                    if (places[1] == average11 || places[1] == average12)
                    {
                        if (alignment[1] > 0.8f)
                        {//inversion
                            return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items2[1] + "_" + items1[1] + " to form an inverted duplication";//218
                        }
                        else if (alignment[1] < 0.2f)
                        {//inversion
                            return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//222
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[3]);
                    string[] items2 = processIAnnotationString(annotations2[4]);//h
                    if (alignment[1] < 0.2f)
                    {//inversion
                        return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];//232
                    }
                    else if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }
                }
                else if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[3]);
                    string[] items2 = processIAnnotationString(annotations2[4]);//h
                    if (alignment[0] < 0.2f)
                    {//inversion
                        return "Duplication: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];//232
                    }
                    else if (alignment[1] < 0.2f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }
                }

            }
            else if (annotations1[4].StartsWith("o") && annotations2[4].StartsWith("o"))
            {
                if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[4]);
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
                        else if (alignment[0] > 0.8f)
                        {
                            return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];//218
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
                else if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[4]);//h
                    if (alignment[1] < 0.2f)
                    {//inversion
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }

                }
            }
            else if (annotations1[4].StartsWith("o") && annotations2[3].StartsWith("o"))
            {
                if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[3]);
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
                        else if (alignment[0] > 0.8f)
                        {
                            return "Duplication: " + items1[0] + "." + items2[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];//218
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[3]);//h
                    if (alignment[1] < 0.2f)
                    {//inversion
                        return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];//232
                    }
                    else if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }

                }
                else if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[3]);//h
                    if (alignment[1] < 0.2f)
                    {//inversion
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }

                }
            }
            else if (annotations1[0].StartsWith("o") && annotations2[0].StartsWith("o"))
            {
                if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]);
                    if (places[1] == average11 || places[1] == average12)
                    {
                        if (alignment[1] > 0.8f)
                        {//inversion
                            //get common and make either side share it
                           System.Diagnostics.Debug.WriteLine(items1[0] + "\t" + items1[1] + "\t" + items1[2] + "\t" + items1[3] + "\t" + items2[0] + "\t" + items2[1] + "\t" + items2[2] + "\t" + items2[3]);


                            return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[1];
                            //clash 2 Duplication: the reverse complement of chr7.43,599,999_43,750,001 is inserted at chr7.43,589,996_43,590,002 can swap last two
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,600,000-43,610,000_ONT_no_2nd.bam
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,610,000-43,610,000_ONT_no_2nd.bam
                        }
                        else if (alignment[1] < 0.2f)
                        {//inversion
                            return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//222
                        }
                        else if (alignment[0] > 0.8f)
                        {
                            return "Duplication: " + items1[0] + "." + items2[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];//218
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]);//h
                    if (alignment[1] < 0.2f)
                    {//inversion
                        return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];//232
                        //clash insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,760,000-43,760,000_ONT_no_2nd.bam

                    }
                    else if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }

                }
                else if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]);//h
                    if (alignment[1] < 0.2f)
                    {//inversion
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {//inversion
                        return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];//236
                    }
                    else if (alignment[0] > 0.8f)
                    {//duplication RC
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];//6
                    }

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
