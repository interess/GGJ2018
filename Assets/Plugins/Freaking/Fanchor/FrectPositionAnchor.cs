using UnityEngine;

namespace Freaking.TransformAnchor
{
	[ExecuteInEditMode]
	public class FrectPositionAnchor : MonoBehaviour
	{
		public RectTransform anchor;
		public Vector2 positionOffset;

		private RectTransform _rectTransform;
		private RectTransform rectTransform
		{
			get
			{
				if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
				return _rectTransform;
			}
		}

		private void LateUpdate()
		{
			if (anchor != null)
			{
				var anch = anchor.anchoredPosition;
				rectTransform.anchoredPosition = new Vector2(
					Mathf.Round((anch.x + positionOffset.x) * 100f) / 100f,
					Mathf.Round((anch.y + positionOffset.y) * 100f) / 100f
				);
			}
		}
	}
}
