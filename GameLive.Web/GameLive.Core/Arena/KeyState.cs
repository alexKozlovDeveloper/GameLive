using System;

namespace GameLive.Core.Arena
{
    [Flags]
    public enum KeyState : byte
    {
        Up = 1,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3,
        IsAttack = 1 << 4,
        ClockwiseRotation = 1 << 5,        // Rotation to right (increment)
        CounterclockwiseRotation = 1 << 6  // Rotation to left  (decrement)
    }
}
