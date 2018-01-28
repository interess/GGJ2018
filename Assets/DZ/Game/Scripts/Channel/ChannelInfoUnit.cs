using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
	public class ChannelInfoUnit : MonoBehaviour
	{
		public Sprite channelVoiceActiveSprite;
		public Sprite channelVoiceInactiveSprite;
		public Image[] channelImages;
		public Transform lineTransform;
		public Transform[] lineAnchors;

		public void Reset()
		{
			SetActive(1);
			SetVoiceActive(false, 1);
			SetVoiceActive(false, 2);
			SetVoiceActive(false, 3);
			SetVoiceActive(false, 4);
		}

		public void SetActive(int channelIndex)
		{
			lineTransform.DOMoveY(lineAnchors[channelIndex - 1].position.y, 0.5f);
		}

		public void SetVoiceActive(bool value, int channelIndex)
		{
			channelImages[channelIndex - 1].sprite = value ? channelVoiceActiveSprite : channelVoiceInactiveSprite;
		}
	}
}
