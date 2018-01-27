using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DZ.Game.Scripts
{
    public class LightUnit : MonoBehaviour
    {
        private Light __light;
        protected Light _light { get { if (__light == null) { __light = GetComponent<Light>(); } return __light; } }

        private LightBlimmingUnit __lightBlimmintUnit;
        protected LightBlimmingUnit _lightBlimmintUnit { get { if (__lightBlimmintUnit == null) { __lightBlimmintUnit = GetComponent<LightBlimmingUnit>(); } return __lightBlimmintUnit; } }

        public string lightId;

        private float __cahcedIntensity;

        private void Start()
        {
            __cahcedIntensity = _light.intensity;
        }

        public void SetActive(bool value)
        {
            _light.DOKill(false);
            _light.DOIntensity(value ? __cahcedIntensity : 0f, 1f);

            if (_lightBlimmintUnit != null)
            {
                _lightBlimmintUnit.enabled = value;
            }
        }
    }
}
