using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Arena.Core.Enums;
using Arena.Core.Interfaces;
using Arena.Core.Map;
using Arena.Core.Map.Entityes;
using Arena.Core.ShipInfrastructure;
using Arena.WcfService.Interfaces;
using GameLive.Core;
using GameLive.Core.Arena;
using GameLive.Core.Interfaces;
using GameLive.Core.WindowsService;

namespace Arena.WcfService.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ArenaWcfServer : BaseLoggerObject, IArenaWcfService, IServiceComponent
    {
        private readonly Uri _address;

        private ServiceHost _serviceHost;

        private MapObjectStore _mapObjectStore;

        public ArenaWcfServer(string addressUri, ILogger logger) : base(logger)
        {
            _address = new Uri(addressUri);
        }

        public void Start()
        {
            Logger.Info("Starting ArenaWcfServer...");

            _mapObjectStore = new MapObjectStore();
            //_movableObjects = new List<IMovableObject>();

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

            _mapObjectStore.NextTick();
        }

        public void Move(string userId, KeyState keyState)
        {
            Logger.Info($"Processing 'Move' [{userId} {keyState}]");

            var user = _mapObjectStore.Users.FirstOrDefault(a => a.Id == userId);

            user?.Move(keyState);
        }

        public string AddUser(string name)
        {
            Logger.Info($"Processing 'AddUser' [{name}]");

            var user = new User
            {
                Name = name,
                Id = Guid.NewGuid().ToString(),
                Position = new Position(Global.Random.Next(300,800), Global.Random.Next(100, 400), 0, 25),
                StarShip = new StarShip()
                {
                    HitPoints = 100,
                    Cooldown = new Cooldown()
                },
                UserState = UserState.Alive,
                TimeToLive = 10_000_000
            };

            user.Shot += User_Shot;
            user.Dead += User_Dead;

            _mapObjectStore.AddMapObject(user);

            return user.Id;
        }

        private void User_Dead(string killerUserId)
        {
            var user = _mapObjectStore.Users.FirstOrDefault(a => a.Id == killerUserId);

            if (user != null)
            {
                user.KillCount++;
            }
        }

        private void User_Shot(Bullet bullet)
        {
            _mapObjectStore.AddMapObject(bullet);
        }

        public List<User> GetUsers()
        {
            Logger.Info($"Processing 'GetUsers'");

            return _mapObjectStore.Users.ToList();
        }

        public List<Bullet> GetBullets()
        {
            Logger.Info($"Processing 'GetBullets'");

            var bullets = _mapObjectStore.MapObjects.OfType<Bullet>().ToList();

            return bullets;
        }
    }
}
