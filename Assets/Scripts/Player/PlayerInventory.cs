using System;
using System.Collections.Generic;
using Generators.Resource;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInventory : ScriptableObject
    {
        public readonly UnityEvent<IReadOnlyCollection<ResourceData>, Vector3> OnCollectResources = new UnityEvent<IReadOnlyCollection<ResourceData>, Vector3>();
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