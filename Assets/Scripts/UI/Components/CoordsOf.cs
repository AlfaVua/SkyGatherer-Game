using TMPro;
using UnityEngine;

namespace UI.Components
{
    public class CoordsOf : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private TextMeshProUGUI text;


        private void Start()
        {
            text.text = ConvertToCoords();
        }

        private void Update()
        {
            if (target.gameObject.isStatic) return;
            text.text = ConvertToCoords();
        }

        private string ConvertToCoords()
        {
            return Mathf.Floor(target.position.x) + ":" + Mathf.Floor(target.position.y);
        }
    }
}