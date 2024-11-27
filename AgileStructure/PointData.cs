using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileStructure
{
    class PointData
    {
        private int average1;
        private int average2;
        private float primary5primeOfPlace1;
        private float primary5primeOfPlace2;
        private float secondary5primeOfPlace1;
        private float secondary5primeOfPlace2;
        string[] annotations;


        public PointData(int average1, int average2, float primary5primeOfPlace1, float primary5primeOfPlace2, float secondary5primeOfPlace1, float secondary5primeOfPlace2, string[] annotations)
        {
            this.average1 = average1;
            this.average2 = average2;
            this.primary5primeOfPlace1 = primary5primeOfPlace1;
            this.primary5primeOfPlace2 = primary5primeOfPlace2;
            this.secondary5primeOfPlace1 = secondary5primeOfPlace1;
            this.secondary5primeOfPlace2 = secondary5primeOfPlace2;

            this.annotations = annotations;
        }

        public int Average1 { get { return average1; } }
        public int Average2 { get { return average2; } }

        public float Primary5primeOfPlace1 { get { return primary5primeOfPlace1; } }

        public float Primary5primeOfPlace2 { get { return primary5primeOfPlace2; } }

        public float Secondary5primeOfPlace1 { get { return secondary5primeOfPlace1; } }

        public float Secondary5primeOfPlace2 { get { return secondary5primeOfPlace2; } }

        public string[] Annotations { get { return annotations; } }       
    }
}
