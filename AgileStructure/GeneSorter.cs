using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class GeneSorter : IComparer<Gene>
    {

        int IComparer<Gene>.Compare(Gene x, Gene y)
        {            
            if (x.GetLocation.GetChromosomeName.ToLower() != y.GetLocation.GetChromosomeName.ToLower())
            { return x.GetLocation.GetChromosomeName.ToLower().CompareTo(y.GetLocation.GetChromosomeName.ToLower()); }
            else
            { return x.GetLocation.GetRegionStart - y.GetLocation.GetRegionStart; }
            //else if (x.GetLocation.GetRegionStart != y.GetLocation.GetRegionStart)
            //{ return x.GetLocation.GetRegionStart - y.GetLocation.GetRegionStart; }
            //else
            //{ return x.GetLocation.GetRegionEnd - y.GetLocation.GetRegionEnd; }

        }
    }
}
