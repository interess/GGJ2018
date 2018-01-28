using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class SubsManagerUnit : MonoBehaviour
	{
		public Canvas subsCanvas;
		public RectTransform subsSelectorTriggerAnchor;
		public SubsSelectorUnit subsSelectorUnit;
		public GameObject channelInfoPrefab;
		public GameObject subsWordPrefab;
		public RectTransform channelInfoRectTransform;
		public RectTransform[] channelRectTransforms;
		public float spaceWidth = 70f;

		public Color[] dialogOwnerColors;

		private FS.PrefabFactory.Scripts.FactoryUnit __subsWordFactoryUnit;
		private FS.PrefabFactory.Scripts.FactoryUnit __channelInfoFactoryUnit;
		private List<SubsWordUnit> __wordUnitsLookup = new List<SubsWordUnit>();
		private int __currentChannelIndex;

		public void Initialize()
		{
			if (__subsWordFactoryUnit == null)
			{
				__subsWordFactoryUnit = gameObject.AddComponent<FS.PrefabFactory.Scripts.FactoryUnit>();
				__subsWordFactoryUnit.Initialize(subsWordPrefab, 40);
			}

			if (__channelInfoFactoryUnit == null)
			{
				__channelInfoFactoryUnit = gameObject.AddComponent<FS.PrefabFactory.Scripts.FactoryUnit>();
				__channelInfoFactoryUnit.Initialize(channelInfoPrefab, 10);
			}

			if (subsCanvas == null)
			{
				Debug.LogError("SubsManagerUnit | SubsCanvas is null. This will cause errors");
			}

			if (subsSelectorTriggerAnchor == null)
			{
				Debug.LogError("SubsManagerUnit | SubsSelectorTriggerAnchor is null. This will cause errors");
			}

			if (subsSelectorUnit == null)
			{
				Debug.LogError("SubsManagerUnit | SubsSelectorUnit is null. This will cause errors");
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
			var subsTextAssets = Resources.LoadAll<TextAsset>("SubsRus/" + dayIndex.ToString());

			for (int i = 0; i < subsTextAssets.Length; i++)
			{
				yield return new WaitForEndOfFrame();

				var channelIndex = i + 1;
				var subsTextAsset = subsTextAssets[i];

				var channelName = "Channel " + channelIndex;

				var subsString = subsTextAsset.ToString();
				subsString = Regex.Replace(subsString, "(\r\n|\r|\n)", " ", RegexOptions.Multiline);
				var words = subsString.Split(' ');

				var isTargetMode = false;
				var dialogOwnerIndex = 0;
				var currentWord = "";
				var currentIsTargetMode = false;
				var currentWordList = new List<SubsWordUnit>();
				var isEmpty = false;
				var isEnd = false;

				foreach (var word in words)
				{
					currentWord = word;
					currentIsTargetMode = word.Contains("*");

					currentWord = Regex.Replace(currentWord, @"\*", "");

					if (currentWord == "." || string.IsNullOrEmpty(currentWord.Trim()) || currentWord == " ")
					{
						isEmpty = true;
					}
					else
					{
						isEmpty = false;
					}

					if (isTargetMode) { currentIsTargetMode = true; }

					isEnd = word == "END";

					if (isEnd)
					{
						currentWord = " ";
					}

					if (word.Contains("---"))
					{
						currentWord = Regex.Replace(word, "---", "");
						if (currentWord.Length == 0) { continue; }
						var indexString = currentWord.Substring(0, 1);
						currentWord = currentWord.Substring(1, currentWord.Length - 1);
						int.TryParse(indexString, out dialogOwnerIndex);
						if (dialogOwnerIndex < 0) { dialogOwnerIndex = 0; }
						else if (dialogOwnerIndex > 10) { dialogOwnerIndex = 10; }
					}

					currentWord = currentWord;

					var productUnit = (SubsWordProductUnit) __subsWordFactoryUnit.Spawn();
					var wordUnit = productUnit.subsWordUnit;
					wordUnit.isTarget = currentIsTargetMode && !isEmpty;
					wordUnit.isEmpty = isEmpty;
					wordUnit.isMale = IsMale(dialogOwnerIndex);
					wordUnit.channelIndex = channelIndex;
					wordUnit.dialogOwnerIndex = dayIndex * 10000 + i * 100 + dialogOwnerIndex;
					wordUnit.SetColor(dialogOwnerColors[dialogOwnerIndex]);
					wordUnit.SetText(currentWord);
					wordUnit.transform.SetParent(channelRectTransforms[channelIndex - 1], false);
					wordUnit.isMarked = false;
					wordUnit.isMarkedDev = false;
					wordUnit.isSpoken = false;
					wordUnit.isScored = false;
					wordUnit.isEnd = isEnd;
					__wordUnitsLookup.Add(wordUnit);
					currentWordList.Add(wordUnit);

					var entity = Contexts.state.CreateViewEntity();
					entity.productUnit = productUnit;
					entity.levelPart = true;
				}

				// for (int n = 0; n < 3; n++)
				// {
				// 	yield return new WaitForEndOfFrame();
				// }

				// var cumulativeWidth = 0f;

				// foreach (var wordUnit in currentWordList)
				// {
				// 	wordUnit.rectTransform.anchoredPosition = new Vector2(cumulativeWidth, 0f);
				// 	cumulativeWidth = cumulativeWidth + wordUnit.GetWidth();
				// 	wordUnit.text.raycastTarget = true;
				// }

				var phoneChannelProductUnit = Contexts.state.phoneManagerUnit.Spawn();
				var phoneChannelUnit = phoneChannelProductUnit.GetComponent<PhoneChannelUnit>();
				phoneChannelUnit.Initialize();
				phoneChannelUnit.SetActive(false);

				var channelEntity = Contexts.state.CreateViewEntity();
				channelEntity.levelPart = true;
				channelEntity.productUnit = phoneChannelProductUnit;
				channelEntity.channel = channelIndex;
				channelEntity.phoneChannelUnit = phoneChannelUnit;
			}

			if (callback != null) { callback(); }
		}

		public void SetChannel(int channelIndex)
		{
			for (int i = 0; i < channelRectTransforms.Length; i++)
			{
				var canvasGroup = channelRectTransforms[i].GetComponentInParent<CanvasGroup>();
				if (i == channelIndex - 1)
				{
					canvasGroup.alpha = 1f;
					// canvasGroup.blocksRaycasts = true;
				}
				else
				{
					canvasGroup.alpha = 0f;
					// canvasGroup.blocksRaycasts = false;
				}
			}

			__currentChannelIndex = channelIndex;
		}

		public void MoveSubs(float speed)
		{
			foreach (var item in channelRectTransforms)
			{
				item.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
			}
		}

		public void SetRecording(bool value)
		{
			subsSelectorUnit.SetSelectction(value);
		}

		public void Reset()
		{
			foreach (var item in channelRectTransforms)
			{
				item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			}

			__wordUnitsLookup.Clear();
		}

		private bool IsMale(int dialogOwnerIndex)
		{
			return dialogOwnerIndex == 3 || dialogOwnerIndex == 4;
		}
	}
}
