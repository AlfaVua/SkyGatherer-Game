using Player.Modifiers.Data;

namespace Player.Modifiers.Modifications
{
    public class PlayerJumpHeightModifier : PlayerModifiersBase
    {
        private float _startingJumpHeight = 0;
        public PlayerJumpHeightModifier(PlayerModifierData staticData) : base(staticData)
        {
        }

        public override void Apply(PlayerModifierHandler handler)
        {
            if (_startingJumpHeight == 0) _startingJumpHeight = handler.PlayerMovement.jumpHeightMultiplier;
            handler.PlayerMovement.jumpHeightMultiplier = _startingJumpHeight + 4 * _modifier / 100f;
        }
    }
}