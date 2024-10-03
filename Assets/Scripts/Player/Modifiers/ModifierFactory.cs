using Player.Modifiers.Data;
using Player.Modifiers.Modifications;

namespace Player.Modifiers
{
    public static class ModifierFactory
    {
        public static PlayerModifiersBase Create(PlayerModifierData data)
        {
            return data.Type switch
            {
                PlayerModifierType.JumpHeight => new PlayerJumpHeightModifier(data),
                PlayerModifierType.MaxHpIncrease => new PlayerMaxHealthModifier(data),
                PlayerModifierType.Heal => new PlayerHealModifier(data),
                _ => null
            };
        }
    }
}