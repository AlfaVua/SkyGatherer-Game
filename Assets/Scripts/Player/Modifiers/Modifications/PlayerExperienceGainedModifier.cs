using Player.Modifiers.Data;

namespace Player.Modifiers.Modifications
{
    public class PlayerExperienceGainedModifier : PlayerModifiersBase
    {
        public PlayerExperienceGainedModifier(PlayerModifierData staticData) : base(staticData)
        {
        }

        public override void Apply(PlayerModifierHandler handler)
        {
            handler.ExperienceController.expModifier += .25f;
        }
    }
}