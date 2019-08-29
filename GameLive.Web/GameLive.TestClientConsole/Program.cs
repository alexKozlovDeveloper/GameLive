using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Core.Enums;
using Arena.WcfService.Client;
using GameLive.Core.Arena;
using GameLive.Core.Configuration;
using GameLive.Core.Logging;
using GameLive.Core.WcfService;
using GameLive.Core.WcfService.Client;

namespace GameLive.TestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            List<int> items = new List<int>() { 1, 2, 3, 4, 5 };

            List<string> result = new List<string>();

            for (int i = 0; i < items.Count; i++)
            {
                for (int j = i + 1; j < items.Count; j++)
                {
                    if (i != j)
                    {
                        result.Add($"{i} - {j}");
                    }
                }
            }

            //var logFolder = ConfigHelper.LogFolder;
            //var wcfServiceUri = ConfigHelper.WcfServiceUri;

            //var a = ConfigHelper.DefaultMapWidth;
            //var b = ConfigHelper.DefaultMapHeight;

            //var logger = new Logger("log");
            //var client = new GameWcfClient("http://localhost:8080/IChatService4");

            //client.GetCurrentMap();

            var f = KeyState.Down | KeyState.Left | KeyState.Up | KeyState.Right | KeyState.IsAttack;

            var str = f.ToString();

            //if ((f & KeyState.Down) == KeyState.Down)
            //{

            //}

            //if ((f & KeyState.Left) == KeyState.Left)
            //{

            //}

            //if ((f & KeyState.Right) == KeyState.Right)
            //{

            //}


            var client = new ArenaWcfClient(ConfigHelper.ArenaWcfServiceUri);

            var userId = client.AddUser("ak test");

            client.Move(userId, KeyState.Down | KeyState.Left);

            var users = client.GetUsers();


        }
    }
}
