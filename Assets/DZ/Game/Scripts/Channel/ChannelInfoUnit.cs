using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
	public class ChannelInfoUnit : MonoBehaviour
	{
		private Text __channelNameText;
		protected Text _channelNameText { get { if (__channelNameText == null) { __channelNameText = GetComponent<Text>(); } return __channelNameText; } }

		private Image __voiceIconImage;
		protected Image _voiceIconImage { get { if (__voiceIconImage == null) { __voiceIconImage = GetComponent<Image>(); } return __voiceIconImage; } }

		public void SetName(string value)
		{
			_channelNameText.text = value;
		}

		public void SetVoiceActive(bool value)
		{

		}
	}
}
