using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.WcfService
{
    [ServiceContract]
    public interface IGameWcfService
    {
        [OperationContract]
        string GetCurrentMap(string message);
    }
}
