using System.Runtime.Serialization;
using Arena.Core.Enums;
using GameLive.Core.Arena;

namespace Arena.Core.Map.Entityes
{
    [DataContract]
    public abstract class BaseMapObject
    {
        [DataMember]
        public Position Position { get; set; }

        [DataMember]
        public MapObjectState ObjectState { get; set; }

        [DataMember]
        public string TexturePath { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        protected BaseMapObject()
        {
            ObjectState = MapObjectState.Alive;
        }

        protected BaseMapObject(Position position)
        {
            ObjectState = MapObjectState.Alive;
            Position = position;
        }

        public virtual void NextTick()
        {
            
        }
    }
}
