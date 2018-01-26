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

        public const int TotalComponents = 3;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"FlagEvent", 0 },
            {"FlagTrash", 1 },
            {"FlagTrashValidated", 2 }
        };

        public static readonly string[] componentNames = {
            "FlagEvent",
            "FlagTrash",
            "FlagTrashValidated"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.Input.FlagEvent),
            typeof(Components.Input.FlagTrash),
            typeof(Components.Input.FlagTrashValidated)
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
        }


    }

    public static class StateComponentsLookup {
        public const int FlagTrash = 0;
        public const int FlagTrashValidated = 1;
        public const int StageManagerUnit = 2;

        public const int TotalComponents = 3;

        public static readonly Dictionary<string, int> componentsDict = new Dictionary<string, int> () {
            {"FlagTrash", 0 },
            {"FlagTrashValidated", 1 },
            {"StageManagerUnit", 2 }
        };

        public static readonly string[] componentNames = {
            "FlagTrash",
            "FlagTrashValidated",
            "StageManagerUnit"
        };

        public static readonly System.Type[] componentTypes = {
            typeof(Components.State.FlagTrash),
            typeof(Components.State.FlagTrashValidated),
            typeof(Components.State.StageManagerUnit)
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
