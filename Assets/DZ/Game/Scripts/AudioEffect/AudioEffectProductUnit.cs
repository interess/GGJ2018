using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class AudioEffectProductUnit : FS.PrefabFactory.Scripts.ProductUnit
    {
        private AudioEffectUnit __audioEffectUnit;
        public AudioEffectUnit audioEffectUnit { get { if (__audioEffectUnit == null) __audioEffectUnit = GetComponent<AudioEffectUnit>(); return __audioEffectUnit; } }
    }
}
