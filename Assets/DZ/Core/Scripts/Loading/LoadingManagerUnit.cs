using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Core.Scripts
{
    public class LoadingManagerUnit : MonoBehaviour
    {
        new public Camera camera;
        public CanvasGroup canvasGroup;
        public Text textProgress;
        public LoadingBarUnit loadingBarUnit;

        public float canvasFadeDuration;
        public float secondsToChangeTip = 1f;

        private float __secondsPassed = 100f;
        private int __currentTipIndex;

        public void SetProgress(int value)
        {
            if (textProgress == null) { return; }

            textProgress.text = value + "%";

            if (loadingBarUnit == null) { return; }

            __secondsPassed += Time.deltaTime;
            loadingBarUnit.SetValue(value / 100f);
        }

        [FreakingEditor.FbuttonPlay]
        public void DoHide()
        {
            canvasGroup.DOFade(0f, 0.1f).SetDelay(.5f);
            canvasGroup.blocksRaycasts = false;
        }
    }
}
