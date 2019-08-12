using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Arena.Core.Enums;
using Arena.Core.Interfaces;
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

        private List<UserInfo> _users;
        //private List<Bullet> _bullets;

        private List<BaseMapObject> _mapObjects;

        private List<IInteractable> _interactables;
        //private List<IMovableObject> _movableObjects;

        private Random _rnd = new Random();

        public ArenaWcfServer(string addressUri, ILogger logger) : base(logger)
        {
            _address = new Uri(addressUri);
        }

        public void Start()
        {
            Logger.Info("Starting ArenaWcfServer...");

            _users = new List<UserInfo>();
            //_bullets = new List<Bullet>();

            _mapObjects = new List<BaseMapObject>();
            _interactables = new List<IInteractable>();
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

        private void AddMapObject(BaseMapObject obj)
        {
            _mapObjects.Add(obj);

            if (obj is IInteractable interactable)
            {
                _interactables.Add(interactable);
            }

            //if (obj is Bullet bullet)
            //{
            //    _bullets.Add(bullet);
            //}

            if (obj is UserInfo user)
            {
                _users.Add(user);
            }

            //if (obj is IMovableObject movableObject)
            //{
            //    _movableObjects.Add(movableObject);
            //}
        }

        private void RemoveMapObject(BaseMapObject obj)
        {
            _mapObjects.Remove(obj);

            if (obj is IInteractable interactable)
            {
                _interactables.Remove(interactable);
            }
        }

        public void NextTick(int millisecondsTickDelay)
        {
            Logger.Info("NextTick ArenaWcfServer...");
            //Thread.Sleep(millisecondsTickDelay);

            foreach (var baseMapObject in _mapObjects)
            {
                baseMapObject.NextTick();
            }

            for (int i = 0; i < _interactables.Count; i++)
            {
                for (int j = i + 1; j < _interactables.Count; j++)
                {
                    if (i == j) { continue; }

                    if (_interactables[i].IsIntersect(_interactables[j]))
                    {
                        _interactables[i].Intersect(_interactables[j]);
                        _interactables[j].Intersect(_interactables[i]);
                    }
                }
            }

            var objectToRemove = _mapObjects.Where(a => a.ObjectState == MapObjectState.RemovalCandidate).ToList();

            foreach (var mapObject in objectToRemove)
            {
                RemoveMapObject(mapObject);
            }

        }

        public void Move(string userId, KeyState keyState)
        {
            Logger.Info($"Processing 'Move' [{userId} {keyState}]");

            var user = _users.FirstOrDefault(a => a.Id == userId);

            user?.Move(keyState);
        }

        public string AddUser(string name)
        {
            Logger.Info($"Processing 'AddUser' [{name}]");

            var user = new UserInfo
            {
                Name = name,
                Id = Guid.NewGuid().ToString(),
                Position = new Position(_rnd.Next(300,800), _rnd.Next(100, 400), 0, 25),
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

            //_users.Add(user);
            AddMapObject(user);

            return user.Id;
        }

        private void User_Dead(string killerUserId)
        {
            var user = _users.FirstOrDefault(a => a.Id == killerUserId);

            if (user != null)
            {
                user.KillCount++;
            }
        }

        private void User_Shot(Bullet bullet)
        {
            AddMapObject(bullet);
            //_bullets.Add(bullet);
            //_interactables.Add(bullet);
        }

        public List<UserInfo> GetUsers()
        {
            Logger.Info($"Processing 'GetUsers'");

            return _users.ToList();
        }

        public List<Bullet> GetBullets()
        {
            Logger.Info($"Processing 'GetBullets'");

            var bullets = _mapObjects.OfType<Bullet>().ToList();

            return bullets;
        }
    }
}
