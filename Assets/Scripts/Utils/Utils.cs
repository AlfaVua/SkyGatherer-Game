using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static bool IsPlayer(GameObject obj)
        {
            return obj.tag.Equals("Player");
        }

        public static float IndexToX => 17.8f;

        public static float IndexToY => 10f;
    }
}