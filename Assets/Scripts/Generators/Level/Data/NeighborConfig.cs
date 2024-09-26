using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generators.Level.Data
{
    [CreateAssetMenu(menuName = "Level/Neighbor Config")]
    public class NeighborConfig : ScriptableObject
    {
        public NeighboursData left;
        public NeighboursData right;
        public NeighboursData top;
        public NeighboursData bottom;
    }

    [Serializable]
    public class NeighboursData
    {
        public List<int> customNeighbourIds;
        public bool enabled;
    }
}