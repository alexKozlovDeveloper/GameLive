using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.WcfService
{
    public class GameWcfClient
    {
        private readonly Uri _address;

        public GameWcfClient(string addressUri)
        {
            _address = new Uri(addressUri);
        }

        public string GetCurrentMap()
        {
            BasicHttpBinding binding = new BasicHttpBinding(); // Привязка
            binding.MaxBufferSize = 20_000_000;
            binding.MaxBufferPoolSize = 20_000_000;
            binding.MaxReceivedMessageSize = 20_000_000;
            // Создаём конечную точку.
            EndpointAddress endpoint = new EndpointAddress(_address);
            // Создаём фабрику каналов.
            ChannelFactory<IGameWcfService> channelFactory = new ChannelFactory<IGameWcfService>(binding, endpoint);
            // Создаём канал
            IGameWcfService channel = channelFactory.CreateChannel();
            // Отправляем сообщение
            var res = channel.GetCurrentMap("From client");

            //Console.WriteLine($"from server '{res}'");
            // После нажатия клавиши клиент завершит свою работу
            //Console.ReadKey();
            return res;
        }
    }
}
