using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ.Game.Systems.Level
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.Level")
        {
            Add(new InitSubsManagerUnit());
            Add(new LoadSandboxSubs());
        }

        public class InitSubsManagerUnit : InitializeSystem
        {
            protected override void Act()
            {
                var managerUnit = GameObject.FindObjectOfType<Scripts.SubsManagerUnit>();

                if (managerUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.StageManagerUnit));
                }

                managerUnit.Initialize();

                var entity = state.CreateEntity();
                entity.subsManagerUnit = managerUnit;
            }
        }

        public class LoadSandboxSubs : InitializeSystem
        {
            protected override void Act()
            {
                state.subsManagerUnit.LoadSubs("1_1", () =>
                {
                    state.subsManagerUnitEntity.flagLoaded = true;
                });
            }
        }
    }
}
