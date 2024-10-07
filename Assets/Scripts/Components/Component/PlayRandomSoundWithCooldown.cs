using System.Collections.Generic;
using UnityEngine;
using Utils.ListExtension;

namespace Components.Component
{
    public class PlayRandomSoundWithCooldown : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> sounds;
        [SerializeField] private float cooldown;

        private float _cooldown;
        
        private void Update()
        {
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;
        }

        public void TryPlay()
        {
            if (_cooldown > 0) return;
            _cooldown = cooldown;
            sounds.GetRandom().Play();
        }
    }
}