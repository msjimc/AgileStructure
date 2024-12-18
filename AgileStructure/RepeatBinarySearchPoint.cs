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
            {
                if (a.GetLocation.GetRegionStart <= b.Base && a.GetLocation.GetRegionEnd >= b.Base)
                { return 0; }
                else if (a.GetLocation.GetRegionStart > b.Base)
                { return a.GetLocation.GetRegionStart - b.Base; }
                else
                { return a.GetLocation.GetRegionEnd - b.Base; }
            }
        }
    }
}
