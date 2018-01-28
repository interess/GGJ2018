using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class HudUnit : MonoBehaviour
    {
        private CanvasGroup __canvasGroup;
        protected CanvasGroup _canvasGroup { get { if (__canvasGroup == null) { __canvasGroup = GetComponent<CanvasGroup>(); } return __canvasGroup; } }

        public float fadeDuration;

        public void SetActive(bool value, bool fast = false)
        {
            _canvasGroup.DOFade(value ? 1f : 0f, fast ? 0f : fadeDuration);
        }
    }
}
