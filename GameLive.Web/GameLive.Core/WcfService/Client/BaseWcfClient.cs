using System;
using System.ServiceModel;

namespace GameLive.Core.WcfService.Client
{
    public abstract class BaseWcfClient<T>
    {
        public T GetChannel(Uri address)
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                MaxBufferSize = 20_000_000,
                MaxBufferPoolSize = 20_000_000,
                MaxReceivedMessageSize = 20_000_000
            };

            // Создаём конечную точку.
            EndpointAddress endpoint = new EndpointAddress(address);

            // Создаём фабрику каналов.
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(binding, endpoint);

            // Создаём канал
            T channel = channelFactory.CreateChannel();

            return channel;
        }
    }
}
