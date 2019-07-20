using System.ServiceModel;

namespace GameLive.Core.WcfService.Interfaces
{
    [ServiceContract]
    public interface IGameWcfService
    {
        [OperationContract]
        string GetCurrentMap();

        [OperationContract]
        void ResetMap(int width, int height);
    }
}
