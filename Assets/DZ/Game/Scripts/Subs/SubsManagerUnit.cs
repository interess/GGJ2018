using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class SubsManagerUnit : MonoBehaviour
	{
		public GameObject subsWordPrefab;
		public RectTransform[] channelRectTransforms;
		public float spaceWidth = 10f;

		public Color[] dialogOwnerColors;

		private FS.PrefabFactory.Scripts.FactoryUnit __subsWordFactoryUnit;
		private List<SubsWordUnit> __wordUnitsLookup = new List<SubsWordUnit>();
		private float __cumulativeWidth;
		private int __currentChannelIndex;

		public void Initialize()
		{
			if (__subsWordFactoryUnit == null)
			{
				__subsWordFactoryUnit = gameObject.AddComponent<FS.PrefabFactory.Scripts.FactoryUnit>();
				__subsWordFactoryUnit.Initialize(subsWordPrefab, 40);
			}

			if (dialogOwnerColors.Length < 10)
			{
				Debug.LogError("SubsManagerUnit | DialogOwnerColors array must have more than 10 colors");
			}

			if (channelRectTransforms.Length < 10)
			{
				Debug.LogError("SubsManagerUnit | SubsWrapperRectTransforms array must have more than 10 RectTransforms");
			}
		}

		public void LoadSubs(int dayIndex, System.Action callback)
		{
			StartCoroutine(LoadSubsRoutine(dayIndex, callback));
		}

		private IEnumerator LoadSubsRoutine(int dayIndex, System.Action callback)
		{
			var subsTextAssets = Resources.LoadAll<TextAsset>("Subs/" + dayIndex.ToString());

			for (int i = 0; i < subsTextAssets.Length; i++)
			{
				var channelIndex = i + 1;
				var subsTextAsset = subsTextAssets[i];

				var subsString = subsTextAsset.ToString();
				subsString = Regex.Replace(subsString, "(\r\n|\r|\n)", " ", RegexOptions.Multiline);
				var words = subsString.Split(' ');

				var isTargetMode = false;
				var dialogOwnerIndex = 0;
				var currentWord = "";
				var currentIsTargetMode = false;

				foreach (var word in words)
				{
					currentWord = word;
					currentIsTargetMode = isTargetMode;

					if (word.Contains("*"))
					{
						currentWord = Regex.Replace(word, @"\*", "");
						isTargetMode = !isTargetMode;
					}

					if (isTargetMode) { currentIsTargetMode = true; }

					if (word.Contains("---"))
					{
						currentWord = Regex.Replace(word, "---", "");
						var indexString = currentWord.Substring(0, 1);
						currentWord = currentWord.Substring(1, currentWord.Length - 1);
						int.TryParse(indexString, out dialogOwnerIndex);
						if (dialogOwnerIndex < 0) { dialogOwnerIndex = 0; }
						else if (dialogOwnerIndex > 10) { dialogOwnerIndex = 10; }
					}

					var productUnit = (SubsWordProductUnit) __subsWordFactoryUnit.Spawn();
					var wordUnit = productUnit.subsWordUnit;
					wordUnit.isTarget = false;
					wordUnit.SetColor(dialogOwnerColors[dialogOwnerIndex]);
					wordUnit.SetText(currentWord);
					wordUnit.transform.SetParent(channelRectTransforms[channelIndex - 1], false);
					__wordUnitsLookup.Add(wordUnit);
				}

				yield return new WaitForEndOfFrame();

				var baseOffset = 20f;

				foreach (var wordUnit in __wordUnitsLookup)
				{
					wordUnit.rectTransform.anchoredPosition = new Vector2(__cumulativeWidth + baseOffset, 0f);
					__cumulativeWidth += spaceWidth + wordUnit.GetWitdth();
				}

				var channelEntity = Contexts.state.CreateViewEntity();
				channelEntity.levelPart = true;
				channelEntity.channelInfoUnit = channelRectTransforms[channelIndex - 1].GetComponent<ChannelInfoUnit>();
				channelEntity.channel = channelIndex;
			}

			if (callback != null) { callback(); }
		}

		public void SetChannel(int channelIndex)
		{
			for (int i = 0; i < channelRectTransforms.Length; i++)
			{
				if (i == channelIndex - 1)
				{
					channelRectTransforms[i].GetComponent<CanvasGroup>().alpha = 1f;
				}
				else
				{
					channelRectTransforms[i].GetComponent<CanvasGroup>().alpha = 0f;
				}
			}

			__currentChannelIndex = channelIndex;
		}

		public void Reset()
		{
			foreach (var wordUnit in __wordUnitsLookup)
			{
				wordUnit.productUnit.Despawn();
			}

			__wordUnitsLookup.Clear();

			__cumulativeWidth = 0f;
		}
	}
}
