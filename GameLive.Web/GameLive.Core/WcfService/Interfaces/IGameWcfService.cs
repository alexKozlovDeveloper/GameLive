using System.ServiceModel;

namespace GameLive.Core.WcfService.Interfaces
{
    [ServiceContract]
    public interface IGameWcfService
    {
        [OperationContract]
        string GetCurrentMap(string message);

        [OperationContract]
        void ResetMap(int width, int height);
    }
}
