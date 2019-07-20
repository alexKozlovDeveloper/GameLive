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
using GameLive.Core.WindowsService;
using Newtonsoft.Json;

namespace SecondWindowsService
{
    public partial class GameWindowService : ServiceBase
    {
        private readonly ILogger _log;
        private readonly int _tickDelay;

        private readonly List<IServiceComponent> _serviceComponents;

        private bool _isWork;
        private Thread _mainServiceThread;
        
        public GameWindowService()
        {
            InitializeComponent();

            _log = new Logger(ConfigHelper.LogFolder);
            _tickDelay = ConfigHelper.GameWindowServiceTickDelay;

            _serviceComponents = new List<IServiceComponent>
            {
                new GameWcfServer(ConfigHelper.WcfServiceUri, ConfigHelper.DefaultMapWidth, ConfigHelper.DefaultMapHeight, _log)
            };
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("Starting GameWindowService...");

            try
            {
                _isWork = true;

                foreach (var serviceComponent in _serviceComponents)
                {
                    serviceComponent.Start();
                }

                _mainServiceThread = new Thread(TickServiceFunction);
                _mainServiceThread.Start();
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
                foreach (var serviceComponent in _serviceComponents)
                {
                    serviceComponent.Stop();
                }

                _isWork = false;

                Thread.Sleep(_tickDelay * 2);
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }

            _log.Info("GameWindowService stoped.");
        }

        private void TickServiceFunction()
        {
            _log.Info("Main GameWindowService function started.");

            try
            {
                while (_isWork)
                {
                    _log.Info("Next GameWindowService tick...");

                    foreach (var serviceComponent in _serviceComponents)
                    {
                        serviceComponent.NextTick(_tickDelay);
                    }

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
