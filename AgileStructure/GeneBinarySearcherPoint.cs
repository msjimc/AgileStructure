using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class GeneBinarySearcherPoint : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Gene a = (Gene)x;
            ChromosomalPoint b = (ChromosomalPoint)y;

            if (a.GetLocation.GetChromosomeName.ToLower() != b.Name.ToLower())
            { return a.GetLocation.GetChromosomeName.ToLower().CompareTo(b.Name.ToLower()); }
            else
            { return a.GetLocation.GetRegionStart - b.Base; }

        }
    }
}
