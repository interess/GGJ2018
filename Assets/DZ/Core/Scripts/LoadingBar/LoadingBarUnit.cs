﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Core.Scripts
{
    [ExecuteInEditMode]
    public class LoadingBarUnit : MonoBehaviour
    {
        public RectTransform outer;
        public RectTransform inner;

        public float transitionDuration;
        public float padding;

        [SerializeField]
        private float __targetValue;

        private float __currentValue;

        public void SetValue(float value)
        {
            if (outer == null || inner == null) return;
            if (value > 1f) value = 1f;

            var offset = inner.offsetMax;
            offset.x = (outer.rect.width - padding * 2) * (value - 1) - padding;
            offset.y = -padding;

            inner.offsetMax = offset;
            inner.offsetMin = new Vector2(padding, padding);

            __currentValue = value;
        }

#if UNITY_EDITOR
        void Update()
        {
            if (__currentValue != __targetValue)
            {
                SetValue(__targetValue);
            }
        }
#endif
    }
}
