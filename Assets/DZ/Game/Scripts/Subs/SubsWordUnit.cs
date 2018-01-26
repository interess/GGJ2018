using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class SubsWordUnit : MonoBehaviour
    {
        private Text __text;
        protected Text _text { get { if (__text == null) { __text = GetComponent<Text>(); } return __text; } }

        private RectTransform __rectTransform;
        public RectTransform rectTransform { get { if (__rectTransform == null) { __rectTransform = GetComponent<RectTransform>(); } return __rectTransform; } }

        private FS.PrefabFactory.Scripts.ProductUnit __productUnit;
        public FS.PrefabFactory.Scripts.ProductUnit productUnit { get { if (__productUnit == null) { __productUnit = GetComponent<FS.PrefabFactory.Scripts.ProductUnit>(); } return __productUnit; } }

        public bool isTarget;

        public void SetColor(Color value)
        {
            _text.color = value;
        }

        public void SetText(string value)
        {
            _text.text = value;
        }

        public float GetWitdth()
        {
            return rectTransform.sizeDelta.x;
        }
    }
}
