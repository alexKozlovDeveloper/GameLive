using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.MapEntityes
{
    public class Map
    {
        public List<List<MapCell>> Cells { get; set; }

        public Map()
        {
            Cells = new List<List<MapCell>>();
        }
    }
}
