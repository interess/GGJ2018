using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FS.PrefabFactory.Scripts
{
    public class ProductUnit : MonoBehaviour
    {
        #region Props

        private Uid.Scripts.UidUnit __uidUnit;
        public Uid.Scripts.UidUnit uidUnit
        {
            get
            {
                if (__uidUnit == null) { __uidUnit = GetComponent<Uid.Scripts.UidUnit>(); }
                if (__uidUnit == null)
                {
                    __uidUnit = gameObject.AddComponent<Uid.Scripts.UidUnit>();
                    __uidUnit.hideFlags = HideFlags.HideAndDontSave;
                }
                return __uidUnit;
            }
        }

        #endregion

        public delegate void FactoryDespawn(ProductUnit unit);
        public FactoryDespawn factoryDespawn;

        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);
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
