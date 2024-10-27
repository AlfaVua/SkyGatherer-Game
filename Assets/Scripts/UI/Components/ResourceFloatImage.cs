using System.Collections;
using Generators.Resource;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Components
{
    public class ResourceFloatImage : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void Init(ResourceData resourceData, Vector3 targetPosition)
        {
            image.sprite = resourceData.Icon;
            StartCoroutine(AnimationRoutine(targetPosition));
        }

        private IEnumerator AnimationRoutine(Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 30f)
            {
                transform.position = Vector3.Slerp(transform.position, targetPosition, 0.1f * Time.deltaTime * 30f);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
    }
}