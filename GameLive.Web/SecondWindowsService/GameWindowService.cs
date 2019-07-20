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
using GameLive.Core.Configuration;
using GameLive.Core.Interfaces;
using GameLive.Core.Logging;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService;
using Newtonsoft.Json;

namespace SecondWindowsService
{
    public partial class GameWindowService : ServiceBase
    {
        private readonly ILogger _log;
        private readonly int _tickDelay;

        private bool _isWork;
        private Thread _mainServiceThread;
        private GameWcfServer _server;
        private MapController _mapController;

        public GameWindowService()
        {
            InitializeComponent();

            _log = new Logger(ConfigHelper.LogFolder);

            _tickDelay = ConfigHelper.GameWindowServiceTickDelay;
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("Starting GameWindowService...");

            try
            {
                var mapFactory = new MapFactory();
                var map = mapFactory.GetRandomMap(ConfigHelper.DefaultMapWidth, ConfigHelper.DefaultMapHeight);
                _mapController = new MapController(map);

                _server = new GameWcfServer(ConfigHelper.WcfServiceUri, _log, _mapController);

                _isWork = true;

                _mainServiceThread = new Thread(MainServiceFunction);
                _mainServiceThread.Start();

                _server.Start();
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }

            _log.Info("GameWindowService started.");
        }

        protected override void OnStop()
        {
            _log.Info("Stopping GameWindowService...");

            try
            {
                _server.Stop();
                
                _isWork = false;

                Thread.Sleep(_tickDelay * 2);
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }

            _log.Info("GameWindowService stoped.");
        }

        private void MainServiceFunction()
        {
            _log.Info("Main GameWindowService function started.");

            try
            {
                while (_isWork)
                {
                    _log.Info("Next GameWindowService tick...");
                    _mapController.NextTick();
                    Thread.Sleep(_tickDelay);
                }
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }

            _log.Info("Main GameWindowService function stoped.");
        }
    }
}
