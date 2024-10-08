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

        private void FixedUpdate()
        {
            var magnitude = target.velocity.magnitude;
            if (!effectSound.isFadingOut)
            {
                if (magnitude < deactivationThreshold)
                    effectSound.FadeOut();
            }
            else if (magnitude > activationThreshold)
                effectSound.FadeIn();
        }
    }
}