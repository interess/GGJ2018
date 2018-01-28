using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class SubsWordUnit : MonoBehaviour
    {
        private Text __text;
        public Text text { get { if (__text == null) { __text = GetComponent<Text>(); } return __text; } }

        private RectTransform __rectTransform;
        public RectTransform rectTransform { get { if (__rectTransform == null) { __rectTransform = GetComponent<RectTransform>(); } return __rectTransform; } }

        private FS.PrefabFactory.Scripts.ProductUnit __productUnit;
        public FS.PrefabFactory.Scripts.ProductUnit productUnit { get { if (__productUnit == null) { __productUnit = GetComponent<FS.PrefabFactory.Scripts.ProductUnit>(); } return __productUnit; } }

        public bool isTarget;
        public bool isEmpty;
        public bool isMale;
        public int dialogOwnerIndex;
        public int channelIndex;
        public bool isMarked;
        public bool isMarkedDev;
        public bool isSpoken;
        public bool isScored;
        public bool isEnd;

        public void SetColor(Color value)
        {
            text.color = value;
        }

        public void SetText(string value)
        {
            text.text = value;
        }

        public float GetWidth()
        {
            return rectTransform.sizeDelta.x;
        }
    }
}
