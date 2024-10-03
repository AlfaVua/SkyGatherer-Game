using Player.Modifiers.Data;

namespace Player.Modifiers
{
    public abstract class PlayerModifiersBase
    {
        private PlayerModifierData _staticData;

        protected uint _modifier = 1;

        protected PlayerModifiersBase(PlayerModifierData staticData)
        {
            _staticData = staticData;
        }

        public virtual void IncreaseModifier()
        {
            _modifier++;
        }

        public abstract void Apply(PlayerModifierHandler handler);
    }
}