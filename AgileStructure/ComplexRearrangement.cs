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
                    t = scondary5primeOfPlace1;
                    scondary5primeOfPlace1 = scondary5primeOfPlace2;
                    scondary5primeOfPlace2 = t;
                    lblPrimary1.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary1.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                }

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
                    t = scondary5primeOfPlace1;
                    scondary5primeOfPlace1 = scondary5primeOfPlace2;
                    scondary5primeOfPlace2 = t;
                    lblPrimary2.Text = "Break point 1: " + first[1].getReferenceName + ":" + average1.ToString("N0");
                    lblSecondary2.Text = "Break point 2: " + first[0].getReferenceName + ":" + average2.ToString("N0");
                }

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

            average11 = pda.Average1;
            average12 = pda.Average2;
            primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
            primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
            secondary5primeOfPlace11 = pda.Secondary5primeOfPlace1;
            secondary5primeOfPlace12 = pda.Primary5primeOfPlace2;
            annotations1 = pda.Annotations;

            average21 = pdb.Average1;
            average22 = pdb.Average2;
            primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
            primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
            secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
            secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
            annotations2 = pdb.Annotations;
                       
            if (chromosomes.Count == 1)
            { return Find(); }
            else
            { return differentFind(); }

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

        private string Find()
        {
            try
            {
                txtAnswer.Clear();
                int[] places = getUniquePlaces();
                float[] alignment = setPrimaryAlignmentLocation(places);

                if (places.Length == 3)
                {
                    return inversionWithCommonBreaKPoint(alignment, places);
                }
                else if (places.Length == 2)
                {
                    return "This applears to be a simple rearrangement, please use the basic annotation functions";
                }
                else if (places.Length == 4)
                {

                    if (alignment[1] < 0.2f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2]; }
                        else
                        {
                            string answer = "Insertion-deletion: " + items1[0] + "." + items2[2] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[1] + "\r\nor\r\n" +
                            "Inversion: " + items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                            return answer; //9 line 1, 49 line 2
                        }
                    }
                    else if (alignment[1] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith("o") == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }
                        else
                        {
                            string answer = "Insertion-deletion: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2] + "\r\nor\r\n";// +
                            answer = "Inversion: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                            return answer; //41 line 1
                        }
                    }
                    else if (annotations1[3].StartsWith("o") == true && annotations2[4].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[3]);
                        string[] items2 = processIAnnotationString(annotations2[4]);
                        if (alignment[1] > 0.8f)
                        { return "Insertion - deletion: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                        else if (alignment[2] > 0.8f)
                        { return "Insertion-deletion: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is deleted and replaced by " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                    }
                    else if (alignment[2] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return "Inversion: " + items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2]; }
                        else
                        {
                            string answer = "Insertion - deletion: " + items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2] + "\r\nor\r\n" +
                                "Inversion: " + items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                            return answer;// 1 line 1, 41 line 2
                        }
                    }
                    else if (alignment[3] > 0.8f && annotations1[0].StartsWith("o") == true && annotations2[0].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[0]);
                        string[] items2 = processIAnnotationString(annotations2[0]);
                        if (average12 < average22)
                        { return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2]; }
                        else
                        {
                            string answer = "Insertion - deletion: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items2[0] + "." + items2[1] + "_" + items1[1] + "\r\nor\r\n" +
                                 "Inversion: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                            return answer;// 9 line 1, 50 line 2
                        }
                    }
                    else if (annotations1[4].StartsWith("o") == true && annotations2[3].StartsWith('o') == true)
                    {
                        string[] items1 = processIAnnotationString(annotations1[4]);
                        string[] items2 = processIAnnotationString(annotations2[3]);
                        if (alignment[3] > 0.8f)
                        { return "Insertion - deletion: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items1[1] + "_" + items2[1]; }
                        else
                        { return "Insertion - deletion: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is deleted and replaced by " + items1[0] + "." + items1[1] + "_" + items2[1]; }
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
                        {
                            //return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                        else if (alignment[1] < 0.2f)
                        {
                            //return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]);
                    if (alignment[1] < 0.2f)
                    {
                        //return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        //return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
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
                        {
                            return "Duplication: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items2[1] + "_" + items1[1];
                        }

                        else if (alignment[1] < 0.2f)
                        {
                            //return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[3]);
                    string[] items2 = processIAnnotationString(annotations2[4]); 
                    if (alignment[0] < 0.2f)
                    {
                        return items1[0] + "." + items2[1] + "_" + items1[1] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        //         return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                }
                else if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[3]);
                    string[] items2 = processIAnnotationString(annotations2[4]);
                    if (alignment[0] < 0.2f)
                    {
                        return "Duplication: " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[1];
                    }
                    else if (alignment[1] < 0.2f)
                    {
                        //         return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
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
                        {
                            //       return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                        else if (alignment[1] < 0.2f)
                        {
                            //       return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                        else if (alignment[0] > 0.8f)
                        {
                            return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];
                        }
                    }
                    else
                    {
                        if (alignment[0] > 0.8f)
                        {
                            return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[4]); 
                    if (alignment[1] < 0.2f)
                    {
                        //     return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        //          return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }

                }
                else if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[4]); 
                    if (alignment[1] < 0.2f)
                    {
                         return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        //   return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
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
                        {
                            //return items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                        else if (alignment[1] < 0.2f)
                        {
                            //return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                        else if (alignment[0] > 0.8f)
                        {
                            return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[2];
                        }
                    }
                    else
                    {
                        if (alignment[2] < 0.2f)
                        {
                            return items1[0] + "." + items2[2] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[1];
                        }
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[3]); if (alignment[1] < 0.2f)
                    {
                        return "Duplication: " + items1[0] + "." + items1[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        // return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }

                }
                else if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[4]);
                    string[] items2 = processIAnnotationString(annotations2[3]); if (alignment[1] < 0.2f)
                    {
                        // return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        //return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
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
                        {
                            string answer = "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[1] + "\r\nor\r\n" +
                                 "Duplication: the reverse complement of " + items1[0] + "." + items2[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items1[2] + "\r\nor\r\n" +
                                 "Inversion: " + items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                            return answer;//2 line 1, 5 line 2, 44 line 2 or 3
                        }
                        else if (alignment[1] < 0.2f)
                        {
                            return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                        else if (alignment[0] > 0.8f)
                        {
                            //return "Duplication: " + items1[0] + "." + items2[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                        else if (alignment[2] < 0.2f)
                        {
                            return "The reverse complement of " + items1[0] + "." + items2[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                        else if (alignment[2] > 0.8f)
                        {
                            return "Inversion: " + items1[0] + "." + items2[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }                      
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        return "Duplication: the reverse complement of " +items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " +  items1[0] + "." + items2[1] + "_" + items1[2] ;
                    }
                    else if (alignment[1] < 0.2f)
                    {
                        return "The reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2] + " is inserted at " + items1[0] + "." + items2[2] + "_" + items1[1];
                    }
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]);
                    if (alignment[1] < 0.2f)
                    {
                        string answer = "Duplication: the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[2] + "\r\nor\r\n" +
                            "Inversion: " + items1[0] + "." + items2[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items1[1] + "_" + items1[2];
                        return answer;// 8 line 1, 48 line 2
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                    else if (alignment[0] < 0.2f)
                    {
                        string answer = "The reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2] + " is inserted at " + items1[0] + "." + items2[1] + "_" + items2[2];// + "\r\nor\r\n" +
                            //"Duplication: the reverse complement of " + items1[0] + "." + items2[2] + "_" + items1[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[1];

                        return answer;
                    }

                }
                else if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]);
                    if (alignment[1] < 0.2f)
                    {
                        //return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        // return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                    else if (alignment[0] > 0.8f)
                    {
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];
                    }

                }
            }
            else if (annotations1[0].StartsWith("o") && annotations2[4].StartsWith("C"))
            {
                if (alignment[0] > 0.4f && alignment[0] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);

                    if (places[1] == average11 || places[1] == average12)
                    {
                        if (alignment[1] > 0.8f)
                        {
                            //string answer = "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[1] + "\r\nor\r\n" +
                            //    "Duplication: the reverse complement of " + items1[0] + "." + items2[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items1[2];
                            //return answer;
                        }
                        else if (alignment[1] < 0.2f)
                        {
                            string[] items2 = processIAnnotationString(annotations2[0]);
                            return items1[0] + "." + items1[1] + "_" + items2[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                        }
                        else if (alignment[0] > 0.8f)
                        {
                            string[] items2 = processIAnnotationString(annotations2[0]);
                            return "Duplication: " + items1[0] + "." + items2[1] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                        }
                    }
                    else
                    {
                        return "Duplication: " + items1[0] + "." + average22.ToString() + "_" + items1[2] + " is inserted at " + items1[0] + "." + average12.ToString("N0") + "_" + average22.ToString();
                    }
                    
                }
                else if (alignment[2] > 0.4f && alignment[2] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string answer = "Duplication: The reverse complement of " + items1[0] + "." + average22.ToString("N0") + "_" + items1[2] + " is inserted at " + items1[0] + "." + items1[1] + "_" + average22.ToString();
                    return answer;
                }
                else if (alignment[1] > 0.4f && alignment[1] < 0.6f)
                {
                    string[] items1 = processIAnnotationString(annotations1[0]);
                    string[] items2 = processIAnnotationString(annotations2[0]); if (alignment[1] < 0.2f)
                    {
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[2] + "_" + items2[1] + " is inserted at " + items1[0] + "." + items1[1] + "_" + items2[2];
                    }
                    else if (alignment[1] > 0.8f)
                    {
                        return items1[0] + "." + items1[1] + "_" + items1[2] + " is deleted and replaced by the reverse complement of " + items1[0] + "." + items2[1] + "_" + items1[2];
                    }
                    else if (alignment[0] > 0.8f)
                    {
                        return "Duplication: the reverse complement of " + items1[0] + "." + items1[1] + "_" + items2[2] + " is inserted at " + items1[0] + "." + items1[2] + "_" + items2[1];
                    }

                }
            }

            return "AgileStructure is unable to annotate this rearrangement";
        }

        private bool stringMathEquals(string one, string two)
        {
            bool answer = false;
            try
            {
                int iOne = Convert.ToInt32(one.Replace(",", ""));
                int iTwo = Convert.ToInt32(two.Replace(",", ""));
                int iAnswer = Math.Abs(iOne - iTwo);

                if (iAnswer < 200) { answer = true; }

            }
            catch { }
            return answer;
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
                            if (average12 <= average22)
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            {
                                return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; 
                            }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22) { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; } else { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                        }
                        else
                        {
                            if (average12 <= average22)
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[5] + "_" + fragments[4] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
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
                            if (average12 <= average22) { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[2] + "_" + fragments[1]; }
                        }
                        else
                        {
                            if (average12 <= average22)
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[2] + "_" + fragments[1]; }
                        }
                    }
                    else
                    {
                        if (fragments[3] == primaryReference)
                        {
                            if (average12 <= average22)
                            { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; }
                            else
                            { return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[2] + "_" + fragments[1]; }
                        }
                        else
                        {
                            if (average12 <= average22) { return fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[1] + "_" + fragments[2]; } else { return "The reverse complement of " + fragments[3] + ":" + fragments[4] + "_" + fragments[5] + " has been inserted in to " + fragments[0] + ":" + fragments[2] + "_" + fragments[1]; }
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

        private void btnDrawBP_Click(object sender, EventArgs e)
        {
            try
            {
                string[] items1 = drawbreakPoint(lblPrimary1.Text);
                string[] items2 = drawbreakPoint(lblPrimary2.Text);
                string[] items3 = drawbreakPoint(lblSecondary1.Text);
                string[] items4 = drawbreakPoint(lblSecondary2.Text);

                form.drawBreakpoints(items1[0], Convert.ToInt32(items1[1]), items2[0], Convert.ToInt32(items2[1]), items3[0], Convert.ToInt32(items3[1]), items4[0], Convert.ToInt32(items4[1]));
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
