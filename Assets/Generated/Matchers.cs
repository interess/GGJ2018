// <auto-generated>
//     This code was generated with love by Gentitas.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using Entitas;
using System.Collections.Generic;

namespace DZ.Core {
    public sealed partial class StateMatcher {
        public static Entitas.IAllOfMatcher<StateEntity> AllOf(params int[] indices) {
            return Entitas.Matcher<StateEntity>.AllOf(indices);
        }

        public static Entitas.IAllOfMatcher<StateEntity> AllOf(params Entitas.IMatcher<StateEntity>[] matchers) {
            return Entitas.Matcher<StateEntity>.AllOf(matchers);
        }

        public static Entitas.IAnyOfMatcher<StateEntity> AnyOf(params int[] indices) {
            return Entitas.Matcher<StateEntity>.AnyOf(indices);
        }

        public static Entitas.IAnyOfMatcher<StateEntity> AnyOf(params Entitas.IMatcher<StateEntity>[] matchers) {
            return Entitas.Matcher<StateEntity>.AnyOf(matchers);
        }
        
        static IMatcher<StateEntity> _matcherLoaded;

        public static IMatcher<StateEntity> Loaded {
            get {
                if(_matcherLoaded == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.Loaded);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLoaded = matcher;
                }

                return _matcherLoaded;
            }
        }

        static IMatcher<StateEntity> _matcherApplication;

        public static IMatcher<StateEntity> Application {
            get {
                if(_matcherApplication == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.Application);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherApplication = matcher;
                }

                return _matcherApplication;
            }
        }

        static IMatcher<StateEntity> _matcherLoadingProgress;

        public static IMatcher<StateEntity> LoadingProgress {
            get {
                if(_matcherLoadingProgress == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.LoadingProgress);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLoadingProgress = matcher;
                }

                return _matcherLoadingProgress;
            }
        }

        static IMatcher<StateEntity> _matcherSceneName;

        public static IMatcher<StateEntity> SceneName {
            get {
                if(_matcherSceneName == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.SceneName);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherSceneName = matcher;
                }

                return _matcherSceneName;
            }
        }

        static IMatcher<StateEntity> _matcherLoadingManagerUnit;

        public static IMatcher<StateEntity> LoadingManagerUnit {
            get {
                if(_matcherLoadingManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.LoadingManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLoadingManagerUnit = matcher;
                }

                return _matcherLoadingManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherLoadingSeconds;

        public static IMatcher<StateEntity> LoadingSeconds {
            get {
                if(_matcherLoadingSeconds == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.LoadingSeconds);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLoadingSeconds = matcher;
                }

                return _matcherLoadingSeconds;
            }
        }

    }

}

namespace DZ.Game {
    public sealed partial class InputMatcher {
        public static Entitas.IAllOfMatcher<InputEntity> AllOf(params int[] indices) {
            return Entitas.Matcher<InputEntity>.AllOf(indices);
        }

        public static Entitas.IAllOfMatcher<InputEntity> AllOf(params Entitas.IMatcher<InputEntity>[] matchers) {
            return Entitas.Matcher<InputEntity>.AllOf(matchers);
        }

        public static Entitas.IAnyOfMatcher<InputEntity> AnyOf(params int[] indices) {
            return Entitas.Matcher<InputEntity>.AnyOf(indices);
        }

        public static Entitas.IAnyOfMatcher<InputEntity> AnyOf(params Entitas.IMatcher<InputEntity>[] matchers) {
            return Entitas.Matcher<InputEntity>.AnyOf(matchers);
        }
        
        static IMatcher<InputEntity> _matcherFlagEvent;

        public static IMatcher<InputEntity> FlagEvent {
            get {
                if(_matcherFlagEvent == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.FlagEvent);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherFlagEvent = matcher;
                }

                return _matcherFlagEvent;
            }
        }

        static IMatcher<InputEntity> _matcherFlagTrash;

        public static IMatcher<InputEntity> FlagTrash {
            get {
                if(_matcherFlagTrash == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.FlagTrash);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherFlagTrash = matcher;
                }

                return _matcherFlagTrash;
            }
        }

        static IMatcher<InputEntity> _matcherFlagTrashValidated;

        public static IMatcher<InputEntity> FlagTrashValidated {
            get {
                if(_matcherFlagTrashValidated == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.FlagTrashValidated);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherFlagTrashValidated = matcher;
                }

                return _matcherFlagTrashValidated;
            }
        }

        static IMatcher<InputEntity> _matcherTrashTimer;

        public static IMatcher<InputEntity> TrashTimer {
            get {
                if(_matcherTrashTimer == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.TrashTimer);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherTrashTimer = matcher;
                }

                return _matcherTrashTimer;
            }
        }

        static IMatcher<InputEntity> _matcherEventId;

        public static IMatcher<InputEntity> EventId {
            get {
                if(_matcherEventId == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.EventId);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherEventId = matcher;
                }

                return _matcherEventId;
            }
        }

        static IMatcher<InputEntity> _matcherChannelSwitchEvent;

        public static IMatcher<InputEntity> ChannelSwitchEvent {
            get {
                if(_matcherChannelSwitchEvent == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.ChannelSwitchEvent);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherChannelSwitchEvent = matcher;
                }

                return _matcherChannelSwitchEvent;
            }
        }

        static IMatcher<InputEntity> _matcherSubsRecordStartEvent;

        public static IMatcher<InputEntity> SubsRecordStartEvent {
            get {
                if(_matcherSubsRecordStartEvent == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.SubsRecordStartEvent);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherSubsRecordStartEvent = matcher;
                }

                return _matcherSubsRecordStartEvent;
            }
        }

        static IMatcher<InputEntity> _matcherSubsRecordStopEvent;

        public static IMatcher<InputEntity> SubsRecordStopEvent {
            get {
                if(_matcherSubsRecordStopEvent == null) {
                    var matcher = (Matcher<InputEntity>)Matcher<InputEntity>.AllOf(InputComponentsLookup.SubsRecordStopEvent);
                    matcher.componentNames = InputComponentsLookup.componentNames;
                    _matcherSubsRecordStopEvent = matcher;
                }

                return _matcherSubsRecordStopEvent;
            }
        }

    }

    public sealed partial class StateMatcher {
        public static Entitas.IAllOfMatcher<StateEntity> AllOf(params int[] indices) {
            return Entitas.Matcher<StateEntity>.AllOf(indices);
        }

        public static Entitas.IAllOfMatcher<StateEntity> AllOf(params Entitas.IMatcher<StateEntity>[] matchers) {
            return Entitas.Matcher<StateEntity>.AllOf(matchers);
        }

        public static Entitas.IAnyOfMatcher<StateEntity> AnyOf(params int[] indices) {
            return Entitas.Matcher<StateEntity>.AnyOf(indices);
        }

        public static Entitas.IAnyOfMatcher<StateEntity> AnyOf(params Entitas.IMatcher<StateEntity>[] matchers) {
            return Entitas.Matcher<StateEntity>.AnyOf(matchers);
        }
        
        static IMatcher<StateEntity> _matcherFlagTrash;

        public static IMatcher<StateEntity> FlagTrash {
            get {
                if(_matcherFlagTrash == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.FlagTrash);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherFlagTrash = matcher;
                }

                return _matcherFlagTrash;
            }
        }

        static IMatcher<StateEntity> _matcherFlagTrashValidated;

        public static IMatcher<StateEntity> FlagTrashValidated {
            get {
                if(_matcherFlagTrashValidated == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.FlagTrashValidated);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherFlagTrashValidated = matcher;
                }

                return _matcherFlagTrashValidated;
            }
        }

        static IMatcher<StateEntity> _matcherFlagLoaded;

        public static IMatcher<StateEntity> FlagLoaded {
            get {
                if(_matcherFlagLoaded == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.FlagLoaded);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherFlagLoaded = matcher;
                }

                return _matcherFlagLoaded;
            }
        }

        static IMatcher<StateEntity> _matcherFlagActive;

        public static IMatcher<StateEntity> FlagActive {
            get {
                if(_matcherFlagActive == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.FlagActive);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherFlagActive = matcher;
                }

                return _matcherFlagActive;
            }
        }

        static IMatcher<StateEntity> _matcherTrashTimer;

        public static IMatcher<StateEntity> TrashTimer {
            get {
                if(_matcherTrashTimer == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.TrashTimer);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherTrashTimer = matcher;
                }

                return _matcherTrashTimer;
            }
        }

        static IMatcher<StateEntity> _matcherFactoryUnit;

        public static IMatcher<StateEntity> FactoryUnit {
            get {
                if(_matcherFactoryUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.FactoryUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherFactoryUnit = matcher;
                }

                return _matcherFactoryUnit;
            }
        }

        static IMatcher<StateEntity> _matcherFactoryId;

        public static IMatcher<StateEntity> FactoryId {
            get {
                if(_matcherFactoryId == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.FactoryId);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherFactoryId = matcher;
                }

                return _matcherFactoryId;
            }
        }

        static IMatcher<StateEntity> _matcherStageManagerUnit;

        public static IMatcher<StateEntity> StageManagerUnit {
            get {
                if(_matcherStageManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.StageManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherStageManagerUnit = matcher;
                }

                return _matcherStageManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherLevelPart;

        public static IMatcher<StateEntity> LevelPart {
            get {
                if(_matcherLevelPart == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.LevelPart);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLevelPart = matcher;
                }

                return _matcherLevelPart;
            }
        }

        static IMatcher<StateEntity> _matcherLevel;

        public static IMatcher<StateEntity> Level {
            get {
                if(_matcherLevel == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.Level);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLevel = matcher;
                }

                return _matcherLevel;
            }
        }

        static IMatcher<StateEntity> _matcherLevelIndex;

        public static IMatcher<StateEntity> LevelIndex {
            get {
                if(_matcherLevelIndex == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.LevelIndex);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherLevelIndex = matcher;
                }

                return _matcherLevelIndex;
            }
        }

        static IMatcher<StateEntity> _matcherUid;

        public static IMatcher<StateEntity> Uid {
            get {
                if(_matcherUid == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.Uid);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherUid = matcher;
                }

                return _matcherUid;
            }
        }

        static IMatcher<StateEntity> _matcherUidUnit;

        public static IMatcher<StateEntity> UidUnit {
            get {
                if(_matcherUidUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.UidUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherUidUnit = matcher;
                }

                return _matcherUidUnit;
            }
        }

        static IMatcher<StateEntity> _matcherProductUnit;

        public static IMatcher<StateEntity> ProductUnit {
            get {
                if(_matcherProductUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.ProductUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherProductUnit = matcher;
                }

                return _matcherProductUnit;
            }
        }

        static IMatcher<StateEntity> _matcherView;

        public static IMatcher<StateEntity> View {
            get {
                if(_matcherView == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.View);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherView = matcher;
                }

                return _matcherView;
            }
        }

        static IMatcher<StateEntity> _matcherAudioManagerUnit;

        public static IMatcher<StateEntity> AudioManagerUnit {
            get {
                if(_matcherAudioManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.AudioManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherAudioManagerUnit = matcher;
                }

                return _matcherAudioManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherAudioEffectManagerUnit;

        public static IMatcher<StateEntity> AudioEffectManagerUnit {
            get {
                if(_matcherAudioEffectManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.AudioEffectManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherAudioEffectManagerUnit = matcher;
                }

                return _matcherAudioEffectManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherAudioEffectUnit;

        public static IMatcher<StateEntity> AudioEffectUnit {
            get {
                if(_matcherAudioEffectUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.AudioEffectUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherAudioEffectUnit = matcher;
                }

                return _matcherAudioEffectUnit;
            }
        }

        static IMatcher<StateEntity> _matcherAudioEffectProductUnit;

        public static IMatcher<StateEntity> AudioEffectProductUnit {
            get {
                if(_matcherAudioEffectProductUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.AudioEffectProductUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherAudioEffectProductUnit = matcher;
                }

                return _matcherAudioEffectProductUnit;
            }
        }

        static IMatcher<StateEntity> _matcherMusicManagerUnit;

        public static IMatcher<StateEntity> MusicManagerUnit {
            get {
                if(_matcherMusicManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.MusicManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherMusicManagerUnit = matcher;
                }

                return _matcherMusicManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherSubsManagerUnit;

        public static IMatcher<StateEntity> SubsManagerUnit {
            get {
                if(_matcherSubsManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.SubsManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherSubsManagerUnit = matcher;
                }

                return _matcherSubsManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherChannel;

        public static IMatcher<StateEntity> Channel {
            get {
                if(_matcherChannel == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.Channel);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherChannel = matcher;
                }

                return _matcherChannel;
            }
        }

        static IMatcher<StateEntity> _matcherChannelInfoUnit;

        public static IMatcher<StateEntity> ChannelInfoUnit {
            get {
                if(_matcherChannelInfoUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.ChannelInfoUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherChannelInfoUnit = matcher;
                }

                return _matcherChannelInfoUnit;
            }
        }

        static IMatcher<StateEntity> _matcherChannelRecording;

        public static IMatcher<StateEntity> ChannelRecording {
            get {
                if(_matcherChannelRecording == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.ChannelRecording);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherChannelRecording = matcher;
                }

                return _matcherChannelRecording;
            }
        }

        static IMatcher<StateEntity> _matcherPhoneManagerUnit;

        public static IMatcher<StateEntity> PhoneManagerUnit {
            get {
                if(_matcherPhoneManagerUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.PhoneManagerUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherPhoneManagerUnit = matcher;
                }

                return _matcherPhoneManagerUnit;
            }
        }

        static IMatcher<StateEntity> _matcherWorldTime;

        public static IMatcher<StateEntity> WorldTime {
            get {
                if(_matcherWorldTime == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.WorldTime);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherWorldTime = matcher;
                }

                return _matcherWorldTime;
            }
        }

        static IMatcher<StateEntity> _matcherWorldTimeSpeed;

        public static IMatcher<StateEntity> WorldTimeSpeed {
            get {
                if(_matcherWorldTimeSpeed == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.WorldTimeSpeed);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherWorldTimeSpeed = matcher;
                }

                return _matcherWorldTimeSpeed;
            }
        }

        static IMatcher<StateEntity> _matcherEffect;

        public static IMatcher<StateEntity> Effect {
            get {
                if(_matcherEffect == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.Effect);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherEffect = matcher;
                }

                return _matcherEffect;
            }
        }

        static IMatcher<StateEntity> _matcherEffectId;

        public static IMatcher<StateEntity> EffectId {
            get {
                if(_matcherEffectId == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.EffectId);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherEffectId = matcher;
                }

                return _matcherEffectId;
            }
        }

        static IMatcher<StateEntity> _matcherEffectFactoryUnit;

        public static IMatcher<StateEntity> EffectFactoryUnit {
            get {
                if(_matcherEffectFactoryUnit == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.EffectFactoryUnit);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherEffectFactoryUnit = matcher;
                }

                return _matcherEffectFactoryUnit;
            }
        }

        static IMatcher<StateEntity> _matcherConfigId;

        public static IMatcher<StateEntity> ConfigId {
            get {
                if(_matcherConfigId == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.ConfigId);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherConfigId = matcher;
                }

                return _matcherConfigId;
            }
        }

        static IMatcher<StateEntity> _matcherPositionWorld;

        public static IMatcher<StateEntity> PositionWorld {
            get {
                if(_matcherPositionWorld == null) {
                    var matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(StateComponentsLookup.PositionWorld);
                    matcher.componentNames = StateComponentsLookup.componentNames;
                    _matcherPositionWorld = matcher;
                }

                return _matcherPositionWorld;
            }
        }

    }

}

