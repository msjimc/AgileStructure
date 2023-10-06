using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class RepeatBinarySearchPoint : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Repeat a = (Repeat)x;
            ChromosomalPoint b = (ChromosomalPoint)y;

            if (a.GetLocation.GetChromosomeName.ToLower() != b.Name.ToLower())
            { return a.GetLocation.GetChromosomeName.ToLower().CompareTo(b.Name.ToLower()); }
            else
            { return a.GetLocation.GetRegionStart - b.Base; }
        }
    }
}
