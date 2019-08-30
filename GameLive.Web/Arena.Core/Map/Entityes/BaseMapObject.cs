using System;
using System.Runtime.Serialization;
using Arena.Core.Enums;
using Arena.Core.ServiceEntityes;
using GameLive.Core.Arena;

namespace Arena.Core.Map.Entityes
{
    [DataContract]
    public abstract class BaseMapObject
    {
        [DataMember]
        public string Id { get; set; }

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
            Id = Guid.NewGuid().ToString();
        }

        protected BaseMapObject(Position position)
        {
            ObjectState = MapObjectState.Alive;
            Position = position;
        }

        public virtual void NextTick()
        {
            
        }

        public virtual Block ToBlock()
        {
            var block = new Block()
            {
                X = (int)Position.X,
                Y = (int)Position.Y,
                Angle = (int)Position.Angle,
                Height = Height,
                Width = Width,
                Image = TexturePath,
                Id = Id
            };

            return block;
        }
    }
}
