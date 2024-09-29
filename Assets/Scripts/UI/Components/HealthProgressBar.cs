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
            progressBar.SetValue(target.CurrentHealth);
        }

        private void UpdateValue(float newValue)
        {
            progressBar.SetValue(newValue, true);
        }
        
        private void OnEnable()
        {
            target.OnHealthChanged.AddListener(UpdateValue);
        }
        
        private void OnDisable()
        {
            target.OnHealthChanged.RemoveListener(UpdateValue);
        }
    }
}