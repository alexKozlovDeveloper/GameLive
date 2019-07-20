using System;
using System.Diagnostics;
using System.IO;
using GameLive.Core.Interfaces;

namespace GameLive.Core.Logging
{
    public class Logger : ILogger
    {
        private readonly Guid _id = Guid.NewGuid();

        private readonly string _folderPath;
        private readonly bool _writeToConsole;

        private readonly object _lockObject;

        private readonly string _loggerName;

        private string FilePath => Path.Combine(_folderPath, $"[{DateTime.Now:MM-dd-yyyy}] [{_loggerName}] Log.txt");

        public Logger(string folderPath, bool writeToConsole = false)
        {
            _folderPath = folderPath;
            _writeToConsole = writeToConsole;

            if (Directory.Exists(_folderPath) == false)
            {
                Directory.CreateDirectory(_folderPath);
            }

            _lockObject = new object();

            _loggerName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
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
            message = $"[{DateTime.Now:MM/dd/yyyy HH:mm:ss.ffff}] " + message;

            WriteToFile(message);
            WriteToConsole(message);
        }

        private void WriteToConsole(string message)
        {
            if (_writeToConsole)
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
