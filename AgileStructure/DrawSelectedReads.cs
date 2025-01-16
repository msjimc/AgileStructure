using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
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

            pd1 = null;
            pd2 = null;

            lblPrimary1.Text = "not set";
            lblPrimary2.Text = "not set";
            lblSecondary1.Text = "not set";
            lblSecondary2.Text = "not set";
            blankImage();

            btnAnnotate.Enabled = false;

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

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();
                if (first[1] == null)
                { first[1] = first[0]; }

                int average1 = 0;
                int average2 = 0;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);
                float secondary5primeOfPlace1 = form.SecondaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float secondary5primeOfPlace2 = form.SecondaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

                chromosomes.Add(first[0].getReferenceName);
                if (first[1].getReferenceName != first[0].getReferenceName)
                { chromosomes.Add(first[1].getReferenceName); }

                average1 = first[0].getAveragePlace;
                average2 = first[1].getAveragePlace;
                lblPrimary1.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                lblSecondary1.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");

                pd1 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, secondary5primeOfPlace1, secondary5primeOfPlace2, annotations, form.GetSelectedReads(), first);

                form.deleteSelectedList();
                p1.Image = Draw();
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

                string[] annotations = form.externalannotations();

                BreakPointData[] first = form.getTwobreakPoints();
                if (first[1] == null)
                { first[1] = first[0]; }

                chromosomes = new List<string>();

                int average1 = 0;
                int average2 = 0;
                float primary5primeOfPlace1 = form.PrimaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float primary5primeOfPlace2 = form.PrimaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);
                float secondary5primeOfPlace1 = form.SecondaryAlignment5PrimeOfbreakPoint(first[0].getAveragePlace, first[0].getReferenceName);
                float secondary5primeOfPlace2 = form.SecondaryAlignment5PrimeOfbreakPoint(first[1].getAveragePlace, first[1].getReferenceName);

                chromosomes.Add(first[0].getReferenceName);
                if (first[1].getReferenceName != first[0].getReferenceName)
                { chromosomes.Add(first[1].getReferenceName); }

                if (chromosomes.Contains(first[0].getReferenceName) == false) { chromosomes.Add(first[0].getReferenceName); }
                if (chromosomes.Contains(first[1].getReferenceName) == false) { chromosomes.Add(first[1].getReferenceName); }

                average1 = first[0].getAveragePlace;
                average2 = first[1].getAveragePlace;
                lblPrimary2.Text = "Break point 1: " + first[0].getReferenceName + ":" + average1.ToString("N0");
                lblSecondary2.Text = "Break point 2: " + first[1].getReferenceName + ":" + average2.ToString("N0");

                pd2 = new PointData(average1, average2, primary5primeOfPlace1, primary5primeOfPlace2, secondary5primeOfPlace1, secondary5primeOfPlace2, annotations, form.GetSelectedReads(), first);

                form.deleteSelectedList();
                p1.Image = Draw();
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
            btnAnnotate.Enabled = false;
            if (pda == null && pdb == null)
            { MessageBox.Show("Please set the break point(s)", "No break points selected"); }
            else if (pda != null && pdb != null)
            {
                btnAnnotate.Enabled = true;
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
            try
            {
                Bitmap bmp = new Bitmap(p1.Width, p1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);

                g.Clear(Color.White);
                p1.Image = bmp; ;
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            string filename = FileString.SaveAs("Enter image file name", "Image file (*.png;*.jpg;*.tiff;*.bmp)|*.png;*.jpg;*.tiff;*.bmp");
            if (filename == "Cancel") { return; }
            try
            {
                Bitmap bmp = Draw();
                bmp.Save(filename);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error"); }
        }

        private Bitmap Draw()
        {
            if (WindowState == FormWindowState.Minimized) { return null; }
            if (pd1 == null && pd2 == null) { return null; }
            Find(pd1, pd2);

            Bitmap bmp = new Bitmap(p1.Width, p1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            try
            {
                drawReferenceAndBreakpoints(chromosomes.Count, new Rectangle(0, 0, bmp.Width, bmp.Height), g);
            }
            catch { }
            return bmp;
        }

        private void drawReferenceAndBreakpoints(int chromosomeCount, Rectangle area, Graphics g)
        {

            int referenceLength = area.Width - 50;
            int QuarterWidth = (area.Width - 80) / 4;
            int referenceLineHeight = (area.Height - 30);

            Point first = new Point(10, QuarterWidth + 10);
            Point second = new Point(QuarterWidth + 30, (2 * QuarterWidth) + 30);
            Point third = new Point((2 * QuarterWidth) + 50, (3 * QuarterWidth) + 50);
            Point fourth = new Point((3 * QuarterWidth) + 70, referenceLength + 10);

            breakpointBasic[] bpb = GetBreakpointBasicArray(first, second, third, fourth);

            referenceLineHeight = drawReferenceAndBreakpointsSingleChromosome(area, g, bpb, first, second, third, fourth, 50);

            Pen referencePen = new Pen(Brushes.Black, 2);
            if (chromosomeCount == 1)
            {
                g.DrawLine(referencePen, first.X, referenceLineHeight, fourth.Y, referenceLineHeight);
            }
            else
            {
                g.DrawLine(referencePen, first.X, referenceLineHeight, second.Y, referenceLineHeight);
                g.DrawLine(referencePen, third.X, referenceLineHeight, fourth.Y, referenceLineHeight);
            }
        }

        private int drawReferenceAndBreakpointsSingleChromosome(Rectangle area, Graphics g, breakpointBasic[] bpb, Point first, Point second, Point third, Point fourth, int ReferenceHeight)
        {

            Pen verticalPen = new Pen(Brushes.Black, 1);
            verticalPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Font f = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);

            g.TranslateTransform(p1.Width - 15, 10);
            g.RotateTransform(90);
            g.DrawString("Counts", f, Brushes.Black, 0, 0);
            g.ResetTransform();

            Dictionary<string, int> orientations = new Dictionary<string, int>();
            orientations = GetAligmnentSets(breakPoints1, bp1, orientations);
            orientations = GetAligmnentSets(breakPoints2, bp2, orientations);

            int lineHeight = DrawBlocks(g, f, orientations, bpb, area, ReferenceHeight, 10);

            Writelables(g, f, first, ref bpb[0], verticalPen, lineHeight);
            Writelables(g, f, second, ref bpb[1], verticalPen, lineHeight);
            if (bpb.Length > 2) { Writelables(g, f, third, ref bpb[2], verticalPen, lineHeight); }
            if (bpb.Length > 2) { Writelables(g, f, fourth, ref bpb[3], verticalPen, lineHeight); }

            return lineHeight;
        }

        private int DrawBlocks(Graphics g, Font f, Dictionary<string, int> orientations, breakpointBasic[] bpb, Rectangle area, int ReferenceHeight, int itemHeight)
        {
            int counter = 1;
            Rectangle r1 = new Rectangle(0, 0, 0, 0);
            Rectangle r2 = new Rectangle(0, 0, 0, 0);

            foreach (string data in orientations.Keys)
            {
                try
                {
                    string[] items = data.Split(':');
                    int primaryBreakPoint = Convert.ToInt32(items[2]);
                    int secondaryBreakPoint = Convert.ToInt32(items[6]);

                    foreach (breakpointBasic place in bpb)
                    {
                        if (place.Position == primaryBreakPoint && place.Chromosme == items[1])
                        {
                            r1 = new Rectangle(0, ReferenceHeight + (3 * counter * itemHeight), 60, itemHeight);
                            if (items[3] == "r") { r1.X = place.ImagePlace; }
                            else { r1.X = place.ImagePlace - 60; }

                            if (items[0] == "+")
                            { g.FillRectangle(Brushes.Green, r1); }
                            else
                            { g.FillRectangle(Brushes.Red, r1); }
                            g.DrawRectangle(Pens.Black, r1);
                            break;
                        }
                    }
                    foreach (breakpointBasic place in bpb)
                    {
                        if (place.Position == secondaryBreakPoint && place.Chromosme == items[5])
                        {
                            r2 = new Rectangle(0, ReferenceHeight + (3 * counter * itemHeight), 60, itemHeight);
                            if (items[7] == "r") { r2.X = place.ImagePlace; }
                            else { r2.X = place.ImagePlace - 60; }

                            if (OverlappingRectangles(r1, r2) == true)
                            {
                                counter++;
                                r2.Y = ReferenceHeight + (3 * counter * itemHeight);
                            }

                            if (items[4] == "+")
                            { g.FillRectangle(Brushes.Green, r2); }
                            else { g.FillRectangle(Brushes.Red, r2); }
                            g.DrawRectangle(Pens.Black, r2);
                            break;
                        }
                    }
                    counter++;

                    g.DrawString(orientations[data].ToString(), f, Brushes.Black, area.Right - 30, ((r1.Top + r2.Top) / 2) - 2);

                    Pen connector = new Pen(Brushes.Black, 1.5f);
                    connector.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    if (r1.X > 0 && r2.X > 0)
                    {
                        int left = 0;
                        int right = 0;


                        if (r1.Y != r2.Y)
                        {
                            if (items[3] == "r" && items[7] == "r")
                            {
                                left = r1.Left - 15;
                                g.DrawLine(connector, r1.Left, (r1.Top + r1.Bottom) / 2, left, (r1.Top + r1.Bottom) / 2);
                                g.DrawLine(connector, r2.Left, (r2.Top + r2.Bottom) / 2, left, (r2.Top + r2.Bottom) / 2);
                                g.DrawLine(connector, left, (r1.Top + r1.Bottom) / 2, left, ((r2.Top + r2.Bottom) / 2));
                            }
                            else
                            {
                                right = r1.Right + 15;
                                g.DrawLine(connector, r1.Right, (r1.Top + r1.Bottom) / 2, right, (r1.Top + r1.Bottom) / 2);
                                g.DrawLine(connector, r2.Right, (r2.Top + r2.Bottom) / 2, right, (r2.Top + r2.Bottom) / 2);
                                g.DrawLine(connector, right, (r1.Top + r1.Bottom) / 2, right, ((r2.Top + r2.Bottom) / 2));
                            }
                        }
                        else if (items[3] == "l" && items[7] == "r" && r1.Right < r2.Right)
                        {
                            g.DrawLine(connector, r1.Right, ((r1.Top + r1.Bottom) / 2), r2.Left, ((r2.Top + r2.Bottom) / 2));
                        }
                        else
                        {
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
                catch { }
            }

            return ReferenceHeight + (3 * counter * itemHeight);

        }

        private bool OverlappingRectangles(Rectangle r1, Rectangle r2)
        {
            bool answer = false;

            if (Math.Abs(r1.X - r2.X) < 15)
            { answer = true; }

            return answer;
        }

        private void Writelables(Graphics g, Font f, Point region, ref breakpointBasic data, Pen verticalPen, int lineHeight)
        {
            int ThirdHieght = 60;

            g.DrawLine(verticalPen, data.ImagePlace, lineHeight, data.ImagePlace, ThirdHieght);
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
            { g.DrawLine(verticalPen, point1 - (length / 2), ThirdHieght - 15, data.ImagePlace, ThirdHieght); }
            else if (point1 + (length / 2) < data.ImagePlace)
            { g.DrawLine(verticalPen, point1 + (length / 2), ThirdHieght - 15, data.ImagePlace, ThirdHieght); }
            else
            { g.DrawLine(verticalPen, point1, ThirdHieght - 15, data.ImagePlace, ThirdHieght); }
        }

        private breakpointBasic[] GetBreakpointBasicArray(Point first, Point second, Point third, Point Fourth)
        {
            int mergeOn = (int)numericUpDown1.Value;

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

            if (bpb.Count == 2)
            {
                breakpointBasic bpb1 = bpb[0];
                breakpointBasic bpb2 = bpb[1];
                bpb.Add(bpb1);
                bpb.Add(bpb2);
            }

            bpb.Sort(new breakpointBasicSorter());

            if (bpb.Count > 3 && bpb[0].Chromosme == bpb[3].Chromosme && Math.Abs(bpb[0].Position - bpb[3].Position) < mergeOn)
            {
                int place = (first.X + Fourth.Y) / 2;
                bpb[0].ImagePlace = place - 6;
                bpb[1].ImagePlace = place - 2;
                bpb[2].ImagePlace = place + 2;
                bpb[3].ImagePlace = place + 6;
            }
            else
            {
                if (bpb.Count > 2 && bpb[0].Chromosme == bpb[2].Chromosme && Math.Abs(bpb[0].Position - bpb[2].Position) < mergeOn)
                {
                    int place = (first.X + third.Y) / 2;
                    bpb[0].ImagePlace = place - 4;
                    bpb[1].ImagePlace = place;
                    bpb[2].ImagePlace = place + 4;
                }
                else if (bpb.Count > 3 && bpb[1].Chromosme == bpb[3].Chromosme && Math.Abs(bpb[1].Position - bpb[3].Position) < mergeOn)
                {
                    int place = (second.X + Fourth.Y) / 2;
                    bpb[1].ImagePlace = place - 4;
                    bpb[2].ImagePlace = place;
                    bpb[3].ImagePlace = place + 4;
                }
                else
                {
                    if (bpb.Count > 1 && bpb[0].Chromosme == bpb[1].Chromosme && Math.Abs(bpb[0].Position - bpb[1].Position) < mergeOn)
                    {
                        int place = (first.X + second.Y) / 2;
                        int gap = 0;
                        if (bpb[0].Position != bpb[1].Position) { gap = 2; }
                        bpb[0].ImagePlace = place - gap;
                        bpb[1].ImagePlace = place + gap;
                    }
                    if (bpb.Count > 2 && bpb[1].Chromosme == bpb[2].Chromosme && Math.Abs(bpb[1].Position - bpb[2].Position) < mergeOn)
                    {
                        int place = (second.X + third.Y) / 2;
                        int gap = 0;
                        if (bpb[1].Position != bpb[2].Position) { gap = 2; }
                        bpb[1].ImagePlace = place - gap;
                        bpb[2].ImagePlace = place + gap;
                    }
                    if (bpb.Count > 3 && bpb[2].Chromosme == bpb[3].Chromosme && Math.Abs(bpb[2].Position - bpb[3].Position) < mergeOn)
                    {
                        int place = (third.X + Fourth.Y) / 2;
                        int gap = 0;
                        if (bpb[2].Position != bpb[3].Position) { gap = 2; }
                        bpb[2].ImagePlace = place - gap;
                        bpb[3].ImagePlace = place + gap;
                    }
                }
            }
            if (bpb[0].ImagePlace == -1)
            { bpb[0].ImagePlace = (first.Y + first.X) / 2; }

            if (bpb[1].ImagePlace == -1)
            { bpb[1].ImagePlace = (second.Y + second.X) / 2; }

            if (bpb.Count > 2 && bpb[2].ImagePlace == -1)
            { bpb[2].ImagePlace = (third.Y + third.X) / 2; }

            if (bpb.Count > 2 && bpb[3].ImagePlace == -1)
            { bpb[3].ImagePlace = (Fourth.Y + Fourth.X) / 2; }

            columnPlaces(bpb);

            return bpb.ToArray();
        }

        private void columnPlaces(List<breakpointBasic> bpb)
        {

            if (bpb.Count > 3 && bpb[0].Chromosme == bpb[3].Chromosme && bpb[0].Position == bpb[3].Position )
            {
                int place = (bpb[0].ImagePlace + bpb[3].ImagePlace) / 2;
                bpb[0].ImagePlace = place ;
                bpb[1].ImagePlace = place ;
                bpb[2].ImagePlace = place ;
                bpb[3].ImagePlace = place ;
            }
            else
            {
                if (bpb.Count > 2 && bpb[0].Chromosme == bpb[2].Chromosme && bpb[0].Position == bpb[2].Position)
                {
                    int place = (bpb[0].ImagePlace + bpb[2].ImagePlace) / 2;
                    bpb[0].ImagePlace = place ;
                    bpb[1].ImagePlace = place;
                    bpb[2].ImagePlace = place ;
                }
                else if (bpb.Count > 3 && bpb[1].Chromosme == bpb[3].Chromosme && bpb[1].Position == bpb[3].Position)
                {
                    int place = (bpb[1].ImagePlace + bpb[3].ImagePlace) / 2;
                    bpb[1].ImagePlace = place - 4;
                    bpb[2].ImagePlace = place;
                    bpb[3].ImagePlace = place + 4;
                }
                else
                {
                    if (bpb.Count > 1 && bpb[0].Chromosme == bpb[1].Chromosme && bpb[0].Position == bpb[1].Position) 
                    {
                        int place = (bpb[0].ImagePlace + bpb[1].ImagePlace) / 2;
                        bpb[0].ImagePlace = place ;
                        bpb[1].ImagePlace = place ;
                    }
                    if (bpb.Count > 2 && bpb[1].Chromosme == bpb[2].Chromosme && bpb[1].Position == bpb[2].Position) 
                    {
                        int place = (bpb[1].ImagePlace + bpb[2].ImagePlace) / 2;
                       bpb[1].ImagePlace = place ;
                        bpb[2].ImagePlace = place ;
                    }
                    if (bpb.Count > 3 && bpb[2].Chromosme == bpb[3].Chromosme && bpb[2].Position == bpb[3].Position) 
                    {
                        int place = (bpb[2].ImagePlace + bpb[3].ImagePlace) / 2;
                        bpb[2].ImagePlace = place ;
                        bpb[3].ImagePlace = place ;
                    }
                }
            }

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
                if (ar != null)
                {
                    string primaryStrand = "+";
                    if (ar.getForward == false) { primaryStrand = "-"; }
                    string primaryChromosome = "";
                    int arStart = ar.getPosition;
                    int arEnd = ar.getEndPosition;
                    bool skip = false;
                    string arDescription = "";
                    int pStart = -1;

                    if (bestPlaces[0].inPlaces(arStart) == true)
                    {
                        arDescription += bestPlaces[0].getReferenceName + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "r";
                        pStart = bestPlaces[0].getAveragePlace;
                        primaryChromosome = bestPlaces[0].getReferenceName;
                    }
                    else if (bestPlaces[0].inPlaces(arEnd) == true)
                    {
                        arDescription += bestPlaces[0].getReferenceName + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "l";
                        pStart = bestPlaces[0].getAveragePlace;
                        primaryChromosome = bestPlaces[0].getReferenceName;
                    }
                    else if (bestPlaces[1].inPlaces(arStart) == true)
                    {
                        arDescription += bestPlaces[1].getReferenceName + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "r";
                        pStart = bestPlaces[1].getAveragePlace;
                        primaryChromosome = bestPlaces[1].getReferenceName;
                    }
                    else if (bestPlaces[1].inPlaces(arEnd) == true)
                    {
                        arDescription += bestPlaces[1].getReferenceName + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "l";
                        pStart = bestPlaces[1].getAveragePlace;
                        primaryChromosome = bestPlaces[1].getReferenceName;
                    }
                    else { skip = true; }


                    if (skip == false && string.IsNullOrEmpty(ar.getSecondaryAlignmentTag) == false)
                    {
                        string[] hits = ar.getSecondaryAlignmentTag.Substring(2).Split(';');
                        foreach (string hit in hits)
                        {
                            if (string.IsNullOrEmpty(hit) == false)
                            {
                                string[] items = hit.Split(',');
                                if (items[0].ToLower().Equals(bestPlaces[0].getReferenceName.ToLower()) == true || items[0].ToLower().Equals(bestPlaces[1].getReferenceName.ToLower()))
                                {
                                    string secondaryStrandStrand = "";
                                    int startPoint = Convert.ToInt32(items[1]);
                                    int endPoint = startPoint + getAlignedLength(items[3]);
                                    secondaryStrandStrand = items[2];
                                    bool found = true;

                                    string arDescriptionSec = "";
                                    int sStart = -1;
                                    if (bestPlaces[0].inPlaces(startPoint) == true)
                                    { arDescriptionSec += ":" + items[0] + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "r"; sStart = bestPlaces[0].getAveragePlace; }
                                    else if (bestPlaces[0].inPlaces(endPoint) == true)
                                    { arDescriptionSec += ":" + items[0] + ":" + bestPlaces[0].getAveragePlace.ToString() + ":" + "l"; sStart = bestPlaces[0].getAveragePlace; }
                                    else if (bestPlaces[1].inPlaces(startPoint) == true)
                                    { arDescriptionSec += ":" + items[0] + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "r"; sStart = bestPlaces[1].getAveragePlace; }
                                    else if (bestPlaces[1].inPlaces(endPoint) == true)
                                    { arDescriptionSec += ":" + items[0] + ":" + bestPlaces[1].getAveragePlace.ToString() + ":" + "l"; sStart = bestPlaces[1].getAveragePlace; }
                                    else { found = false; }

                                    if (found == true)
                                    {
                                        string alignments = "";
                                        if (secondaryStrandStrand == primaryStrand)
                                        { alignments = "+"; }
                                        else { alignments = "-"; }

                                        int diff = orderOfChromosomes(primaryChromosome, items[0]);

                                        string key = "";

                                        if (diff < 0)
                                        { key = "+:" + arDescription + ":" + alignments + arDescriptionSec; }
                                        else if (diff > 0)
                                        { key = "+" + arDescriptionSec + ":" + alignments + ":" + arDescription; }
                                        else
                                        {
                                            if (pStart > sStart)
                                            { key = "+" + arDescriptionSec + ":" + alignments + ":" + arDescription; }
                                            else
                                            { key = "+:" + arDescription + ":" + alignments + arDescriptionSec; }
                                        }


                                        if (orientations.ContainsKey(key) == false)
                                        { orientations.Add(key, 1); }
                                        else { orientations[key]++; }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return orientations;

        }

        private int orderOfChromosomes(string one, string two)
        {
            int iOne = getNumber(one);
            int iTwo = getNumber(two);

            if (iOne == -1 || iTwo == -1)
            { return one.CompareTo(two); }
            else
            { return iOne - iTwo; }

        }

        private int getNumber(string value)
        {
            value = value.ToLower();
            if (value.StartsWith("chr")) { value = value.Substring(3); }

            int ia = -1;
            int counter = 0;

            for (int index = 0; index < value.Length; index++)
            {
                if (value.Length > index && char.IsDigit(value[index]) == true)
                {
                    counter++;
                }
                else { break; }
            }

            if (counter > 0)
            { ia = int.Parse(value.Substring(0, counter)); }

            return ia;
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

        private bool resizing = false;
        private void DrawSelectedReads_Resize(object sender, EventArgs e)
        {
            resizing = true;
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (resizing == true) { resizing = false; }
            else
            {
                timer1.Enabled = false;
                p1.Image = Draw();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            p1.Image = Draw();
        }

        private void btnAnnotate_Click(object sender, EventArgs e)
        {
            string[] labels = { lblPrimary1.Text, lblPrimary2.Text, lblSecondary1.Text, lblSecondary2.Text };

            form.AnnotateFromDrawing(pd1, pd2, labels, chromosomes);
        }

        internal void SchematicfromAnnotation(PointData pdA, PointData pdB, string[] labels, List<string> Chromosomes)
        {
            clean();
            pd1 = pdA;
            pd2 = pdB;
            lblPrimary1.Text = labels[0];
            lblPrimary2.Text = labels[1];
            lblSecondary1.Text = labels[2];
            lblSecondary2.Text = labels[3];
            chromosomes = Chromosomes;
            p1.Image = Draw();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clean();
            blankImage();
        }
    }
}
