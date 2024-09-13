using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static bool IsPlayer(GameObject obj)
        {
            return obj.tag.Equals("Player");
        }
    }
}