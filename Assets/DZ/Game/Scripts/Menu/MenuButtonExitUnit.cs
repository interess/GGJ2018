using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DZ.Game.Scripts
{
    public class MenuButtonExitUnit : MenuButtonUnit
    {
        public Color invertedColor;

        public override void SetInverted(bool value)
        {
            if (value)
            {
                spriteRenderer.DOColor(invertedColor, 0.3f);
            }
            else
            {
                if (spriteRenderer.color != _cachedColor) { spriteRenderer.DOColor(_cachedColor, 0.3f); }
            }

            _isInverted = value;
        }

        public override void OnMouseOver()
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (_isInverted && extraSprite == null) { return; }
            // Incrase small light intensity

            Contexts.state.CreateEffectEntity("TVOverEffect");

            foreach (var item in lights)
            {
                item.intensity = mouseOverInensity;
                var blimmin = item.GetComponent<LightBlimmingUnit>();
                if (blimmin != null)
                {
                    blimmin.enabled = false;
                }
            }
        }

        public override void OnMouseExit()
        {
            if (_isInverted && extraSprite == null) { return; }

            foreach (var item in lights)
            {
                item.intensity = 0f;
                var blimmin = item.GetComponent<LightBlimmingUnit>();
                if (blimmin != null)
                {
                    blimmin.enabled = true;
                }
            }
        }

        public override void OnMouseDown()
        {
            if (_isInverted)
            {
                return;
            }


            var entity = Contexts.input.CreateEventEntity();
            entity.eventId = buttonId;

            Contexts.state.CreateEffectEntity("TVClickEffect");

            foreach (var item in lights)
            {
                item.intensity = mouseDownInensity;
            }
        }
    }
}
