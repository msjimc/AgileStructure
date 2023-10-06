using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    struct APoint
    {
        public int referenceIndex;
        public UInt64 BPStart;
        public UInt64 BPEnd;
        public UInt64 accumulatedStart;
    }


    class IntervalPointsBinarySearcher : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            IntervalPoints a = (IntervalPoints)x;
            APoint b = (APoint)y;

            if (a.NameIndex == b.referenceIndex)
            {
                if ((ulong)a.getBPStart > b.BPStart)
                { return 1; }
                else if ((ulong)a.getBPStart + 16384 < b.BPStart)
                { return -1; }
                else { return 0; }
            }
            else { return a.NameIndex - b.referenceIndex; }            
        }

    }
}
