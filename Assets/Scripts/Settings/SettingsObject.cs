using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class SettingsObject : ScriptableObject
    {
        public readonly UnityEvent RefreshSignal = new UnityEvent();
        public bool postProcessing = true;
    }
}