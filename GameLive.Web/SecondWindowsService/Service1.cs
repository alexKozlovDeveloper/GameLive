using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;
using GameLive.Core.Logging;
using GameLive.Core.WcfService;

namespace SecondWindowsService
{
    public partial class Service1 : ServiceBase
    {
        private ILogger _log;
        private bool _isWork;
        private Thread _thread;
        private GameWcfServer _server;

        public Service1()
        {
            InitializeComponent();

            _log = new Logger(@"C:\Schneider Electric\GameLive\GlobalLog");

            _thread = new Thread(Func);
            _server = new GameWcfServer("http://localhost:8000/IChatService", _log);
        }

        protected override void OnStart(string[] args)
        {
            _log.Info("Start.");
            _isWork = true;
            _thread.Start();
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Stop();
            _log.Info("End.");
            _isWork = false;
            Thread.Sleep(500);
        }

        private void Func()
        {
            while (_isWork)
            {
                _log.Info("Working...");
                Thread.Sleep(1000);
            }
        }
    }
}
