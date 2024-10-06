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

        private Bloom _bloomVolume;

        protected override void UpdateView()
        {
            if (!_bloomVolume)
            {
                volumeProfile.TryGet(out _bloomVolume);
            }

            postProcessingToggle.isOn = settingsObject.postProcessing;
            bloomSlider.value = _bloomVolume.intensity.value;
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

        private void OnEnable()
        {
            bloomSlider.onValueChanged.AddListener(UpdateBloom);
            postProcessingToggle.onValueChanged.AddListener(UpdatePostProcessing);
        }

        private void OnDisable()
        {
            bloomSlider.onValueChanged.RemoveListener(UpdateBloom);
            postProcessingToggle.onValueChanged.RemoveListener(UpdatePostProcessing);
        }
    }
}