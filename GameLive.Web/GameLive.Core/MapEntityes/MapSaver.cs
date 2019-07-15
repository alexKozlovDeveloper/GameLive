using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLive.Core.MapEntityes
{
    public static class MapSaver
    {
        public static void Save(Map map, string filePath)
        {
            var lines = new List<string>();

            for (int y = 0; y < map.Height; y++)
            {
                var str = "";

                for (int x = 0; x < map.Width; x++)
                {
                    str += map.Cells[x][y].Status == CellStatus.Alive ? 1 : 0;
                }

                lines.Add(str);
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
