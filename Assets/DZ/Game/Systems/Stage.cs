using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ.Game.Systems.Stage
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.Stage")
        {
            Add(new InitStageManagerUnit());
            Add(new SetGameStage());
        }

        public class InitStageManagerUnit : InitializeSystem
        {
            protected override void Act()
            {
                var managerUnit = GameObject.FindObjectOfType<Scripts.StageManagerUnit>();

                if (managerUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.StageManagerUnit));
                }

                managerUnit.Initialize();

                var entity = state.CreateEntity();
                entity.stageManagerUnit = managerUnit;
            }
        }

        public class SetGameStage : DZ.Core.Systems.StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(DZ.Core.StateMatcher.AllOf(DZ.Core.StateMatcher.Application, DZ.Core.StateMatcher.Loaded).Added());
            }

            protected override void Act(List<DZ.Core.StateEntity> entities)
            {
                if (DZ.Core.Contexts.state.applicationEntity.loaded)
                {
                    Contexts.state.stageManagerUnit.SetLoaded(true);
                }
            }
        }
    }
}
