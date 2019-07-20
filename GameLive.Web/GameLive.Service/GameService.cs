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
using GameLive.Core.Interfaces;
using GameLive.Core.Logging;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService;

namespace GameLive.Service
{
    public partial class GameService : ServiceBase
    {
        //private readonly string _filePath;
        //private readonly string _fileName;

        //private string FilePath => Path.Combine(_filePath, _fileName);

        //private readonly Thread _gameThread;
        //private bool _isWorking;

        //private MapController _mapController;

        //private readonly object _mapAccessLock = new object();

        //private ILogger _log;
        //private GameWcfServer server;

        

        public GameService()
        {
            InitializeComponent();

            //_log = new Logger(@"C:\Schneider Electric\GameLive\GlobalLog");
            //server = new GameWcfServer("http://localhost:8000/IChatService", _log);

            //this.ServiceName = "GameService";

            //_filePath = @"C:\Schneider Electric\GameLive\Service";
            //_fileName = "serviceLog.txt";

            //_gameThread = new Thread(GameMainFunction);
           // _log.Info("Init Done");
        }

        private void GameMainFunction()
        {
            

            //server.Start();

            //while (_isWorking)
            //{
            //    Thread.Sleep(100);
            //}

            //try
            //{
            //    while (_isWorking)
            //    {
            //        lock (_mapAccessLock)
            //        {
            //            _mapController.NextTick();
            //        }

            //        MapSaver.Save(_mapController.Map, @"C:\Schneider Electric\GameLive\Service\ActualMap.txt");

            //        //File.AppendAllText(FilePath, "working2..." + Environment.NewLine);

            //        Thread.Sleep(1000);
            //    }
            //}
            //catch (Exception e)
            //{
            //    File.AppendAllText(FilePath, e.Message + Environment.NewLine);
            //}
        }

        protected override void OnStart(string[] args)
        {
            //_log.Info("Start");
            //server.Start();
            
            //_isWorking = true;
            //_gameThread.Start();

            //try
            //{
            //    var mapFactory = new MapFactory();
            //    var map = mapFactory.GetRandomMap(50, 50);
            //    _mapController = new MapController(map);

            //    _isWorking = true;
            //    _gameThread.Start();
            //}
            //catch (Exception e)
            //{
            //    File.AppendAllText(FilePath,e.Message + Environment.NewLine);
            //}
        }

        protected override void OnStop()
        {
            //_log.Info("Stop");
            //_isWorking = false;
            //server.Stop();
           // Thread.Sleep(1500);
            //_gameThread.Abort();
        }
    }
}
