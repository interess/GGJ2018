using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class EffectUnit : MonoBehaviour
    {
        private EffectProductUnit _effectProductUnit;
        protected EffectProductUnit effectProductUnit { get { if (_effectProductUnit == null) _effectProductUnit = GetComponent<EffectProductUnit>(); return _effectProductUnit; } }

        public float duration;
        [HideInInspector]
        public bool isGroupPart;

        private float _timer;
        private float _isPlayingCheckInterval = 2f;
        private bool _isPlaying;
        protected bool _isInitialized;

        public delegate void DespawnHandler();
        private DespawnHandler onDespawn;

        public virtual void Initialize()
        {
            _isInitialized = true;
        }

        public virtual EffectUnit SetOnDespawn(DespawnHandler handler)
        {
            onDespawn = handler;
            return this;
        }

        public virtual EffectUnit Play()
        {
            _timer = 0f;
            _isPlaying = true;
            return this;
        }

        public virtual EffectUnit Stop()
        {
            _isPlaying = false;
            return this;
        }

        public void Despawn()
        {
            Stop();
            effectProductUnit.Despawn();
            if (onDespawn != null)
            {
                onDespawn();
            }
        }

        private void Update()
        {
            if (isGroupPart)
            {
                return;
            }

            if (duration > 0f && _isPlaying)
            {
                if (_timer >= _isPlayingCheckInterval)
                {
                    _timer = 0f;
                    Despawn();
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }
    }
}
