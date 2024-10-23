using System.Collections.Generic;
using Components.Collection;
using Player.Modifiers.Data;
using UnityEngine;

namespace Player.Modifiers
{
    public class PlayerModifierHandler : MonoBehaviour
    {
        [SerializeField] private KeyValueList<ModifierType, MonoBehaviour> modifiers;
        private Dictionary<ModifierType, IModifiable> _sessionModifiers;

        public void Init()
        {
            _sessionModifiers = new Dictionary<ModifierType, IModifiable>();
        }

        public void ApplyModifier(ModifierType modifierType)
        {
            _sessionModifiers.TryAdd(modifierType, modifiers[modifierType] as IModifiable);
            _sessionModifiers[modifierType].Modify(modifierType);
        }
    }
}