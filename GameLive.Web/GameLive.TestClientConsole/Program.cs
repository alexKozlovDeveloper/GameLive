using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Logging;
using GameLive.Core.WcfService;

namespace GameLive.TestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new Logger("log");
            var client = new GameWcfClient("http://localhost:8080/IChatService4");

            client.GetCurrentMap();


        }
    }
}
