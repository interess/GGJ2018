using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class SubsWordProductUnit : FS.PrefabFactory.Scripts.ProductUnit
    {
        private SubsWordUnit __subsWordUnit;
        public SubsWordUnit subsWordUnit { get { if (__subsWordUnit == null) __subsWordUnit = GetComponent<SubsWordUnit>(); return __subsWordUnit; } }

        [FreakingEditor.FbuttonPlay]
        public override void Despawn()
        {
            Destroy(gameObject);
        }
    }
}
