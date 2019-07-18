using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.WcfService
{
    public class GameWcfService : IGameWcfService
    {
        public string GetCurrentMap(string message)
        {
            Console.WriteLine($"Сервер получил сообщение:  {message}");
            return "lol kek";
        }
    }
}
