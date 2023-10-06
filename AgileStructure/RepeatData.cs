using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    struct RepeatIndexes
    {
        public int chromosomeIndex;
        public int strandIndex;
        public int StartIndex;
        public int EndIndex;
        public int NameIndex;
        public int ClassIndex;
        public int FamilyIndex;
        public bool isGood;
    }

    class RepeatData
    {
        private string fileName;
        private Repeat[] repeats;
        

        public RepeatData(string FileName, string chromosomeP1, int startPointP1, int endPointP1, string chromosomeP2, int startPointP2, int endPointP2)
        {
            fileName = FileName;
            ReadAnnotationFile(fileName, chromosomeP1, startPointP1, endPointP1, chromosomeP2, startPointP2, endPointP2);
        }

       private void ReadAnnotationFile(string fileName, string chromosomeP1, int startPointP1, int endPointP1, string chromosomeP2, int startPointP2, int endPointP2)
        {
            System.IO.StreamReader fr = null;

            try
            {
                fr = new System.IO.StreamReader(fileName);
                string line = "";
                string[] items = null;

                line = fr.ReadLine();
                items = line.Split('\t');

                RepeatIndexes rI = getIndexes(items);
                if (rI.isGood == false) { return; }

                Dictionary<string, Repeat> dRepeats = new Dictionary<string, Repeat>();

                chromosomeP1 = chromosomeP1.ToLower();
                chromosomeP2 = chromosomeP2.ToLower();
                bool save = false;

                while (fr.Peek() > 0)
                {
                    line = fr.ReadLine();
                    items = line.Split('\t');


                    if (items[rI.chromosomeIndex].ToLower() == chromosomeP1 && Convert.ToInt32(items[rI.StartIndex]) >= startPointP1 && Convert.ToInt32(items[rI.EndIndex]) <= endPointP1)
                    { save = true; }
                    else if (items[rI.chromosomeIndex].ToLower() == chromosomeP2 && Convert.ToInt32(items[rI.StartIndex]) >= startPointP2 && Convert.ToInt32(items[rI.EndIndex]) <= endPointP2)
                    { save = true;}

                    if (save == true)
                    {
                        if (dRepeats.ContainsKey(items[rI.NameIndex] + items[rI.chromosomeIndex] + items[rI.StartIndex])  == false)
                        {
                            Region geneLocation = new Region(items[rI.chromosomeIndex], Convert.ToInt32(items[rI.StartIndex]), Convert.ToInt32(items[rI.EndIndex]));
                            bool strand = items[rI.strandIndex] == "+";

                            Repeat r = new Repeat(geneLocation, strand, items[rI.NameIndex], items[rI.ClassIndex], items[rI.FamilyIndex]);

                            dRepeats.Add(items[rI.NameIndex] + items[rI.chromosomeIndex] + items[rI.StartIndex], r);
                            save=false;
                        }
                    }                         
                }

                repeats = dRepeats.Values.ToArray();
                dRepeats = null;
                RepeatSorter rs = new RepeatSorter();
                Array.Sort(repeats, rs);

            }
            catch (Exception ex)
            {
                throw new Exception("error reading file", ex);
            }
            finally { if (fr != null) { fr.Close(); } }
        }

        public Repeat[] Repeats
        { get { return repeats; } }

        private RepeatIndexes getIndexes(string[] items)
        {
            RepeatIndexes indexes = new RepeatIndexes();
            int counter = 0;

            for (int index = 0; index < items.Length; index++)
            {
                switch (items[index].ToLower())
                {
                    case "#genoname":
                        indexes.chromosomeIndex = index;
                        counter += 1;
                        break;
                    case "genostart":
                        indexes.StartIndex = index;
                        counter += 2;
                        break;
                    case "genoend":
                        indexes.EndIndex = index;
                        counter += 4;
                        break;
                    case "strand":
                        indexes.strandIndex = index;
                        counter += 8;
                        break;
                    case "repname":
                        indexes.NameIndex = index;
                        counter += 16;
                        break;
                    case "repclass":
                        indexes.ClassIndex = index;
                        counter += 32;
                        break;
                    case "repfamily":
                        indexes.FamilyIndex = index;
                        counter += 64;
                        break;                    
                }
            }
            if (counter == 127)
            { indexes.isGood = true; }
            else
            { indexes.isGood = false; }

            return indexes;
        }
    }
}
