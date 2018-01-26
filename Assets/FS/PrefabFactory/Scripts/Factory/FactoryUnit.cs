﻿using System.Collections;
using System.Collections.Generic;
using Freaking;
using Freaking.Extensions.GameObject;
using UnityEngine;

namespace FS.PrefabFactory.Scripts
{
    public class FactoryUnit : MonoBehaviour
    {
        private GameObject _prefab;
        private Queue<ProductUnit> _productQueue = new Queue<ProductUnit>();
        private int _currentIndex;

        public void Initialize(GameObject prefab, int preloadCount)
        {
            this._prefab = prefab;

            if (prefab.GetComponent<ProductUnit>() == null)
            {
                Debug.LogErrorFormat("Factory prefab does not have ProductUnit - {0}", gameObject.GetPath());
                gameObject.SetActive(false);
                return;
            }

            for (int i = 0; i < preloadCount; i++)
            {
                Preload();
            }
        }

        protected void Preload()
        {
            _currentIndex++;

            var go = Instantiate(_prefab, transform, true);
            go.name = _currentIndex.ToString();

            var product = go.GetComponent<ProductUnit>();
            product.factoryDespawn = Despawn;
            product.transform.localPosition = Vector3.zero;
            product.SetActive(false);

            _productQueue.Enqueue(product);
        }

        [FreakingEditor.FbuttonPlay]
        public virtual ProductUnit Spawn()
        {
            if (_productQueue.Count == 0) Preload();

            var product = _productQueue.Dequeue();
            product.SetActive(true);

            return product;
        }

        public virtual void Despawn(ProductUnit product)
        {
            product.SetActive(false);
            _productQueue.Enqueue(product);
        }
    }
}
