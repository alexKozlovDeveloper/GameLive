using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;
using GameLive.Core.Logging;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService;
using Newtonsoft.Json;

namespace SecondWindowsService
{
    public partial class Service1 : ServiceBase, IGameWcfService
    {
        private ILogger _log;
        private bool _isWork;
        private Thread _thread;
        private GameWcfServer _server;
        private MapController _mapController;

        public Service1()
        {
            InitializeComponent();

            _log = new Logger(@"C:\Schneider Electric\GameLive\GlobalLog");

            _thread = new Thread(Func);
            
            var mapFactory = new MapFactory();
            var map = mapFactory.GetRandomMap(70, 70);
            _mapController = new MapController(map);

            _server = new GameWcfServer("http://localhost:8080/IChatService4", _log, _mapController);

            var json = JsonConvert.SerializeObject(map);
            //IntiWcfService();
        }

        //private void IntiWcfService()
        //{
        //    // Адрес, на котором будет работать служба.
        //    Uri address = new Uri("http://localhost:8000/IChatService");
        //    // Привязка, т.е. транспорт, по которому будет происходить обмен сообщениями
        //    BasicHttpBinding binding = new BasicHttpBinding();
        //    // Контракт
        //    Type contract = typeof(IGameWcfService);

        //    // Создаём хост с указанием сервиса
        //    ServiceHost serviceHost = new ServiceHost(this);
        //    // Добавляем конечную точку
        //    serviceHost.AddServiceEndpoint(contract, binding, address);
        //    // Запускаем наш хост
        //    serviceHost.Open();
        //    _log.Info("Сервер запущен.");
        //}

        public string GetCurrentMap(string message)
        {
            //Console.WriteLine($"Сервер получил сообщение:  {message}");
            return "lol kek servise 2";
        }

        public void ResetMap(int width, int height)
        {
            
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //IntiWcfService();
                _log.Info("Start.");
                _isWork = true;
                _thread.Start();
                _server.Start();
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _server.Stop();
                _log.Info("End.");
                _isWork = false;
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }
        }

        private void Func()
        {
            try
            {
                while (_isWork)
                {
                    _log.Info("Working...");
                    _mapController.NextTic();
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }
        }
    }
}
