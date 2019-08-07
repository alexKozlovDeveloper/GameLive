using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class Bullet : BaseMapObject
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int TimeToLive { get; set; }

        public void Move()
        {
            Position.X += (int)(Math.Sin(Position.Angle / 57.2) * 10);
            Position.Y += (int)(Math.Cos(Position.Angle / 57.2) * 10);

            //Position.X += 1 + (int)Math.Sin(Position.Angle);
            //Position.Y += 1;
        }
    }
}
