using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AgileStructure
{
    class Region
    {
        int referenceIndex = -1;
        string referenceFileName = "";
        int startPoint = -1;
        int endPoint = -1;
        int length;

        public Region(string ChromosomeName, int RegionStart, int RegionEnd)
        {
            referenceFileName = ChromosomeName;
            startPoint = RegionStart;
            endPoint = RegionEnd;
            CheckEnds();
        }

        public Region(string ChromosomeName, int ChromosomeIndex, int RegionStart, int RegionEnd)
        {
            referenceIndex = ChromosomeIndex;
            referenceFileName = ChromosomeName;
            startPoint = RegionStart;
            endPoint = RegionEnd;
            CheckEnds();                       
        }

        public double DrawRegionBoth(Graphics g, int Xoffset, int Yoffset, double XScale, int Featureheight, int regionStart, SolidBrush brush, Pen pen)
        {           
            double X = Xoffset + ((startPoint - regionStart) * XScale);
            double w = length * XScale;
            if (w < 1)
            { g.DrawRectangle(new Pen(Color.Black), (int)X, Yoffset, 1, Featureheight); }
            else if (w < 6)
            { g.FillRectangle(brush, (int)X, Yoffset, (float)w, Featureheight); }
            else
            {
                g.FillRectangle(brush, (int)X, Yoffset, (float)w, Featureheight);
                g.DrawRectangle(pen, (int)X, Yoffset, (float)w, Featureheight);
            }

            return w;
        }

        public double DrawRegionFill(Graphics g, int Xoffset, int Yoffset, double XScale, int Featureheight, int regionStart, SolidBrush brush)
        {
            double X = Xoffset + ((startPoint - regionStart) * XScale);
            double w = length * XScale;
            g.FillRectangle(brush, (int)X, Yoffset, (float)w, Featureheight); 

            return w;
        }

        public double DrawRegionBox(Graphics g, int Xoffset, int Yoffset, double XScale, int Featureheight, int regionStart, Pen pen)
        {
            double X = Xoffset + ((startPoint - regionStart) * XScale);
            double w = length * XScale;
            g.DrawRectangle(pen, (int)X, Yoffset, (float)w, Featureheight);

            return w;
        }

        private void CheckEnds()
        {
            if (startPoint > endPoint)
            {
                int t = endPoint;
                endPoint = startPoint;
                startPoint = t;
            }
            length = endPoint - startPoint;
        }

        public int ChromosomeIndex 
        { 
            get { return referenceIndex; }
            set { referenceIndex = value; }
        }

        public string GetChromosomeName { get { return referenceFileName; } }
        public int GetRegionStart 
        { 
            get { return startPoint; }
            set { 
                startPoint = value;
                CheckEnds();
            }
        }

        public int GetRegionEnd 
        { 
            get { return endPoint; }
            set { 
                endPoint = value;
                CheckEnds();
            }
        }

        public int GetLength { get { return length; } }

    }
}
