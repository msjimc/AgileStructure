using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileStructure
{
    class IntervalPoints
    {
        string name = "";
        int indexName = -1;
        int startPoint = -1;
        UInt64 PointinBam = 0;
        UInt64 PointInArray = 0;

        public IntervalPoints(int Index, int bpInRef, UInt64 bgzfPoint, UInt32 StreamPoint)
        {
            indexName = Index;
            startPoint = bpInRef;
            PointinBam = bgzfPoint;
            PointInArray = StreamPoint;
        }

        public IntervalPoints(int Index, int bpInRef)
        {
            indexName = Index;
            startPoint = bpInRef;            
        }

        public void setVirtualOffsets(UInt64 bgzfPoint, UInt64 StreamPoint)
        {
            PointinBam = bgzfPoint;
            PointInArray = StreamPoint;
        }

        public string Name
        { set { name = value; } get { return name; } }

        public int NameIndex { get { return indexName; } }
        public int getBPStart { get { return startPoint; } }
        public UInt64 get_bgzfPoint { get { return PointinBam; } }
        public UInt64 get_StreamPoint { get { return PointInArray; } }

    }
}
