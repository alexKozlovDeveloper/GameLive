using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.Arena.Interfaces
{
    public interface IInteractable
    {
        Position Position { get; set; }
        bool IsIntersect(IInteractable obj);
    }
}
