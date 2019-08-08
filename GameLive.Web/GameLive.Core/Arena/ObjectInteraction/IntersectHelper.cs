using System;

namespace GameLive.Core.Arena.ObjectInteraction
{
    public static class IntersectHelper
    {
        public static bool IsIntersect(this Position pos1, Position pos2)
        {
            var x = Math.Abs(pos2.X - pos1.X);
            var y = Math.Abs(pos2.Y - pos1.Y);

            var distance = Math.Sqrt(x * x + y * y);

            var intersectDistance = pos1.Radius + pos2.Radius;

            if (distance > intersectDistance)
            {
                return false;
            }
            
            return true;
        }
    }
}
