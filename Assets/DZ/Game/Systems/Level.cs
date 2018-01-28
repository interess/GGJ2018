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
            Add(new InitStickUnit());
            Add(new InitCharacterUnit());
            Add(new InitChannelInfoUnit());
            Add(new InitHUD());
            Add(new InitTicketManager());

            Add(new CreateLevelControllers());

            Add(new LoadActiveLevel());
            Add(new SetActiveChannelInfoUnit());
            Add(new SetActiveChannelVoiceUnit());
            Add(new SetActiveChannelOnManagerUnit());
            Add(new MoveSubsOnActiveLevel());

            Add(new UpdatePhoneChannelUnitOnChannel());
            Add(new PlayCharacterAnimationOnChannelActive());

            Add(new StartSubsRecordingOnEvent());
            Add(new StopSubsRecordingOnEvent());
            Add(new SetRecordingStateOnSubsManager());

            Add(new RayCastWords());
            Add(new TriggerPhoneTalks());
            Add(new TriggerScore());

            Add(new UnloadActiveLevel());

            Add(new SwitchChannelOnEvent());

            Add(new HandlePlayButton());
            Add(new HandleContinueButton());

            Add(new DelegateLevelStartLogicToController());
            Add(new DelegateLevelEvents());
        }

        public class InitTicketManager : InitializeSystem
        {
            protected override void Act()
            {
                var ticketManagerUnit = GameObject.FindObjectOfType<Scripts.TicketManagerUnit>();
                if (ticketManagerUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.TicketManagerUnit));
                }

                ticketManagerUnit.Init(0, 0);

                var entity = state.CreateEntity();
                entity.ticketManagerUnit = ticketManagerUnit;
            }
        }

        public class InitHUD : InitializeSystem
        {
            protected override void Act()
            {
                var hudUnit = GameObject.FindObjectOfType<Scripts.HudUnit>();
                if (hudUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.HudUnit));
                }

                hudUnit.SetActive(false, true);

                var entity = state.CreateEntity();
                entity.hudUnit = hudUnit;
            }
        }

        public class InitChannelInfoUnit : InitializeSystem
        {
            protected override void Act()
            {
                var channelInfoUnit = GameObject.FindObjectOfType<Scripts.ChannelInfoUnit>();
                if (channelInfoUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.ChannelInfoUnit));
                }

                channelInfoUnit.Reset();

                var entity = state.CreateEntity();
                entity.channelInfoUnit = channelInfoUnit;
            }
        }

        public class InitCharacterUnit : InitializeSystem
        {
            protected override void Act()
            {
                var characterUnit = GameObject.FindObjectOfType<Scripts.CharacterUnit>();
                if (characterUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.CharacterUnit));
                }

                characterUnit.Initialize();

                var entity = state.CreateEntity();
                entity.characterUnit = characterUnit;
            }
        }

        public class InitStickUnit : InitializeSystem
        {
            protected override void Act()
            {
                var stickUnit = GameObject.FindObjectOfType<Scripts.StickUnit>();
                if (stickUnit == null)
                {
                    throw new FS.Exceptions.ObjectOfTypeNotFoundException(typeof(Scripts.StickUnit));
                }

                stickUnit.Initialize();

                var entity = state.CreateEntity();
                entity.stickUnit = stickUnit;
            }
        }

        public class CreateLevelControllers : InitializeSystem
        {
            protected override void Act()
            {
                var controllers = GameObject.FindObjectsOfType<Scripts.LevelControllerUnit>();

                foreach (var item in controllers)
                {
                    var index = item.index;
                    var entity = state.CreateEntity();
                    entity.level = true;
                    entity.levelIndex = index;
                    entity.levelControllerUnit = item;
                }

            }
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
                        state.subsManagerUnit.LoadSubs(1, () =>
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
                state.hudUnit.SetActive(false);

                var levelPartGroup = state.levelPartGroup;
                foreach (var entity in levelPartGroup)
                {
                    entity.flagTrash = true;
                }
            }
        }

        public class PlayCharacterAnimationOnChannelActive : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelRecording).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelRecording).Removed());
            }

            protected override void Act(List<StateEntity> entities)
            {
                var active = state.HasChannelActive() && state.channelActiveEntity.channelRecording;
                state.stickUnit.SetSelected(active);
                if (active) { state.characterUnit.MoveStick(); }
            }
        }

        public class MoveSubsOnActiveLevel : ExecuteSystem
        {
            protected override void Act()
            {
                if (state.HasLevelActiveLoaded())
                {
                    if (state.levelActiveEntity.HasLevelSubsSpeed() && state.levelActiveEntity.levelSubsSpeed > 0)
                    {
                        state.subsManagerUnit.MoveSubs(state.levelActiveEntity.levelSubsSpeed);
                    }
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
                if (state.HasChannelActive())
                {
                    state.CreateEffectEntity("ChannelSwitchEffect");
                    state.stickUnit.Move(Random.Range(1, 3));
                    state.characterUnit.MoveStick();

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

        public class UpdatePhoneChannelUnitOnChannel : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.PhoneChannelUnit, StateMatcher.FlagActive).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.PhoneChannelUnit).NoneOf(StateMatcher.FlagActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                foreach (var entity in entities)
                {
                    entity.phoneChannelUnit.SetActive(entity.flagActive);
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
                                if (wordUnit.channelIndex != state.channelActiveEntity.channel) { continue; }
                                if (!wordUnit.isEmpty)
                                {
                                    // Debug.Log(wordUnit.text.text); 
                                    wordUnit.SetColor(Color.white);
                                    wordUnit.isMarked = true;
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
                if (!state.HasLevelActiveLoaded()) { return; }

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
                        var channelEntity = state.channelIndex.FindSingle(wordUnit.channelIndex);
                        if (channelEntity == null) { continue; }

                        if (!wordUnit.isEmpty)
                        {
                            channelEntity.channelVoiceActive = true;
                            if (wordUnit.isMale)
                            {
                                channelEntity.phoneChannelUnit.PlayMan(__prevDialogIndex == wordUnit.dialogOwnerIndex);
                            }
                            else
                            {
                                channelEntity.phoneChannelUnit.PlayWoman(__prevDialogIndex == wordUnit.dialogOwnerIndex);
                            }

                            __prevDialogIndex = wordUnit.dialogOwnerIndex;
                        }
                        else
                        {
                            channelEntity.channelVoiceActive = false;
                            channelEntity.phoneChannelUnit.Stop();
                        }
                    }
                }
            }
        }

        public class TriggerScore : ExecuteSystem
        {
            private int __prevDialogIndex;

            protected override void Act()
            {
                if (!state.HasLevelActiveLoaded()) { return; }

                var anchor = state.phoneManagerUnit.scoreTriggerAnchor;
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
                        if (wordUnit.isTarget)
                        {
                            if (!wordUnit.isMarked)
                            {
                                Debug.Log("Big fuckup: word " + wordUnit.text.text);
                            }
                        }
                        else
                        {
                            if (wordUnit.isMarked)
                            {
                                Debug.Log("Small fuckup: word " + wordUnit.text.text);
                            }
                        }
                    }
                }
            }
        }

        public class SetActiveChannelVoiceUnit : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.ChannelVoiceActive).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Channel).NoneOf(StateMatcher.ChannelVoiceActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                foreach (var entity in entities)
                {
                    state.channelInfoUnit.SetVoiceActive(entity.channelVoiceActive, entity.channel);
                }

            }
        }

        public class SetActiveChannelInfoUnit : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Channel, StateMatcher.FlagActive).Added());
                Trigger(StateMatcher.AllOf(StateMatcher.Channel).NoneOf(StateMatcher.FlagActive).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                foreach (var entity in entities)
                {
                    state.channelInfoUnit.SetActive(entity.channel);
                }
            }
        }

        public class HandlePlayButton : InputReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(InputMatcher.AllOf(InputMatcher.EventId).Added());
            }

            protected override bool Filter(InputEntity entity)
            {
                return entity.eventId == "PlayButton";
            }

            protected override void Act(List<InputEntity> entities)
            {
                var currentLevelIndex = PlayerPrefs.GetInt("LastPassedLevel");
                if (currentLevelIndex == 0) { currentLevelIndex = 1; }

                var levelEntity = state.levelIndexIndex.FindSingle(currentLevelIndex);
                if (levelEntity == null)
                {
                    Debug.LogErrorFormat("Level with index {0} was not found", currentLevelIndex);
                    return;
                }
                levelEntity.flagActive = true;
            }
        }

        public class HandleContinueButton : InputReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(InputMatcher.AllOf(InputMatcher.EventId).Added());
            }

            protected override bool Filter(InputEntity entity)
            {
                return entity.eventId == "ContinueButton";
            }

            protected override void Act(List<InputEntity> entities)
            {
                if (state.HasLevelActive())
                {
                    state.levelActiveEntity.flagActive = false;
                }
            }
        }

        public class DelegateLevelStartLogicToController : StateReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(StateMatcher.AllOf(StateMatcher.Level, StateMatcher.FlagActive, StateMatcher.FlagLoaded).Added());
            }

            protected override void Act(List<StateEntity> entities)
            {
                if (state.HasLevelActiveLoaded())
                {
                    state.levelActiveLoadedEntity.levelControllerUnit.OnStart();
                }
            }
        }

        public class DelegateLevelEvents : InputReactiveSystem
        {
            protected override void SetTriggers()
            {
                Trigger(InputMatcher.LevelEvent.Added());
            }

            protected override void Act(List<InputEntity> entities)
            {
                if (state.HasLevelActiveLoaded())
                {
                    foreach (var entity in entities)
                    {
                        state.levelActiveLoadedEntity.levelControllerUnit.HandleLevelEvent(entity);
                    }
                }
            }
        }
    }
}
