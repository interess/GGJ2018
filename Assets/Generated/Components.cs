// <auto-generated>
//     This code was generated with love by Gentitas.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

using UnityEngine;
using System.Collections.Generic;

namespace DZ.Core.Components {
    namespace State {
        // Flag Components
        public partial class Loaded: Entitas.IComponent { }
        public partial class Application: Entitas.IComponent { }
        // Value Components
        public partial class LoadingProgress: Entitas.IComponent { public float value; }
        public partial class SceneName: Entitas.IComponent { public string value; }
        public partial class LoadingManagerUnit: Entitas.IComponent { public Scripts.LoadingManagerUnit value; }
    }
}