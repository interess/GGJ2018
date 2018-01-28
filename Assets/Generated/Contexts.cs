// <auto-generated>
//     This code was generated with love by Gentitas.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using Entitas;
using System.Collections.Generic;

namespace DZ.Core {
    public partial class Contexts : IContexts {
        public static Contexts _sharedInstance {
            get {
                if(__sharedInstance == null) {
                    __sharedInstance = new Contexts();
                }

                return __sharedInstance;
            }
            set { 
                __sharedInstance = value; 
            }
        }

        static Contexts __sharedInstance;

        public static void CreateContextObserver(IContext context) {
#if(!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
            if(UnityEngine.Application.isPlaying) {
                var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
                UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
            }
#endif
        }

        // Contexts
        StateContext _state;
        public static StateContext state { get { return _sharedInstance._state; } }


        // IndiciesValue

        public IContext[] allContexts { get { return new IContext [] { _state }; } }

        public Contexts() {
            _state = new StateContext();

            CreateContextObserver(_state);
        }

        public static void Reset() {
            var contexts = _sharedInstance.allContexts;
            for (int i = 0; i < contexts.Length; i++) {
                contexts[i].Reset();
            }
        }

        public static void HardReset() {
            __sharedInstance = new Contexts();
        }
    }

    public partial class StateContext : Context<StateEntity>  {
        public StateContext()
            : base(
                StateComponentsLookup.TotalComponents,
                0,
                new ContextInfo(
                    "DZ.Core.State",
                    StateComponentsLookup.componentNames,
                    StateComponentsLookup.componentTypes
                ),
                (entity) =>

    #if (ENTITAS_FAST_AND_UNSAFE)
                    new Entitas.UnsafeAERC()
    #else
                    new Entitas.SafeAERC(entity)
    #endif

            ) 
        {
            sceneNameIndex = new DZ.Core.Indexes.State.SceneNameIndex(this);
        }

        /// Value: SceneName  
        public DZ.Core.Indexes.State.ISceneNameIndex sceneNameIndex;

    }

    public static class StateComponentsLookup {
        public const int Loaded = 0;
        public const int Application = 1;
        public const int LoadingProgress = 2;
        public const int SceneName = 3;
        public const int LoadingManagerUnit = 4;
        public const int LoadingSeconds = 5;

        public const int TotalComponents = 6;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"Loaded", 0 },
            {"Application", 1 },
            {"LoadingProgress", 2 },
            {"SceneName", 3 },
            {"LoadingManagerUnit", 4 },
            {"LoadingSeconds", 5 }
        };

        public static readonly string[] componentNames = {
            "Loaded",
            "Application",
            "LoadingProgress",
            "SceneName",
            "LoadingManagerUnit",
            "LoadingSeconds"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.State.Loaded),
            typeof(Components.State.Application),
            typeof(Components.State.LoadingProgress),
            typeof(Components.State.SceneName),
            typeof(Components.State.LoadingManagerUnit),
            typeof(Components.State.LoadingSeconds)
        };

        public static int GetComponentIndex(string name) {
            int resultIndex = -1;
            if (componentsDict.TryGetValue(name, out resultIndex)) {
                return resultIndex;
            }

            return -1;
        }

        public static int GetComponentIndex(System.Type t) {
            for (var i = 0; i < componentTypes.Length; i++) {
                if (componentTypes[i] == t) return i;
            }

            return -1;
        }
    }
}
namespace DZ.Game {
    public partial class Contexts : IContexts {
        public static Contexts _sharedInstance {
            get {
                if(__sharedInstance == null) {
                    __sharedInstance = new Contexts();
                }

                return __sharedInstance;
            }
            set { 
                __sharedInstance = value; 
            }
        }

        static Contexts __sharedInstance;

        public static void CreateContextObserver(IContext context) {
#if(!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
            if(UnityEngine.Application.isPlaying) {
                var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
                UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
            }
#endif
        }

        // Contexts
        InputContext _input;
        public static InputContext input { get { return _sharedInstance._input; } }

        StateContext _state;
        public static StateContext state { get { return _sharedInstance._state; } }


        // IndiciesValue

        public IContext[] allContexts { get { return new IContext [] { _input, _state }; } }

        public Contexts() {
            _input = new InputContext();
            _state = new StateContext();

            CreateContextObserver(_input);
            CreateContextObserver(_state);
        }

        public static void Reset() {
            var contexts = _sharedInstance.allContexts;
            for (int i = 0; i < contexts.Length; i++) {
                contexts[i].Reset();
            }
        }

        public static void HardReset() {
            __sharedInstance = new Contexts();
        }
    }

    public partial class InputContext : Context<InputEntity>  {
        public InputContext()
            : base(
                InputComponentsLookup.TotalComponents,
                0,
                new ContextInfo(
                    "DZ.Game.Input",
                    InputComponentsLookup.componentNames,
                    InputComponentsLookup.componentTypes
                ),
                (entity) =>

    #if (ENTITAS_FAST_AND_UNSAFE)
                    new Entitas.UnsafeAERC()
    #else
                    new Entitas.SafeAERC(entity)
    #endif

            ) 
        {
        }


    }

    public static class InputComponentsLookup {
        public const int FlagEvent = 0;
        public const int FlagTrash = 1;
        public const int FlagTrashValidated = 2;
        public const int TrashTimer = 3;
        public const int EventId = 4;
        public const int ChannelSwitchEvent = 5;
        public const int SubsRecordStartEvent = 6;
        public const int SubsRecordStopEvent = 7;
        public const int ModalOpenEvent = 8;
        public const int ModalCloseEvent = 9;
        public const int ModalId = 10;
        public const int LevelEvent = 11;

        public const int TotalComponents = 12;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"FlagEvent", 0 },
            {"FlagTrash", 1 },
            {"FlagTrashValidated", 2 },
            {"TrashTimer", 3 },
            {"EventId", 4 },
            {"ChannelSwitchEvent", 5 },
            {"SubsRecordStartEvent", 6 },
            {"SubsRecordStopEvent", 7 },
            {"ModalOpenEvent", 8 },
            {"ModalCloseEvent", 9 },
            {"ModalId", 10 },
            {"LevelEvent", 11 }
        };

        public static readonly string[] componentNames = {
            "FlagEvent",
            "FlagTrash",
            "FlagTrashValidated",
            "TrashTimer",
            "EventId",
            "ChannelSwitchEvent",
            "SubsRecordStartEvent",
            "SubsRecordStopEvent",
            "ModalOpenEvent",
            "ModalCloseEvent",
            "ModalId",
            "LevelEvent"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.Input.FlagEvent),
            typeof(Components.Input.FlagTrash),
            typeof(Components.Input.FlagTrashValidated),
            typeof(Components.Input.TrashTimer),
            typeof(Components.Input.EventId),
            typeof(Components.Input.ChannelSwitchEvent),
            typeof(Components.Input.SubsRecordStartEvent),
            typeof(Components.Input.SubsRecordStopEvent),
            typeof(Components.Input.ModalOpenEvent),
            typeof(Components.Input.ModalCloseEvent),
            typeof(Components.Input.ModalId),
            typeof(Components.Input.LevelEvent)
        };

        public static int GetComponentIndex(string name) {
            int resultIndex = -1;
            if (componentsDict.TryGetValue(name, out resultIndex)) {
                return resultIndex;
            }

            return -1;
        }

        public static int GetComponentIndex(System.Type t) {
            for (var i = 0; i < componentTypes.Length; i++) {
                if (componentTypes[i] == t) return i;
            }

            return -1;
        }
    }
    public partial class StateContext : Context<StateEntity>  {
        public StateContext()
            : base(
                StateComponentsLookup.TotalComponents,
                0,
                new ContextInfo(
                    "DZ.Game.State",
                    StateComponentsLookup.componentNames,
                    StateComponentsLookup.componentTypes
                ),
                (entity) =>

    #if (ENTITAS_FAST_AND_UNSAFE)
                    new Entitas.UnsafeAERC()
    #else
                    new Entitas.SafeAERC(entity)
    #endif

            ) 
        {
            factoryIdIndex = new DZ.Game.Indexes.State.FactoryIdIndex(this);
            levelIndexIndex = new DZ.Game.Indexes.State.LevelIndexIndex(this);
            uidIndex = new DZ.Game.Indexes.State.UidIndex(this);
            channelIndex = new DZ.Game.Indexes.State.ChannelIndex(this);
            configIdIndex = new DZ.Game.Indexes.State.ConfigIdIndex(this);
            menuButtonIdIndex = new DZ.Game.Indexes.State.MenuButtonIdIndex(this);
            ligthIdIndex = new DZ.Game.Indexes.State.LigthIdIndex(this);
            modalIdIndex = new DZ.Game.Indexes.State.ModalIdIndex(this);
        }

        /// Value: FactoryId  
        public DZ.Game.Indexes.State.IFactoryIdIndex factoryIdIndex;
        /// Value: LevelIndex  
        public DZ.Game.Indexes.State.ILevelIndexIndex levelIndexIndex;
        /// Value: Uid  
        public DZ.Game.Indexes.State.IUidIndex uidIndex;
        /// Value: Channel  
        public DZ.Game.Indexes.State.IChannelIndex channelIndex;
        /// Value: ConfigId  
        public DZ.Game.Indexes.State.IConfigIdIndex configIdIndex;
        /// Value: MenuButtonId  
        public DZ.Game.Indexes.State.IMenuButtonIdIndex menuButtonIdIndex;
        /// Value: LigthId  
        public DZ.Game.Indexes.State.ILigthIdIndex ligthIdIndex;
        /// Value: ModalId  
        public DZ.Game.Indexes.State.IModalIdIndex modalIdIndex;

    }

    public static class StateComponentsLookup {
        public const int FlagTrash = 0;
        public const int FlagTrashValidated = 1;
        public const int FlagLoaded = 2;
        public const int FlagActive = 3;
        public const int FlagOpened = 4;
        public const int TrashTimer = 5;
        public const int FactoryUnit = 6;
        public const int FactoryId = 7;
        public const int StageManagerUnit = 8;
        public const int LevelPart = 9;
        public const int Level = 10;
        public const int LevelIndex = 11;
        public const int LevelControllerUnit = 12;
        public const int Uid = 13;
        public const int UidUnit = 14;
        public const int ProductUnit = 15;
        public const int View = 16;
        public const int AudioManagerUnit = 17;
        public const int AudioEffectManagerUnit = 18;
        public const int AudioEffectUnit = 19;
        public const int AudioEffectProductUnit = 20;
        public const int MusicManagerUnit = 21;
        public const int SubsManagerUnit = 22;
        public const int Channel = 23;
        public const int ChannelInfoUnit = 24;
        public const int ChannelRecording = 25;
        public const int ChannelVoiceActive = 26;
        public const int PhoneChannelUnit = 27;
        public const int PhoneManagerUnit = 28;
        public const int WorldTime = 29;
        public const int WorldTimeSpeed = 30;
        public const int Effect = 31;
        public const int EffectId = 32;
        public const int EffectFactoryUnit = 33;
        public const int ConfigId = 34;
        public const int PositionWorld = 35;
        public const int MenuButtonUnit = 36;
        public const int MenuButtonId = 37;
        public const int LightUnit = 38;
        public const int LigthId = 39;
        public const int ModalUnit = 40;
        public const int ModalId = 41;
        public const int OverlayUnit = 42;
        public const int StickUnit = 43;
        public const int CharacterUnit = 44;
        public const int HudUnit = 45;

        public const int TotalComponents = 46;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"FlagTrash", 0 },
            {"FlagTrashValidated", 1 },
            {"FlagLoaded", 2 },
            {"FlagActive", 3 },
            {"FlagOpened", 4 },
            {"TrashTimer", 5 },
            {"FactoryUnit", 6 },
            {"FactoryId", 7 },
            {"StageManagerUnit", 8 },
            {"LevelPart", 9 },
            {"Level", 10 },
            {"LevelIndex", 11 },
            {"LevelControllerUnit", 12 },
            {"Uid", 13 },
            {"UidUnit", 14 },
            {"ProductUnit", 15 },
            {"View", 16 },
            {"AudioManagerUnit", 17 },
            {"AudioEffectManagerUnit", 18 },
            {"AudioEffectUnit", 19 },
            {"AudioEffectProductUnit", 20 },
            {"MusicManagerUnit", 21 },
            {"SubsManagerUnit", 22 },
            {"Channel", 23 },
            {"ChannelInfoUnit", 24 },
            {"ChannelRecording", 25 },
            {"ChannelVoiceActive", 26 },
            {"PhoneChannelUnit", 27 },
            {"PhoneManagerUnit", 28 },
            {"WorldTime", 29 },
            {"WorldTimeSpeed", 30 },
            {"Effect", 31 },
            {"EffectId", 32 },
            {"EffectFactoryUnit", 33 },
            {"ConfigId", 34 },
            {"PositionWorld", 35 },
            {"MenuButtonUnit", 36 },
            {"MenuButtonId", 37 },
            {"LightUnit", 38 },
            {"LigthId", 39 },
            {"ModalUnit", 40 },
            {"ModalId", 41 },
            {"OverlayUnit", 42 },
            {"StickUnit", 43 },
            {"CharacterUnit", 44 },
            {"HudUnit", 45 }
        };

        public static readonly string[] componentNames = {
            "FlagTrash",
            "FlagTrashValidated",
            "FlagLoaded",
            "FlagActive",
            "FlagOpened",
            "TrashTimer",
            "FactoryUnit",
            "FactoryId",
            "StageManagerUnit",
            "LevelPart",
            "Level",
            "LevelIndex",
            "LevelControllerUnit",
            "Uid",
            "UidUnit",
            "ProductUnit",
            "View",
            "AudioManagerUnit",
            "AudioEffectManagerUnit",
            "AudioEffectUnit",
            "AudioEffectProductUnit",
            "MusicManagerUnit",
            "SubsManagerUnit",
            "Channel",
            "ChannelInfoUnit",
            "ChannelRecording",
            "ChannelVoiceActive",
            "PhoneChannelUnit",
            "PhoneManagerUnit",
            "WorldTime",
            "WorldTimeSpeed",
            "Effect",
            "EffectId",
            "EffectFactoryUnit",
            "ConfigId",
            "PositionWorld",
            "MenuButtonUnit",
            "MenuButtonId",
            "LightUnit",
            "LigthId",
            "ModalUnit",
            "ModalId",
            "OverlayUnit",
            "StickUnit",
            "CharacterUnit",
            "HudUnit"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.State.FlagTrash),
            typeof(Components.State.FlagTrashValidated),
            typeof(Components.State.FlagLoaded),
            typeof(Components.State.FlagActive),
            typeof(Components.State.FlagOpened),
            typeof(Components.State.TrashTimer),
            typeof(Components.State.FactoryUnit),
            typeof(Components.State.FactoryId),
            typeof(Components.State.StageManagerUnit),
            typeof(Components.State.LevelPart),
            typeof(Components.State.Level),
            typeof(Components.State.LevelIndex),
            typeof(Components.State.LevelControllerUnit),
            typeof(Components.State.Uid),
            typeof(Components.State.UidUnit),
            typeof(Components.State.ProductUnit),
            typeof(Components.State.View),
            typeof(Components.State.AudioManagerUnit),
            typeof(Components.State.AudioEffectManagerUnit),
            typeof(Components.State.AudioEffectUnit),
            typeof(Components.State.AudioEffectProductUnit),
            typeof(Components.State.MusicManagerUnit),
            typeof(Components.State.SubsManagerUnit),
            typeof(Components.State.Channel),
            typeof(Components.State.ChannelInfoUnit),
            typeof(Components.State.ChannelRecording),
            typeof(Components.State.ChannelVoiceActive),
            typeof(Components.State.PhoneChannelUnit),
            typeof(Components.State.PhoneManagerUnit),
            typeof(Components.State.WorldTime),
            typeof(Components.State.WorldTimeSpeed),
            typeof(Components.State.Effect),
            typeof(Components.State.EffectId),
            typeof(Components.State.EffectFactoryUnit),
            typeof(Components.State.ConfigId),
            typeof(Components.State.PositionWorld),
            typeof(Components.State.MenuButtonUnit),
            typeof(Components.State.MenuButtonId),
            typeof(Components.State.LightUnit),
            typeof(Components.State.LigthId),
            typeof(Components.State.ModalUnit),
            typeof(Components.State.ModalId),
            typeof(Components.State.OverlayUnit),
            typeof(Components.State.StickUnit),
            typeof(Components.State.CharacterUnit),
            typeof(Components.State.HudUnit)
        };

        public static int GetComponentIndex(string name) {
            int resultIndex = -1;
            if (componentsDict.TryGetValue(name, out resultIndex)) {
                return resultIndex;
            }

            return -1;
        }

        public static int GetComponentIndex(System.Type t) {
            for (var i = 0; i < componentTypes.Length; i++) {
                if (componentTypes[i] == t) return i;
            }

            return -1;
        }
    }
}
