using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DZ.Game.Scripts
{
    public class OverlayUnit : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        public void SetActive(bool value)
        {
            canvasGroup.alpha = value ? 1f : 0;
            canvasGroup.blocksRaycasts = value;
        }
    }
}
