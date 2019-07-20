using System;
using System.ServiceModel;
using System.Threading;
using GameLive.Core.Interfaces;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService.Interfaces;
using GameLive.Core.WindowsService;

namespace GameLive.Core.WcfService.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameWcfServer : BaseLoggerObject, IGameWcfService, IServiceComponent
    {
        private readonly Uri _address;
        private readonly MapController _mapcontroller;

        private ServiceHost _serviceHost;

        public GameWcfServer(string addressUri, int width, int height, ILogger logger) : base(logger)
        {
            var mapFactory = new MapFactory();
            var map = mapFactory.GetRandomMap(width, height);
            _mapcontroller = new MapController(map);

            _address = new Uri(addressUri);
        }

        public void Start()
        {
            Logger.Info("Starting GameWcfServer...");

            try
            {
                // Адрес, на котором будет работать служба.
                //Uri address = new Uri("http://localhost:8000/IChatService");
                // Привязка, т.е. транспорт, по которому будет происходить обмен сообщениями
                BasicHttpBinding binding = new BasicHttpBinding();
                // Контракт
                Type contract = typeof(IGameWcfService);

                // Создаём хост с указанием сервиса
                _serviceHost = new ServiceHost(this);
                // Добавляем конечную точку
                _serviceHost.AddServiceEndpoint(contract, binding, _address);
                // Запускаем наш хост
                _serviceHost.Open();
                Logger.Info("Сервер запущен.");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public void Stop()
        {
            Logger.Info("Stoping GameWcfServer...");
            _serviceHost.Close();
        }

        public void NextTick(int millisecondsTickDelay)
        {
            _mapcontroller.NextTick();
            Thread.Sleep(millisecondsTickDelay);
        }


        public string GetCurrentMap(string message)
        {
            return _mapcontroller.GetMapAsJson();
        }

        public void ResetMap(int width, int height)
        {
            var mapFactory = new MapFactory();
            var newMap = mapFactory.GetRandomMap(width, height);

            _mapcontroller.ResetMap(newMap);
        }
    }
}
