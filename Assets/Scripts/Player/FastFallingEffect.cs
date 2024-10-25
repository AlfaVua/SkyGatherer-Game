using System.Collections.Generic;
using Components.Component;
using UnityEngine;

namespace Player
{
    public class FastFallingEffect : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D target;
        [SerializeField] private FadeInOutAudioSource effectSound;
        [SerializeField] private float activationThreshold;
        [SerializeField] private float deactivationThreshold;
        [SerializeField] private ParticleSystem particleEffect;
        [SerializeField] private List<TrailRenderer> trails;
        [SerializeField] private CameraShake shake;

        private bool _isActivated = false;

        private void FixedUpdate()
        {
            var magnitude = target.velocity.magnitude;
            if (_isActivated)
            {
                shake.UpdateOriginalPos(target.position);
                if (magnitude < deactivationThreshold)
                    DeactivateEffect();
            }
            else if (magnitude > activationThreshold)
                ActivateEffect();
        }

        private void ActivateEffect()
        {
            if (_isActivated) return;
            _isActivated = true;
            shake.Play(.05f, 0, .5f);
            trails.ForEach(trail => trail.emitting = true);
            particleEffect.Play();
            effectSound.FadeIn();
        }

        private void DeactivateEffect()
        {
            if (!_isActivated) return;
            _isActivated = false;
            shake.Stop();
            trails.ForEach(trail => trail.emitting = false);
            particleEffect.Stop();
            effectSound.FadeOut();
        }
    }
}