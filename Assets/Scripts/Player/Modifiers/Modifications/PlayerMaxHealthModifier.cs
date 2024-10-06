using Player.Modifiers.Data;

namespace Player.Modifiers.Modifications
{
    public class PlayerMaxHealthModifier : PlayerModifiersBase
    {
        public PlayerMaxHealthModifier(PlayerModifierData staticData) : base(staticData)
        {
        }

        public override void Apply(PlayerModifierHandler handler)
        {
            handler.PlayerHealth.IncreaseMaxHealthByPercent(1 + .1f * _modifier);
        }
    }
}