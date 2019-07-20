using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.WindowsService
{
    public interface IServiceComponent
    {
        void Start();
        void Stop();
        void NextTick();
    }
}
