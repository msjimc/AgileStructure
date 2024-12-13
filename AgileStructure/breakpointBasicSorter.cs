using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AgileStructure
{
    internal class breakpointBasicSorter : IComparer<breakpointBasic>
    {
        int IComparer<breakpointBasic>.Compare(breakpointBasic a, breakpointBasic b)
        {
            if (a == null) { return -1; }
            else if (b== null){ return 1; }

            if (a.Chromosme.ToLower() != b.Chromosme.ToLower())
            {
                string aa = a.Chromosme.ToLower();
                string bb = b.Chromosme.ToLower();
                if (aa.StartsWith("chr")) aa = aa.Substring(3);
                if (bb.StartsWith("chr")) bb = bb.Substring(3);

                int ia = getNumber(aa);
                int ib = getNumber(bb);
                if (ia ==-1 || ib == -1)
                { return a.Chromosme.CompareTo(b.Chromosme); }
                 else
                { return ia - ib; }
            }
            else { return a.Position - b.Position; }

        }

        private int getNumber(string value)
        {
            int ia = -1;
            int counter = 0;

            for (int index = 0; index < value.Length; index++)
            {
                if (value.Length > index && char.IsDigit(value[index]) == true)
                {
                    counter++;
                }
                else { break; }
            }
            
            if (counter>0)
            { ia = int.Parse(value.Substring(0, counter)); }

            return ia;
        }
    }
}
