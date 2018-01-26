using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ.Game.Systems.Common
{
    public class Before : Entitas.Gentitas.Systems.ChainSystem
    {
        public Before() : base("DZ.Game.Common.Before")
        {
            Add(new UpdateUidUnitOnProductUnit());
            Add(new UpdateUidOnUidUnit());
        }
    }

    public class After : Entitas.Gentitas.Systems.ChainSystem
    {
        public After() : base("DZ.Game.Common.After")
        {
            Add(new CreateUid());
            Add(new TickTrashTimer());
            Add(new DespawnTrashProductUnit());
        }
    }

    public class Trash : Entitas.Gentitas.Systems.ChainSystem
    {
        public Trash() : base("DZ.Game.Common.Trash")
        {
            Add(new DestroyValidatedTrash());
            Add(new ValidateTrash());
            Add(new TrashEvents());
        }
    }

    public class CreateUid : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.LevelPart).NoneOf(StateMatcher.Uid).Added());
        }

        protected override bool Filter(StateEntity entity)
        {
            return !entity.HasUid();
        }

        protected override void Act(List<StateEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.uid = FS.Uid.Scripts.UidGenerator.nextUid;
            }
        }
    }

    public class UpdateUidUnitOnProductUnit : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.ProductUnit).Added());
        }

        protected override bool Filter(StateEntity entity)
        {
            return entity.HasProductUnit();
        }

        protected override void Act(List<StateEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (!entity.HasUidUnit() || entity.uidUnit != entity.productUnit.uidUnit) entity.uidUnit = entity.productUnit.uidUnit;
            }
        }
    }

    public class UpdateUidOnUidUnit : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.UidUnit, StateMatcher.Uid).Added());
        }

        protected override bool Filter(StateEntity entity)
        {
            return entity.HasUid() && entity.HasUidUnit();
        }

        protected override void Act(List<StateEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.uidUnit.uid = entity.uid;
            }
        }
    }

    public class DespawnTrashProductUnit : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.ProductUnit, StateMatcher.FlagTrash).Added());
        }

        protected override bool Filter(StateEntity entity)
        {
            return entity.isEnabled && entity.HasProductUnit() && entity.HasFlagTrash();
        }

        protected override void Act(List<StateEntity> entities)
        {
            foreach (var entity in entities)
            {
                var entityLink = entity.productUnit.gameObject.GetEntityLink();
                if (entityLink != null && entityLink.entity != null)
                {
                    entity.productUnit.gameObject.UnlinkSafe();
                }

                entity.productUnit.Despawn();
            }
        }
    }

    public class TickTrashTimer : ExecuteSystem
    {
        protected override void Act()
        {
            var trashTimerGroup = state.trashTimerGroup;

            foreach (var entity in trashTimerGroup)
            {
                entity.trashTimer -= Time.deltaTime;
                if (entity.trashTimer <= 0)
                {
                    entity.flagTrash = true;
                }
            }
        }
    }

    public class TrashEvents : InputReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(InputMatcher.AllOf(InputMatcher.FlagEvent).Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.HasFlagEvent();
        }

        protected override void Act(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.flagTrash = true;
            }
        }
    }

    public class ValidateTrash : MultiReactiveSystem<FlagTrashEntity>
    {
        protected override void SetTriggers()
        {
            Trigger(Contexts.input.CreateCollector(InputMatcher.AllOf(InputMatcher.FlagTrash).Added()));
            Trigger(Contexts.state.CreateCollector(StateMatcher.AllOf(StateMatcher.FlagTrash).Added()));
        }

        protected override bool Filter(FlagTrashEntity entity)
        {
            return entity.HasFlagTrash();
        }

        protected override void Act(List<FlagTrashEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.flagTrashValidated = true;
            }
        }
    }

    public class DestroyValidatedTrash : MultiReactiveSystem<FlagTrashEntity>
    {
        protected override void SetTriggers()
        {
            Trigger(Contexts.input.CreateCollector(InputMatcher.AllOf(InputMatcher.FlagTrash, InputMatcher.FlagTrashValidated).Added()));
            Trigger(Contexts.state.CreateCollector(StateMatcher.AllOf(StateMatcher.FlagTrash, StateMatcher.FlagTrashValidated).Added()));
        }

        protected override bool Filter(FlagTrashEntity entity)
        {
            return entity.HasFlagTrash() && entity.HasFlagTrashValidated();
        }

        protected override void Act(List<FlagTrashEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Destroy();
            }
        }
    }
}

namespace DZ.Game
{
    public interface FlagTrashEntity : IFlagTrashEntity, IFlagTrashValidatedEntity { }
    public partial class StateEntity : FlagTrashEntity { }
    public partial class InputEntity : FlagTrashEntity { }
}
