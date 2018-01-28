using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
	public class ScrollSwitcherUnit : MonoBehaviour
	{
		public RectTransform rectToFit;
		public RectTransform rectToCheck;
		public ScrollRect scrollRect;
		public bool vertical;
		public bool horizontal;

		void Start()
		{
			if (rectToFit == null) throw new System.Exception("RectToFit must be defined | " + gameObject.name);
			if (rectToCheck == null) throw new System.Exception("RectToCheck must be defined | " + gameObject.name);
			if (scrollRect == null) throw new System.Exception("ScrollRect must be defined | " + gameObject.name);
		}

		void OnGUI()
		{
			if (vertical && rectToFit.rect.height < rectToCheck.rect.height)
			{
				scrollRect.vertical = true;
				if (scrollRect.verticalScrollbar != null) scrollRect.verticalScrollbar.gameObject.SetActive(true);
			}
			else
			{
				scrollRect.vertical = false;
				if (scrollRect.verticalScrollbar != null) scrollRect.verticalScrollbar.gameObject.SetActive(false);
			}

			if (horizontal && rectToFit.rect.width < rectToCheck.rect.width)
			{
				scrollRect.horizontal = true;
				if (scrollRect.horizontalScrollbar != null) scrollRect.horizontalScrollbar.gameObject.SetActive(true);
			}
			else
			{
				scrollRect.horizontal = false;
				if (scrollRect.horizontalScrollbar != null) scrollRect.horizontalScrollbar.gameObject.SetActive(false);
			}
		}
	}
}
