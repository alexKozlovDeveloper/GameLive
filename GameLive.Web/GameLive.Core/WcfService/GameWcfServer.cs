using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;
using GameLive.Core.MapEntityes;
using GameLive.Core.WindowsService;

namespace GameLive.Core.WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameWcfServer : BaseLoggerObject, IGameWcfService, IServiceComponent
    {
        private readonly Uri _address;
        private readonly MapController _mapcontroller;
        private readonly int _listeningFunctionTickDelay;

        private Thread _listeningThread;
        private bool _isServerWork;

        private ServiceHost _serviceHost;

        public GameWcfServer(string addressUri, int width, int height, ILogger logger) : base(logger)
        {
            var mapFactory = new MapFactory();
            var map = mapFactory.GetRandomMap(width, height);
            _mapcontroller = new MapController(map);

            _listeningFunctionTickDelay = 50;

            _address = new Uri(addressUri);
        }

        public void Start()
        {
            Logger.Info("Starting GameWcfServer...");
            _isServerWork = true;

            //_listeningThread = new Thread(ListeningFunction);
            //_listeningThread.Start();
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

                //while (_isServerWork)
                //{
                //    Logger.Info("ListeningFunction working...");
                //    Thread.Sleep(_listeningFunctionTickDelay);
                //}

                

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
            //_isServerWork = false;
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
            _mapcontroller.ResetMap(width, height);
        }

        //private void ListeningFunction()
        //{
        //    try
        //    {
        //        // Адрес, на котором будет работать служба.
        //        //Uri address = new Uri("http://localhost:8000/IChatService");
        //        // Привязка, т.е. транспорт, по которому будет происходить обмен сообщениями
        //        BasicHttpBinding binding = new BasicHttpBinding();
        //        // Контракт
        //        Type contract = typeof(IGameWcfService);

        //        // Создаём хост с указанием сервиса
        //        ServiceHost serviceHost = new ServiceHost(this);
        //        // Добавляем конечную точку
        //        serviceHost.AddServiceEndpoint(contract, binding, _address);
        //        // Запускаем наш хост
        //        serviceHost.Open();
        //        Logger.Info("Сервер запущен.");

        //        while (_isServerWork)
        //        {
        //            Logger.Info("ListeningFunction working...");
        //            Thread.Sleep(_listeningFunctionTickDelay);
        //        }

        //        serviceHost.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(e.Message);
        //    }
        //}

    }
}
