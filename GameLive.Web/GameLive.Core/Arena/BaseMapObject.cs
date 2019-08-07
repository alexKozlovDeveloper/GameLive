using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class BaseMapObject
    {
        [DataMember]
        public Position Position { get; set; }

        public BaseMapObject()
        {
            
        }

        public BaseMapObject(Position position)
        {
            Position = position;
        }
    }
}
