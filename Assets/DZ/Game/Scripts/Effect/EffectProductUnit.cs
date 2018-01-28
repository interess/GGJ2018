using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EffectProductUnit : FS.PrefabFactory.Scripts.ProductUnit
    {
        EffectUnit _effectUnit;
        public EffectUnit effectUnit
        {
            get
            {
                if (_effectUnit == null) { _effectUnit = GetComponent<EffectGroupUnit>(); }
                if (_effectUnit == null) { _effectUnit = GetComponent<EffectUnit>(); }
                return _effectUnit;
            }
        }
    }
}
