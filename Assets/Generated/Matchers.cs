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

    }

}

