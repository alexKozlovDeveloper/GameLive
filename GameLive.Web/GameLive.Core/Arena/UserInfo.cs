using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.Arena
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Position Position { get; set; }
    }
}
