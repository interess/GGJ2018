using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class PhoneChannelUnit : MonoBehaviour
    {
        public AudioClip[] maleAudioClips;
        public AudioClip[] femaleAudioClips;

        private AudioSource __audioSource;
        private bool __currentIsMale;
        private bool __isPlaying;

        private float __timeSinceLastPlay;

        public void Initialize()
        {
            if (maleAudioClips.Length < 1)
            {
                Debug.LogError("PhoneManagerUnit | MaleAudioClips is less than 2. This may cayse errors");
            }

            if (femaleAudioClips.Length < 1)
            {
                Debug.LogError("PhoneManagerUnit | FemaleAudioClips is less than 2. This may cayse errors");
            }

            __audioSource = gameObject.AddComponent<AudioSource>();
            __audioSource.playOnAwake = false;
            __audioSource.loop = true;
            __audioSource.outputAudioMixerGroup = Contexts.state.audioManagerUnit.audioMixerRadioGroup;
        }

        public void SetActive(bool value)
        {
            __audioSource.volume = value ? 1f : 0f;
        }

        public void Stop()
        {
            __audioSource.Stop();
            __isPlaying = false;
            __audioSource.clip = null;
        }

        private void Update()
        {
            __timeSinceLastPlay += Time.deltaTime;

            if (__timeSinceLastPlay > 1f)
            {
                Stop();
            }
        }

        public void PlayMan(bool allowContinue = true)
        {
            __timeSinceLastPlay = 0;
            if (__isPlaying) { return; }
            if (__currentIsMale && allowContinue && __audioSource.clip != null)
            {
                __audioSource.Play();
            }
            else
            {
                var randomNumber = Random.Range(0, maleAudioClips.Length);
                __audioSource.clip = maleAudioClips[randomNumber];
                __audioSource.time = Random.Range(0, 20f);
                __audioSource.Play();
            }

            __isPlaying = true;
            __currentIsMale = true;
        }

        public void PlayWoman(bool allowContinue = true)
        {
            __timeSinceLastPlay = 0;
            if (__isPlaying) { return; }
            if (!__currentIsMale && allowContinue && __audioSource.clip != null)
            {
                __audioSource.Play();
            }
            else
            {
                var randomNumber = Random.Range(0, femaleAudioClips.Length);
                __audioSource.clip = femaleAudioClips[randomNumber];
                __audioSource.time = Random.Range(0, 20f);
                __audioSource.Play();
            }

            __isPlaying = true;
            __currentIsMale = false;
        }
    }
}
