using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameLive.Core.MapEntityes;
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
            var xSize = int.Parse(x);
            var ySize = int.Parse(y);

            var mapFactory = new MapFactory();
            var map = mapFactory.GetRandomMap(xSize, ySize);
            return JsonConvert.SerializeObject(map);
        }
    }
}