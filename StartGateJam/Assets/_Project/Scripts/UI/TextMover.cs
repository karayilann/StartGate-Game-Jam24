using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.UI
{
    public class TextMover : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public float topPosition;
        private const float MoveSpeed = 50f;
        public Vector2 startingPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            startingPosition = _rectTransform.anchoredPosition;
            topPosition = _rectTransform.anchoredPosition.y + (_rectTransform.rect.height * 0.5f) + 245.1968f;
        }

        private void Update()
        {
            if (_rectTransform.anchoredPosition.y < topPosition)
            {
                _rectTransform.anchoredPosition += Vector2.up * (MoveSpeed * Time.deltaTime);
            }
        }
    }
}