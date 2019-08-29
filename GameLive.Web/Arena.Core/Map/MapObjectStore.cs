using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arena.Core.Enums;
using Arena.Core.Interfaces;
using Arena.Core.Map.Entityes;

namespace Arena.Core.Map
{
    public class MapObjectStore
    {
        public List<User> Users { get; private set; }

        public List<BaseMapObject> MapObjects { get; private set; }

        public List<IInteractable> Interactables { get; private set; }

        public MapObjectStore()
        {
            Users = new List<User>();

            MapObjects = new List<BaseMapObject>();
            Interactables = new List<IInteractable>();
        }

        public void NextTick()
        {
            foreach (var baseMapObject in MapObjects)
            {
                baseMapObject.NextTick();
            }

            for (var i = 0; i < Interactables.Count; i++)
            {
                for (var j = i + 1; j < Interactables.Count; j++)
                {
                    if (i == j) { continue; }

                    if (Interactables[i].IsIntersect(Interactables[j]))
                    {
                        Interactables[i].Intersect(Interactables[j]);
                        Interactables[j].Intersect(Interactables[i]);
                    }
                }
            }

            var objectToRemove = MapObjects.Where(a => a.ObjectState == MapObjectState.RemovalCandidate).ToList();

            foreach (var mapObject in objectToRemove)
            {
                RemoveMapObject(mapObject);
            }
        }

        public void AddMapObject(BaseMapObject obj)
        {
            MapObjects.Add(obj);

            if (obj is IInteractable interactable)
            {
                Interactables.Add(interactable);
            }

            if (obj is User user)
            {
                Users.Add(user);
            }
        }

        public void RemoveMapObject(BaseMapObject obj)
        {
            MapObjects.Remove(obj);

            if (obj is IInteractable interactable)
            {
                Interactables.Remove(interactable);
            }

            if (obj is User user)
            {
                Users.Remove(user);
            }
        }
    }
}
