using System.Collections;
using System.Collections.Generic;
using FFramework;
using Freaking.Extensions.GameObject;
using UnityEngine;
using UnityEngine.Assertions;

namespace DZ
{
	public class FactoryUnit : FUnit
	{
		private GameObject __prefab;
		private Queue<ProductUnit> __productQueue = new Queue<ProductUnit>();
		private int __currentIndex;

		public void Init(GameObject prefab, int preloadCount = 0)
		{
			this.__prefab = prefab;

			if (prefab.GetComponent<ProductUnit>() == null)
			{
				Debug.LogErrorFormat("Factory '{1}' does not have ProductUnit on prefab '{0}'", prefab.name, gameObject.GetPath());
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
			__currentIndex++;

			var go = Instantiate(__prefab, transform, true);
			go.name = __currentIndex.ToString();

			var product = go.GetComponent<ProductUnit>();
			product.factoryDespawn = Despawn;
			product.transform.localPosition = Vector3.zero;
			product.SetActive(false);

			__productQueue.Enqueue(product);
		}

		[FreakingEditor.FbuttonPlay]
		public virtual ProductUnit Spawn()
		{
			if (__productQueue.Count == 0) { Preload(); }

			var product = __productQueue.Dequeue();
			product.SetActive(true);

			return product;
		}

		public virtual void Despawn(ProductUnit product)
		{
			if (!product.productIsActive) { return; }

			product.SetActive(false);
			product.transform.SetParent(transform);
			product.transform.localPosition = Vector3.zero;
			__productQueue.Enqueue(product);
		}

		public static FactoryUnit Create(GameObject targetGameObject, GameObject prefabGameObject, int preloadCount = 10)
		{
			var factoryUnit = targetGameObject.AddComponent<FactoryUnit>();
			factoryUnit.Init(prefabGameObject, preloadCount);
			return factoryUnit;
		}
	}
}
