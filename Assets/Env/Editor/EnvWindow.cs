using System.Reflection;
using UnityEditor;
using UnityEngine;

public class EnvWindow : EditorWindow
{
    private static EnvWindow windowInstance;
    private Vector2 scrollPosition;

    private static GUIStyle _foldoutStyle;
    private static GUIStyle foldoutStyle
    {
        get
        {
            if (_foldoutStyle == null)
            {
                _foldoutStyle = EditorStyles.foldout;
                _foldoutStyle.fontStyle = FontStyle.Bold;
            }
            return _foldoutStyle;
        }
    }

    [MenuItem("Tools/Env")]
    public static void GetWindow()
    {
        windowInstance = EditorWindow.GetWindow<EnvWindow>(false, "Env");
        EnvUtil.Restore();

    }

    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        var members = typeof(Env).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

        var visible = true;

        foreach (var member in members)
        {
            if (member.FieldType == typeof(string) && member.IsPrivate)
            {
                EditorGUILayout.Space();
                visible = EditorGUILayout.Foldout(EditorPrefs.GetBool("EnvFoldout" + member.Name), ToUpperCase(member.Name), foldoutStyle);
                EditorPrefs.SetBool("EnvFoldout" + member.Name, visible);
            }
            else if (!visible)
            {
                continue;
            }
            else if (member.FieldType == typeof(bool))
            {
                var value = (bool) member.GetValue(null);
                var newValue = EditorGUILayout.ToggleLeft(ToUpperCase(member.Name), value);
                if (newValue != value) member.SetValue(null, newValue);
            }
            else if (member.FieldType == typeof(string))
            {
                var value = (string) member.GetValue(null);
                var newValue = EditorGUILayout.TextField(ToUpperCase(member.Name), value);
                if (newValue != value) member.SetValue(null, newValue);
            }
            else if (member.FieldType == typeof(int))
            {
                var value = (int) member.GetValue(null);
                var newValue = EditorGUILayout.IntField(ToUpperCase(member.Name), value);
                if (newValue != value) member.SetValue(null, newValue);
            }
            else if (member.FieldType == typeof(float))
            {
                var value = (float) member.GetValue(null);
                var newValue = EditorGUILayout.FloatField(ToUpperCase(member.Name), value);
                if (newValue != value) member.SetValue(null, newValue);
            }
            else if (member.FieldType == typeof(System.Action))
            {
                if (GUILayout.Button(member.Name))
                {
                    var action = (System.Action) member.GetValue(null);
                    if (action != null) { action(); }
                }
            }
        }

        EditorGUILayout.EndScrollView();

        if (EditorGUI.EndChangeCheck())
        {
            EnvUtil.Save();
        }
    }

    string ToUpperCase(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        return char.ToUpper(str[0]) + str.Substring(1);
    }
}
