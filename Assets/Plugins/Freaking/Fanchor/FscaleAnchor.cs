using UnityEngine;

namespace Freaking.TransformAnchor
{
	[ExecuteInEditMode]
	public class FscaleAnchor : MonoBehaviour
	{
		public Transform anchor;
		public Vector3 scaleOffset;

		private Transform _transform;
		new private Transform transform
		{
			get
			{
				if (_transform == null) _transform = base.transform;
				return _transform;
			}
		}

		private void LateUpdate()
		{
			if (anchor != null)
			{
				transform.localScale = new Vector3(
					Mathf.Round((anchor.localScale.x + scaleOffset.x) * 100f) / 100f,
					Mathf.Round((anchor.localScale.y + scaleOffset.y) * 100f) / 100f,
					Mathf.Round((anchor.localScale.z + scaleOffset.z) * 100f) / 100f
				);
			}
		}
	}
}
