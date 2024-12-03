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
        private float secondary5primeOfPlace11;
        private float secondary5primeOfPlace12;
        string[] annotations1;

        private int average21;
        private int average22;
        private float primary5primeOfPlace21;
        private float primary5primeOfPlace22;
        private float secondary5primeOfPlace21;
        private float secondary5primeOfPlace22;
        string[] annotations2;

        PointData pd1 = null;
        PointData pd2 = null;
        List<string> chromosomes;


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
            if (form.isSecondRefSet() == false)
            { MessageBox.Show("Please select a region in the lower panel", "Error"); return; }


            btnAccept1.Enabled = true;
            btnAccept2.Enabled = true;
            btnFind.Enabled = true;

            try
            {
                if (form == null) { return; }
                txtAnswer.Clear();

                chromosomes = new List<string>();

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();
                if (first[1] == null)
                { first[1] = first[0]; }

                int average1 = 0;
                int average2 = 0;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);
                float scondary5primeOfPlace1 = form.SecondaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float scondary5primeOfPlace2 = form.SecondaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

                chromosomes.Add(first[0].getReferenceName);
                if (first[1].getReferenceName != first[0].getReferenceName)
                { chromosomes.Add(first[1].getReferenceName); }

                average1 = first[0].getAveragePlace;
                average2 = first[1].getAveragePlace;
                lblPrimary1.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                lblSecondary1.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");


                //if (primary5primeOfPlace1 > 0.8f || primary5primeOfPlace1 < 0.2f)
                //{
                //    average1 = first[0].getAveragePlace;
                //    average2 = first[1].getAveragePlace;
                //    lblPrimary1.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                //    lblSecondary1.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");
                //}
                //else if (primary5primeOfPlace2 > 0.8f || primary5primeOfPlace2 < 0.2f)
                //{
                //    average1 = first[1].getAveragePlace;
                //    average2 = first[0].getAveragePlace;
                //    float t = primary5primeOfPlace1;
                //    primary5primeOfPlace1 = primary5primeOfPlace2;
                //    primary5primeOfPlace2 = t;
                //    t = scondary5primeOfPlace1;
                //    scondary5primeOfPlace1 = scondary5primeOfPlace2;
                //    scondary5primeOfPlace2 = t;
                //    lblPrimary1.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                //    lblSecondary1.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                //}

                pd1 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, scondary5primeOfPlace1, scondary5primeOfPlace2, annotations);

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
            if (form.isSecondRefSet() == false)
            { MessageBox.Show("Please select a region in the lower panel", "Error"); return; }

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
                float scondary5primeOfPlace1 = form.SecondaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float scondary5primeOfPlace2 = form.SecondaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

                if (chromosomes.Contains(first[0].getReferenceName) == false) { chromosomes.Add(first[0].getReferenceName); }
                if (chromosomes.Contains(first[1].getReferenceName) == false) { chromosomes.Add(first[1].getReferenceName); }

                average1 = first[0].getAveragePlace;
                average2 = first[1].getAveragePlace;
                lblPrimary2.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                lblSecondary2.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");


                //if (primary5primeOfPlace1 > 0.8f || primary5primeOfPlace1 < 0.2f)
                //{
                //    average1 = first[0].getAveragePlace;
                //    average2 = first[1].getAveragePlace;
                //    lblPrimary2.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                //    lblSecondary2.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");
                //}
                //else if (primary5primeOfPlace2 > 0.8f || primary5primeOfPlace2 < 0.2f)
                //{
                //    average1 = first[1].getAveragePlace;
                //    average2 = first[0].getAveragePlace;
                //    float t = primary5primeOfPlace1;
                //    primary5primeOfPlace1 = primary5primeOfPlace2;
                //    primary5primeOfPlace2 = t;
                //    t = scondary5primeOfPlace1;
                //    scondary5primeOfPlace1 = scondary5primeOfPlace2;
                //    scondary5primeOfPlace2 = t;
                //    lblPrimary2.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                //    lblSecondary2.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                //}

                pd2 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, scondary5primeOfPlace1, scondary5primeOfPlace2, annotations);

                form.deleteSelectedList();
            }
            catch
            {
                lblPrimary2.Text = "Error";
                lblSecondary2.Text = "Error";
            }
        }

        public void clean()
        {
            average11 = 0;
            average12 = 0;
            primary5primeOfPlace11 = 0;
            primary5primeOfPlace12 = 0;
            secondary5primeOfPlace11 = 0;
            secondary5primeOfPlace12 = 0;
            annotations1 = null;

            average21 = 0;
            average22 = 0;
            primary5primeOfPlace21 = 0;
            primary5primeOfPlace22 = 0;
            secondary5primeOfPlace21 = 9;
            secondary5primeOfPlace22 = 9;
            annotations2 = null;

            lblPrimary1.Text = "not set";
            lblPrimary2.Text = "not set";
            lblSecondary1.Text = "not set";
            lblSecondary2.Text = "not set";
            txtAnswer.Text = "";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            txtAnswer.Text = Find(pd1, pd2);
        }

        private string Find(PointData pda, PointData pdb)
        {
            if (pda == null || pdb == null) { return "Please set both break points"; }

            if (chromosomes.Count == 1)
            {
                if (pda.Average1 + 50 < pdb.Average1)
                {
                    average11 = pda.Average1;
                    average12 = pda.Average2;
                    primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
                    primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
                    secondary5primeOfPlace11 = pda.Secondary5primeOfPlace1;
                    secondary5primeOfPlace12 = pda.Secondary5primeOfPlace2;
                    annotations1 = pda.Annotations;

                    average21 = pdb.Average1;
                    average22 = pdb.Average2;
                    primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                    primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                    secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                    secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                    annotations2 = pdb.Annotations;
                }
                else if (pdb.Average1 + 50 < pda.Average1)
                {
                    average11 = pdb.Average1;
                    average12 = pdb.Average2;
                    primary5primeOfPlace11 = pdb.Primary5primeOfPlace1;
                    primary5primeOfPlace12 = pdb.Primary5primeOfPlace2;
                    secondary5primeOfPlace11 = pdb.Secondary5primeOfPlace1;
                    secondary5primeOfPlace12 = pdb.Secondary5primeOfPlace2;
                    annotations1 = pdb.Annotations;

                    average21 = pda.Average1;
                    average22 = pda.Average2;
                    primary5primeOfPlace21 = pda.Primary5primeOfPlace1;
                    primary5primeOfPlace22 = pda.Primary5primeOfPlace2;
                    secondary5primeOfPlace21 = pda.Secondary5primeOfPlace1;
                    secondary5primeOfPlace22 = pda.Secondary5primeOfPlace2;
                    annotations2 = pda.Annotations;
                }
                else
                {
                    if (pda.Average2 < pdb.Average2)
                    {
                        average11 = pda.Average1;
                        average12 = pda.Average2;
                        primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
                        primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
                        secondary5primeOfPlace11 = pda.Secondary5primeOfPlace1;
                        secondary5primeOfPlace12 = pda.Secondary5primeOfPlace2;
                        annotations1 = pda.Annotations;

                        average21 = pdb.Average1;
                        average22 = pdb.Average2;
                        primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                        primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                        secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                        secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                        annotations2 = pdb.Annotations;
                    }
                    else
                    {
                        average11 = pdb.Average1;
                        average12 = pdb.Average2;
                        primary5primeOfPlace11 = pdb.Primary5primeOfPlace1;
                        primary5primeOfPlace12 = pdb.Primary5primeOfPlace2;
                        secondary5primeOfPlace11 = pdb.Secondary5primeOfPlace1;
                        secondary5primeOfPlace12 = pdb.Secondary5primeOfPlace2;
                        annotations1 = pdb.Annotations;

                        average21 = pda.Average1;
                        average22 = pda.Average2;
                        primary5primeOfPlace21 = pda.Primary5primeOfPlace1;
                        primary5primeOfPlace22 = pda.Primary5primeOfPlace2;
                        secondary5primeOfPlace21 = pda.Secondary5primeOfPlace1;
                        secondary5primeOfPlace22 = pda.Secondary5primeOfPlace2;
                        annotations2 = pda.Annotations;
                    }
                }
                return Find();
            }
            else
            {
                average11 = pda.Average1;
                average12 = pda.Average2;
                primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
                primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
                secondary5primeOfPlace11 = pda.Secondary5primeOfPlace1;
                secondary5primeOfPlace12 = pda.Secondary5primeOfPlace2;
                annotations1 = pda.Annotations;

                average21 = pdb.Average1;
                average22 = pdb.Average2;
                primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                annotations2 = pdb.Annotations;

                return differentFind();
            }
        }

        private string differentFind()
        {
            try
            {
                txtAnswer.Clear();
                int[] places = getUniquePlaces();
                float[] alignment = setPrimaryAlignmentLocation(places);
                return insertionOnOtherChromosome(alignment, places);
            }
            catch { }

            return "Error";
        }

        private int Difference(string a, string b)
        {
            int iA = Convert.ToInt32(a.Replace(",", ""));
            int iB = Convert.ToInt32(b.Replace(",", ""));
            int answer = iB - iA;
            return answer;
        }

        private int Pairs(int[] places)
        {
            int[] orderd = new int[4];
            Array.Copy(places, orderd, 4);
            Array.Sort(orderd);

            for (int index = 1; index < orderd.Length;index++)
            {
                if (orderd[index] - orderd[index-1] < 50)
                { return index; }
            }
            return -1;
        }

        private int DownOverlapping()
        {
            int i = average11;
            int o = average12;
            int p = average21;
            int a = average22;
            if (average11 < average22 && average21 < average12)
            { return -1; }
            else 
            return 1;
        }

        private string Find()
        {
            try
            {
                txtAnswer.Clear();
                int[] places = getUniquePlaces();
                float[] alignment = setPrimaryAlignmentLocation(places);
                int placeCount = getNumberofUniquePlaces();
                if (placeCount == 2)
                {
                    return "This applears to be a simple rearrangement, please use the basic annotation functions";
                }
                else if (places.Length == 4)
                {
                    string combination = getAlignmentCombination(alignment);

                    if (annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (combination == "0011" && placeCount == 3)
                        {
                            return "The reverse complement of " + items1[0] + ":" + items2[1] + "-" + items1[2] + " is inserted into " + items1[0] + ":" + items1[1] + "-" + items2[2]; 
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,600,000-43,800,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,550,000-43,750,000_ONT_no_2nd 1 and 2
                        }
                        if (combination == "0011")
                        {
                            string answer = "The reverse complement of " + items1[0] + ":" + items2[1] + "-" + items1[2] + " is inserted into " + items1[0] + ":" + items1[1] + "-" + items2[2];

                            if (Difference(items2[2], items1[2]) > 50)
                            { answer = "The reverse complement of " + items1[0] + ":" + items2[2] + "-" + items1[2] + " is inserted into " + items1[0] + ":" + items1[1] + "-" + items2[1] + "\r\nor\r\n" + answer; }
                            return answer;
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,550,000-43,800,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_20000000-21,000,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,550,000-43,700,000_ONT_no_2nd 1 and 2
                        }
                        else if (combination == "1100" && placeCount == 3)
                        {
                            int common = Pairs(places);
                            if (common == 1)
                            {
                                return "The reverse complement of " + items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2];
                                //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,600,000-43,610,000_ONT_no_2nd 1 and 2
                                //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,600,000-43,700,000_ONT_no_2nd 1 and 2
                            }
                            else if (common == 2)
                            {
                                return "The reverse complement of " + items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2];
                                //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,610,000-43,610,000_ONT_no_2nd 1 and 2
                            }
                            else if (common == 3)
                            {
                                return "The reverse complement of " + items1[0] + ":" + items1[1] + "-" + items2[1] + " is inserted into " + items1[0] + ":" + items2[2] + "-" + items1[2] + "\r\nor\r\n" +
                                "The reverse complement of " + items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2];
                                //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,760,000-43,760,000_ONT_no_2nd 1 and 2
                                //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,650,000-43,750,000_ONT_no_2nd 1 and 2
                                //
                            }
                        }
                        else if (combination == "1100")
                        {
                            string answer = "The reverse complement of " + items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2];
                            if (Difference(items2[1], items1[2]) < 50)
                            {answer = "The reverse complement of " + items1[0] + ":" + items1[1] + "-" + items2[1] + " is inserted into " + items1[0] + ":" + items2[2] + "-" + items1[2] + "\r\nor\r\n" + answer; }
                            return answer;
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_50000000-51,000,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,650,000-43,700,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_RC_target_chr7_60_43,650,000-43,800,000_ONT_no_2nd 1 and 2
                        }
                        else if (combination == "1100" && DownOverlapping() == 1)
                        {
                            return items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2];
                            //
                        }                       
                    }
                    else if (annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    { }
                    else if (annotations1[3].StartsWith("o") == true && annotations2[4].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[3]);
                        string[] items2 = processIAnnotationString(annotations2[4]);
                        if (combination == "0110")
                        { 
                            return items1[0] + ":" + items1[2] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items1[1] + "-" + items2[1];
                            //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_43,590,000-43,600,000_ONT_no_2nd_1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_43,590,000-43,590,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_20000000-21,000,000_ONT_no_2nd 1 and 2
                        }
                    }
                    else if (alignment[2] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    { }
                    else if (alignment[3] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    { }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[3].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[3]);
                        if (combination == "1001")
                        { 
                            return items1[0] + ":" + items1[1] + "-" + items2[1] + " is inserted into " + items1[0] + ":" + items1[2] + "-" + items2[2];
                            //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_43,750,000-43,760,000_ONT_no_2nd 1 and 2 
                            //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_43,760,000-43,760,000_ONT_no_2nd 1 and 2
                            //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_50000000-51,000,000_ONT_no_2nd 1 and 2
                        }
                    }
                    else if (annotations1[1].StartsWith("o") == true && annotations2[1].StartsWith('o') == true)
                    { }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[4].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[4]);
                        if (combination == "1010")
                        { return items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2]; }
                        //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_43,610,000-43,610,000_ONT_no_2nd 1 and 2
                        //insert_chr7_60_43,600,000-43,750,000_target_chr7_60_43,740,000-43,740,000_ONT_no_2nd 1 and 2
                        //
                    }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[2].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[2]);
                        if (combination == "1010")
                        { return items1[0] + ":" + items1[1] + "-" + items2[2] + " is inserted into " + items1[0] + ":" + items2[1] + "-" + items1[2]; }
                    }

                }
            }
            catch (Exception ex)
            { return "Error: an error occured please check read selections"; }
            return "AgileStructure is unable to annotate this rearrangement";
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
          
        private string getAlignmentCombination(float[] alignment)
        {
            string combination = "";

            for (int index = 0; index < alignment.Length; index++)
            {
                if (alignment[index] <= 0.2f) { combination += "0"; }
                else if (alignment[index] <= 0.7f && alignment[index] >= 0.3f) { combination += "-"; }
                else if (alignment[index] >= 0.8f) { combination += "1"; }
            }
            return combination;
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
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "-" + fragments[4] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                        }
                        else
                        {
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "-" + fragments[4] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "-" + fragments[4] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                        }
                        else
                        {
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "-" + fragments[4] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
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
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[2] + "-" + fragments[1];
                            }
                        }
                        else
                        {
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[2] + "-" + fragments[1];
                            }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[2] + "-" + fragments[1];
                            }
                        }
                        else
                        {
                            if (average12 <= average22)
                            {
                                return fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[1] + "-" + fragments[2];
                            }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "-" + fragments[5] + " is inserted into " + fragments[0] + ":" + fragments[2] + "-" + fragments[1];
                            }
                        }
                    }
                }
            }
            return "AgileStructure couldn't annotate this feature";
        }

        private int[] getUniquePlaces()
        {
            int[] uniquePlaces = { average11, average12, average21, average22 };
            return uniquePlaces;
        }

        private int getNumberofUniquePlaces()
        {
            int[] uniquePlaces = { average11, average12, average21, average22 };

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
            return result.Count;
        }
        private float[] setPrimaryAlignmentLocation(int[] places)
        {
            float[] ratio = new float[places.Length];

            for (int index = 0; index < places.Length; index++)
            {
                if (index ==0)
                {
                    if (primary5primeOfPlace11 > -1)
                    { ratio[index] = primary5primeOfPlace11; }
                    else
                    { ratio[index] = secondary5primeOfPlace11; }
                }
                else if (index==1)
                {
                    if (primary5primeOfPlace12 > -1)
                    { ratio[index] = primary5primeOfPlace12; }
                    else
                    { ratio[index] = secondary5primeOfPlace12; }
                }
                else if (index==2)
                {
                    if (primary5primeOfPlace21 > -1)
                    { ratio[index] = primary5primeOfPlace21; }
                    else
                    { ratio[index] = secondary5primeOfPlace21; }
                }
                else if (index==3)
                {
                    if (primary5primeOfPlace22 > -1)
                    { ratio[index] = primary5primeOfPlace22; }
                    else
                    { ratio[index] = secondary5primeOfPlace22; }
                }


                //if (places[index] - 50 < average11 && places[index] + 50 > average11)
                //{  if (primary5primeOfPlace11 > -1)
                //    { ratio[index] = primary5primeOfPlace11; }
                //else
                //    { ratio[index] = secondary5primeOfPlace11; }
                //}
                //else if (places[index] - 50 < average12 && places[index] + 50 > average12)
                //{
                //    if (primary5primeOfPlace12 > -1)
                //    { ratio[index] = primary5primeOfPlace12; }
                //    else
                //    { ratio[index] = secondary5primeOfPlace12; }
                //}
                //else if (places[index] - 50 < average21 && places[index] + 50 > average21)
                //{
                //    if (primary5primeOfPlace21 > -1)
                //    { ratio[index] = primary5primeOfPlace21; }
                //    else
                //    { ratio[index] = secondary5primeOfPlace21; }
                //}
                //else if (places[index] - 50 < average22 && places[index] + 50 > average22)
                //{
                //    if (primary5primeOfPlace22 > -1)
                //    { ratio[index] = primary5primeOfPlace22; }
                //    else
                //    { ratio[index] = secondary5primeOfPlace22; }
                //}
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

        private void btnDrawBP_Click(object sender, EventArgs e)
        {
            try
            {
                string[] items1 = drawbreakPoint(lblPrimary1.Text);
                string[] items2 = drawbreakPoint(lblPrimary2.Text);
                string[] items3 = drawbreakPoint(lblSecondary1.Text);
                string[] items4 = drawbreakPoint(lblSecondary2.Text);

                form.drawBreakpoints(chkColour.Checked, items1[0], Convert.ToInt32(items1[1]), items2[0], Convert.ToInt32(items2[1]), items3[0], Convert.ToInt32(items3[1]), items4[0], Convert.ToInt32(items4[1]));
            }
            catch { }
        }

        private string[] drawbreakPoint(string data)
        {
            string[] answer = { "#", "-1"}; ;
            string[] items = data.Split(":");
            if (items.Length == 3)
            {
                answer[0] = items[1].Trim();
                answer[1] = items[2].Replace(",", "");
            }
            return answer;
        }
    }
}
