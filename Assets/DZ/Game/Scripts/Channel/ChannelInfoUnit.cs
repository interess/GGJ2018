using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
	public class ChannelInfoUnit : MonoBehaviour
	{
		private Text __channelNameText;
		protected Text _channelNameText { get { if (__channelNameText == null) { __channelNameText = transform.Find("Text").GetComponent<Text>(); } return __channelNameText; } }

		private Image __activeIconImage;
		protected Image _activeIconImage { get { if (__activeIconImage == null) { __activeIconImage = transform.Find("ActiveIcon").GetComponent<Image>(); } return __activeIconImage; } }

		private Image __voiceIconImage;
		protected Image _voiceIconImage { get { if (__voiceIconImage == null) { __voiceIconImage = transform.Find("VoiceIcon").GetComponent<Image>(); } return __voiceIconImage; } }

		public void Reset()
		{
			SetActive(false);
			SetVoiceActive(false);
		}

		public void SetName(string value)
		{
			_channelNameText.text = value;
		}

		public void SetActive(bool value)
		{
			_activeIconImage.enabled = value;
		}

		public void SetVoiceActive(bool value)
		{
			_voiceIconImage.enabled = value;
		}
	}
}
