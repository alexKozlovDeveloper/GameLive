using System.Collections.Generic;
using System.ServiceModel;
using Arena.Core.Enums;
using Arena.Core.Map.Entityes;
using Arena.Core.ServiceEntityes;

namespace Arena.WcfService.Interfaces
{
    [ServiceContract]
    public interface IArenaWcfService
    {
        [OperationContract]
        void Move(string userId, KeyState keyState);

        [OperationContract]
        string AddUser(string name);

        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        List<Bullet> GetBullets();

        [OperationContract]
        List<Block> GetBlocks();
    }
}
