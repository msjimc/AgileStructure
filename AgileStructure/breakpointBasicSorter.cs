using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileStructure
{
    internal class breakpointBasicSorter : IComparer<breakpointBasic>
    {
        int IComparer<breakpointBasic>.Compare(breakpointBasic a, breakpointBasic b)
        {
            if (a == null) { return -1; }
            else if (b== null){ return 1; }

            if (a.Chromosme != b.Chromosme)
            { return a.Chromosme.CompareTo(b.Chromosme); }
            else { return a.Position - b.Position; }

        }
    }
}
