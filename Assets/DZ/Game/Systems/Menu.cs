using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ.Game.Systems.Menu
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.Menu")
        {
            Add(new InitMenuButtonUnits());
            Add(new InitLightUnits());
            Add(new InvertMenuButtonUnitsOnLevelActive());
            Add(new SetLightsOnLevelActive());
        }

        public class InitMenuButtonUnits : InitializeSystem
        {
            protected override void Act()
            {
                var menuButtonUnits = GameObject.FindObjectsOfType<Scripts.MenuButtonUnit>();
                foreach (var item in menuButtonUnits)
                {
                    item.Initialize();
                    var entity = state.CreateEntity();
                    entity.menuButtonUnit = item;
                    entity.menuButtonId = item.buttonId;
                }
            }
        }

        public class InitLightUnits : InitializeSystem
        {
            protected override void Act()
            {
                var lightUnits = GameObject.FindObjectsOfType<Scripts.LightUnit>();
                foreach (var item in lightUnits)
                {
                    var entity = state.CreateEntity();
                    entity.lightUnit = item;
                    entity.ligthId = item.lightId;
                }
            }
        }

        public class InvertMenuButtonUnitsOnLevelActive : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Level, StateMatcher.FlagActive).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Level).NoneOf(StateMatcher.FlagActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                var inverted = state.HasLevelActive();
                var buttonGroup = state.menuButtonIdGroup;
                foreach (var entity in buttonGroup)
                {
                    entity.menuButtonUnit.SetInverted(inverted);
                }
            }
        }

        public class SetLightsOnLevelActive : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Level, StateMatcher.FlagActive).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Level).NoneOf(StateMatcher.FlagActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                var isLevel = state.HasLevelActive();
                var levelLights = state.ligthIdIndex.Find("Level");
                var menuLights = state.ligthIdIndex.Find("Menu");

                foreach (var item in levelLights)
                {
                    item.lightUnit.SetActive(isLevel);
                }

                foreach (var item in menuLights)
                {
                    item.lightUnit.SetActive(!isLevel);
                }
            }
        }
    }
}
