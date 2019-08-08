using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Arena.Enums;
using GameLive.Core.Arena.Interfaces;
using GameLive.Core.Arena.ObjectInteraction;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class UserInfo : BaseMapObject, IMapObject, IMovableObject
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int HitPoints { get; set; }

        [DataMember]
        public UserState UserState { get; set; }

        [DataMember]
        public int TimeToLive { get; set; }

        [DataMember]
        public int Cooldown { get; set; }

        public delegate void ShotHandler(Bullet bullet);
        public event ShotHandler Shot;

        private Random _rnd = new Random();

        public void NextTick()
        {
            if (Cooldown > 0)
            {
                Cooldown--;
            }

            TimeToLive--;

            if (UserState == UserState.Dead && TimeToLive <= 0)
            {
                UserState = UserState.Alive;
                TimeToLive = 10_000_000;
                Position.X = _rnd.Next(300, 800);
                Position.Y = _rnd.Next(100, 400);
                HitPoints = 100;
            }
        }

        public void Move(KeyState keyState)
        {
            double speed = 1.3;
            double angleSpeed = 2.1;

            if (UserState == UserState.Dead)
            {
                return;
            }

            if ((keyState & KeyState.Up) == KeyState.Up && Position.Y < 500)
            {
                Position.Y += speed;
            }

            if ((keyState & KeyState.Down) == KeyState.Down && Position.Y > -170)
            {
                Position.Y -= speed;
            }

            if ((keyState & KeyState.Left) == KeyState.Left && Position.X > 0)
            {
                Position.X -= speed;
            }

            if ((keyState & KeyState.Right) == KeyState.Right && Position.X < 1100)
            {
                Position.X += speed;
            }

            if ((keyState & KeyState.ClockwiseRotation) == KeyState.ClockwiseRotation)
            {
                Position.Angle += angleSpeed;

                if (Position.Angle > 360)
                {
                    Position.Angle -= 360;
                }
            }

            if ((keyState & KeyState.CounterclockwiseRotation) == KeyState.CounterclockwiseRotation)
            {
                Position.Angle -= angleSpeed;

                if (Position.Angle < 0)
                {
                    Position.Angle += 360;
                }
            }

            if ((keyState & KeyState.IsAttack) == KeyState.IsAttack)
            {
                if (Cooldown == 0)
                {
                    var bullet = new Bullet
                    {
                        UserId = Id,
                        TimeToLive = 100,
                        Position = new Position(Position.X, Position.Y, Position.Angle, 5),
                        Damage = 10
                    };

                    Shot(bullet);

                    Cooldown = 10;
                }
            }
        }

      
    }
}

