using System.Collections.Generic;
using System.Linq;
using Generators.Level.Data;
using Level.Core;
using UnityEngine;
using Utils.ListExtension;

namespace Generators.Level
{
    public enum LevelSide
    {
        Left, Right, Top, Bottom
    }

    public static class OppositeSide
    {
        public static LevelSide Get(LevelSide oppositeTo)
        {
            return oppositeTo switch
            {
                LevelSide.Bottom => LevelSide.Top,
                LevelSide.Left => LevelSide.Right,
                LevelSide.Top => LevelSide.Bottom,
                _ => LevelSide.Left
            };
        }
    }

    public class LevelGenerator
    {
        private readonly Dictionary<string, LevelBase> _activeLevels;
        private readonly LevelCache _cache;

        private readonly LevelDataManager _dataManager;

        public LevelGenerator(LevelDataManager dataManager)
        {
            _cache = new LevelCache();
            _activeLevels = new Dictionary<string, LevelBase>();
            _dataManager = dataManager;
        }

        public LevelBase GetLevel(int indexX, uint indexY)
        {
            return GetGeneratedLevel(indexX, indexY) ?? CreateAvailableLevelAt(indexX, indexY); 
        }

        private LevelBase GetGeneratedLevel(int indexX, uint indexY)
        {
            if (IsCoordsActive(indexX, indexY)) return GetActiveLevelAt(indexX, indexY);
            return IsCached(indexX, indexY) ? GetCachedLevelAndUpdate(indexX, indexY) : null;
        }

        private LevelBase GetCachedLevelAndUpdate(int indexX, uint indexY)
        {
            var levelData = GetCachedLevelData(indexX, indexY);
            var level = GetLevelDataById(levelData.LevelID).GetNewInstance();
            level.Init(indexX, indexY, levelData);
            _activeLevels.Add(level.Name, level);
            return level;
        }

        private bool IsCoordsActive(int indexX, uint indexY)
        {
            return _activeLevels.ContainsKey(LevelCache.GetKey(indexX, indexY));
        }
        
        private LevelBase GetActiveLevelAt(int indexX, uint indexY)
        {
            return _activeLevels[LevelCache.GetKey(indexX, indexY)];
        }

        private bool IsCached(int indexX, uint indexY)
        {
            return _cache.HasLevel(indexX, indexY);
        }
        
        private CachedLevelData GetCachedLevelData(int indexX, uint indexY)
        {
            return _cache.GetLevelData(indexX, indexY);
        }

        private LevelData GetLevelDataById(int id)
        {
            return _dataManager.GetLevelData(id);
        }

        private LevelBase CreateAvailableLevelAt(int indexX, uint indexY)
        {
            var neighbors = GetCoordNeighbors(indexX, indexY);
            var foundNeighbors = _dataManager.FindLevelsWithMatchingNeighbors(neighbors, indexY == 0);
            if (foundNeighbors.Count == 0) return null;
            var level = _dataManager.GetLevelData(foundNeighbors.GetRandom()).GetNewInstance();
            level.Init(indexX, indexY);
            _activeLevels.Add(level.Name, level);
            return level;
        }

        private Dictionary<LevelSide, LevelBase> GetCoordNeighbors(int indexX, uint indexY)
        {
            return new Dictionary<LevelSide, LevelBase>
            {
                { LevelSide.Left, GetGeneratedLevel(indexX - 1, indexY) },
                { LevelSide.Right, GetGeneratedLevel(indexX + 1, indexY) },
                { LevelSide.Top, GetGeneratedLevel(indexX, indexY + 1) },
                { LevelSide.Bottom, indexY == 0 ? null : GetGeneratedLevel(indexX, indexY - 1) }
            };
        }

        private void RemoveLevel(LevelBase level)
        {
            _activeLevels.Remove(level.Name);
            Object.Destroy(level.gameObject);
            _cache.SaveLevel(level);
        }

        public void RemoveFarLevels(int indexX, uint indexY)
        {
            _activeLevels.Values.Where(level => Mathf.Abs(indexX - level.IndexX) + Mathf.Abs((int)indexY - level.IndexY) > 1).ToList().ForEach(RemoveLevel);
        }   

        public void AddStartingLevel(LevelBase level)
        {
            _activeLevels.Add(level.Name, level);
        }
    }
}