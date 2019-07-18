﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;

namespace GameLive.Core.Logging
{
    public class Logger : ILogger
    {
        private readonly string _folderPath;
        private readonly bool _writeToConsole;

        private readonly Object _lockObject;

        private string FilePath => Path.Combine(_folderPath, $"[{DateTime.Now:MM-dd-yyyy}] Log.txt");

        public Logger(string folderPath, bool writeToConsole = false)
        {
            _folderPath = folderPath;
            _writeToConsole = writeToConsole;

            if (Directory.Exists(_folderPath) == false)
            {
                Directory.CreateDirectory(_folderPath);
            }

            _lockObject = new object();
        }

        public void Error(string message)
        {
            Write("ERROR: " + message);
        }

        public void Info(string message)
        {
            Write("INFO: " + message);
        }

        public void Warn(string message)
        {
            Write("WARN: " + message);
        }

        private void Write(string message)
        {
            message = $"[{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.ffff")}] " + message;

            WriteToFile(message);
            WriteToConsole(message);
        }

        private void WriteToConsole(string message)
        {
            if (_writeToConsole == true)
            {
                Console.WriteLine(message);
            }
        }

        private void WriteToFile(string message)
        {
            lock (_lockObject)
            {
                File.AppendAllText(FilePath, message + Environment.NewLine);
            }
        }
    }
}
