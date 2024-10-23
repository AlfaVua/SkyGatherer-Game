using Player.Modifiers.Data;

namespace Player.Modifiers
{
    public interface IModifiable
    {
        public void Modify(ModifierType type);
    }
}