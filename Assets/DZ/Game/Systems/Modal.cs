using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace DZ.Game.Systems.Modal
{
    public class Chain : Entitas.Gentitas.Systems.ChainSystem
    {
        public Chain() : base("DZ.Game.Modal")
        {
            Add(new InitializeModalUnits());

            Add(new OpenModal());
            Add(new CloseModal());
            Add(new ShowOverlayOnModalActive());
            Add(new PauseGameOnActiveModal());

            Add(new SetModalUnitState());
        }
    }

    public class PauseGameOnActiveModal : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.ModalId, StateMatcher.FlagActive).Added());
            Trigger(StateMatcher.AllOf(StateMatcher.ModalId).NoneOf(StateMatcher.FlagActive).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            if (state.HasModalActive())
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public class InitializeModalUnits : InitializeSystem
    {
        protected override void Act()
        {
            var modalUnits = GameObject.FindObjectsOfType<Scripts.ModalUnit>();

            foreach (var item in modalUnits)
            {
                if (state.configIdIndex.GetCount(item.id) > 0)
                {
                    Debug.LogErrorFormat("Modal with id '{0}' already registered", item.id);
                    continue;
                }

                var modalEntity = state.CreateEntity();
                modalEntity.modalId = item.id;
                modalEntity.modalUnit = item;

                item.gameObject.LinkSafe(modalEntity, state);
            }

            var overlayUnit = GameObject.FindObjectOfType<Scripts.OverlayUnit>();

            if (overlayUnit == null)
            {
                throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.OverlayUnit));
            }

            overlayUnit.SetActive(false);

            var overlayEntity = state.CreateEntity();
            overlayEntity.overlayUnit = overlayUnit;

            overlayUnit.gameObject.LinkSafe(overlayEntity, state);
        }
    }

    public class OpenModal : InputReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(InputMatcher.AllOf(InputMatcher.ModalOpenEvent, InputMatcher.ModalId).Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.HasModalId() && entity.modalOpenEvent;
        }

        protected override void Act(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                var modalId = entity.modalId;
                var modalEntity = state.modalIdIndex.FindSingle(modalId);

                if (modalEntity != null)
                {
                    modalEntity.flagOpened = true;

                    if (!modalEntity.HasFlagActive() && state.HasModalActive())
                    {
                        state.modalActiveEntity.flagActive = false;
                    }

                    modalEntity.flagActive = true;

                    // TODO: effect manager play
                }
            }
        }
    }

    public class CloseModal : InputReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(InputMatcher.AllOf(InputMatcher.ModalCloseEvent).Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.modalCloseEvent;
        }

        protected override void Act(List<InputEntity> entities)
        {
            if (state.HasModalActive())
            {
                // TODO: effect manager play
            }

            foreach (var entity in entities)
            {
                if (entity.HasModalId())
                {
                    var modalId = entity.modalId;
                    var modalEntity = state.modalIdIndex.FindSingle(modalId);

                    modalEntity.flagOpened = false;
                    modalEntity.flagActive = false;
                }
                else
                {
                    if (state.HasModalActive())
                    {
                        var modalActiveEntity = state.modalActiveEntity;
                        modalActiveEntity.flagOpened = false;
                        modalActiveEntity.flagActive = false;
                    }
                }

                var otherFlagOpenedModalEntites = state.modalOpenedEntities;
                if (otherFlagOpenedModalEntites.Length > 0) otherFlagOpenedModalEntites[0].flagActive = true;
            }
        }
    }

    public class ShowOverlayOnModalActive : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.ModalUnit).NoneOf(StateMatcher.FlagActive).Added());
            Trigger(StateMatcher.AllOf(StateMatcher.ModalUnit, StateMatcher.FlagActive).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            Time.timeScale = state.HasModalActive() ? 0f : 1f;

            if (state.HasOverlayUnit())
            {
                state.overlayUnitEntity.overlayUnit.SetActive(state.HasModalActive());
            }
        }
    }

    public class SetModalUnitState : StateReactiveSystem
    {
        protected override void SetTriggers()
        {
            Trigger(StateMatcher.AllOf(StateMatcher.ModalUnit, StateMatcher.FlagActive).Added());
            Trigger(StateMatcher.AllOf(StateMatcher.ModalUnit, StateMatcher.FlagOpened).Added());
            Trigger(StateMatcher.AllOf(StateMatcher.ModalUnit).NoneOf(StateMatcher.FlagActive).Added());
            Trigger(StateMatcher.AllOf(StateMatcher.ModalUnit).NoneOf(StateMatcher.FlagOpened).Added());
        }

        protected override void Act(List<StateEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.modalUnit.SetOpened(entity.flagOpened);
                entity.modalUnit.SetActive(entity.HasFlagActive());
            }
        }
    }
}
