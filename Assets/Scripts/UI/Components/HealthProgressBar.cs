using System;
using Components.Component;
using UnityEngine;

namespace UI.Components
{
    public class HealthProgressBar : MonoBehaviour
    {
        [SerializeField] private HealthComponent target;
        [SerializeField] private ProgressBar progressBar;

        private void Start()
        {
            progressBar.SetMaxValue(target.CurrentHealth);
            progressBar.SetValue(target.CurrentHealth);
        }

        private void UpdateValue(float newValue)
        {
            progressBar.SetValue(newValue, true);
        }

        private void UpdateMaxValue(float newValue)
        {
            progressBar.SetMaxValue(newValue);
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