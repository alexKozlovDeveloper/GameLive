using GameLive.Core.Arena;

namespace Arena.Core.Interfaces
{
    public interface IInteractable
    {
        Position Position { get; set; }
        bool IsIntersect(IInteractable obj);
        void Intersect(IInteractable intersectedObject);
    }
}
