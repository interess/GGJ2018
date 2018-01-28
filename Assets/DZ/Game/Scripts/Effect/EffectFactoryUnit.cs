using System.Collections;
using System.Collections.Generic;
using Freaking.Extensions.GameObject;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EffectFactoryUnit : MonoBehaviour
    {
        public GameObject prefab;
        public int preloadCount = 10;

        public string id { get { return gameObject.name; } }
        [HideInInspector]
        public FS.PrefabFactory.Scripts.FactoryUnit factoryUnit;

        public void Initialize()
        {
            if (prefab == null)
            {
                Debug.LogErrorFormat("Prefab is not set on {0}", gameObject.GetPath());
                return;
            }

            factoryUnit = gameObject.AddComponent<FS.PrefabFactory.Scripts.FactoryUnit>();
            factoryUnit.Initialize(prefab, preloadCount);
        }

        public EffectProductUnit Spawn()
        {
            var product = (EffectProductUnit) factoryUnit.Spawn();
            product.effectUnit.Initialize();
            return product;
        }
    }
}
