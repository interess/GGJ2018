using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class EnvUtil : ScriptableObject
{
    const string _prefix = "__Env_";

    [UnityEditor.InitializeOnLoadMethod]
    static void Init()
    {
        Restore();
    }

    public static void Save()
    {
        var members = typeof(Env).GetFields();

        foreach (var member in members)
        {
            var name = member.Name;
            var prefixedName = _prefix + name;
            var value = member.GetValue(null);
            if (member.FieldType == typeof(int)) UnityEditor.EditorPrefs.SetInt(prefixedName, (int)value);
            if (member.FieldType == typeof(float)) UnityEditor.EditorPrefs.SetFloat(prefixedName, (float)value);
            if (member.FieldType == typeof(bool)) UnityEditor.EditorPrefs.SetBool(prefixedName, (bool)value);
            if (member.FieldType == typeof(string)) UnityEditor.EditorPrefs.SetString(prefixedName, (string)value);
        }
    }

    public static void Restore()
    {
        var members = typeof(Env).GetFields();

        foreach (var member in members)
        {
            var name = member.Name;
            var prefixedName = _prefix + name;
            var value = member.GetValue(null);
            if (member.FieldType == typeof(int)) value = UnityEditor.EditorPrefs.GetInt(prefixedName);
            if (member.FieldType == typeof(float)) value = UnityEditor.EditorPrefs.GetFloat(prefixedName);
            if (member.FieldType == typeof(bool)) value = UnityEditor.EditorPrefs.GetBool(prefixedName);
            if (member.FieldType == typeof(string)) value = UnityEditor.EditorPrefs.GetString(prefixedName);
            if (value != null) member.SetValue(null, value);
        }
    }
}
