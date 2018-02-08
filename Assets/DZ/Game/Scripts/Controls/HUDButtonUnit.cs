using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class HUDButtonUnit : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
    {
        public event System.Action onPointerDown;
        public event System.Action onPointerUp;
        public event System.Action onPointerClick;

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            if (onPointerDown != null) { onPointerDown.Invoke(); }
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (onPointerClick != null) { onPointerClick.Invoke(); }
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            if (onPointerUp != null) { onPointerUp.Invoke(); }
        }
    }
}
