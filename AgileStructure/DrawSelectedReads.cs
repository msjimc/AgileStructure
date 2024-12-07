using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

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
        BreakPointData[] breakPoints1 = null;
        private int average21;
        private int average22;
        private float primary5primeOfPlace21;
        private float primary5primeOfPlace22;
        private float secondary5primeOfPlace21;
        private float secondary5primeOfPlace22;
        private AlignedRead[] bp2 = null;
        BreakPointData[] breakPoints2 = null;

        PointData pd1 = null;
        PointData pd2 = null;
        List<string> chromosomes;


        public DrawSelectedReads(Form1 parentForm, string PrimaryReference, string[] referenceSequences)
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
            breakPoints1 = null;
            average21 = 0;
            average22 = 0;
            primary5primeOfPlace21 = 0;
            primary5primeOfPlace22 = 0;
            secondary5primeOfPlace21 = 9;
            secondary5primeOfPlace22 = 9;
            bp2 = null;
            breakPoints2 = null;

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

                pd1 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, scondary5primeOfPlace1, scondary5primeOfPlace2, annotations, form.GetSelectedReads(), first);

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

                pd2 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, scondary5primeOfPlace1, scondary5primeOfPlace2, annotations, form.GetSelectedReads(), first);

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
            { MessageBox.Show("Please set the break point(s)", "No break points selected"); }
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
                        breakPoints1 = pda.BreakPoints;
                        average21 = pdb.Average1;
                        average22 = pdb.Average2;
                        primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                        primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                        secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                        secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                        bp2 = pdb.SelectedRead;
                        breakPoints2 = pdb.BreakPoints;
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
                        breakPoints1 = pdb.BreakPoints;

                        average21 = pda.Average1;
                        average22 = pda.Average2;
                        primary5primeOfPlace21 = pda.Primary5primeOfPlace1;
                        primary5primeOfPlace22 = pda.Primary5primeOfPlace2;
                        secondary5primeOfPlace21 = pda.Secondary5primeOfPlace1;
                        secondary5primeOfPlace22 = pda.Secondary5primeOfPlace2;
                        bp2 = pda.SelectedRead;
                        breakPoints2 = pda.BreakPoints;
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
                            breakPoints1 = pda.BreakPoints;

                            average21 = pdb.Average1;
                            average22 = pdb.Average2;
                            primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                            primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                            secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                            secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                            bp2 = pdb.SelectedRead;
                            breakPoints2 = pdb.BreakPoints;
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
                            breakPoints1 = pdb.BreakPoints;

                            average21 = pda.Average1;
                            average22 = pda.Average2;
                            primary5primeOfPlace21 = pda.Primary5primeOfPlace1;
                            primary5primeOfPlace22 = pda.Primary5primeOfPlace2;
                            secondary5primeOfPlace21 = pda.Secondary5primeOfPlace1;
                            secondary5primeOfPlace22 = pda.Secondary5primeOfPlace2;
                            bp2 = pda.SelectedRead;
                            breakPoints2 = pda.BreakPoints;
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
                    breakPoints1 = pda.BreakPoints;

                    average21 = pdb.Average1;
                    average22 = pdb.Average2;
                    primary5primeOfPlace21 = pdb.Primary5primeOfPlace1;
                    primary5primeOfPlace22 = pdb.Primary5primeOfPlace2;
                    secondary5primeOfPlace21 = pdb.Secondary5primeOfPlace1;
                    secondary5primeOfPlace22 = pdb.Secondary5primeOfPlace2;
                    bp2 = pdb.SelectedRead;
                    breakPoints2 = pdb.BreakPoints;
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
                breakPoints1 = pda.BreakPoints;
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
                breakPoints1 = pdb.BreakPoints;
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

            int referencelength = area.Width - 20;
            int QuarterWidth = (area.Width - 80) / 4;
            int referencelineHieght = (area.Height - 30);

            Point first = new Point(10, QuarterWidth + 10);
            Point second = new Point(QuarterWidth + 30, (2 * QuarterWidth) + 30);
            Point third = new Point((2 * QuarterWidth) + 50, (3 * QuarterWidth) + 50);
            Point fourth = new Point((3 * QuarterWidth) + 70, referencelength + 10);

            breakpointBasic[] bpb = GetBreakpointBasicArray(first, second, third, fourth);

            Pen referencePen = new Pen(Brushes.Black, 2);
            if (chromosomeCount == 1)
            {
                g.DrawLine(referencePen, first.X, referencelineHieght, fourth.Y, referencelineHieght);
                drawReferenceAndBreakpointsSingleChromosome(area, g, bpb, first, second, third, fourth, referencelineHieght);
            }
            else
            {
                g.DrawLine(referencePen, first.X, referencelineHieght, second.Y, referencelineHieght);
                g.DrawLine(referencePen, third.X, referencelineHieght, fourth.Y, referencelineHieght);
                drawReferenceAndBreakpointsSingleChromosome(area, g, bpb, first, second, third, fourth, referencelineHieght);
            }
        }

        private void drawReferenceAndBreakpointsDualChromosome(Rectangle area, Graphics g, breakpointBasic[] bpb, Point first, Point second, Point third, Point fourth, int ReferenceHeight)
        { }

        private void drawReferenceAndBreakpointsSingleChromosome(Rectangle area, Graphics g, breakpointBasic[] bpb, Point first, Point second, Point third, Point fourth, int ReferenceHeight)
        {

            Pen verticalPen = new Pen(Brushes.Black, 1);
            verticalPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font f = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);

            Writelables(g, f, first, ref bpb[0], verticalPen);
            Writelables(g, f, second, ref bpb[1], verticalPen);
            Writelables(g, f, third, ref bpb[2], verticalPen);
            Writelables(g, f, fourth, ref bpb[3], verticalPen);


            Dictionary<string, int> orientations = new Dictionary<string, int>();
            orientations = GetAligmnentSets(breakPoints1, bp1, orientations);
            orientations = GetAligmnentSets(breakPoints2, bp2, orientations);

            DrawBlocks(g, f, orientations, bpb, area, ReferenceHeight, 10);

        }

        private void DrawBlocks(Graphics g, Font f, Dictionary<string, int> orientations, breakpointBasic[] bpb, Rectangle area, int ReferenceHeight, int itemHeight)
        {
            // primary(orientation  chr breackpoint side) sec(ori   chr breakpoint  side)
            int counter = 1;
            Rectangle r1 = new Rectangle(0, 0, 0, 0); ;
            Rectangle r2 = new Rectangle(0, 0, 0, 0); ;

            foreach (string data in orientations.Keys)
            {
                string[] items = data.Split(':');
                int primaryBreakPoint = Convert.ToInt32(items[2]);
                int secondaryBreakPoint = Convert.ToInt32(items[6]);


                foreach (breakpointBasic place in bpb)
                {
                    if (place.Position == primaryBreakPoint && place.Chromosme == items[1])
                    {
                        r1 = new Rectangle(0, ReferenceHeight - (3 * counter * itemHeight), 100, itemHeight);
                        if (items[3] == "r") { r1.X = place.ImagePlace; }
                        else { r1.X = place.ImagePlace - 100; }

                        g.FillRectangle(Brushes.Green, r1);
                        g.DrawRectangle(Pens.Black, r1);
                        break;
                    }
                }
                foreach (breakpointBasic place in bpb)
                {
                    if (place.Position == secondaryBreakPoint && place.Chromosme == items[5])
                    {
                        r2 = new Rectangle(0, ReferenceHeight - (3 * counter * itemHeight), 100, itemHeight);
                        if (items[7] == "r") { r2.X = place.ImagePlace; }
                        else { r2.X = place.ImagePlace - 100; }
                        if (items[0] == items[4])
                        { g.FillRectangle(Brushes.Green, r2); }
                        else { g.FillRectangle(Brushes.Red, r2); }
                        g.DrawRectangle(Pens.Black, r2);
                        break;
                    }
                }
                counter++;

                Pen connector = new Pen(Brushes.Black, 1.5f);
                connector.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                if (r1.X > 0 && r2.X > 0)
                {
                    int left = 0;
                    int right = 0;
                    if (items[3] == "l")
                    {
                        left = r1.Right + 15;
                        g.DrawLine(connector, r1.Right, (r1.Top + r1.Bottom) / 2, left, (r1.Top + r1.Bottom) / 2);
                        g.DrawLine(connector, left, (r1.Top + r1.Bottom) / 2, left, ((r1.Top + r1.Bottom) / 2) - (1.5f * itemHeight));
                    }
                    else
                    {
                        left = r1.Left - 15;
                        g.DrawLine(connector, r1.Left, (r1.Top + r1.Bottom) / 2, left, (r1.Top + r1.Bottom) / 2);
                        g.DrawLine(connector, left, (r1.Top + r1.Bottom) / 2, left, ((r1.Top + r1.Bottom) / 2) - (1.5f * itemHeight));
                    }

                    if (items[7] == "l")
                    {
                        right = r2.Right + 15;
                        g.DrawLine(connector, r2.Right, (r2.Top + r2.Bottom) / 2, right, (r2.Top + r2.Bottom) / 2);
                        g.DrawLine(connector, right, (r2.Top + r2.Bottom) / 2, right, ((r2.Top + r2.Bottom) / 2) - (1.5f * itemHeight));
                    }
                    else
                    {
                        right = r2.Left - 15;
                        g.DrawLine(connector, r2.Left, (r2.Top + r2.Bottom) / 2, right, (r2.Top + r2.Bottom) / 2);
                        g.DrawLine(connector, right, (r2.Top + r2.Bottom) / 2, right, ((r2.Top + r2.Bottom) / 2) - (1.5f * itemHeight));
                    }
                    g.DrawLine(connector, left, ((r1.Top + r1.Bottom) / 2) - (1.5f * itemHeight), right, ((r2.Top + r2.Bottom) / 2) - (1.5f * itemHeight));
                }

            }

        }

        private void Writelables(Graphics g, Font f, Point region, ref breakpointBasic data, Pen verticalPen)
        {
            int ThirdHieght = 60;

            g.DrawLine(verticalPen, data.ImagePlace, p1.Bottom - 10, data.ImagePlace, ThirdHieght);
            SizeF length1 = g.MeasureString(data.Chromosme, f);
            int point1 = ((region.Y - region.X) / 2) + region.X;
            g.DrawString(data.Chromosme, f, Brushes.Black, point1 - ((length1.Width - 8) / 2), 10);
            SizeF length2 = g.MeasureString(data.Position.ToString("N0"), f);
            g.DrawString(data.Position.ToString("N0"), f, Brushes.Black, point1 - ((length2.Width - 8) / 2), 25);

            int length = (int)(length1.Width - 8);
            if (length2.Width > length1.Width)
            { length = (int)(length2.Width - 8); }
            g.DrawLine(Pens.Black, point1 - (length / 2), ThirdHieght - 15, point1 + (length / 2), ThirdHieght - 15);

            if (point1 - (length / 2) > data.ImagePlace)
            { g.DrawLine(Pens.Black, point1 - (length / 2), ThirdHieght - 15, data.ImagePlace, ThirdHieght); }
            else if (point1 + (length / 2) < data.ImagePlace)
            { g.DrawLine(Pens.Black, point1 + (length / 2), ThirdHieght - 15, data.ImagePlace, ThirdHieght); }
            else
            { g.DrawLine(Pens.Black, point1, ThirdHieght - 15, data.ImagePlace, ThirdHieght); }

        }

        private breakpointBasic[] GetBreakpointBasicArray(Point first, Point second, Point third, Point Fourth)
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

            if (bpb.Count >3 && bpb[0].Chromosme == bpb[3].Chromosme && Math.Abs(bpb[0].Position - bpb[3].Position) < 10)
            {
                int place = (first.X + Fourth.Y) / 2;
                bpb[0].ImagePlace = place;
                bpb[1].ImagePlace = place;
                bpb[2].ImagePlace = place;
                bpb[3].ImagePlace = place;
            }
            else
            {
                if (bpb.Count > 2 && bpb[0].Chromosme == bpb[2].Chromosme && Math.Abs(bpb[0].Position - bpb[2].Position) < 10)
                {
                    int place = (first.X + third.Y) / 2;
                    bpb[0].ImagePlace = place;
                    bpb[1].ImagePlace = place;
                    bpb[2].ImagePlace = place;
                }
                else if (bpb.Count > 3 && bpb[1].Chromosme == bpb[3].Chromosme && Math.Abs(bpb[1].Position - bpb[3].Position) < 10)
                {
                    int place = (second.X + Fourth.Y) / 2;
                    bpb[1].ImagePlace = place;
                    bpb[2].ImagePlace = place;
                    bpb[3].ImagePlace = place;
                }
                else
                {
                    if (bpb.Count > 1 && bpb[0].Chromosme == bpb[1].Chromosme && Math.Abs(bpb[0].Position - bpb[1].Position) < 10)
                    {
                        int place = (first.X + second.Y) / 2;
                        bpb[0].ImagePlace = place;
                        bpb[1].ImagePlace = place;
                    }
                    else if (bpb.Count > 2 && bpb[1].Chromosme == bpb[2].Chromosme && Math.Abs(bpb[1].Position - bpb[2].Position) < 10)
                    {
                        int place = (second.X + third.Y) / 2;
                        bpb[1].ImagePlace = place;
                        bpb[2].ImagePlace = place;
                    }
                    else if (bpb.Count > 3 && bpb[2].Chromosme == bpb[3].Chromosme && Math.Abs(bpb[2].Position - bpb[3].Position) < 10)
                    {
                        int place = (third.X + Fourth.Y) / 2;
                        bpb[2].ImagePlace = place;
                        bpb[3].ImagePlace = place;
                    }
                }
            }
            if (bpb[0].ImagePlace == -1)
            { bpb[0].ImagePlace = (first.Y + first.X) / 2; }

            if (bpb[1].ImagePlace == -1)
            { bpb[1].ImagePlace = (second.Y + second.X) / 2; }

            if (bpb[2].ImagePlace == -1)
            { bpb[2].ImagePlace = (third.Y + third.X) / 2; }

            if (bpb[3].ImagePlace == -1)
            { bpb[3].ImagePlace = (Fourth.Y + Fourth.X) / 2; }

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

        private Dictionary<string, int> GetAligmnentSets(BreakPointData[] bestPlaces, AlignedRead[] ARs, Dictionary<string, int> orientations)
        {
            if (bestPlaces == null || ARs == null) { return orientations; }


            foreach (AlignedRead ar in ARs)
            {

                string primaryStrand = "+";
                if (ar.getForward == false) { primaryStrand = "-"; }
                int arStart = ar.getPosition;
                int arEnd = ar.getEndPosition;

                string arDescription = primaryStrand + ":";

                if (bestPlaces[0].inPlaces(arStart) == true)
                { arDescription += bestPlaces[0].getReferenceName + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "r:"; }
                else if (bestPlaces[0].inPlaces(arEnd) == true)
                { arDescription += bestPlaces[0].getReferenceName + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "l:"; }
                else if (bestPlaces[1].inPlaces(arStart) == true)
                { arDescription += bestPlaces[0].getReferenceName + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "r:"; }
                else if (bestPlaces[1].inPlaces(arEnd) == true)
                { arDescription += bestPlaces[0].getReferenceName + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "l:"; }

                if (string.IsNullOrEmpty(ar.getSecondaryAlignmentTag) == false)
                {
                    string[] hits = ar.getSecondaryAlignmentTag.Substring(2).Split(';');
                    foreach (string hit in hits)
                    {
                        if (string.IsNullOrEmpty(hit) == false)
                        {
                            string[] items = hit.Split(',');
                            if (items[0].ToLower().Equals(bestPlaces[0].getReferenceName.ToLower()) == true || items[0].ToLower().Equals(bestPlaces[1].getReferenceName.ToLower()))
                            {                                
                                string secondaryStrandtrand = "";
                                int startPoint = Convert.ToInt32(items[1]);
                                int endPoint = startPoint + getAlignedLength(items[3]);
                                secondaryStrandtrand = items[2];
                                bool found = true;

                                if (bestPlaces[0].inPlaces(startPoint) == true)
                                { arDescription += secondaryStrandtrand + ":" + items[0] + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "r"; }
                                else if (bestPlaces[0].inPlaces(endPoint) == true)
                                { arDescription += secondaryStrandtrand + ":" + items[0] + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "l"; }
                                else if (bestPlaces[1].inPlaces(startPoint) == true)
                                { arDescription += secondaryStrandtrand + ":" + items[0] + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "r"; }
                                else if (bestPlaces[1].inPlaces(endPoint) == true)
                                { arDescription += secondaryStrandtrand + ":" + items[0] + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "l"; }
                                else { found = false; }

                                if (found == true)
                                {
                                    if (orientations.ContainsKey(arDescription) == false)
                                    { orientations.Add(arDescription, 1); }
                                    else { orientations[arDescription]++; }
                                }
                            }
                        }
                    }
                }
            }

            return orientations;

        }

        private int getAlignedLength(string CIGAR)
        {
            int sum = 0;
            string number = "";
            for (int index = 0; index < CIGAR.Length; index++)
            {
                switch (CIGAR[index])
                {
                    case 'H':
                        number = "";
                        break;
                    case 'S':
                        number = "";
                        break;
                    case 'I':
                        number = "";
                        break;
                    case 'D':
                    case 'N':
                    case 'P':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    case 'M':
                    case '=':
                    case 'X':
                        sum += Convert.ToInt32(number);
                        number = "";
                        break;
                    default:
                        number += CIGAR[index];
                        break;
                }
            }

            return sum;
        }

        private void DrawSelectedReads_FormClosing(object sender, FormClosingEventArgs e)
        {
            form.dSR_Closing();
        }
    }
}
