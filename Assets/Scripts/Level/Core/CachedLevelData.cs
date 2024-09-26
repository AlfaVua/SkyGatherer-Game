using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Level.Core
{
    [Serializable]
    public struct CachedLevelData
    {
        public List<CachedLevelObjectData> Objects;
        public string name;
        [FormerlySerializedAs("sessionID")] public int levelID;
    }
}