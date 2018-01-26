using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DZ.Game.Scripts
{
	public class SubsManagerUnit : MonoBehaviour
	{
		public GameObject subsWordPrefab;
		public RectTransform subsWrapperRectTransform;
		public float spaceWidth = 10f;

		public Color[] dialogOwnerColors;

		private FS.PrefabFactory.Scripts.FactoryUnit __subsWordFactoryUnit;
		private List<SubsWordUnit> __wordUnitsLookup = new List<SubsWordUnit>();
		private float __cumulativeWidth;

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
		}

		public void LoadSubs(string id, System.Action callback)
		{
			StartCoroutine(LoadSubsRoutine(id, callback));
		}

		private IEnumerator LoadSubsRoutine(string id, System.Action callback)
		{
			yield return new WaitForEndOfFrame();

			var subsTextAsset = Resources.Load<TextAsset>("Subs/" + id);
			var subsString = subsTextAsset.ToString();
			subsString = Regex.Replace(subsString, "(\r\n|\r|\n)", " ", RegexOptions.Multiline);
			var words = subsString.Split(' ');

			var isTargetMode = false;
			var dialogOwnerIndex = 0;
			var currentWord = "";

			foreach (var word in words)
			{
				currentWord = word;
				var currentIsTargetMode = isTargetMode;

				if (word.Contains("*"))
				{
					currentWord = Regex.Replace(word, @"\*", "");
					isTargetMode = !isTargetMode;
				}

				if (isTargetMode) { currentIsTargetMode = true; }

				if (word.Contains("---"))
				{
					currentWord = Regex.Replace(word, "---", "");
					var indexString = currentWord.Substring(1);
					int.TryParse(indexString, out dialogOwnerIndex);
				}

				var productUnit = (SubsWordProductUnit) __subsWordFactoryUnit.Spawn();
				var wordUnit = productUnit.subsWordUnit;
				wordUnit.isTarget = false;
				wordUnit.SetColor(currentIsTargetMode ? Color.red : Color.black);
				wordUnit.SetText(currentWord);
				wordUnit.transform.SetParent(subsWrapperRectTransform, false);
				__wordUnitsLookup.Add(wordUnit);
			}

			yield return new WaitForEndOfFrame();

			var baseOffset = 960f;

			foreach (var wordUnit in __wordUnitsLookup)
			{
				wordUnit.rectTransform.anchoredPosition = new Vector2(__cumulativeWidth + baseOffset, 0f);
				__cumulativeWidth += spaceWidth + wordUnit.GetWitdth();
			}

			if (callback != null) { callback(); }
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
