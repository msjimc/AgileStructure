using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class RepeatSorter : IComparer<Repeat>
    {

        int IComparer<Repeat>.Compare(Repeat x, Repeat y)
        {
            if (x.GetLocation.GetChromosomeName.ToLower() != y.GetLocation.GetChromosomeName.ToLower())
            { return x.GetLocation.GetChromosomeName.ToLower().CompareTo(y.GetLocation.GetChromosomeName.ToLower()); }
            else
            { return x.GetLocation.GetRegionStart - y.GetLocation.GetRegionStart; }    
        }
    }
}
