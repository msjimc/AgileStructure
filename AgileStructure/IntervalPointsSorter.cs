using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class IntervalPointsSorter : IComparer<IntervalPoints>
    {
        int IComparer<IntervalPoints>.Compare(IntervalPoints a, IntervalPoints b)
        {
            if (a == null) { return -1; }
            else if (b== null){return 1;}

            if (a.NameIndex == b.NameIndex)
            {

                if (a.getBPStart > b.getBPStart)
                { return 1; }
                if (a.getBPStart < b.getBPStart)
                { return -1; }
                else
                { return 0; }
            }
            else { return a.NameIndex - b.NameIndex; }

        }
    }
}
