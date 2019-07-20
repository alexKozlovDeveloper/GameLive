using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLive.Core.Interfaces;

namespace GameLive.Core
{
    public class BaseLoggerObject
    {
        public ILogger Logger { get; }

        public BaseLoggerObject(ILogger logger)
        {
            Logger = logger;
        }
    }
}
