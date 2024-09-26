using Level.Core;
using UnityEngine;

namespace Generators.Level.Data
{
    [CreateAssetMenu(menuName = "Level/Data")]
    public class LevelData : ScriptableObject
    {
        [HideInInspector] public int id;
        [SerializeField] private LevelBase prefab;
        [SerializeField] private NeighborConfig neighbors;

        public bool IsLeftUnique => Left.customNeighbourIds.Count != 0;
        public bool IsRightUnique => Right.customNeighbourIds.Count != 0;
        public bool IsTopUnique => Top.customNeighbourIds.Count != 0;
        public bool IsBottomUnique => Bottom.customNeighbourIds.Count != 0;

        public NeighboursData Left => neighbors.left;
        public NeighboursData Right => neighbors.right;
        public NeighboursData Top => neighbors.top;
        public NeighboursData Bottom => neighbors.bottom;

        public LevelBase GetNewInstance()
        {
            var level = Instantiate(prefab);
            level.staticData = this;
            return level;
        }
    }
}