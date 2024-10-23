using UnityEngine;

namespace Player.Modifiers.Data
{
    [CreateAssetMenu(menuName = "Modifier/Data")]
    public class ModifierData : ScriptableObject
    {
        [SerializeField] private ModifierType type;
        [SerializeField] private string title;
        [SerializeField] private string description;

        public ModifierType Type => type;
        public string Title => title;
        public string Description => description;
    }
}