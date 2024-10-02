using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUI inventoryUI;

        private UIBase _activeUI;

        public void Init()
        {
            UISignal.ToggleInventory.AddListener(ToggleInventory);
        }

        private void ToggleUI(UIBase ui)
        {
            if (_activeUI) _activeUI.Hide();
            if (ui == _activeUI)
            {
                _activeUI = null;
                UISignal.OnOverlayUIChanged.Invoke(false);
                return;
            }
            var isFirstOpened = !_activeUI;
            ui.Show();
            _activeUI = ui;
            if (isFirstOpened)
                UISignal.OnOverlayUIChanged.Invoke(true);
        }

        private void ToggleInventory()
        {
            ToggleUI(inventoryUI);
        }

        private void OnDestroy()
        {
            UISignal.ToggleInventory.RemoveListener(ToggleInventory);
        }
    }
}
