using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUI inventoryUI;
        [SerializeField] private LevelUpUI levelUpUI;
        [SerializeField] private SettingsUI settingsUI;

        private UIBase _activeUI;

        public void Init()
        {
            UISignal.ToggleInventory.AddListener(ToggleInventory);
            UISignal.ToggleLevelUp.AddListener(ToggleLevelUp);
            UISignal.ToggleSettings.AddListener(TogglePause);
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

        private void ToggleLevelUp()
        {
            ToggleUI(levelUpUI);
        }

        private void TogglePause()
        {
            ToggleUI(settingsUI);
        }

        private void OnDestroy()
        {
            UISignal.ToggleInventory.RemoveListener(ToggleInventory);
            UISignal.ToggleLevelUp.RemoveListener(ToggleLevelUp);
            UISignal.ToggleSettings.RemoveListener(TogglePause);
        }
    }
}
