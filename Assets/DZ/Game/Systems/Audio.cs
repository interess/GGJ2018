using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace DZ.Game.Systems.Audio
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.Audio")
        {
            Add(new InitializeAudioManagerUnit());
            Add(new InitializeMusicManagerUnit());
            Add(new InitializeAudioEffectManagerUnit());
        }
    }

    public class InitializeAudioManagerUnit : InitializeSystem
    {
        protected override void Act()
        {
            var managerUnit = GameObject.FindObjectOfType<Scripts.AudioManagerUnit>();

            if (managerUnit == null)
            {
                throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.AudioManagerUnit));
            }

            var entity = state.CreateEntity();
            entity.audioManagerUnit = managerUnit;

            if (Env.linkEntitiesToGameObjects) { managerUnit.gameObject.LinkSafe(entity, state); }
        }
    }

    public class InitializeMusicManagerUnit : InitializeSystem
    {
        protected override void Act()
        {
            var managerUnit = GameObject.FindObjectOfType<Scripts.MusicManagerUnit>();

            if (managerUnit == null)
            {
                throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.MusicManagerUnit));
            }

            managerUnit.Initialize();
            managerUnit.PlayNormal();

            var entity = state.CreateEntity();
            entity.musicManagerUnit = managerUnit;

            if (Env.linkEntitiesToGameObjects) { managerUnit.gameObject.LinkSafe(entity, state); }
        }
    }

    public class InitializeAudioEffectManagerUnit : InitializeSystem
    {
        protected override void Act()
        {
            var managerUnit = GameObject.FindObjectOfType<Scripts.AudioEffectManagerUnit>();

            if (managerUnit == null)
            {
                throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.AudioEffectManagerUnit));
            }

            managerUnit.Initialize();

            var entity = state.CreateEntity();
            entity.audioEffectManagerUnit = managerUnit;

            if (Env.linkEntitiesToGameObjects) { managerUnit.gameObject.LinkSafe(entity, state); }
        }
    }
}
