using System.Reflection;
using FreakingEditor;
using UnityEditor;
using UnityEngine;

namespace DevTools.Scripts
{
    public class NamespaceFextraInspector : IFextraMonoBehaviourBeforeInspector, IFextraScriptableObjectBeforeInspector
    {
        public void OnBeforeInspectorGUI(Object[] targets)
        {
            var targetType = targets[0].GetType();
            Flayout.Label(targetType.Namespace, Fstyle.miniLabelCenter);
        }
    }
}
