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

namespace GameLive.Service
{
    public partial class GameService : ServiceBase
    {
        private readonly string _filePath;
        private readonly string _fileName;

        private string FilePath => Path.Combine(_filePath, _fileName);

        private Thread _gameThread;
        private bool _isWorking;

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
                File.AppendAllText(FilePath, "working1..." + Environment.NewLine);

                Thread.Sleep(1000);
            }
        }

        protected override void OnStart(string[] args)
        {
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
