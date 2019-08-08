using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Arena.Enums;
using GameLive.Core.Arena.Interfaces;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class Bullet : BaseMapObject, IMapObject
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int Damage { get; set; }

        [DataMember]
        public int TimeToLive { get; set; }

        public void NextTick()
        {
            double speed = 14;

            Position.X += Math.Sin(Position.Angle / 57.2) * speed;
            Position.Y += Math.Cos(Position.Angle / 57.2) * speed;

            TimeToLive--;

            if (TimeToLive <= 0)
            {
                ObjectState = MapObjectState.RemovalCandidate;
            }

            if (Position.X < 0 || Position.Y < 0 || Position.X > 1100 || Position.Y > 670)
            {
                ObjectState = MapObjectState.RemovalCandidate;
            }
        }
    }
}
