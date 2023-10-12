using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AgileStructure
{
    class Repeat
    {
        Region location = null;
        bool forwardStrand;
        string repName;
        string repClass;
        string repFamily;

        public Repeat(Region Location, bool ForwardStrand, string Name, string className, string family)
        {
            location = Location;
            forwardStrand = ForwardStrand;
            repName =Name;
            repClass = className;
            repFamily = family;
        }

        public void DrawRepeat(Graphics g, int Xoffset, int Yoffset, int FeatureHeight, double XScale, int regionStart)
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = null;
            if (forwardStrand == true)
            { brush = new SolidBrush(Color.LightSkyBlue); }
            else { brush= new SolidBrush(Color.PaleGoldenrod); }

            double length = location.GetLength * XScale;
            location.DrawRegionFill(g, Xoffset, Yoffset, XScale, FeatureHeight, regionStart, brush);
            location.DrawRegionBox(g, Xoffset, Yoffset, XScale, FeatureHeight, regionStart, Pens.Black);
        }

        public string getChromosome { get { return location.GetChromosomeName; } }
        public string getName { get { return repName; } }
        public string getClass { get { return repClass; } }
        public string getFamily { get { return repFamily; } }
        public string getLongName { get { return repName + ", " + repClass + ", " + repFamily; } }
        public string getKey { get { return getLongName + location.GetChromosomeName + location.GetRegionStart.ToString(); } }
        public Region GetLocation { get { return location; } }
        public bool IsNoForwardStrand { get { return forwardStrand; } }

    }
}
