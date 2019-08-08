using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Arena;
using GameLive.Core.Arena.Enums;
using GameLive.Core.WcfService.Interfaces;

namespace GameLive.Core.WcfService.Client
{
    public class ArenaWcfClient : BaseWcfClient<IArenaWcfService>
    {
        private readonly Uri _address;

        public ArenaWcfClient(string addressUri)
        {
            _address = new Uri(addressUri);
        }

        public string AddUser(string name)
        {
            IArenaWcfService channel = GetChannel(_address);

            return channel.AddUser(name);
        }

        public void Move(string userId, KeyState keyState)
        {
            IArenaWcfService channel = GetChannel(_address);

            channel.Move(userId, keyState);
        }

        public List<UserInfo> GetUsers()
        {
            IArenaWcfService channel = GetChannel(_address);

            return channel.GetUsers();
        }

        public List<Bullet> GetBullets()
        {
            IArenaWcfService channel = GetChannel(_address);

            return channel.GetBullets();
        }
    }
}
