using System.Collections.Generic;
using Player.Modifiers.Data;

namespace Player.Modifiers
{
    public class PlayerModifierHandler
    {
        private Dictionary<uint, PlayerModifiersBase> _sessionModifiers;
        private PlayerHandler _playerHandler;
        private PlayerModifiersList _modifiersList;
        
        public PlayerModifierHandler(PlayerModifiersList modifiersList, PlayerHandler playerHandler)
        {
            _sessionModifiers = new Dictionary<uint, PlayerModifiersBase>();
            _playerHandler = playerHandler;
            _modifiersList = modifiersList;
            _modifiersList.Init();
        }

        public PlayerMovement PlayerMovement => _playerHandler.PlayerMovement;

        public void ApplyModifier(PlayerModifierData modifier)
        {
            if (_sessionModifiers.ContainsKey(modifier.id)) _sessionModifiers[modifier.id].IncreaseModifier();
            else _sessionModifiers.Add(modifier.id, ModifierFactory.Create(modifier));
            _sessionModifiers[modifier.id].Apply(this);
        }

        public PlayerModifierData GetRandomModifier()
        {
            return _modifiersList.modifiers.GetRandomData();
        }
    }
}