using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Configuration;
using GameLive.Core.Logging;
using GameLive.Core.WcfService;

namespace GameLive.TestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logFolder = ConfigHelper.LogFolder;
            var wcfServiceUri = ConfigHelper.WcfServiceUri;

            var a = ConfigHelper.DefaultMapWidth;
            var b = ConfigHelper.DefaultMapHeight;

            var logger = new Logger("log");
            var client = new GameWcfClient("http://localhost:8080/IChatService4");

            client.GetCurrentMap();


        }
    }
}
