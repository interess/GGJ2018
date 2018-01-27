using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DZ.Game.Scripts
{
	public class AudioManagerUnit : MonoBehaviour
	{
		public string effectsGroupName;
		public string radioGroupName;
		public string musicGroupName;

		public AudioMixer audioMixer;
		public AudioMixerGroup audioMixerEffectsGroup
		{
			get
			{
				if (audioMixer == null) return null;
				var groups = audioMixer.FindMatchingGroups(effectsGroupName);
				if (groups.Length > 0) return groups[0];
				return null;
			}
		}

		public AudioMixerGroup audioMixerRadioGroup
		{
			get
			{
				if (audioMixer == null) return null;
				var groups = audioMixer.FindMatchingGroups(radioGroupName);
				if (groups.Length > 0) return groups[0];
				return null;
			}
		}

		public AudioMixerGroup audioMixerMusicGroup
		{
			get
			{
				if (audioMixer == null) return null;
				var groups = audioMixer.FindMatchingGroups(musicGroupName);
				if (groups.Length > 0) return groups[0];
				return null;
			}
		}

		
	}
}
