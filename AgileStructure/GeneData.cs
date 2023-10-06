using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    struct GeneIndexes
    {
        //public int transcriptIndex;
        public int chromosomeIndex;
        public int strandIndex;
        public int transcriptStartIndex;
        public int transcriptEndIndex;
        public int ORStartIndex;
        public int ORFEndIndex;
        public int ExonStartsIndex;
        public int ExonEndsIndex;
        public int NameIndex;
        public bool isGood;
    }

    class GeneData
    {
        private string fileName;
        private Gene[] genes;
        
        public GeneData(string FileName)
        {
            fileName = FileName;
            ReadAnnotationFile(fileName);
        }

        private void ReadAnnotationFile(string filename)
        {
            System.IO.StreamReader fr = null;

            try
            {
                fr = new System.IO.StreamReader(filename);
                string line = "";
                string[] items = null;
                
                line = fr.ReadLine();
                items = line.Split('\t');

                GeneIndexes gI = getIndexes(items);
                if (gI.isGood == false) { return; }
                
                Dictionary<string, Gene> dDenes = new Dictionary<string, Gene>();
                
                while (fr.Peek() > 0)   
                {
                    line = fr.ReadLine();
                    items = line.Split('\t');
                    string name = items[gI.NameIndex];
                   
                    if (dDenes.ContainsKey(items[gI.NameIndex] + items[gI.chromosomeIndex]) == false)
                    {
                        Region geneLocation = new Region(items[gI.chromosomeIndex], Convert.ToInt32(items[gI.transcriptStartIndex]), Convert.ToInt32(items[gI.transcriptEndIndex]));
                        Region orfLocation = new Region(items[gI.chromosomeIndex], Convert.ToInt32(items[gI.ORStartIndex]), Convert.ToInt32(items[gI.ORFEndIndex]));
                        bool strand = items[gI.strandIndex] == "+";

                        Gene g = new Gene(geneLocation, orfLocation, strand, name);

                        dDenes.Add(items[gI.NameIndex] + items[gI.chromosomeIndex], g);
                    }

                    string[] starts = items[gI.ExonStartsIndex].Split(',');
                    string[] ends = items[gI.ExonEndsIndex].Split(',');

                    for (int index =0; index < starts.Length; index++)
                    {
                        if (string.IsNullOrEmpty(starts[index]) == false && string.IsNullOrEmpty(ends[index]) == false)
                        { dDenes[items[gI.NameIndex] + items[gI.chromosomeIndex]].AddAnExon(Convert.ToInt32(starts[index]), Convert.ToInt32(ends[index])); }
                    }
                }

                genes = dDenes.Values.ToArray();
                dDenes = null;
                GeneSorter gs = new GeneSorter();
                Array.Sort(genes, gs);
               
            }
            catch (Exception ex)
            {
                throw new Exception("error reading file", ex);
            }
            finally { if (fr != null) { fr.Close(); } }
        }

        public Gene[] Genes
        { get { return genes; } }

        private GeneIndexes getIndexes(string[] items)
        {
            GeneIndexes indexes = new GeneIndexes();
            int counter = 0;

            for (int index = 0; index < items.Length; index++)
            {
                switch (items[index].ToLower())
                {
                    case "#name":
                        indexes.transcriptStartIndex = index;
                        counter += 1;
                        break;
                    case "chrom":
                        indexes.chromosomeIndex = index;
                        counter += 2;
                        break;
                    case "strand":
                        indexes.strandIndex = index;
                        counter += 4;
                        break;
                    case "txstart":
                        indexes.transcriptStartIndex = index;
                        counter += 8;
                        break;
                    case "txend":
                        indexes.transcriptEndIndex = index;
                        counter += 16;
                        break;
                    case "cdsstart":
                        indexes.ORStartIndex = index;
                        counter += 32;
                        break;
                    case "cdsend":
                        indexes.ORFEndIndex = index;
                        counter += 64;
                        break;
                    case "exonstarts":
                        indexes.ExonStartsIndex = index;
                        counter += 128;
                        break;
                    case "exonends":
                        indexes.ExonEndsIndex = index;
                        counter += 256;
                        break;
                    case "name2":
                        indexes.NameIndex = index;
                        counter += 512;
                        break;
                }
            }
            if (counter == 1023)
            { indexes.isGood = true; }
            else
            { indexes.isGood = false; }

            return indexes;                                   
        }

        public Point getIndexsRangeOffGenesInARegion(ChromosomalPoint StartHere, ChromosomalPoint EndHere)
        {
            Point range = new Point(0,0);

            int index1 = Array.BinarySearch(genes, StartHere, new GeneBinarySearcherPoint());
            int index2 = Array.BinarySearch(genes, EndHere, new GeneBinarySearcherPoint());

            if (index1 < 0)
            {
                index1 = -index1 - 1;
                if (index1 < 0)
                { index1 = 0; }
            }

            for (int lookIndex = index1; lookIndex > index1 - 10; lookIndex--)
            {
                if (lookIndex > -1)
                {
                    if (genes[lookIndex].getChromosome == genes[index1].getChromosome)
                    {
                        if (StartHere.Base < genes[lookIndex].GetLocation.GetRegionEnd)
                        { index1 = lookIndex; }
                    }
                }
            }

            if (index2 < 0)
            {
                index2 = -index2;
                if (index2 > genes.GetUpperBound(0))
                { index2 = genes.GetUpperBound(0); }
            }

            for (int lookIndex = index1; lookIndex > index1 + 10; lookIndex++)
            {
                if (lookIndex < genes.Length)
                {
                    if (genes[lookIndex].getChromosome == genes[index1].getChromosome)
                    {
                        if (StartHere.Base < genes[lookIndex].GetLocation.GetRegionEnd)
                        { index1 = lookIndex; }
                    }
                }
            }

            range.X = index1;
            range.Y=index2;

            return range;

        }
    }
}
