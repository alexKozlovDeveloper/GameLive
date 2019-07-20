namespace GameLive.Core.MapEntityes
{
    public class MapCell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public CellStatus Status { get; set; }

        public int Age { get; set; }
    }
}
