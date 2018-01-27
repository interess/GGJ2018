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
            Add(new InitPhoneManagerUnit());

            Add(new LoadActiveLevel());
            Add(new SwitchChannelOnEvent());
            Add(new SetActiveChannelInfoUnit());
            Add(new SetActiveChannelOnManagerUnit());
            Add(new MoveSubsOnActiveLevel());

            Add(new StartSubsRecordingOnEvent());
            Add(new StopSubsRecordingOnEvent());
            Add(new SetRecordingStateOnSubsManager());

            Add(new RayCastWords());
            Add(new TriggerPhoneTalks());

            Add(new UnloadActiveLevel());

            Add(new LoadSandboxSubs());
        }

        public class InitSubsManagerUnit : InitializeSystem
        {
            protected override void Act()
            {
                var managerUnit = GameObject.FindObjectOfType<Scripts.SubsManagerUnit>();

                if (managerUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.SubsManagerUnit));
                }

                managerUnit.Initialize();

                var entity = state.CreateEntity();
                entity.subsManagerUnit = managerUnit;
            }
        }

        public class InitPhoneManagerUnit : InitializeSystem
        {
            protected override void Act()
            {
                var managerUnit = GameObject.FindObjectOfType<Scripts.PhoneManagerUnit>();

                if (managerUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.PhoneManagerUnit));
                }

                managerUnit.Initialize();

                var entity = state.CreateEntity();
                entity.phoneManagerUnit = managerUnit;
            }
        }

        public class LoadActiveLevel : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Level, StateMatcher.FlagActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                if (state.HasLevelActive())
                {
                    var levelActiveEntity = state.levelActiveEntity;
                    if (!levelActiveEntity.flagLoaded)
                    {
                        Debug.Log("Loading active level");
                        Freaking.Fwait.Until(() =>
                        {
                            return state.subsManagerUnitEntity.flagLoaded;
                        }).Done(() =>
                        {
                            levelActiveEntity.flagLoaded = true;

                            var channelSwitchEventEntity = input.CreateEventEntity();
                            channelSwitchEventEntity.channelSwitchEvent = true;
                        });
                    }
                }
            }
        }

        public class UnloadActiveLevel : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Level, StateMatcher.FlagActive).Removed());
            }

            protected override void Act(List<StateEntity> entities)
            {
                foreach (var entity in entities)
                {
                    // TODO: Clean level

                    entity.flagLoaded = false;
                }

                // TODO: Add loading subs manager
                state.subsManagerUnitEntity.flagLoaded = false;
                state.subsManagerUnit.Reset();
            }
        }

        public class MoveSubsOnActiveLevel : ExecuteSystem
        {
            protected override void Act()
            {
                if (state.HasLevelActiveLoaded())
                {
                    state.subsManagerUnit.MoveSubs(state.worldTimeEntity.worldTimeSpeed);
                }
            }
        }

        public class SetActiveChannelOnManagerUnit : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.FlagActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                state.subsManagerUnit.SetChannel(state.channelActiveEntity.channel);
            }
        }

        public class SwitchChannelOnEvent : InputReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(InputMatcher.AllOf(InputMatcher.ChannelSwitchEvent).Added());
            }

            protected override bool Filter(InputEntity entity)
            {
                return state.HasLevelActive();
            }

            protected override void Act(List<InputEntity> entities)
            {
                foreach (var entity in entities)
                {
                    if (state.HasChannelActive())
                    {
                        Contexts.state.phoneManagerUnit.Stop();
                        state.CreateEffectEntity("ChannelSwitchEffect");

                        var activeIndex = state.channelActiveEntity.channel;
                        state.channelActiveEntity.flagActive = false;

                        var foundNextChannel = false;
                        var failedCounter = 0;

                        while (!foundNextChannel)
                        {
                            if (failedCounter > 2) { activeIndex = 1; }
                            else
                            {
                                failedCounter += 1;

                                activeIndex += 1;
                            }

                            var nextChannelEntity = state.channelIndex.FindSingle(activeIndex);
                            if (nextChannelEntity != null)
                            {
                                nextChannelEntity.flagActive = true;
                                foundNextChannel = true;
                            }
                            else if (failedCounter > 3)
                            {
                                Debug.LogError("No default channel found. This is unexpected and will cause errors");
                            }
                        }
                    }
                    else
                    {
                        var defaultChannelEntity = state.channelIndex.FindSingle(1);
                        if (defaultChannelEntity != null)
                        {
                            defaultChannelEntity.flagActive = true;
                        }
                        else
                        {
                            Debug.LogError("No default channel found. This is unexpected and will cause errors");
                        }
                    }
                }
            }
        }

        public class StartSubsRecordingOnEvent : InputReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(InputMatcher.SubsRecordStartEvent.Added());
            }

            protected override void Act(List<InputEntity> entities)
            {
                if (state.HasChannelActive())
                {
                    state.channelActiveEntity.channelRecording = true;
                }
            }
        }

        public class StopSubsRecordingOnEvent : InputReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(InputMatcher.SubsRecordStopEvent.Added());
            }

            protected override void Act(List<InputEntity> entities)
            {
                if (state.HasChannelActive())
                {
                    state.channelActiveEntity.channelRecording = false;
                }
            }
        }

        public class SetRecordingStateOnSubsManager : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelRecording).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelRecording).Removed());
            }

            protected override bool Filter(StateEntity entity)
            {
                return entity.HasChannel();
            }

            protected override void Act(List<StateEntity> entities)
            {
                state.subsManagerUnit.SetRecording(state.channelActiveEntity.channelRecording);
            }
        }

        public class RayCastWords : ExecuteSystem
        {
            protected override void Act()
            {
                if (state.HasChannelActive())
                {
                    if (state.channelActiveEntity.channelRecording)
                    {
                        var anchor = state.subsManagerUnit.subsSelectorTriggerAnchor;
                        var gameCamera = state.stageManagerUnit.gameCameraUnit.camera;

                        var anchorPositionScreen = gameCamera.WorldToScreenPoint(anchor.position);
                        UnityEngine.EventSystems.PointerEventData pointerData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
                        pointerData.position = anchorPositionScreen;
                        var results = new List<UnityEngine.EventSystems.RaycastResult>();
                        UnityEngine.EventSystems.EventSystem.current.RaycastAll(pointerData, results);

                        foreach (var item in results)
                        {
                            var wordUnit = item.gameObject.GetComponent<Scripts.SubsWordUnit>();
                            if (wordUnit != null)
                            {
                                if (!wordUnit.isEmpty)
                                {
                                    // Debug.Log(wordUnit.text.text); 
                                    wordUnit.SetColor(Color.white);
                                }
                            }
                        }

                    }
                }
            }
        }

        public class TriggerPhoneTalks : ExecuteSystem
        {
            private int __prevDialogIndex;
            protected override void Act()
            {
                if (state.HasChannelActive())
                {
                    var anchor = state.phoneManagerUnit.phoneTriggerAnchor;
                    var gameCamera = state.stageManagerUnit.gameCameraUnit.camera;

                    var anchorPositionScreen = gameCamera.WorldToScreenPoint(anchor.position);
                    UnityEngine.EventSystems.PointerEventData pointerData = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
                    pointerData.position = anchorPositionScreen;
                    var results = new List<UnityEngine.EventSystems.RaycastResult>();
                    UnityEngine.EventSystems.EventSystem.current.RaycastAll(pointerData, results);

                    foreach (var item in results)
                    {
                        var wordUnit = item.gameObject.GetComponent<Scripts.SubsWordUnit>();
                        if (wordUnit != null)
                        {
                            if (!wordUnit.isEmpty)
                            {
                                if (wordUnit.isMale)
                                {
                                    Contexts.state.phoneManagerUnit.PlayMan(__prevDialogIndex == wordUnit.dialogOwnerIndex);
                                }
                                else
                                {
                                    Contexts.state.phoneManagerUnit.PlayWoman(__prevDialogIndex == wordUnit.dialogOwnerIndex);
                                }

                                __prevDialogIndex = wordUnit.dialogOwnerIndex;
                            }
                            else
                            {
                                Contexts.state.phoneManagerUnit.Stop();
                            }
                        }
                        else
                        {
                            Contexts.state.phoneManagerUnit.Stop();
                        }
                    }
                }
                else
                {
                    Contexts.state.phoneManagerUnit.Stop();
                }
            }
        }

        public class SetActiveChannelInfoUnit : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelInfoUnit, StateMatcher.FlagActive).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelInfoUnit).NoneOf(StateMatcher.FlagActive).Added());
            }

            protected override bool Filter(StateEntity entity)
            {
                return entity.HasChannelInfoUnit();
            }

            protected override void Act(List<StateEntity> entities)
            {
                foreach (var entity in entities)
                {
                    entity.channelInfoUnit.SetActive(entity.flagActive);
                }
            }
        }

        // --- SANDBOX

        public class LoadSandboxSubs : InitializeSystem
        {
            protected override void Act()
            {
                var levelEntity = state.CreateEntity();
                levelEntity.level = true;
                levelEntity.flagActive = true;

                state.subsManagerUnit.LoadSubs(1, () =>
                {
                    state.subsManagerUnitEntity.flagLoaded = true;
                });
            }
        }
    }
}
