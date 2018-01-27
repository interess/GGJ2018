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

        UnityEngine.UI.GraphicRaycaster _graphicsRaycaster;
        protected UnityEngine.UI.GraphicRaycaster graphicsRaycaster { get { if (_graphicsRaycaster == null) _graphicsRaycaster = GetComponent<UnityEngine.UI.GraphicRaycaster>(); return _graphicsRaycaster; } }

        bool _isOpened;

        public void SetOpened(bool value)
        {
            modalCanvasGroup.alpha = value ? 1f : 0f;
            modalCanvasGroup.blocksRaycasts = value;
            _isOpened = value;
            if (graphicsRaycaster != null) { graphicsRaycaster.enabled = value; }
        }

        public void SetActive(bool value)
        {
            if (value)
            {
                SetOpened(_isOpened);
            }
            else
            {
                modalCanvasGroup.alpha = 0f;
                modalCanvasGroup.blocksRaycasts = false;
            }
        }
    }
}
