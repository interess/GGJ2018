using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public class FbuttonFextraInspector : IFextraMonoBehaviourAfterInspector, IFextraScriptableObjectAfterInspector
    {
        object[][] parameters = new object[0][];

        public void OnAfterInspectorGUI(Object[] targets)
        {
            if (targets.Length == 0) return;
            var target = targets[0];
            var type = target.GetType();
            var methodInfos = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            var methodInfosAll = new MethodInfo[targets.Length][];

            for (int i = 0; i < targets.Length; i++)
            {
                methodInfosAll[i] = new MethodInfo[methodInfos.Length];
                var currentInfos = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (methodInfosAll[i].Length != currentInfos.Length)
                {
                    return;
                }

                methodInfosAll[i] = currentInfos;
            }

            if (parameters.Length != methodInfos.Length)
            {
                parameters = new object[methodInfos.Length][];
            }

            GUILayout.Space(10f);
            for (var m = 0; m < methodInfos.Length; m++)
            {
                var method = methodInfos[m];

                var editorAttr = method.GetCustomAttributes(typeof(FbuttonEditorAttribute), true);
                var playAttr = method.GetCustomAttributes(typeof(FbuttonPlayAttribute), true);
                var attrs = Application.isPlaying ? playAttr : editorAttr;

                if (attrs.Length > 0)
                {
                    EditorGUILayout.BeginVertical();
                    var methodsParameters = method.GetParameters();

                    if (parameters[m] == null) {
                        parameters[m] = new object[0];
                    }
                    if (parameters[m].Length != methodsParameters.Length) parameters[m] = new object[methodsParameters.Length];

                    for (int i = 0; i < parameters[m].Length; i++)
                    {
                        var t = methodsParameters[i].ParameterType;
                        var fieldName = methodsParameters[i].Name;
                        fieldName = char.ToUpper(fieldName[0]) + fieldName.Substring(1);

                        if (t.IsEnum)
                        {
                            EditorGUILayout.LabelField(fieldName);
                            EditorGUILayout.HelpBox("Editor Buttons can't work with enums. Use custom editor for this.", MessageType.Error);
                        }
                        else
                        {
                            switch (t.ToString())
                            {
                                case "System.Single":
                                    if (parameters[m][i] == null) parameters[m][i] = default(float);
                                    parameters[m][i] = EditorGUILayout.FloatField(fieldName, (float) parameters[m][i]);
                                    break;
                                case "System.Double":
                                    if (parameters[m][i] == null) parameters[m][i] = default(double);
                                    parameters[m][i] = EditorGUILayout.DoubleField(fieldName, (double) parameters[m][i]);
                                    break;
                                case "System.Int32":
                                    if (parameters[m][i] == null) parameters[m][i] = default(int);
                                    parameters[m][i] = EditorGUILayout.IntField(fieldName, (int) parameters[m][i]);
                                    break;
                                case "System.Boolean":
                                    if (parameters[m][i] == null) parameters[m][i] = default(bool);
                                    parameters[m][i] = EditorGUILayout.Toggle(fieldName, (bool) parameters[m][i]);
                                    break;
                                case "System.String":
                                    if (parameters[m][i] == null) parameters[m][i] = default(string);
                                    parameters[m][i] = EditorGUILayout.TextField(fieldName, (string) parameters[m][i]);
                                    break;

                                case "UnityEngine.Color":
                                    if (parameters[m][i] == null) parameters[m][i] = default(Color);
                                    parameters[m][i] = EditorGUILayout.ColorField(fieldName, (Color) parameters[m][i]);
                                    break;
                                case "UnityEngine.Vector2":
                                    if (parameters[m][i] == null) parameters[m][i] = default(Vector2);
                                    parameters[m][i] = EditorGUILayout.Vector2Field(fieldName, (Vector2) parameters[m][i]);
                                    break;
                                case "UnityEngine.Vector3":
                                    if (parameters[m][i] == null) parameters[m][i] = default(Vector3);
                                    parameters[m][i] = EditorGUILayout.Vector3Field(fieldName, (Vector3) parameters[m][i]);
                                    break;
                                case "UnityEngine.Vector4":
                                    if (parameters[m][i] == null) parameters[m][i] = default(Vector4);
                                    parameters[m][i] = EditorGUILayout.Vector4Field(fieldName, (Vector4) parameters[m][i]);
                                    break;

                                case "UnityEngine.Sprite":
                                    if (parameters[m][i] == null) parameters[m][i] = default(Sprite);
                                    parameters[m][i] = (Sprite) EditorGUILayout.ObjectField(fieldName, (Sprite) parameters[m][i], typeof(Sprite), false);
                                    break;
                            }
                        }
                    }

                    if (GUILayout.Button(method.Name, Fstyle.button))
                    {
                        InvokeMethod(method, targets, parameters[m]);
                    }
                    EditorGUILayout.EndVertical();
                }
            }
        }

        private void InvokeMethod(MethodInfo method, object[] targets, object[] parameters)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                method.Invoke(targets[i], parameters);
            }
        }
    }
}
