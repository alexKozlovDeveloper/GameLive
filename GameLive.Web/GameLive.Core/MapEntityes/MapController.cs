using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameLive.Core.MapEntityes
{
    public class MapController
    {
        private readonly object _lock = new object();

        public Map Map { get; private set; }

        public MapController(Map map)
        {
            Map = map;
        }

        public void NextTick()
        {
            lock (_lock)
            {
                var newCells = new List<List<MapCell>>();

                foreach (var row in Map.Cells)
                {
                    var newRow = new List<MapCell>();

                    foreach (var mapCell in row)
                    {
                        var neighboringCell = Map.GetNeighboringCells(mapCell);

                        var aliveCount = GetAliveCount(neighboringCell);

                        var newCell = new MapCell
                        {
                            X = mapCell.X,
                            Y = mapCell.Y
                        };

                        if (mapCell.Status == CellStatus.Dead && aliveCount == 3)
                        {
                            newCell.Age = 0;
                            newCell.Status = CellStatus.Alive;
                        }
                        else if (mapCell.Status == CellStatus.Alive && (aliveCount == 2 || aliveCount == 3))
                        {
                            newCell.Age = mapCell.Age + 1;
                            newCell.Status = CellStatus.Alive;
                        }
                        else
                        {
                            newCell.Age = 0;
                            newCell.Status = CellStatus.Dead;
                        }

                        newRow.Add(newCell);
                    }

                    newCells.Add(newRow);
                }

                Map.Cells = newCells;
            }
        }

        //TODO: remove this method to another class
        public string GetMapAsJson()
        {
            string result;

            lock (_lock)
            {
                result = JsonConvert.SerializeObject(Map);
            }

            return result;
        }

        public void ResetMap(Map newMap)
        {
            lock (_lock)
            {
                Map = newMap;
            }
        }

        private int GetAliveCount(IEnumerable<MapCell> cells)
        {
            var count = 0;

            foreach (var mapCell in cells)
            {
                if (mapCell.Status == CellStatus.Alive)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
