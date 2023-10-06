using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class ChromosomalPoint
    {
        private string chromosomalName;
        private int place;
        public ChromosomalPoint(string ChromosomeName, int Place)
        {
            chromosomalName = ChromosomeName;
            place = Place;
        }

        public string Name { get { return chromosomalName; } }
        public int Base { get { return place; } }
    }
}
