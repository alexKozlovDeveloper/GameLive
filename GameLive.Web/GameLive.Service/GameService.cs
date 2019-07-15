using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameLive.Core.MapEntityes;

namespace GameLive.Service
{
    public partial class GameService : ServiceBase
    {
        private readonly string _filePath;
        private readonly string _fileName;

        private string FilePath => Path.Combine(_filePath, _fileName);

        private readonly Thread _gameThread;
        private bool _isWorking;

        private MapController _mapController;

        private readonly object _mapAccessLock = new object();

        public GameService()
        {
            InitializeComponent();

            this.ServiceName = "GameService";

            _filePath = @"C:\Schneider Electric\GameLive\Service";
            _fileName = "serviceLog.txt";

            _gameThread = new Thread(GameFunction);
        }

        private void GameFunction()
        {
            while (_isWorking)
            {
                lock (_mapAccessLock)
                {
                    _mapController.NextTic();
                }

                File.AppendAllText(FilePath, "working2..." + Environment.NewLine);

                Thread.Sleep(1000);
            }
        }

        protected override void OnStart(string[] args)
        {
            var mapFactory = new MapFactory();
            var map = mapFactory.GetRandomMap(50, 50);
            _mapController = new MapController(map);

            _isWorking = true;
            _gameThread.Start();
        }

        protected override void OnStop()
        {
            _isWorking = false;
            Thread.Sleep(1500);
            _gameThread.Abort();
        }
    }
}
