// <auto-generated>
//     This code was generated with love by Gentitas.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Entitas.Gentitas.DevTools.Generated {
  public static class DevTools
  {
    #if UNITY_EDITOR
    [MenuItem("Tools/Gentitas Generated/Reset All Contexts", false, 501)]
    #endif
    public static void ResetAllContexts()
    {
      #if UNITY_EDITOR
      DZ.Core.Contexts.Reset();
      DZ.Game.Contexts.Reset();
      #endif
    }

    #if UNITY_EDITOR
    [MenuItem("Tools/Gentitas Generated/Hard Reset All Contexts", false, 502)]
    #endif
    public static void HardResetAllContexts()
    {
      #if UNITY_EDITOR
      DZ.Core.Contexts.HardReset();
      DZ.Game.Contexts.HardReset();
      #endif
    }
  }
}
