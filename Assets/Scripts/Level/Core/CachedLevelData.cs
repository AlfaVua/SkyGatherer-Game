using System;
using System.Collections.Generic;

namespace Level.Core
{
    [Serializable]
    public struct CachedLevelData
    {
        public List<CachedLevelObjectData> Objects;
        public string name;
        public int sessionID;
    }
}