using Player.Modifiers.Data;

namespace Player.Modifiers
{
    public static class ModifierFactory
    {
        public static PlayerModifiersBase Create(PlayerModifierData data)
        {
            return data.Type switch
            {
                PlayerModifierType.JumpHeight => new PlayerJumpHeightModifier(data),    
                _ => null
            };
        }
    }
}