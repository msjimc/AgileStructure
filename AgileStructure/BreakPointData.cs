using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class BreakPointData
    {
        private int place = -1;
        private int[] places = null;
        private string referenceName = "";
        
        public BreakPointData(int Place, int[] Places,string ReferenceName)
        {
            place = Place;
            places = Places;
            referenceName = ReferenceName;
        }

        public string getReferenceName
        { get { return referenceName; } }

        public int getPlace
        { get { return place; } }

        public int getAveragePlace
        {
            get
            {
                long AveragePlace = 0;
                foreach (int p in places)
                { AveragePlace += p; }

                return (int)(AveragePlace / places.Count());
            }
        }

        public bool inPlaces(int place)
        {
            return places.Contains(place);
        }


    }
}
