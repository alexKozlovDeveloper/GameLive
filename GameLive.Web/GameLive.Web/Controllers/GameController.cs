using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;
using GameLive.Core.MapEntityes;
using GameLive.Core.WcfService;
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
            //var xSize = int.Parse(x);
            //var ySize = int.Parse(y);

            //var mapFactory = new MapFactory();
            //var map = mapFactory.GetRandomMap(xSize, ySize);

            //TestFuncDepracate();

            //MapSaver.Save(map, @"C:\Schneider Electric\GameLive\Service\ActualMap.txt");



            //var map = MapSaver.Load(@"C:\Schneider Electric\GameLive\Service\ActualMap.txt");

            //return JsonConvert.SerializeObject(map);


            var client = new GameWcfClient("http://localhost:8080/IChatService4");

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
            //mapController.NextTic();
            //MapSaver.Save(mapController.Map, @"C:\Schneider Electric\GameLive\Service\map2.txt");
        }
    }
}