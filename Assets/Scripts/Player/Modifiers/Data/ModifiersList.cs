using System;
using Components.Collection;
using UnityEngine;

namespace Player.Modifiers.Data
{
    [CreateAssetMenu(menuName = "Modifier/ModifierList")]
    public class ModifiersList : ScriptableObject
    {
        [SerializeField] private WeightsCollection<ModifierData> modifiers;

        public void Init()
        {
            modifiers.UpdateWeights();
        }

        public ModifierData GetRandomData()
        {
            return modifiers.GetRandomData();
        }
    }
}