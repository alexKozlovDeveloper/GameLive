using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class Position
    {
        [DataMember]
        public double X { get; set; }

        [DataMember]
        public double Y { get; set; }

        [DataMember]
        public double Angle { get; set; }
        
        [DataMember]
        public double Radius { get; set; }

        [DataMember]
        public double Speed { get; set; }

        public Position()
        {
            
        }

        public Position(double x, double y, double angle = 0, double radius = 0)
        {
            X = x;
            Y = y;
            Angle = angle;
            Radius = radius;
        }
    }
}
