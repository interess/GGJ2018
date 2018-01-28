using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DZ.Game.Scripts
{
    public class AudioEffectManagerUnit : MonoBehaviour
    {
        private GameObject audioUnitPrefab;
        private FS.PrefabFactory.Scripts.FactoryUnit audioFactoryUnit;

        public void Initialize()
        {
            audioUnitPrefab = CreatePrefab();

            audioFactoryUnit = gameObject.AddComponent<FS.PrefabFactory.Scripts.FactoryUnit>();
            audioFactoryUnit.Initialize(audioUnitPrefab, 10);
        }

        private GameObject CreatePrefab()
        {
            var audioUnitPrefab = new GameObject();
            audioUnitPrefab.transform.SetParent(transform);
            audioUnitPrefab.name = "AudioUnitPrefab";
            audioUnitPrefab.AddComponent<AudioEffectUnit>();
            audioUnitPrefab.AddComponent<AudioEffectProductUnit>();
            var audioSource = audioUnitPrefab.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = Contexts.state.audioManagerUnit.audioMixerEffectsGroup;
            return audioUnitPrefab;
        }

        private AudioEffectProductUnit SpawnAudioUnit()
        {
            return (AudioEffectProductUnit) audioFactoryUnit.Spawn();
        }

        public AudioEffectUnit Create(AudioClip audioClip)
        {
            var audioProductUnit = SpawnAudioUnit();
            var audioEffectUnit = audioProductUnit.audioEffectUnit;
            audioEffectUnit.SetClip(audioClip);
            audioEffectUnit.SetVolume(1f);
            audioEffectUnit.SetPitch(1f);
            audioEffectUnit.SetEffectLoop(false);
            audioEffectUnit.SetDespawnAfterPlay(true);

            return audioEffectUnit;
        }

        public AudioEffectUnit Create(AudioClip audioClip, GameObject targetGameObject)
        {
            return Create(audioClip, targetGameObject.transform);
        }

        public AudioEffectUnit Create(AudioClip audioClip, Transform targetTransform)
        {
            var audioEffectUnit = Create(audioClip);

            if (targetTransform != null)
            {
                audioEffectUnit.transform.SetParent(targetTransform, false);
            }

            return audioEffectUnit;
        }
    }
}
