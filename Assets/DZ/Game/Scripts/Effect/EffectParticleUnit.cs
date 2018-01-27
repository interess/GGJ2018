using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EffectParticleUnit : EffectUnit
    {
        ParticleSystem _particleSystem;
        new protected ParticleSystem particleSystem { get { if (_particleSystem == null) _particleSystem = GetComponentInChildren<ParticleSystem>(); return _particleSystem; } }

        public override void Initialize()
        {
            if (_isInitialized) { return; }
            base.Initialize();
        }

        public override EffectUnit Play()
        {
            base.Play();
            particleSystem.Play();
            return this;
        }

        public override EffectUnit Stop()
        {
            base.Stop();
            particleSystem.Stop();
            return this;
        }
    }
}
