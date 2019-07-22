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
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }

        public Position()
        {
            
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
