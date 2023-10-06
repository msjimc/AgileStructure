using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AgileStructure
{
    class Exon
    {
        Region location = null;
        bool forwardStrand = false;        

        public Exon(Region Location, bool ForwardStrand)
        {
            location = Location;
            forwardStrand = ForwardStrand;
        }

        public void DrawExon(Graphics g, int Xoffset, int Yoffset, double XScale, int Featureheight, int regionStart, SolidBrush brush, Pen pen)
        {
            location.DrawRegionFill(g, Xoffset, Yoffset, XScale, Featureheight, regionStart, brush);
        }

        public Region GetLocation { get { return location; } }
        public bool IsNoForwardStrand { get { return forwardStrand; } }       
        public string key { get { return location.GetRegionStart.ToString() + ":" + location.GetRegionEnd.ToString(); } }
    }
}
