using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class GeneBinarySearcher : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Gene a = (Gene)x;
            Gene b = (Gene)y;

            if (a.GetLocation.GetChromosomeName.ToLower() != b.GetLocation.GetChromosomeName.ToLower())
            { return a.GetLocation.GetChromosomeName.ToLower().CompareTo(b.GetLocation.GetChromosomeName.ToLower()); }
            else if (a.GetLocation.GetRegionStart > b.GetLocation.GetRegionStart)
            { return 1; }
            else if (a.GetLocation.GetRegionEnd < b.GetLocation.GetRegionEnd)
            { return -1; }
            else
            { return 0; }

        }
    }
}
