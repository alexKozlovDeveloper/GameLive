using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Arena;
using GameLive.Core.Arena.Enums;
using GameLive.Core.Arena.ObjectInteraction;
using GameLive.Core.Interfaces;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService.Interfaces;
using GameLive.Core.WindowsService;

namespace GameLive.Core.WcfService.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ArenaWcfServer : BaseLoggerObject, IArenaWcfService, IServiceComponent
    {
        private readonly Uri _address;

        private ServiceHost _serviceHost;

        private Dictionary<string, UserInfo> _users;
        private List<Bullet> _bullets;

        public ArenaWcfServer(string addressUri, ILogger logger) : base(logger)
        {
            _address = new Uri(addressUri);
        }

        public void Start()
        {
            Logger.Info("Starting ArenaWcfServer...");

            _users = new Dictionary<string, UserInfo>();
            _bullets = new List<Bullet>();

            try
            {
                // Адрес, на котором будет работать служба.
                //Uri address = new Uri("http://localhost:8000/IChatService");
                // Привязка, т.е. транспорт, по которому будет происходить обмен сообщениями
                BasicHttpBinding binding = new BasicHttpBinding();
                // Контракт
                Type contract = typeof(IArenaWcfService);

                // Создаём хост с указанием сервиса
                _serviceHost = new ServiceHost(this);
                // Добавляем конечную точку
                _serviceHost.AddServiceEndpoint(contract, binding, _address);
                // Запускаем наш хост
                _serviceHost.Open();
                //Logger.Info("Сервер запущен.");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public void Stop()
        {
            Logger.Info("Stoping ArenaWcfServer...");
            _serviceHost.Close();
        }

        public void NextTick(int millisecondsTickDelay)
        {
            Logger.Info("NextTick ArenaWcfServer...");
            //Thread.Sleep(millisecondsTickDelay);

            foreach (var bullet in _bullets)
            {
                bullet.NextTick();
            }

            var objectToRemove = _bullets.Where(a => a.ObjectState == MapObjectState.RemovalCandidate).ToList();

            foreach (var bullet in objectToRemove)
            {
                _bullets.Remove(bullet);
            }

            foreach (var userInfo in _users)
            {
                userInfo.Value.NextTick();
            }

            foreach (var bullet in _bullets)
            {
                foreach (var userInfo in _users)
                {
                    if (userInfo.Value.Id != bullet.UserId)
                    {
                        if (userInfo.Value.Position.IsIntersect(bullet.Position) && userInfo.Value.UserState != UserState.Dead)
                        {
                            bullet.TimeToLive = 0;
                            userInfo.Value.HitPoints -= bullet.Damage;

                            if (userInfo.Value.HitPoints <= 0)
                            {
                                userInfo.Value.UserState = UserState.Dead;
                                userInfo.Value.TimeToLive = 37;
                            }
                        }
                    }
                }
            }

        }

        public void Move(string userId, KeyState keyState)
        {
            Logger.Info($"Processing 'Move' [{userId} {keyState}]");

            if (_users.Keys.Contains(userId) == false)
            {
                return;
            }
            
            var user = _users[userId];

            user.Move(keyState);
        }

        public string AddUser(string name)
        {
            Logger.Info($"Processing 'AddUser' [{name}]");

            var user = new UserInfo
            {
                Name = name,
                Id = Guid.NewGuid().ToString(),
                Position = new Position(0, 0, 0, 25),
                HitPoints = 100,
                Cooldown = 0,
                UserState = UserState.Alive,
                TimeToLive = 10_000_000
            };

            user.Shot += User_Shot;

            _users.Add(user.Id, user);

            return user.Id;
        }

        private void User_Shot(Bullet bullet)
        {
            _bullets.Add(bullet);
        }

        public List<UserInfo> GetUsers()
        {
            Logger.Info($"Processing 'GetUsers'");

            return _users.Values.ToList();
        }

        public List<Bullet> GetBullets()
        {
            Logger.Info($"Processing 'GetBullets'");

            return _bullets;
        }
    }
}
