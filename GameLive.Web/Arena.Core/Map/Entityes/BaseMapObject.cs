using System.Runtime.Serialization;
using Arena.Core.Enums;
using GameLive.Core.Arena;

namespace Arena.Core.Map.Entityes
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

        public virtual void NextTick()
        {
            
        }
    }
}
