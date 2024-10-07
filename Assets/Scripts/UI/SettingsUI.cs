using Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : UIBase
    {
        [SerializeField] private SettingsObject settingsObject;
        [SerializeField] private Slider bloomSlider;
        [SerializeField] private Toggle postProcessingToggle;
        [SerializeField] private VolumeProfile volumeProfile;
        [SerializeField] private Slider masterVolumeSlider;

        private Bloom _bloomVolume;

        protected override void UpdateView()
        {
            if (!_bloomVolume)
            {
                volumeProfile.TryGet(out _bloomVolume);
            }

            postProcessingToggle.isOn = settingsObject.postProcessing;
            bloomSlider.value = _bloomVolume.intensity.value;
            masterVolumeSlider.value = settingsObject.masterVolume;
        }

        private void UpdateBloom(float value)
        {
            _bloomVolume.intensity.value = value;
        }

        private void UpdatePostProcessing(bool value)
        {
            settingsObject.postProcessing = value;
            settingsObject.RefreshSignal.Invoke();
        }

        private void UpdateMasterVolume(float value)
        {
            settingsObject.masterVolume = value;
            AudioListener.volume = value;
        }

        private void OnEnable()
        {
            bloomSlider.onValueChanged.AddListener(UpdateBloom);
            postProcessingToggle.onValueChanged.AddListener(UpdatePostProcessing);
            masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
        }

        private void OnDisable()
        {
            bloomSlider.onValueChanged.RemoveListener(UpdateBloom);
            postProcessingToggle.onValueChanged.RemoveListener(UpdatePostProcessing);
            masterVolumeSlider.onValueChanged.RemoveListener(UpdateMasterVolume);
        }
    }
}