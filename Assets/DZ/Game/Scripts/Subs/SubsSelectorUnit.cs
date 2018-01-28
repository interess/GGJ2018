using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class SubsSelectorUnit : MonoBehaviour
	{
		private Animator __animator;
		protected Animator _animator { get { if (__animator == null) { __animator = GetComponent<Animator>(); } return __animator; } }

		private RectTransform __rectTransform;
		protected RectTransform _rectTransform { get { if (__rectTransform == null) { __rectTransform = GetComponent<RectTransform>(); } return __rectTransform; } }

		// public float downRotation;
		// public float upRotation;

		// [Header("Down")]
		// public float downDuration;
		// public Ease downEasing;
		// [Header("Up")]
		// public float upDuration;
		// public Ease upEasing;

		public void SetSelectction(bool value)
		{
			_animator.SetBool("Active", value);

			// _rectTransform.DOKill(false);

			// if (value)
			// {
			// 	_rectTransform.DOLocalRotate(new Vector3(0, 0, downRotation), downDuration).SetEase(downEasing);
			// }
			// else
			// {
			// 	_rectTransform.DOLocalRotate(new Vector3(0, 0, upRotation), upDuration).SetEase(upEasing);
			// }
		}
	}
}
