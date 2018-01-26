using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ.Core.Systems.Common
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Core.Common")
        {
            Add(new InitLoadingManagerUnit());

            Add(new LoadGameScene());

            Add(new CreateApplication());
            Add(new UpdateApplicationLoadingProgress());
            Add(new MakeApplicationLoaded());
            Add(new DisableLoadingScreen());
        }
    }

    public class InitLoadingManagerUnit : InitializeSystem
    {
        protected override void Act()
        {
            var managerUnit = GameObject.FindObjectOfType<Scripts.LoadingManagerUnit>();

            if (managerUnit == null)
            {
                throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.LoadingManagerUnit));
            }

            var entity = state.CreateEntity();
            entity.loadingManagerUnit = managerUnit;
        }
    }

    public class LoadGameScene : InitializeSystem
    {
        protected override void Act()
        {
            Scripts.SceneLoader.Load("Game");
        }
    }

    public class CreateApplication : InitializeSystem
    {
        protected override void Act()
        {
            var entity = Contexts.state.CreateEntity();
            entity.application = true;
        }
    }

    public class MakeApplicationLoaded : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.Application, StateMatcher.LoadingProgress).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            if (state.applicationEntity.loadingProgress == 1f)
            {
                Contexts.state.applicationEntity.loaded = true;
            }
        }
    }

    public class DisableLoadingScreen : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.LoadingManagerUnit).Added());
            Trigger(StateMatcher.AllOf(StateMatcher.Application, StateMatcher.Loaded).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            var canDisable = false;

            foreach (var entity in entities)
            {
                if (entity.HasLoadingManagerUnit())
                {
                    if (state.HasApplication() && Contexts.state.applicationEntity.HasLoaded())
                    {
                        canDisable = true;
                        break;
                    }
                }
                else if (entity.HasApplication())
                {
                    if (state.HasLoadingManagerUnit())
                    {
                        canDisable = true;
                        break;
                    }
                }
            }

            if (canDisable)
            {
                Contexts.state.loadingManagerUnitEntity.loadingManagerUnit.DoHide();
            }
        }
    }

    public class UpdateApplicationLoadingProgress : ExecuteSystem
    {
        bool hasLoaded;

        protected override void Act()
        {
            if (hasLoaded) { return; }

            if (!state.HasApplication() || Contexts.state.applicationEntity.loaded)
            {
                hasLoaded = true;
                return;
            }

            var applicationEntity = Contexts.state.applicationEntity;
            var loadingProgress = 0f;

            // To 0.6 - by time, remaining by events

            var containerGameSceneEntity = Contexts.state.sceneNameIndex.FindSingle("Game");

            if (containerGameSceneEntity.loaded)
            {
                loadingProgress += 1f;
            }

            applicationEntity.loadingProgress = loadingProgress;
        }
    }
}
