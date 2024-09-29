using UnityEngine;

namespace Components.Component
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private Transform camTransform;
        [SerializeField] private float decreaseFactor = 1.0f;
        private float _shakeAmount = 0.7f;
        private float _shakeDuration = 0f;
	
        private Vector3 _originalPos;
	
        private void Awake()
        {
            if (camTransform == null)
            {
                camTransform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        private void Update()
        {
            if (!(_shakeDuration > 0)) return;
            camTransform.localPosition = _originalPos + Random.insideUnitSphere * _shakeAmount;
			
            _shakeDuration -= Time.deltaTime * decreaseFactor;
            if (_shakeDuration <= 0)
                camTransform.localPosition = _originalPos;
        }

        public void Play(float power, float duration)
        {
            _originalPos = camTransform.localPosition;
            _shakeAmount = power;
            _shakeDuration = duration;
        }
    }
}