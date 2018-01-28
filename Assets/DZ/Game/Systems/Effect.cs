using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace DZ.Game.Systems.Effect
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.Effect")
        {
            Add(new InitializeManagerUnits());
            Add(new PlayEffect());
        }
    }

    public class InitializeManagerUnits : InitializeSystem
    {
        protected override void Act()
        {
            var managers = GameObject.FindObjectsOfType<Scripts.EffectFactoryUnit>();

            foreach (var item in managers)
            {
                item.Initialize();

                if (state.configIdIndex.GetCount(item.id) > 0)
                {
                    Debug.LogErrorFormat("Manager with id '{0}' already registered", item.id);
                    continue;
                }

                var entity = state.CreateConfigEntity(item.id);
                entity.effectFactoryUnit = item;
                entity.factoryId = item.id;
                entity.factoryUnit = item.factoryUnit;

                item.gameObject.LinkSafe(entity, state);
            }
        }
    }

    public class PlayEffect : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.Effect, StateMatcher.EffectId).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            foreach (var entity in entities)
            {
                var id = entity.effectId;
                var factoryUnitEntity = state.factoryIdIndex.FindSingle(id);
                if (factoryUnitEntity == null)
                {
                    Debug.LogErrorFormat("Effect Factory with id {0} was not found", id);
                    continue;
                }

                var productUnit = factoryUnitEntity.effectFactoryUnit.Spawn();
                entity.productUnit = productUnit;

                if (entity.HasPositionWorld())
                {
                    productUnit.transform.position = entity.positionWorld;
                }

                productUnit.effectUnit.SetOnDespawn(() =>
                {
                    if (entity != null && entity.isEnabled && entity.effect)
                    {
                        entity.flagTrash = true;
                    }
                });

                productUnit.effectUnit.Play();
            }
        }
    }
}
