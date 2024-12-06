using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileStructure
{
    internal class breakpointBasic
    {
        private string chromosome = "";
        private int position = -1;
        private int imagePlace = -1;

        public breakpointBasic(string[] data)
        {
            chromosome = data[0];
            position = int.Parse(data[1]);
        }

        public int Position { get { return position; } }

        public string Chromosme { get { return chromosome; } }

        public int ImagePlace
        {
            get { return imagePlace; }
            set { imagePlace = value; }
        }
    }
}
