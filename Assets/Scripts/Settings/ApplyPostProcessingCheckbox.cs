using System;
using Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Settings
{
    public class ApplyPostProcessingCheckbox : MonoBehaviour
    {
        [SerializeField] private SettingsObject settings;
        [SerializeField] UniversalAdditionalCameraData properties;

        private void Awake()
        {
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            properties.renderPostProcessing = settings.postProcessing;
        }

        private void OnEnable()
        {
            settings.RefreshSignal.AddListener(UpdateProperties);
        }
        
        private void OnDisable()
        {
            settings.RefreshSignal.RemoveListener(UpdateProperties);
        }
    }
}