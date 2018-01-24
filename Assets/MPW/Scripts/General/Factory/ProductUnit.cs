using System.Collections;
using System.Collections.Generic;
using FFramework;
using UnityEngine;

namespace DZ
{
    public class ProductUnit : FUnit
    {
        public delegate void FactoryDespawn(ProductUnit unit);
        public FactoryDespawn factoryDespawn;

        private bool __productIsActive;
        public bool productIsActive { get { return __productIsActive; } }

        public virtual void SetActive(bool value)
        {
            __productIsActive = value;
            gameObject.SetActive(value);

            var funits = GetComponentsInChildren<FUnit>();
            foreach (var funit in funits)
            {
                if (funit == this) { continue; }

                if (value)
                {
                    Debug.Log("Register from product: " + funit.gameObject.name);
                    FKernel.Register(funit);
                }
                else
                {
                    Debug.Log("Deregister from product: " + funit.gameObject.name);
                    FKernel.Deregister(funit);
                }
            }
        }

        [FreakingEditor.FbuttonPlay]
        public virtual void Despawn()
        {
            if (factoryDespawn != null) { factoryDespawn(this); }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
