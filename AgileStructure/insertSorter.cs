using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileStructure
{
    class insertSorter: IComparer<string>
    {
        int IComparer<string>.Compare(string a, string b)
        {
            if (a == null) { return -1; }
            else if (b == null) { return 1; }
            
            int indexS= a.IndexOf(":") + 1;
            int indexE = a.IndexOf("-");
            string startA = a.Substring(indexS, indexE - indexS).Replace(",","");

             indexS = b.IndexOf(":") + 1;
             indexE = b.IndexOf("-");
             string startB = b.Substring(indexS, indexE - indexS).Replace(",", "");

            if (startA == startB)
            {
                indexS = a.IndexOf("ins") + 3;
                indexE = a.IndexOf("bp");
                string endA = a.Substring(indexS, indexE - indexS).Replace(",", "");

                indexS = b.IndexOf("ins") + 3;
                indexE = b.IndexOf("bp");
                string endB = b.Substring(indexS, indexE - indexS).Replace(",", "");

                return Convert.ToInt32(endA) - Convert.ToInt32(endB);
            }
            else { return Convert.ToInt32(startA) - Convert.ToInt32(startB); }

        }
    }
}
   