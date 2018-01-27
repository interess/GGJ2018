using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DZ.Game.Scripts
{
    public class LightBlimmingUnit : MonoBehaviour
    {
        private UnityEngine.Light __light;
        protected Light _light { get { if (__light == null) { __light = GetComponent<Light>(); } return __light; } }

        private float __cachedIntensity;
        private int __framesSkipped;

        private void Start()
        {
            __cachedIntensity = _light.intensity;
        }

        private void Update()
        {
            if (__framesSkipped == 0)
            {
                __framesSkipped = Random.Range(3, 30);
            }

            __framesSkipped--;

            if (__framesSkipped <= 0)
            {
                _light.DOKill(false);
                _light.DOIntensity(__cachedIntensity * Random.Range(0, 1.2f), 0.2f);
            }
        }
    }
}
