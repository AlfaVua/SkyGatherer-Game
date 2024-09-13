using System.Collections.Generic;
using System.Linq;
using Level.Core;
using UnityEngine;

namespace Generators
{
    using Utils.TransformExtenstion;
    public class LevelGenerator
    {
        private readonly Dictionary<string, LevelBase> _activeLevels;
        private readonly LevelGeneratorData _static;
        private readonly Transform _container;

        public LevelGenerator(Transform container, LevelGeneratorData behavior)
        {
            _container = container;
            _activeLevels = new Dictionary<string, LevelBase>();
            _static = behavior;
            behavior.Init();
        }

        private LevelBase InitLevel(LevelBase prefab, int indexX, uint indexY)
        {
            var level = Object.Instantiate(prefab, _container);
            level.Init(indexX, indexY);
            return level;
        }

        public LevelBase GenerateLevel(int indexX, uint indexY, LevelType type)
        {
            var key = LevelCache.GetKey(indexX, indexY);
            if (_activeLevels.TryGetValue(key, out var level)) return level;

            level = InitLevel(_static.GetRandom(indexY == 0, type), indexX, indexY);
            _activeLevels.Add(key, level);
            return level;
        }

        public LevelBase GenerateLevel(int indexX, uint indexY, CachedLevelData data)
        {
            var key = LevelCache.GetKey(indexX, indexY);
            if (_activeLevels.TryGetValue(key, out var level)) return level;
            
            level = InitLevel(_static.GetById(data.id), indexX, indexY);
            _activeLevels.Add(key, level);
            level.UpdateData(data);
            return level;
        }

        public void RemoveLevel(LevelBase level)
        {
            _activeLevels.Remove(level.Name);
        }

        public List<LevelBase> GetRemovableLevels(int indexX, uint indexY)
        {
            return _activeLevels.Values.Where(level => Mathf.Sqrt(Mathf.Pow(indexX - level.IndexX, 2) + Mathf.Pow(indexY - level.IndexY, 2)) > 1).ToList();
        }

        public void Clear()
        {
            _container.ClearChildren();
            _activeLevels.Clear();
            _static.Clear();
        }
    }
}