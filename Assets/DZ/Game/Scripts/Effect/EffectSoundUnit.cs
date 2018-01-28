using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DZ.Game.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class EffectSoundUnit : EffectUnit
    {
        AudioSource _audioSource;
        protected AudioSource audioSource { get { if (_audioSource == null) _audioSource = GetComponent<AudioSource>(); return _audioSource; } }

        public override void Initialize()
        {
            if (_isInitialized) { return; }
            base.Initialize();
            audioSource.outputAudioMixerGroup = Contexts.state.audioManagerUnit.audioMixerEffectsGroup;
        }

        public override EffectUnit Play()
        {
            base.Play();
            audioSource.Play();
            return this;
        }

        public override EffectUnit Stop()
        {
            base.Stop();
            audioSource.Stop();
            return this;
        }
    }
}
