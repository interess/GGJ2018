using UnityEngine;

namespace Freaking.TransformAnchor
{
	[ExecuteInEditMode]
	public class FpositionAnchor : MonoBehaviour
	{
		public Transform anchor;
		public Vector3 positionOffset;
		public bool local;

		private Transform _transform;
		new private Transform transform
		{
			get
			{
				if (_transform == null) _transform = base.transform;
				return _transform;
			}
		}

		private void LateUpdate ()
		{
			if (anchor != null)
			{
				if (local)
				{
					transform.localPosition = new Vector3 (
						Mathf.Round ((anchor.localPosition.x + positionOffset.x) * 100f) / 100f,
						Mathf.Round ((anchor.localPosition.y + positionOffset.y) * 100f) / 100f,
						Mathf.Round ((anchor.localPosition.z + positionOffset.z) * 100f) / 100f
					);
				}
				else
				{
					transform.position = new Vector3 (
						Mathf.Round ((anchor.position.x + positionOffset.x) * 100f) / 100f,
						Mathf.Round ((anchor.position.y + positionOffset.y) * 100f) / 100f,
						Mathf.Round ((anchor.position.z + positionOffset.z) * 100f) / 100f
					);
				}
			}
		}
	}
}
