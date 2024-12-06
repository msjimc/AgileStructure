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
    public partial class DrawSelectedReads : Form
    {
        string primaryReference = "";

        private Form1 form = null;
        private int average11;
        private int average12;
        private float primary5primeOfPlace11;
        private float primary5primeOfPlace12;
        private float secondary5primeOfPlace11;
        private float secondary5primeOfPlace12;
        private AlignedRead[] bp1 = null;
        private int average21;
        private int average22;
        private float primary5primeOfPlace21;
        private float primary5primeOfPlace22;
        private float secondary5primeOfPlace21;
        private float secondary5primeOfPlace22;
        private AlignedRead[] bp2 = null;

        PointData pd1 = null;
        PointData pd2 = null;
        List<string> chromosomes;


        public DrawSelectedReads(Form1 parentForm, string PrimaryReference)
        {
            InitializeComponent();
            form = parentForm;
            this.primaryReference = PrimaryReference;
        }

        public void clean()
        {
            average11 = 0;
            average12 = 0;
            primary5primeOfPlace11 = 0;
            primary5primeOfPlace12 = 0;
            secondary5primeOfPlace11 = 0;
            secondary5primeOfPlace12 = 0;
            bp1 = null;
            average21 = 0;
            average22 = 0;
            primary5primeOfPlace21 = 0;
            primary5primeOfPlace22 = 0;
            secondary5primeOfPlace21 = 9;
            secondary5primeOfPlace22 = 9;
            bp2 = null;

            lblPrimary1.Text = "not set";
            lblPrimary2.Text = "not set";
            lblSecondary1.Text = "not set";
            lblSecondary2.Text = "not set";
            blankImage();
        }

        private void btnAccept1_Click(object sender, EventArgs e)
        {
            if (form.isSecondRefSet() == false)
            { MessageBox.Show("Please select a region in the lower panel", "Error"); return; }


            btnAccept1.Enabled = true;
            btnAccept2.Enabled = true;


            try
            {
                if (form == null) { return; }
                blankImage();

                chromosomes = new List<string>();

                string[] annotations = null;// form.externalannotations();

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

                pd1 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, scondary5primeOfPlace1, scondary5primeOfPlace2, annotations, form.GetSelectedReads());

                form.deleteSelectedList();

            }
            catch
            {
                lblPrimary1.Text = "Error";
                lblSecondary1.Text = "Error";
                pd1 = null;
            }
        }

        private void btnAccept2_Click(object sender, EventArgs e)
        {
            if (form.isSecondRefSet() == false)
            { MessageBox.Show("Please select a region in the lower panel", "Error"); return; }

            try
            {
                if (form == null) { return; }
                blankImage();

                string[] annotations = null; // form.externalannotations();

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

                pd2 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, scondary5primeOfPlace1, scondary5primeOfPlace2, annotations, form.GetSelectedReads());

                form.deleteSelectedList();
            }
            catch
            {
                lblPrimary2.Text = "Error";
                lblSecondary2.Text = "Error";
                pd2 = null;
            }
        }

        private void Find(PointData pda, PointData pdb)
        {
            
            if (pda == null && pdb == null) 
            { MessageBox.Show( "Please set the break point(s)","No break points selected"); }
            else if (pda != null && pdb != null)
            {
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
                        bp1 = pda.SelectedRead;

                        average21 = pdb.Average1;
                        average22 = pdb.Average2;
                        primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                        primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                        secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                        secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                        bp2 = pdb.SelectedRead;
                    }
                    else if (pdb.Average1 + 50 < pda.Average1)
                    {
                        average11 = pdb.Average1;
                        average12 = pdb.Average2;
                        primary5primeOfPlace11 = pdb.Primary5primeOfPlace1;
                        primary5primeOfPlace12 = pdb.Primary5primeOfPlace2;
                        secondary5primeOfPlace11 = pdb.Secondary5primeOfPlace1;
                        secondary5primeOfPlace12 = pdb.Secondary5primeOfPlace2;
                        bp1 = pdb.SelectedRead;

                        average21 = pda.Average1;
                        average22 = pda.Average2;
                        primary5primeOfPlace21 = pda.Primary5primeOfPlace1;
                        primary5primeOfPlace22 = pda.Primary5primeOfPlace2;
                        secondary5primeOfPlace21 = pda.Secondary5primeOfPlace1;
                        secondary5primeOfPlace22 = pda.Secondary5primeOfPlace2;
                        bp2 = pda.SelectedRead;
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
                            bp1 = pda.SelectedRead;

                            average21 = pdb.Average1;
                            average22 = pdb.Average2;
                            primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                            primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                            secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                            secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                            bp2 = pdb.SelectedRead;
                        }
                        else
                        {
                            average11 = pdb.Average1;
                            average12 = pdb.Average2;
                            primary5primeOfPlace11 = pdb.Primary5primeOfPlace1;
                            primary5primeOfPlace12 = pdb.Primary5primeOfPlace2;
                            secondary5primeOfPlace11 = pdb.Secondary5primeOfPlace1;
                            secondary5primeOfPlace12 = pdb.Secondary5primeOfPlace2;
                            bp1 = pdb.SelectedRead;

                            average21 = pda.Average1;
                            average22 = pda.Average2;
                            primary5primeOfPlace21 = pda.Primary5primeOfPlace1;
                            primary5primeOfPlace22 = pda.Primary5primeOfPlace2;
                            secondary5primeOfPlace21 = pda.Secondary5primeOfPlace1;
                            secondary5primeOfPlace22 = pda.Secondary5primeOfPlace2;
                            bp2 = pda.SelectedRead;
                        }
                    }                    
                }
                else
                {
                    average11 = pda.Average1;
                    average12 = pda.Average2;
                    primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
                    primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
                    secondary5primeOfPlace11 = pda.Secondary5primeOfPlace1;
                    secondary5primeOfPlace12 = pda.Secondary5primeOfPlace2;
                    bp1 = pda.SelectedRead;

                    average21 = pdb.Average1;
                    average22 = pdb.Average2;
                    primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                    primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                    secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                    secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                    bp2 = pdb.SelectedRead;
                }
            }
            else if (pda != null)
            {
                average11 = pda.Average1;
                average12 = pda.Average2;
                primary5primeOfPlace11 = pda.Primary5primeOfPlace1;
                primary5primeOfPlace12 = pda.Primary5primeOfPlace2;
                secondary5primeOfPlace11 = pda.Secondary5primeOfPlace1;
                secondary5primeOfPlace12 = pda.Secondary5primeOfPlace2;
                bp1 = pda.SelectedRead;
            }
            else if (pdb != null)
            {
                average11 = pdb.Average1;
                average12 = pdb.Average2;
                primary5primeOfPlace11 = pdb.Primary5primeOfPlace1;
                primary5primeOfPlace12 = pdb.Primary5primeOfPlace2;
                secondary5primeOfPlace11 = pdb.Secondary5primeOfPlace1;
                secondary5primeOfPlace12 = pdb.Secondary5primeOfPlace2;
                bp1 = pdb.SelectedRead;
            }
        }


        private void blankImage()
        {
            Bitmap bmp = new Bitmap(p1.Width, p1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.White);
            p1.Image = bmp; ;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Find(pd1, pd2);

            Bitmap bmp = new Bitmap(p1.Width, p1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            try
            {

                drawReferenceAndBreakpoints(chromosomes.Count, new Rectangle(0, 0, bmp.Width, bmp.Height), g);



            }
            finally
            { p1.Image = bmp; }
        }

        private void drawReferenceAndBreakpoints(int chromosomeCount, Rectangle area, Graphics g)
        {
            breakpointBasic[] bpb = GetBreakpointBasicArray();


            int referencelength = area.Width - 20;
            int QuarterWidth = (area.Width - 80) / 4;
            int referencelineHieght = (int)((area.Height - 60) * 0.5) + 50;
            int ThirdHieght = 50;

            Point first = new Point(10, QuarterWidth + 10);
            Point second = new Point(QuarterWidth + 30, (2 * QuarterWidth) + 30);
            Point third = new Point((2 * QuarterWidth) + 50, (3 * QuarterWidth) + 50);
            Point fourth = new Point((3 * QuarterWidth) + 70, referencelength + 10);

            Pen referencePen = new Pen(Brushes.Black, 2);
            Pen verticalPen = new Pen(Brushes.Black, 1);
            verticalPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            g.DrawLine(referencePen, first.X, referencelineHieght, first.Y, referencelineHieght);
            g.DrawLine(referencePen, second.X, referencelineHieght, second.Y, referencelineHieght);
            g.DrawLine(referencePen, third.X, referencelineHieght, third.Y, referencelineHieght);
            g.DrawLine(referencePen, fourth.X, referencelineHieght, fourth.Y, referencelineHieght);


            if (chromosomeCount == 1)
            {
                g.DrawLine(referencePen, second.Y + 5, referencelineHieght - 10, second.Y - 5, referencelineHieght + 10);
                g.DrawLine(referencePen, third.X + 5, referencelineHieght - 10, third.X - 5, referencelineHieght + 10);                
            }

            Font f = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);

            if (Math.Abs(bpb[0].Position - bpb[1].Position) > 10000)
            {
                g.DrawLine(referencePen, first.Y + 5, referencelineHieght - 10, first.Y - 5, referencelineHieght + 10);
                g.DrawLine(referencePen, second.X + 5, referencelineHieght - 10, second.X - 5, referencelineHieght + 10);                
                
                g.DrawLine(verticalPen, (first.Y /2) + first.X, area.Bottom - 10, (first.Y /2) + first.X, ThirdHieght);               
                SizeF length = g.MeasureString(bpb[0].Position.ToString("N0"), f);
                g.DrawString(bpb[0].Position.ToString("N0"), f, Brushes.Black, (first.Y / 2) + first.X - (length.Width / 2), 10);
                
                
                g.DrawLine(verticalPen, ((second.Y - second.X) /2) + second.X, area.Bottom - 10, ((second.Y - second.X) / 2) + second.X, ThirdHieght);
                length = g.MeasureString(bpb[1].Position.ToString("N0"), f);
                g.DrawString(bpb[1].Position.ToString("N0"), f, Brushes.Black, ((second.Y - second.X) / 2) + second.X - (length.Width / 2), 10);

            }
            else
            { g.DrawLine(referencePen, first.Y, referencelineHieght, second.X, referencelineHieght); }

            if (Math.Abs(bpb[2].Position - bpb[3].Position) > 10000)
            {
                g.DrawLine(referencePen, third.Y + 5, referencelineHieght - 10, third.Y - 5, referencelineHieght + 10);
                g.DrawLine(referencePen, fourth.X + 5, referencelineHieght - 10, fourth.X - 5, referencelineHieght + 10);
               
                g.DrawLine(verticalPen, ((third.Y - third.X) /2) + third.X, area.Bottom - 10, ((third.Y - third.X) / 2) + third.X, ThirdHieght);
                SizeF length = g.MeasureString(bpb[2].Position.ToString("N0"), f);
                g.DrawString(bpb[2].Position.ToString("N0"), f, Brushes.Black, ((third.Y - third.X) / 2) + third.X - (length.Width / 2), 10);

                g.DrawLine(verticalPen, ((fourth.Y - fourth.X) /2) + fourth.X, area.Bottom - 10, ((fourth.Y - fourth.X) / 2) + fourth.X, ThirdHieght);
                length = g.MeasureString(bpb[3].Position.ToString("N0"), f);
                g.DrawString(bpb[3].Position.ToString("N0"), f, Brushes.Black, ((fourth.Y - fourth.X) / 2) + fourth.X - (length.Width / 2), 10);
            }
            else
            { g.DrawLine(referencePen, third.Y, referencelineHieght, third.X, referencelineHieght); }



        }

        private breakpointBasic[] GetBreakpointBasicArray()
        {
            string[] items1 = drawbreakPoint(lblPrimary1.Text);
            string[] items2 = drawbreakPoint(lblPrimary2.Text);
            string[] items3 = drawbreakPoint(lblSecondary1.Text);
            string[] items4 = drawbreakPoint(lblSecondary2.Text);

            List<breakpointBasic> bpb = new List<breakpointBasic>();
            if (items1[0] != "#")
            { bpb.Add(new breakpointBasic(items1)); }
            if (items2[0] != "#")
            { bpb.Add(new breakpointBasic(items2)); }
            if (items3[0] != "#")
            { bpb.Add(new breakpointBasic(items3)); }
            if (items4[0] != "#")
            { bpb.Add(new breakpointBasic(items4)); }

            bpb.Sort(new breakpointBasicSorter());

            return bpb.ToArray();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string[] items1 = drawbreakPoint(lblPrimary1.Text);
                string[] items2 = drawbreakPoint(lblPrimary2.Text);
                string[] items3 = drawbreakPoint(lblSecondary1.Text);
                string[] items4 = drawbreakPoint(lblSecondary2.Text);

                form.drawBreakpoints(true, items1[0], Convert.ToInt32(items1[1]), items2[0], Convert.ToInt32(items2[1]), items3[0], Convert.ToInt32(items3[1]), items4[0], Convert.ToInt32(items4[1]));
            }
            catch { }
        }

        private string[] drawbreakPoint(string data)
        {
            string[] answer = { "#", "-1" }; ;
            string[] items = data.Split(":");
            if (items.Length == 3)
            {
                answer[0] = items[1].Trim();
                answer[1] = items[2].Replace(",", "");
            }
            return answer;
        }

        private int[] getUniquePlaces()
        {
            int[] uniquePlaces = { average11, average12, average21, average22 };
            return uniquePlaces;
        }


    }
}
