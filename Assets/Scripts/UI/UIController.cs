using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryUI inventoryUI;
        [SerializeField] private LevelUpUI levelUpUI;
        [SerializeField] private SettingsUI settingsUI;
        [SerializeField] private LoseUI loseUI;

        private UIBase _activeUI;

        public void Init()
        {
            UISignal.ToggleInventory.AddListener(ToggleInventory);
            UISignal.ToggleLevelUp.AddListener(ToggleLevelUp);
            UISignal.OnEscPressed.AddListener(ToggleActiveUIOrSettings);
            UISignal.ToggleLoseUI.AddListener(ToggleLoseUI);
        }

        private void ToggleUI(UIBase ui)
        {
            if (_activeUI && !_activeUI.CanCloseByOthers && ui != _activeUI) return;
            if (_activeUI) _activeUI.Hide();
            if (!ui || ui == _activeUI)
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

        private void ToggleActiveUIOrSettings()
        {
            ToggleUI(_activeUI ? null : settingsUI);
        }
        
        private void ToggleLoseUI(LoseReason reason)
        {
            loseUI.reason = reason;
            ToggleUI(loseUI);
        }

        private void OnDestroy()
        {
            UISignal.ToggleInventory.RemoveListener(ToggleInventory);
            UISignal.ToggleLevelUp.RemoveListener(ToggleLevelUp);
            UISignal.OnEscPressed.RemoveListener(ToggleActiveUIOrSettings);
            UISignal.ToggleLoseUI.RemoveListener(ToggleLoseUI);
        }
    }
}
