using UnityEngine;

namespace Freaking.TransformAnchor
{
	[ExecuteInEditMode]
	public class FrotationAnchor : MonoBehaviour
	{
		public Transform anchor;
		public Vector3 rotationOffset;
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

		private void LateUpdate()
		{
			if (anchor != null)
			{
				if (local)
				{
					transform.localEulerAngles = new Vector3(
						Mathf.Round((anchor.localEulerAngles.x + rotationOffset.x) * 100f) / 100f,
						Mathf.Round((anchor.localEulerAngles.y + rotationOffset.y) * 100f) / 100f,
						Mathf.Round((anchor.localEulerAngles.z + rotationOffset.z) * 100f) / 100f
					);

				}
				else
				{
					transform.eulerAngles = new Vector3(
						Mathf.Round((anchor.eulerAngles.x + rotationOffset.x) * 100f) / 100f,
						Mathf.Round((anchor.eulerAngles.y + rotationOffset.y) * 100f) / 100f,
						Mathf.Round((anchor.eulerAngles.z + rotationOffset.z) * 100f) / 100f
					);
				}
			}
		}
	}
}
