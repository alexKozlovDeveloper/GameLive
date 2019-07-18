using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;

namespace GameLive.Core.WcfService
{
    public class GameWcfServer
    {
        private ILogger _log;

        private Thread _listeningThread;

        private bool _isServerWork;

        private readonly Uri _address;

        public GameWcfServer(string addressUri, ILogger logger)
        {
            _address = new Uri(addressUri);
            _log = logger;
        }

        public void Start()
        {
            _log.Info("Starting GameWcfServer...");
            _isServerWork = true;

            _listeningThread = new Thread(ListeningFunction);
            _listeningThread.Start();
        }

        public void Stop()
        {
            _log.Info("Stoping GameWcfServer...");
            _isServerWork = false;
        }

        private void ListeningFunction()
        {
            // Адрес, на котором будет работать служба.
            //Uri address = new Uri("http://localhost:8000/IChatService");
            // Привязка, т.е. транспорт, по которому будет происходить обмен сообщениями
            BasicHttpBinding binding = new BasicHttpBinding();
            // Контракт
            Type contract = typeof(IGameWcfService);

            // Создаём хост с указанием сервиса
            ServiceHost serviceHost = new ServiceHost(typeof(GameWcfService));
            // Добавляем конечную точку
            serviceHost.AddServiceEndpoint(contract, binding, _address);
            // Запускаем наш хост
            serviceHost.Open();
            _log.Info("Сервер запущен.");

            while (_isServerWork)
            {
                _log.Info("ListeningFunction working...");
                Thread.Sleep(100);
            }

            serviceHost.Close();
        }

    }
}
