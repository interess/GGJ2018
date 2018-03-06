using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
	public class DevScoreUnit : MonoBehaviour
	{
		public Text scoreText;

		private void Awake()
		{
			if (!Debug.isDebugBuild)
			{
				gameObject.SetActive(false);
			}
		}

		private void Update()
		{
			if (Contexts.state.HasScore())
			{
				scoreText.text = Contexts.state.score.ToString();
			}
		}
	}
}
