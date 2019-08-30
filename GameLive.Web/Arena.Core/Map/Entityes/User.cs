using System;
using System.Runtime.Serialization;
using Arena.Core.Enums;
using Arena.Core.Graphics;
using Arena.Core.Interfaces;
using Arena.Core.ObjectInteraction;
using Arena.Core.ShipInfrastructure;
using GameLive.Core.Arena;

namespace Arena.Core.Map.Entityes
{
    [DataContract]
    public class User : BaseMapObject, IMovableObject, IInteractable // , IMapObject 
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public UserState UserState { get; set; }

        [DataMember]
        public int TimeToLive { get; set; }

        [DataMember]
        public StarShip StarShip { get; set; }

        [DataMember]
        public int KillCount { get; set; }

        [DataMember]
        public int DeadCount { get; set; }

        public delegate void ShotHandler(Bullet bullet);
        public delegate void DeadHandler(string killerUserId);
        public event ShotHandler Shot;
        public event DeadHandler Dead;

        private Random _rnd = new Random();

        public override void NextTick()
        {
            StarShip.Cooldown.NextTick();

            //if (Cooldown > 0)
            //{
            //    Cooldown--;
            //}

            TimeToLive--;

            if (UserState == UserState.Dead && TimeToLive <= 0)
            {
                UserState = UserState.Alive;
                TimeToLive = 10_000_000;
                Position.X = _rnd.Next(300, 800);
                Position.Y = _rnd.Next(100, 400);
                StarShip = new StarShip()
                {
                    HitPoints = 100,
                    Cooldown = new Cooldown()
                };
            }
        }

        public void Move(KeyState keyState)
        {
            double speed = 1.8;
            double angleSpeed = 2.8;

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
                if (StarShip.Cooldown.IsReady)
                {
                    var bullet = new Bullet
                    {
                        TexturePath = ImageUrlHelper.MapEntity.Bullet,
                        Width = 5,
                        Height = 20,
                        UserId = Id,
                        TimeToLive = 100,
                        Position = new Position(Position.X, Position.Y, Position.Angle + _rnd.Next(-3, 3), 15),
                        Damage = 100
                    };

                    Shot?.Invoke(bullet);

                    StarShip.Cooldown.Value = 20;
                }
            }
        }

        public bool IsIntersect(IInteractable obj)
        {
            return Position.IsIntersect(obj.Position);
        }

        public void Intersect(IInteractable intersectedObject)
        {
            if (UserState == UserState.Dead)
            {
                return;
            }

            if (intersectedObject is Bullet bullet)
            {
                if (bullet.UserId != Id)
                {
                    StarShip.HitPoints -= bullet.Damage;

                    if (StarShip.HitPoints <= 0)
                    {
                        UserState = UserState.Dead;
                        TimeToLive = 42;
                        DeadCount++;

                        Dead?.Invoke(bullet.UserId);
                    }
                }
            }
        }
    }
}

