using System.Collections.Generic;
using UnityEngine;

namespace Generators.Level.Data
{
    [CreateAssetMenu(menuName = "Level/Levels List")]
    public class LevelsList : ScriptableObject
    {
        public List<LevelData> levels;
    }
}