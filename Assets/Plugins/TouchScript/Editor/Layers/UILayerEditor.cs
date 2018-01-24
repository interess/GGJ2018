/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using TouchScript.Layers;
using UnityEditor;
using UnityEngine;

namespace TouchScript.Editor.Layers
{
    [CustomEditor(typeof (TouchScript.Layers.UILayer))]
    internal sealed class UILayerEditor : UnityEditor.Editor
    {
        private void OnEnable() {}

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}