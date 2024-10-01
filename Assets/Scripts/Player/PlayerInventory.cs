using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : ScriptableObject
    {
        private Dictionary<uint, int> _resources;

        public void Init()
        {
            _resources = new Dictionary<uint, int>();
        }

        public void AddResource(uint id, int amount = 1)
        {
            _resources.TryAdd(id, 0);
            _resources[id] += amount;
        }

        public Dictionary<uint, int> GetResources()
        {
            return _resources;
        }
    }
}