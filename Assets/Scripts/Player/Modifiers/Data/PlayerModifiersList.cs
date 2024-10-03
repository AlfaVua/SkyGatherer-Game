using Components.Collection;
using UnityEngine;

namespace Player.Modifiers.Data
{
    [CreateAssetMenu(menuName = "Modifier/ModifierList")]
    public class PlayerModifiersList : ScriptableObject
    {
        public WeightsCollection<PlayerModifierData> modifiers;

        public void Init()
        {
            uint id = 0;
            modifiers.ForEach(modifier =>
            {
                modifier.id = id++;
            });
        }
    }
}