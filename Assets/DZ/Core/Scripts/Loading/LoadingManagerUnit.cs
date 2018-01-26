using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DZ.Core.Scripts
{
    public class LoadingManagerUnit : MonoBehaviour
    {
        new public Camera camera;
        public CanvasGroup canvasGroup;
        public Text textProgress;
        public Text textTip;
        public LoadingBarUnit loadingBarUnit;

        public float canvasFadeDuration;
        public float secondsToChangeTip = 1f;

        List<string> cachedTips = new List<string>();

        float secondsPassed = 100f;
        int currentTipIndex;

        void Start()
        {
            var entity = Contexts.state.CreateEntity();
            entity.loadingManagerUnit = this;
        }

        public void SetProgress(int value)
        {
            if (textProgress == null) return;

            textProgress.text = value + "%";

            if (loadingBarUnit == null) return;

            secondsPassed += Time.deltaTime;
            loadingBarUnit.SetValue(value / 100f);

            if (textTip == null) return;

            if (secondsPassed > secondsToChangeTip)
            {
                textTip.text = GetRandomTip();
                secondsPassed = 0f;
            }
        }

        [FreakingEditor.FbuttonPlay]
        public void DoHide()
        {
            canvasGroup.DOFade(0f, 0.1f).SetDelay(.5f);
            canvasGroup.blocksRaycasts = false;
        }

        string GetRandomTip()
        {
            // if (cachedTips.Count == 0)
            // {
            //     var obj = Resources.Load("Loading/Tips");

            //     if (obj != null)
            //     {
            //         var text = obj.ToString();
            //         var tipsJSON = JSON.Parse(text).AsObject;
            //         var cachedTipsArray = tipsJSON["tips"].AsArray;

            //         foreach (JSONNode tip in cachedTipsArray)
            //         {
            //             cachedTips.Add(tip.Value.ToUpper());
            //         }
            //     }
            //     else
            //     {
            //         Debug.LogError("LoadingManager did not find Resources/Loading/Tips.json");
            //     }
            // }

            // if (cachedTips.Count != 0)
            // {
            //     var random = Random.Range(0, cachedTips.Count);
            //     return cachedTips[random];
            // }

            return ("Be Dzen, motherfucker!").ToUpper();
        }
    }
}
