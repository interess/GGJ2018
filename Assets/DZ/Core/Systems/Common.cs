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
            Add(new SetLoadingProgressOnLoadingUnit());
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
            if (state.applicationEntity.loadingProgress >= 1f || Env.skipLoadingScreen)
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

            if (!applicationEntity.HasLoadingSeconds())
            {
                applicationEntity.loadingSeconds = Time.deltaTime;
            }
            else
            {
                applicationEntity.loadingSeconds += Time.deltaTime;
            }

            var baseWaitingTime = 1.5f;

            var loadingProgress = 0f;
            var waitingProgressFactor = applicationEntity.loadingSeconds / baseWaitingTime;
            waitingProgressFactor *= 0.5f;

            loadingProgress += waitingProgressFactor;

            var containerGameSceneEntity = Contexts.state.sceneNameIndex.FindSingle("Game");

            if (containerGameSceneEntity.loaded)
            {
                loadingProgress += 0.5f;
            }

            if (loadingProgress > 1f) { loadingProgress = 1f; }

            applicationEntity.loadingProgress = loadingProgress;
        }
    }

    public class SetLoadingProgressOnLoadingUnit : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.Application, StateMatcher.LoadingProgress).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            var applicationEntity = state.applicationEntity;
            state.loadingManagerUnit.SetProgress((int) (applicationEntity.loadingProgress * 100));
        }
    }
}
