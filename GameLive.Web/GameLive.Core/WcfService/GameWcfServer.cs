using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;
using GameLive.Core.MapEntityes;

namespace GameLive.Core.WcfService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameWcfServer : BaseLoggerObject, IGameWcfService
    {
        private readonly Uri _address;
        private readonly MapController _mapcontroller;

        private Thread _listeningThread;
        private bool _isServerWork;

        private int _tickDelay;

        public GameWcfServer(string addressUri, ILogger logger, MapController mapcontroller) : base(logger)
        {
            _address = new Uri(addressUri);
            _mapcontroller = mapcontroller;

            _tickDelay = 100;
        }

        public void Start()
        {
            Logger.Info("Starting GameWcfServer...");
            _isServerWork = true;

            _listeningThread = new Thread(ListeningFunction);
            _listeningThread.Start();
        }

        public void Stop()
        {
            Logger.Info("Stoping GameWcfServer...");
            _isServerWork = false;
        }

        public string GetCurrentMap(string message)
        {
            return _mapcontroller.GetMapAsJson();
        }

        public void ResetMap(int width, int height)
        {
            _mapcontroller.ResetMap(width, height);
        }

        private void ListeningFunction()
        {
            try
            {
                // Адрес, на котором будет работать служба.
                //Uri address = new Uri("http://localhost:8000/IChatService");
                // Привязка, т.е. транспорт, по которому будет происходить обмен сообщениями
                BasicHttpBinding binding = new BasicHttpBinding();
                // Контракт
                Type contract = typeof(IGameWcfService);

                // Создаём хост с указанием сервиса
                ServiceHost serviceHost = new ServiceHost(this);
                // Добавляем конечную точку
                serviceHost.AddServiceEndpoint(contract, binding, _address);
                // Запускаем наш хост
                serviceHost.Open();
                Logger.Info("Сервер запущен.");

                while (_isServerWork)
                {
                    Logger.Info("ListeningFunction working...");
                    Thread.Sleep(_tickDelay);
                }

                serviceHost.Close();

            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

    }
}
