using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Arena;

namespace GameLive.Core.WcfService.Interfaces
{
    [ServiceContract]
    public interface IArenaWcfService
    {
        [OperationContract]
        void Move(string userId, KeyState keyState);

        [OperationContract]
        string AddUser(string name);

        [OperationContract]
        List<UserInfo> GetUsers();

        [OperationContract]
        List<Bullet> GetBullets();
    }
}
