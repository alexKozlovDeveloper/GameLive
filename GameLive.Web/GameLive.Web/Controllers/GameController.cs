using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService;
using GameLive.Core.WcfService.Client;
using Newtonsoft.Json;

namespace GameLive.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetCurrentMapState()
        {
            var mapFactory = new MapFactory();
            var map = mapFactory.GetRandomMap(10, 10);
            return JsonConvert.SerializeObject(map);
        }

        [HttpPost]
        public string GetCurrentMapState(string x, string y)
        {

            var client = new GameWcfClient("http://localhost:8080/IChatService4");

            return client.GetCurrentMap();
        }

        [HttpPost]
        public string ResetMap(string x, string y)
        {
            var xSize = int.Parse(x);
            var ySize = int.Parse(y);

            var client = new GameWcfClient("http://localhost:8080/IChatService4");

            client.ResetMap(xSize, ySize);

            return client.GetCurrentMap();
        }

        private void TestFuncDepracate()
        {
            ServiceController myController = new ServiceController("GameService");
            if (myController.CanStop)
            {
                System.Diagnostics.Debug.WriteLine(myController.DisplayName + " can be stopped.");
                Console.WriteLine("can!");
            }
            else
            {

                System.Diagnostics.Debug.WriteLine(myController.DisplayName + " cannot stop.");
                Console.WriteLine("can't!");
            }

            

            //MapSaver.Save(map, @"C:\Schneider Electric\GameLive\Service\map1.txt");
            //var mapController = new MapController(map);
            //mapController.NextTick();
            //MapSaver.Save(mapController.Map, @"C:\Schneider Electric\GameLive\Service\map2.txt");
        }
    }
}