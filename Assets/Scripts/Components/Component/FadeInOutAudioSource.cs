using System.Collections;
using UnityEngine;

namespace Components.Component
{
    public class FadeInOutAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] [Min(0.01f)] private float fadeInTime = .5f;
        [SerializeField] [Min(0.01f)] private float fadeOutTime = .1f;

        [HideInInspector] public bool isFadingOut = true;

        private float _startingVolume;

        private void Awake()
        {
            _startingVolume = source.volume;
        }

        public void FadeIn()
        {
            if (!isFadingOut) return;
            
            source.volume = 0;
            StopCoroutine(nameof(FadeOutRoutine));
            StartCoroutine(nameof(FadeInRoutine));
        }

        public void FadeOut()
        {
            if (isFadingOut) return;
            
            source.volume = _startingVolume;
            StopCoroutine(nameof(FadeInRoutine));
            StartCoroutine(nameof(FadeOutRoutine));
        }

        private IEnumerator FadeInRoutine()
        {
            var timeElapsed = 0f;
            source.Play();
            isFadingOut = false;

            while (source.volume < _startingVolume)
            {
                source.volume = Mathf.Lerp(0, _startingVolume, timeElapsed / fadeInTime);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator FadeOutRoutine()
        {
            var timeElapsed = 0f;
            isFadingOut = true;

            while (source.volume > 0)
            {
                source.volume = Mathf.Lerp(_startingVolume, 0, timeElapsed / fadeOutTime);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            source.Pause();
        }
    }
}