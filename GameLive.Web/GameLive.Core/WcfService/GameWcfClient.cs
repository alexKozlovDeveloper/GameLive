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

        public void Connect()
        {
            BasicHttpBinding binding = new BasicHttpBinding(); // Привязка
            // Создаём конечную точку.
            EndpointAddress endpoint = new EndpointAddress(_address);
            // Создаём фабрику каналов.
            ChannelFactory<IGameWcfService> channelFactory = new ChannelFactory<IGameWcfService>(binding, endpoint);
            // Создаём канал
            IGameWcfService channel = channelFactory.CreateChannel();
            // Отправляем сообщение
           var res = channel.GetCurrentMap("Send message using WCF!!!");

            Console.WriteLine($"from server '{res}'");
            // После нажатия клавиши клиент завершит свою работу
            Console.ReadKey();
        }
    }
}
