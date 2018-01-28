using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Freaking.Extensions.GameObject;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class PhoneManagerUnit : MonoBehaviour
    {
        public RectTransform phoneTriggerAnchor;
        public RectTransform scoreTriggerAnchor;
        public GameObject phoneChannelPrefab;

        private FS.PrefabFactory.Scripts.FactoryUnit __factoryUnit;

        public void Initialize()
        {
            if (phoneChannelPrefab == null)
            {
                Debug.LogErrorFormat("Prefab is not set on {0}", gameObject.GetPath());
            }

            if (phoneTriggerAnchor == null)
            {
                Debug.LogError("PhoneManagerUnit | PhoneTriggerAnchor is null. This will cause errors");
            }

            if (scoreTriggerAnchor == null)
            {
                Debug.LogError("PhoneManagerUnit | ScoreTriggerAnchors is null. This will cause errors");
            }

            __factoryUnit = gameObject.AddComponent<FS.PrefabFactory.Scripts.FactoryUnit>();
            __factoryUnit.Initialize(phoneChannelPrefab, 5);
        }

        public FS.PrefabFactory.Scripts.ProductUnit Spawn()
        {
            return __factoryUnit.Spawn();
        }
    }
}
