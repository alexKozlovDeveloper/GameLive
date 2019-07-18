using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Logging;
using GameLive.Core.WcfService;

namespace GameLive.TestServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger("log");

            var server = new GameWcfServer("http://localhost:8000/IChatService", logger);

            server.Start();

            Console.ReadKey();
        }
    }
}
