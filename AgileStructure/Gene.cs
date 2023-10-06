using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AgileStructure
{
    class Gene
    {
        string name = "";
        Region ORF = null;
        Region gene = null;
        bool forwardStrand = false;
        Dictionary<string, Exon> exons;        

        public Gene(Region Location, Region orf, bool ForwardStrand, string Name)
        {
            gene = Location;
            ORF = orf;
            forwardStrand = ForwardStrand;           
            exons = new Dictionary<string, Exon>();
            name = Name;
        }

        public void AddAnExon(int ExonStart, int ExonEnd)
        {
            Region l = new Region(gene.GetChromosomeName, ExonStart, ExonEnd);
            AddAnExon(l);
        }

        public void AddAnExon(Region Location)
        {
            Exon e = new Exon(Location, forwardStrand);
            AddAnExon(e);
        }

        public void AddAnExon(Exon exon)
        {
            if (exons.ContainsKey(exon.key) == false)
            {
                exons.Add(exon.key, exon);

                if (gene.GetRegionStart > exon.GetLocation.GetRegionStart)
                { gene.GetRegionStart = exon.GetLocation.GetRegionStart; }
                if (gene.GetRegionEnd < exon.GetLocation.GetRegionEnd)
                { gene.GetRegionEnd = exon.GetLocation.GetRegionEnd; }
            }
            else
            {; }
        }

        public void AddaCDS(int StartPoint, int EndPoint)
        {
            if (gene.GetRegionStart > StartPoint)
            { gene.GetRegionStart = StartPoint; }
            if (gene.GetRegionEnd < EndPoint)
            { gene.GetRegionEnd = EndPoint; }
        }

        public void DrawGene(Graphics g, int Xoffset, int Yoffset, int FeatureHeight,  double XScale, int regionStart)
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = null;
            if (forwardStrand == true)
            { brush = new SolidBrush(Color.Green); }
            else {brush= new SolidBrush(Color.Orange);}

            double length = gene.GetLength * XScale;
            if (length > 2)
            {
                foreach (Exon e in exons.Values)
                {
                    e.DrawExon(g, Xoffset, Yoffset, XScale, FeatureHeight, regionStart, brush, pen);
                }
            }
            gene.DrawRegionBox(g, Xoffset, Yoffset, XScale, FeatureHeight, regionStart, Pens.Black);
        }

        public string getChromosome { get { return gene.GetChromosomeName; } }
        public string getName { get { return name; } }
        public Region GetLocation { get { return gene; } }
        public bool IsForwardStrand { get { return forwardStrand; } }       

    }
}
