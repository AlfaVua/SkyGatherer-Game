using System.Collections.Generic;
using System.Linq;
using Level.Core;
using UnityEngine;
using Utils.TransformExtenstion;

namespace Generators.Level
{
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

        public LevelBase GetStartingLevel()
        {
            var level = InitLevel(_static.startingLevel, 0, 0);
            level.AfterFirstInit();
            _activeLevels.Add(level.Name, level);
            return level;
        }

        public LevelBase GetActiveLevelAt(int indexX, uint indexY)
        {
            return _activeLevels.TryGetValue(LevelCache.GetKey(indexX, indexY), out var level) ? level : null;
        }

        public LevelBase GetLevelAt(int indexX, uint indexY, LevelType type)
        {
            var level = InitLevel(_static.GetRandom(indexY == 0, type), indexX, indexY);
            level.AfterFirstInit();
            _activeLevels.Add(level.Name, level);
            return level;
        }

        public LevelBase GetLevelAt(int indexX, uint indexY, CachedLevelData data)
        {
            var level = InitLevel(_static.GetById(data.sessionID), indexX, indexY);
            _activeLevels.Add(level.Name, level);
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