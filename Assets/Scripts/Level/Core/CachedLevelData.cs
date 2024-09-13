using System;
using System.Collections.Generic;
using Level.Platforms;

namespace Level.Core
{
    [Serializable]
    public struct CachedLevelData
    {
        public List<CachedPlatformData> platforms;
        public string name;
        public int id;
    }
}