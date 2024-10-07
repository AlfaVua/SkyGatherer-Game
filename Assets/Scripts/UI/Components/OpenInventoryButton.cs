using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class OpenInventoryButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void OnClick()
        {
            UISignal.ToggleInventory.Invoke();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}