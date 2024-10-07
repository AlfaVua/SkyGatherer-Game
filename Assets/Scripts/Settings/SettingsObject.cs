using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class SettingsObject : ScriptableObject
    {
        public readonly UnityEvent RefreshSignal = new UnityEvent();
        public bool postProcessing = true;
        public float masterVolume = 1;

        private void Awake()
        {
            postProcessing = !PlayerPrefs.HasKey("PostProcessing") || PlayerPrefs.GetInt( "PostProcessing") == 1;
            masterVolume = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : 1f;
            AudioListener.volume = masterVolume;
        }
    }
}