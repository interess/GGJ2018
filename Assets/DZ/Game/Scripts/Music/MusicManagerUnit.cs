using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DZ.Game.Scripts
{
    public class MusicManagerUnit : MonoBehaviour
    {
        public AudioClip[] normalAudioClips;
        public AudioClip[] intenseAudioClips;
        public AudioClip[] menuAudioClips;

        public float transitionDuration;

        AudioManagerUnit audioManagerUnit { get { return Contexts.state.audioManagerUnitEntity.audioManagerUnit; } }

        AudioSource audioSource_1;
        AudioSource audioSource_2;

        int audioSourceActiveIndex = 1;
        AudioSource audioSourceActive { get { return audioSourceActiveIndex == 1 ? audioSource_1 : audioSource_2; } }
        AudioSource audioSourceInactive { get { return audioSourceActiveIndex == 1 ? audioSource_2 : audioSource_1; } }

        private int __activeAudioSorce;

        public void Initialize()
        {
            audioSource_1 = gameObject.AddComponent<AudioSource>();
            audioSource_2 = gameObject.AddComponent<AudioSource>();

            audioSource_1.playOnAwake = false;
            audioSource_1.loop = true;
            audioSource_2.playOnAwake = false;
            audioSource_2.loop = true;

            audioSource_1.outputAudioMixerGroup = audioManagerUnit.audioMixerMusicGroup;
            audioSource_2.outputAudioMixerGroup = audioManagerUnit.audioMixerMusicGroup;
        }

        [FreakingEditor.FbuttonPlay]
        public void PlayMenu()
        {
            Transition(menuAudioClips[0]);
        }

        [FreakingEditor.FbuttonPlay]
        public void PlayNormal()
        {
            Transition(normalAudioClips[0]);
        }

        [FreakingEditor.FbuttonPlay]
        public void PlayIntense()
        {
            Transition(intenseAudioClips[0]);
        }

        private void Transition(AudioClip clip)
        {
            if (__activeAudioSorce == 0)
            {
                audioSource_1.clip = clip;
                audioSource_1.Play();
            }
            else
            {
                if (__activeAudioSorce == 1)
                {
                    __activeAudioSorce = 2;
                    audioSource_2.clip = clip;
                    audioSource_2.volume = 0f;
                    audioSource_2.DOFade(1f, 1f);
                    audioSource_2.Play();
                    audioSource_1.DOFade(0f, 1f).OnComplete(() =>
                    {
                        audioSource_1.Stop();
                    });
                }
                else
                {
                    audioSource_1.clip = clip;
                    audioSource_1.volume = 0f;
                    audioSource_1.DOFade(1f, 1f);
                    audioSource_1.Play();
                    audioSource_2.DOFade(0f, 1f).OnComplete(() =>
                    {
                        audioSource_2.Stop();
                    });
                }
            }
        }

        [FreakingEditor.FbuttonPlay]
        public void Stop()
        {
            if (audioSourceActive.isPlaying) { audioSourceActive.Stop(); }
        }

        void SwitchAudioSource()
        {
            audioSourceInactive.DOKill();
            audioSourceActive.DOKill();

            audioSourceInactive.time = 0f;
            audioSourceInactive.volume = 0;
            audioSourceInactive.DOFade(1f, transitionDuration);
            audioSourceInactive.Play();

            var audioSourceActiveCached = audioSourceActive;
            audioSourceActive.DOFade(0f, transitionDuration).OnComplete(() =>
            {
                audioSourceActiveCached.Stop();
            });

            audioSourceActiveIndex = audioSourceActiveIndex == 1 ? 2 : 1;
        }
    }
}
