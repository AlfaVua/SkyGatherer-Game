using System;
using Components.Component;
using TMPro;
using UnityEngine;

namespace UI.Components
{
    public class HealthProgressBar : MonoBehaviour
    {
        [SerializeField] private HealthComponent target;
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI healthInfoText;

        private void Start()
        {
            progressBar.SetMaxValue(target.CurrentHealth);
            progressBar.SetValue(target.CurrentHealth);
            healthInfoText.text = progressBar.ConvertToText();
        }

        private void UpdateValue(float newValue)
        {
            progressBar.SetValue(newValue, true);
            healthInfoText.text = progressBar.ConvertToText();
        }

        private void UpdateMaxValue(float newValue)
        {
            progressBar.SetMaxValue(newValue);
            healthInfoText.text = progressBar.ConvertToText();
        }
        
        private void OnEnable()
        {
            target.OnHealthChanged.AddListener(UpdateValue);
            target.OnMaxHealthChanged.AddListener(UpdateMaxValue);
        }
        
        private void OnDisable()
        {
            target.OnHealthChanged.RemoveListener(UpdateValue);
            target.OnMaxHealthChanged.RemoveListener(UpdateMaxValue);
        }
    }
}