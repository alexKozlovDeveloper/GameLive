using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.WcfService
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
