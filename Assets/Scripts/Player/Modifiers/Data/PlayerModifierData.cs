using UnityEngine;

namespace Player.Modifiers.Data
{
    [CreateAssetMenu(menuName = "Modifier/Data")]
    public class PlayerModifierData : ScriptableObject
    {
        [HideInInspector] public uint id;
        [SerializeField] private PlayerModifierType type;
        [SerializeField] private string title;
        [SerializeField] private string description;

        public PlayerModifierType Type => type;
        public string Title => title;
        public string Description => description;
    }
}