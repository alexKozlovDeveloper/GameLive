using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Arena.Enums;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class BaseMapObject
    {
        [DataMember]
        public Position Position { get; set; }

        [DataMember]
        public MapObjectState ObjectState { get; set; }

        public BaseMapObject()
        {
            ObjectState = MapObjectState.Alive;
        }

        public BaseMapObject(Position position)
        {
            ObjectState = MapObjectState.Alive;
            Position = position;
        }
    }
}
