using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Settings
{
    public class SettingsObject : ScriptableObject
    {
        [SerializeField] private VolumeProfile volumeProfile;
        public readonly UnityEvent RefreshSignal = new UnityEvent();
        [HideInInspector] public bool postProcessing = true;
        [HideInInspector] public float masterVolume = 1;

        private Bloom _bloomVolume;

        private void Awake()
        {
            postProcessing = !PlayerPrefs.HasKey("PostProcessing") || PlayerPrefs.GetInt( "PostProcessing") == 1;
            GetBloom().intensity.value = PlayerPrefs.HasKey("Bloom") ? PlayerPrefs.GetFloat("Bloom") : 1.2f;
            masterVolume = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : 1f;
            AudioListener.volume = masterVolume;
        }
        
        private Bloom GetBloom() {
            if (!_bloomVolume)
            {
                volumeProfile.TryGet(out _bloomVolume);
            }

            return _bloomVolume;
        }

        public float Bloom
        {
            get => GetBloom().intensity.value;
            set
            {
                _bloomVolume.intensity.value = value;
                PlayerPrefs.SetFloat("Bloom", value);
            }
        }

        public void Save()
        {
            PlayerPrefs.SetInt("PostProcessing", postProcessing ? 1 : 0);
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("Bloom", GetBloom().intensity.value);
            PlayerPrefs.Save();
        }
    }
}