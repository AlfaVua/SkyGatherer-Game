using UnityEngine;

namespace Level.Core
{
    [CreateAssetMenu(menuName = "Level/Sides")]
    public class LevelSidesData : ScriptableObject
    {
        public bool leftExit;
        public bool rightExit;
        public bool bottomExit;
        public bool topExit;
    }
}