using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class ModalUnit : MonoBehaviour
    {
        public string id;
        public RectTransform modalRectTransform;
        public CanvasGroup modalCanvasGroup;

        public GameObject[] enableGameObjects;

        UnityEngine.UI.GraphicRaycaster _graphicsRaycaster;
        protected UnityEngine.UI.GraphicRaycaster graphicsRaycaster { get { if (_graphicsRaycaster == null) _graphicsRaycaster = GetComponent<UnityEngine.UI.GraphicRaycaster>(); return _graphicsRaycaster; } }

        bool _isOpened;

        public float openDuration;
        public float closeDuration;

        public ButtonEventUnit __internalSkipButtonEventUnit;
        public bool __internalClosable;

        public void SetOpened(bool value, bool fast = false)
        {
            if (_isOpened == value) { return; }
            if (graphicsRaycaster != null) { graphicsRaycaster.enabled = value; }
            _isOpened = value;
            modalCanvasGroup.blocksRaycasts = value;

            if (value && openDuration <= 0 || !value && closeDuration <= 0)
            {
                modalCanvasGroup.alpha = value ? 1f : 0f;
            }
            else
            {
                modalCanvasGroup.DOKill(false);
                modalCanvasGroup.DOFade(value ? 1f : 0f, value ? openDuration : closeDuration);
            }

        }

        public void SetActive(bool value, bool fast = false)
        {
            if (value)
            {
                SetOpened(value);
            }
            else
            {
                modalCanvasGroup.blocksRaycasts = false;

                if (closeDuration <= 0 || fast)
                {
                    modalCanvasGroup.alpha = 0f;
                }
                else
                {
                    modalCanvasGroup.DOKill(false);
                    modalCanvasGroup.DOFade(0f, closeDuration);
                }
            }

            foreach (var item in enableGameObjects)
            {
                item.SetActive(value);
            }
        }

        public void __InternalSkip()
        {
            if (__internalSkipButtonEventUnit != null)
            {
                __internalSkipButtonEventUnit.SendMessage("HandleClickedButton");
            }
        }
    }
}
