using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AgileStructure
{
    class Transcript
    {
        Region location = null;
        bool forwardStrand = false;
        List<Exon> exons = null;
        int transcriptIndex = -1;
        string name;

        public Transcript(Region Location, bool ForwardStrand, string TranscriptName)
        {
            location = Location;
            forwardStrand = ForwardStrand;
            exons = new List<Exon>();
            transcriptIndex = TranscriptIndex;
            name=TranscriptName;
        }

        public void AddAnExon(int ExonStart, int ExonEnd)
        {
            Region l = new Region(location.GetChromosomeName, ExonStart, ExonEnd);
            //Region l = new Region(location.GetChromosomeName, location.GetChromosomeIndex, ExonStart, ExonEnd);
            AddAnExon(l);
        }

        public void AddAnExon(Region Location)
        {
            Exon e = new Exon(Location, forwardStrand);
            exons.Add(e);            
        }

        public void AddAnExon(Exon exon)
        {
            exons.Add(exon);            
        }

        public void DrawTranScript(Graphics g, int Xoffset, int Yoffset, double XScale, int Featureheight, int regionStart, SolidBrush brush, Pen pen)
        {
            location.DrawRegionBox(g, Xoffset, Yoffset, XScale, Featureheight, regionStart, pen);

            foreach (Exon e in exons)
            { e.DrawExon(g, Xoffset, Yoffset, XScale, Featureheight, regionStart, brush, pen); }
        }        

        public string GetTranscriptName { get { return name; } }
        public Region GetLocation { get { return location; } }
        public bool IsNoForwardStrand { get { return forwardStrand; } }
        public int TranscriptIndex
        {
            get { return transcriptIndex; }
            set { transcriptIndex = value; }
        }
    }
}
