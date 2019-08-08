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
            Position.X += (int)(Math.Sin(Position.Angle / 57.2) * 10);
            Position.Y += (int)(Math.Cos(Position.Angle / 57.2) * 10);

            TimeToLive--;

            if (TimeToLive <= 0)
            {
                ObjectState = MapObjectState.RemovalCandidate;
            }
        }
    }
}
