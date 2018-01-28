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
    }
}
