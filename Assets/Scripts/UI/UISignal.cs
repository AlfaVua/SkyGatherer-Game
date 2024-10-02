using UnityEngine.Events;

namespace UI
{
    public static class UISignal
    {
        public static readonly UnityEvent<bool> OnOverlayUIChanged = new UnityEvent<bool>();
        public static readonly UnityEvent ToggleInventory = new UnityEvent();
        public static readonly UnityEvent ToggleLevelUp = new UnityEvent();

        public static void Clear()
        {
            OnOverlayUIChanged.RemoveAllListeners();
            ToggleInventory.RemoveAllListeners();
            ToggleLevelUp.RemoveAllListeners();
        }
    }
}