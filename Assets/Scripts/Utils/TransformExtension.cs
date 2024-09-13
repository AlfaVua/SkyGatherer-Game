using UnityEngine;

namespace Utils.TransformExtenstion
{
    public static class TransformExtenstion
    {
        public static void ClearChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}