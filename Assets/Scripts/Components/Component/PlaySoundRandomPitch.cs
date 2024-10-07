using UnityEngine;

namespace Components.Component
{
    public class PlaySoundRandomPitch : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void Play()
        {
            audioSource.pitch = Random.value * .4f + 0.8f;
            audioSource.Play();
        }
    }
}