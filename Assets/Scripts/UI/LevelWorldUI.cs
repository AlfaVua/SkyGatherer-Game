using UI.Components;
using UnityEngine;

namespace UI
{
    public class LevelWorldUI : MonoBehaviour
    {
        private static LevelWorldUI _instance;
        [SerializeField] private FloatingText prefab;

        private void Awake()
        {
            _instance = this;
        }

        public static void CreateFloatingText(Vector3 position, Color color, string text)
        {
            var floatingText = Instantiate(_instance.prefab, _instance.transform);
            floatingText.transform.position = position;
            floatingText.Init(color, text);
        }
    }
}