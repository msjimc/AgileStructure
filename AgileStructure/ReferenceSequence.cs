using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class ReferenceSequence
    {
        private int index;
        public readonly string name;
        private long lenght;
        private long accumlatedLength;
        public readonly bool IsGood = false;

        public ReferenceSequence(string line, int Index)
        {
            string[] items = line.Split('\t');
            if (items[0].StartsWith("@SQ") == true)
            {
                name = items[1].Substring(3);
                lenght = Convert.ToInt32(items[2].Substring(3));
                index = Index;
                IsGood = true;
            }
        }

        
        public long Lenght
        { get { return lenght; } }

        public void CalculateAccumulatedlength(long soFar)
        {  accumlatedLength = soFar; }

        public long AccumlatedLengthStart
        { get { return accumlatedLength + 1; } }

        public long AccumlatedLengthEnd
        { get { return accumlatedLength + lenght; } }

    }
}
