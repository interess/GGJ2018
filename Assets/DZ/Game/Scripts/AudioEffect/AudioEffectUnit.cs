using UnityEngine;

namespace DZ.Game.Scripts
{
    public class AudioEffectUnit : MonoBehaviour
    {
        private AudioEffectProductUnit __audioEffectProductUnit;
        protected AudioEffectProductUnit _audioEffectProductUnit { get { if (__audioEffectProductUnit == null) __audioEffectProductUnit = GetComponent<AudioEffectProductUnit>(); return __audioEffectProductUnit; } }

        private AudioSource __audioSource;
        public AudioSource audioSource { get { if (__audioSource == null) __audioSource = GetComponent<AudioSource>(); return __audioSource; } }

        private bool __despawnAfterPlay;
        private AudioClip __audioClip;

        private float __timer;
        private float __isPlayingCheckInterval = 2f;
        private bool __isPlaying;

        public AudioEffectUnit Play()
        {
            __isPlaying = true;
            audioSource.Play();
            return this;
        }

        public AudioEffectUnit Stop()
        {
            __isPlaying = false;
            audioSource.Stop();
            return this;
        }

        public void Despawn()
        {
            Stop();
            _audioEffectProductUnit.Despawn();
        }

        public AudioEffectUnit SetClip(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            return this;
        }

        public AudioEffectUnit SetPitch(float value)
        {
            audioSource.pitch = value;
            return this;
        }

        public AudioEffectUnit SetVolume(float value)
        {
            audioSource.volume = value;
            return this;
        }

        public AudioEffectUnit SetEffectLoop(bool value = true)
        {
            audioSource.loop = value;
            return this;
        }

        public AudioEffectUnit SetDespawnAfterPlay(bool value = true)
        {
            __despawnAfterPlay = true;
            return this;
        }

        private void Update()
        {
            if (__despawnAfterPlay)
            {
                if (__timer >= __isPlayingCheckInterval)
                {
                    __timer = 0f;
                    if (!audioSource.isPlaying)
                    {
                        Despawn();
                    }
                }
                else
                {
                    __timer += Time.unscaledDeltaTime;
                }
            }
        }
    }
}
