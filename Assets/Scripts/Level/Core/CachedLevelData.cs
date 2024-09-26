using System.Collections.Generic;

namespace Level.Core
{
    public struct CachedLevelData
    {
        public List<CachedLevelObjectData> Objects;
        public string Name;
        public int LevelID;
    }
}