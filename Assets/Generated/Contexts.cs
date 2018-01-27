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

        public const int TotalComponents = 8;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"FlagEvent", 0 },
            {"FlagTrash", 1 },
            {"FlagTrashValidated", 2 },
            {"TrashTimer", 3 },
            {"EventId", 4 },
            {"ChannelSwitchEvent", 5 },
            {"SubsRecordStartEvent", 6 },
            {"SubsRecordStopEvent", 7 }
        };

        public static readonly string[] componentNames = {
            "FlagEvent",
            "FlagTrash",
            "FlagTrashValidated",
            "TrashTimer",
            "EventId",
            "ChannelSwitchEvent",
            "SubsRecordStartEvent",
            "SubsRecordStopEvent"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.Input.FlagEvent),
            typeof(Components.Input.FlagTrash),
            typeof(Components.Input.FlagTrashValidated),
            typeof(Components.Input.TrashTimer),
            typeof(Components.Input.EventId),
            typeof(Components.Input.ChannelSwitchEvent),
            typeof(Components.Input.SubsRecordStartEvent),
            typeof(Components.Input.SubsRecordStopEvent)
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
            levelIndexIndex = new DZ.Game.Indexes.State.LevelIndexIndex(this);
            uidIndex = new DZ.Game.Indexes.State.UidIndex(this);
            channelIndex = new DZ.Game.Indexes.State.ChannelIndex(this);
        }

        /// Value: LevelIndex  
        public DZ.Game.Indexes.State.ILevelIndexIndex levelIndexIndex;
        /// Value: Uid  
        public DZ.Game.Indexes.State.IUidIndex uidIndex;
        /// Value: Channel  
        public DZ.Game.Indexes.State.IChannelIndex channelIndex;

    }

    public static class StateComponentsLookup {
        public const int FlagTrash = 0;
        public const int FlagTrashValidated = 1;
        public const int FlagLoaded = 2;
        public const int FlagActive = 3;
        public const int TrashTimer = 4;
        public const int StageManagerUnit = 5;
        public const int LevelPart = 6;
        public const int Level = 7;
        public const int LevelIndex = 8;
        public const int Uid = 9;
        public const int UidUnit = 10;
        public const int ProductUnit = 11;
        public const int View = 12;
        public const int AudioManagerUnit = 13;
        public const int AudioEffectManagerUnit = 14;
        public const int AudioEffectUnit = 15;
        public const int AudioEffectProductUnit = 16;
        public const int MusicManagerUnit = 17;
        public const int SubsManagerUnit = 18;
        public const int Channel = 19;
        public const int ChannelInfoUnit = 20;
        public const int ChannelRecording = 21;
        public const int PhoneManagerUnit = 22;
        public const int WorldTime = 23;
        public const int WorldTimeSpeed = 24;

        public const int TotalComponents = 25;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"FlagTrash", 0 },
            {"FlagTrashValidated", 1 },
            {"FlagLoaded", 2 },
            {"FlagActive", 3 },
            {"TrashTimer", 4 },
            {"StageManagerUnit", 5 },
            {"LevelPart", 6 },
            {"Level", 7 },
            {"LevelIndex", 8 },
            {"Uid", 9 },
            {"UidUnit", 10 },
            {"ProductUnit", 11 },
            {"View", 12 },
            {"AudioManagerUnit", 13 },
            {"AudioEffectManagerUnit", 14 },
            {"AudioEffectUnit", 15 },
            {"AudioEffectProductUnit", 16 },
            {"MusicManagerUnit", 17 },
            {"SubsManagerUnit", 18 },
            {"Channel", 19 },
            {"ChannelInfoUnit", 20 },
            {"ChannelRecording", 21 },
            {"PhoneManagerUnit", 22 },
            {"WorldTime", 23 },
            {"WorldTimeSpeed", 24 }
        };

        public static readonly string[] componentNames = {
            "FlagTrash",
            "FlagTrashValidated",
            "FlagLoaded",
            "FlagActive",
            "TrashTimer",
            "StageManagerUnit",
            "LevelPart",
            "Level",
            "LevelIndex",
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
            "PhoneManagerUnit",
            "WorldTime",
            "WorldTimeSpeed"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.State.FlagTrash),
            typeof(Components.State.FlagTrashValidated),
            typeof(Components.State.FlagLoaded),
            typeof(Components.State.FlagActive),
            typeof(Components.State.TrashTimer),
            typeof(Components.State.StageManagerUnit),
            typeof(Components.State.LevelPart),
            typeof(Components.State.Level),
            typeof(Components.State.LevelIndex),
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
            typeof(Components.State.PhoneManagerUnit),
            typeof(Components.State.WorldTime),
            typeof(Components.State.WorldTimeSpeed)
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
