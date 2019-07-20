using System;
using System.Collections.Generic;

namespace GameLive.Core.MapEntityes
{
    public class MapFactory
    {
        private readonly Random _rnd = new Random();

        public Map GetRandomMap(int width, int height)
        {
            var map = new Map();

            for (int x = 0; x < width; x++)
            {
                var cellsRow = new List<MapCell>();

                for (int y = 0; y < height; y++)
                {
                    var rndStatus = _rnd.Next(2);

                    var mapCell = new MapCell()
                    {
                        X = x,
                        Y = y,
                        Age = 0,
                        Status = (CellStatus)rndStatus
                    };

                    cellsRow.Add(mapCell);
                }

                map.Cells.Add(cellsRow);
            }

            return map;
        }
    }
}
