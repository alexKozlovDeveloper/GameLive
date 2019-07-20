using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Configuration;
using GameLive.Core.Logging;
using GameLive.Core.WcfService;
using GameLive.Core.WcfService.Server;

namespace GameLive.TestServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger("log", true);

            var server = new GameWcfServer("http://localhost:8002/IChatService3", ConfigHelper.DefaultMapWidth, ConfigHelper.DefaultMapHeight, logger);

            server.Start();

            Console.ReadKey();
        }
    }
}
