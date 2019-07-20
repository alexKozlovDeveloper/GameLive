using System;
using GameLive.Core.WcfService.Interfaces;

namespace GameLive.Core.WcfService.Client
{
    public class GameWcfClient : BaseWcfClient<IGameWcfService>
    {
        private readonly Uri _address;

        public GameWcfClient(string addressUri)
        {
            _address = new Uri(addressUri);
        }

        public string GetCurrentMap()
        {
            IGameWcfService channel = GetChannel(_address);

            return channel.GetCurrentMap("From client");
        }

        public void ResetMap(int width, int heigth)
        {
            IGameWcfService channel = GetChannel(_address);

            channel.ResetMap(width, heigth);
        }
    }
}
