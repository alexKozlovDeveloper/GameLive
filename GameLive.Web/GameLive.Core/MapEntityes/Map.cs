using System.Collections.Generic;
using System.Linq;

namespace GameLive.Core.MapEntityes
{
    public class Map
    {
        public List<List<MapCell>> Cells { get; set; }

        public int Width => Cells?.Count ?? 0;
        public int Height => Cells?.FirstOrDefault()?.Count ?? 0;

        public Map()
        {
            Cells = new List<List<MapCell>>();
        }

        public MapCell GetCell(int x, int y)
        {
            x = GetPossibleAddress(x, Width);
            y = GetPossibleAddress(y, Height);

            return Cells[x][y];
        }

        public IEnumerable<MapCell> GetNeighboringCells(MapCell cell)
        {
            var result = new List<MapCell>
            {
                GetCell(cell.X + 1, cell.Y + 1),
                GetCell(cell.X + 1, cell.Y),
                GetCell(cell.X + 1, cell.Y - 1),
                GetCell(cell.X, cell.Y - 1),
                GetCell(cell.X - 1, cell.Y - 1),
                GetCell(cell.X - 1, cell.Y),
                GetCell(cell.X - 1, cell.Y + 1),
                GetCell(cell.X, cell.Y + 1)
            };

            return result;
        }

        private int GetPossibleAddress(int index, int size)
        {
            while (index >= size)
            {
                index -= size;
            }

            while (index < 0)
            {
                index += size;
            }

            return index;
        }
    }
}
