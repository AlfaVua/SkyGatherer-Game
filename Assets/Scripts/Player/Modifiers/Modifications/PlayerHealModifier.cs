using Player.Modifiers.Data;

namespace Player.Modifiers.Modifications
{
    public class PlayerHealModifier : PlayerModifiersBase
    {
        public PlayerHealModifier(PlayerModifierData staticData) : base(staticData)
        {
        }
        public override void IncreaseModifier()
        {
        }

        public override void Apply(PlayerModifierHandler handler)
        {
            handler.PlayerHealth.HealForPercentage(.1f);
        }
    }
}