using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class RadioUnit : MonoBehaviour
	{
		private AudioSource __audioSource;
		protected AudioSource _audioSource { get { if (__audioSource == null) { __audioSource = GetComponent<AudioSource>(); } return __audioSource; } }

		private void OnEnable()
		{
			_audioSource.Play();
		}

		private void OnDisable()
		{
			_audioSource.Stop();
		}
	}
}
