using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Arena;
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

        public ArenaWcfServer(string addressUri, ILogger logger) : base(logger)
        {
            _address = new Uri(addressUri);
        }

        public void Start()
        {
            Logger.Info("Starting ArenaWcfServer...");

            _users = new Dictionary<string, UserInfo>();

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
            
            //TODO: need to delete
            //RandomMove();
        }

        public void RandomMove()
        {
            //TODO: need to delete

            var max = 100;

            foreach (var user in _users)
            {
                if (user.Value.Position.X < max)
                {
                    user.Value.Position.X++;
                }
                else
                {
                    user.Value.Position.X = 0;
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

            if ((keyState & KeyState.Up) == KeyState.Up)
            {
                user.Position.Y += 5;
            }

            if ((keyState & KeyState.Down) == KeyState.Down)
            {
                user.Position.Y -= 5;
            }

            if ((keyState & KeyState.Left) == KeyState.Left)
            {
                user.Position.X -= 5;
            }

            if ((keyState & KeyState.Right) == KeyState.Right)
            {
                user.Position.X += 5;
            }
        }

        public string AddUser(string name)
        {
            Logger.Info($"Processing 'AddUser' [{name}]");

            var user = new UserInfo
            {
                Name = name,
                Id = Guid.NewGuid().ToString(),
                Position = new Position(0, 0)
            };

            _users.Add(user.Id, user);

            return user.Id;
        }

        public List<UserInfo> GetUsers()
        {
            Logger.Info($"Processing 'GetUsers'");

            return _users.Values.ToList();
        }
    }
}
