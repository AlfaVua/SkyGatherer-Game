using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : UIBase
    {
        [SerializeField] private SettingsObject settingsObject;
        [SerializeField] private Slider bloomSlider;
        [SerializeField] private Toggle postProcessingToggle;
        [SerializeField] private Slider masterVolumeSlider;

        protected override void UpdateView()
        {
            postProcessingToggle.isOn = settingsObject.postProcessing;
            bloomSlider.value = settingsObject.Bloom;
            masterVolumeSlider.value = settingsObject.masterVolume;
        }

        private void UpdateBloom(float value)
        {
            settingsObject.Bloom = value;
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
            settingsObject.Save();
        }
    }
}