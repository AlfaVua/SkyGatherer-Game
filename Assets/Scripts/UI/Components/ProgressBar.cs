using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private RectMask2D rectMask;
        [SerializeField] private Image filling;
        [SerializeField][Min(1)] private float maxValue;
        [SerializeField][Min(0)] private float currentValue;
        [SerializeField] private Gradient gradient;
        [SerializeField] [Range(0.001f, 1)] private float animationSpeed;

        private float _componentWidth;

        private float _targetValue;

        private void Start()
        {
            InitComponentWidth();
        }

        private void InitComponentWidth()
        {
            _componentWidth = rectMask.rectTransform.rect.width;
        }

        private void Update()
        {
            TryUpdateView();
        }

        private void TryUpdateView()
        {
            var difference = Mathf.Abs(currentValue - _targetValue);
            switch (difference)
            {
                case 0:
                    return;
                case < 0.1f:
                    SetValueInternal(_targetValue);
                    return;
                default:
                    SetValueInternal(Mathf.Lerp(currentValue, _targetValue, animationSpeed));
                    break;
            }
        }

        private void SetValueInternal(float value)
        {
            currentValue = value;
            var progressValue = currentValue / maxValue;
            rectMask.padding = new Vector4(rectMask.padding.x, rectMask.padding.y, _componentWidth * (1 - progressValue), rectMask.padding.w);
            filling.color = gradient.Evaluate(progressValue);
        }

        public void SetValue(float value, bool animate = false)
        {
            if (animate) _targetValue = value;
            else
            {
                _targetValue = value;
                SetValueInternal(value);
            }
        }

        public void SetMaxValue(float maxValue)
        {
            this.maxValue = maxValue;
        }
    }
}