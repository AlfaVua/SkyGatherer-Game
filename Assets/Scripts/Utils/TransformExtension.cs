using UnityEditor;
using UnityEngine;

namespace Utils.TransformExtenstion
{
    public static class TransformExtenstion
    {
        public static void ClearChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                
#if UNITY_EDITOR
                if (!EditorApplication.isPlaying)
                {
                    Object.DestroyImmediate(child.gameObject);
                    continue;
                }
#endif
                Object.Destroy(child.gameObject, 0);
            }
        }
    }
}