using TMPro;
using UnityEngine;

namespace UI.Components
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textView;
        [SerializeField] private float destroyAfter = 0.4f;
        public void Init(Color color, string text)
        {
            textView.color = color;
            textView.text = text;
            Destroy(gameObject, destroyAfter);
        }
    }
}