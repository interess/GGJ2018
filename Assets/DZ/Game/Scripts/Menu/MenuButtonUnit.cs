using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DZ.Game.Scripts
{
	public class MenuButtonUnit : MonoBehaviour
	{
		public SpriteRenderer spriteRenderer;

		public Light[] lights;
		public float mouseOverInensity;
		public float mouseDownInensity;

		public Sprite extraSprite;
		protected Sprite _cachedSprite;
		protected Color _cachedColor;
		protected bool _isInverted;

		public string buttonId;

		public void Initialize()
		{
			_cachedSprite = spriteRenderer.sprite;
			_cachedColor = spriteRenderer.color;
			OnMouseExit();
		}

		public virtual void SetInverted(bool value)
		{
			if (value)
			{
				if (extraSprite != null)
				{
					spriteRenderer.sprite = extraSprite;
				}
				else
				{
					spriteRenderer.DOColor(Color.black, 0.3f);
				}
			}
			else
			{
				spriteRenderer.sprite = _cachedSprite;
				if (spriteRenderer.color != _cachedColor) { spriteRenderer.DOColor(_cachedColor, 0.3f); }
			}
			_isInverted = value;
		}

		public void OnMouseOver()
		{
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			if (_isInverted && extraSprite == null) { return; }
			// Incrase small light intensity

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

		public void OnMouseExit()
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

		public void OnMouseDown()
		{
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			if (_isInverted && extraSprite == null) { return; }

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
