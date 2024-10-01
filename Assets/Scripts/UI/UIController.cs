using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUI inventoryUI;

        private UIBase _activeUI;

        private void ToggleUI(UIBase ui)
        {
            if (_activeUI) _activeUI.Hide();
            if (ui == _activeUI)
            {
                _activeUI = null;
                return;
            }
            ui.Show();
            _activeUI = ui;
        }

        private void ToggleInventory()
        {
            ToggleUI(inventoryUI);
        }

        private void OnEnable()
        {
            UISignal.ToggleInventory.AddListener(ToggleInventory);
        }

        private void OnDisable()
        {
            UISignal.ToggleInventory.RemoveListener(ToggleInventory);
        }
    }
}
