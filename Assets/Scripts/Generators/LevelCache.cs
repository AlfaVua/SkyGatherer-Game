using System.Collections.Generic;
using Level.Core;

namespace Generators
{
    public class LevelCache
    {
        private Dictionary<string, CachedLevelData> _cache = new();

        public static string GetKey(int indexX, uint indexY)
        {
            return indexX + "_" + indexY;
        }

        public bool HasLevel(int indexX, uint indexY)
        {
            return _cache.ContainsKey(GetKey(indexX, indexY));
        }

        private bool HasLevel(LevelBase level)
        {
            return _cache.ContainsKey(level.Name);
        }

        public CachedLevelData GetLevelData(int indexX, uint indexY)
        {
            return _cache[GetKey(indexX, indexY)];
        }

        public void SaveLevel(LevelBase level)
        {
            if (HasLevel(level)) _cache[level.Name] = level.CachedData;
            else _cache.Add(level.Name, level.CachedData);
        }

        public void ClearCache()
        {
            _cache.Clear();
        }
    }
}