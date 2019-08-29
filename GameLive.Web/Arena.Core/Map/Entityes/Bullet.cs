using System;
using System.Runtime.Serialization;
using Arena.Core.Enums;
using Arena.Core.Interfaces;
using Arena.Core.ObjectInteraction;
using GameLive.Core.Arena;

namespace Arena.Core.Map.Entityes
{
    [DataContract]
    public class Bullet : BaseMapObject, IInteractable //, IMapObject
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public int Damage { get; set; }

        [DataMember]
        public int TimeToLive { get; set; }

        public override void NextTick()
        {
            double speed = 14;

            Position.X += Math.Sin(Position.Angle / 57.2) * speed;
            Position.Y += Math.Cos(Position.Angle / 57.2) * speed;

            TimeToLive--;

            if (TimeToLive <= 0)
            {
                ObjectState = MapObjectState.RemovalCandidate;
            }

            if (Position.X < 0 || Position.Y < -170 || Position.X > 1100 || Position.Y > 500)
            {
                ObjectState = MapObjectState.RemovalCandidate;
            }
        }

        public bool IsIntersect(IInteractable obj)
        {
            return Position.IsIntersect(obj.Position);
        }

        public void Intersect(IInteractable intersectedObject)
        {
            if (ObjectState == MapObjectState.RemovalCandidate)
            {
                return;
            }

            if (intersectedObject is User user)
            {
                if (user.Id == UserId)
                {
                    return;
                }

                if (user.UserState == UserState.Dead)
                {
                    return;
                }
            }

            ObjectState = MapObjectState.RemovalCandidate;
        }
    }
}
