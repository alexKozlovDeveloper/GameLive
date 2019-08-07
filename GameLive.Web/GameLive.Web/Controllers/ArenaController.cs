using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameLive.Core.Arena;
using GameLive.Core.Configuration;
using GameLive.Core.WcfService.Client;
using GameLive.Core.WcfService.Server;
using Newtonsoft.Json;

namespace GameLive.Web.Controllers
{
    public class ArenaController : Controller
    {
        // GET: Arena
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string AddUser(string name)
        {
            var client = new ArenaWcfClient(ConfigHelper.ArenaWcfServiceUri);

            return client.AddUser(name);
        }

        [HttpPost]
        public void Move(string userId, string keyStateString)
        {
            var client = new ArenaWcfClient(ConfigHelper.ArenaWcfServiceUri);

            Enum.TryParse(keyStateString, out KeyState keyState);
            
            client.Move(userId, keyState);
        }

        [HttpPost]
        public string GetUsers()
        {
            var client = new ArenaWcfClient(ConfigHelper.ArenaWcfServiceUri);

            var users = client.GetUsers();

            return JsonConvert.SerializeObject(users);
        }

        [HttpPost]
        public string GetBullets()
        {
            var client = new ArenaWcfClient(ConfigHelper.ArenaWcfServiceUri);

            var bullets = client.GetBullets();

            return JsonConvert.SerializeObject(bullets);
        }
    }
}