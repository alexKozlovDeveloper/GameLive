using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;
using GameLive.Core.Configuration;
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

        [HttpPost]
        public string GetCurrentMapState()
        {
            var client = new GameWcfClient(ConfigHelper.WcfServiceUri);

            return client.GetCurrentMap();
        }

        [HttpPost]
        public string ResetMap(string x, string y)
        {
            var xSize = int.Parse(x);
            var ySize = int.Parse(y);

            var client = new GameWcfClient(ConfigHelper.WcfServiceUri);

            client.ResetMap(xSize, ySize);

            return client.GetCurrentMap();
        }
    }
}