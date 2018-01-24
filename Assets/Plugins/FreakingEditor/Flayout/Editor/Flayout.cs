using System;
using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public static class Flayout
    {
        public static void BeginChangeCheck()
        {
            EditorGUI.BeginChangeCheck();
        }

        public static bool EndChangeCheck()
        {
            return EditorGUI.EndChangeCheck();
        }

        public static void Space()
        {
            EditorGUILayout.Space();
        }

        public static void FlexibleSpace()
        {
            GUILayout.FlexibleSpace();
        }

        public static void BeginVertical(params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginVertical(Fstyle.offsetBox, options);
        }

        public static void BeginVertical()
        {
            EditorGUILayout.BeginVertical(Fstyle.offsetBox);
        }

        public static void BeginVerticalBox(GUIStyle style = null)
        {
            EditorGUILayout.BeginVertical(style ?? Fstyle.box);

        }

        public static void EndVertical()
        {
            EditorGUILayout.EndVertical();
        }

        public static void BeginHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
        }

        public static void EndHorizontal()
        {
            EditorGUILayout.EndHorizontal();
        }

        public static Vector2 BeginScroll(Vector2 position)
        {
            return EditorGUILayout.BeginScrollView(position, false, false);
        }

        public static void EndScroll()
        {
            EditorGUILayout.EndScrollView();
        }

        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        public static GUILayoutOption Width(float width)
        {
            return GUILayout.Width(width);
        }

        public static GUILayoutOption MaxWidth(float width)
        {
            return GUILayout.MaxWidth(width);
        }

        public static GUILayoutOption ExpandWidth(bool expand = true)
        {
            return GUILayout.ExpandWidth(expand);
        }

        public static GUILayoutOption ExpandHeight(bool expand = true)
        {
            return GUILayout.ExpandHeight(expand);
        }

        public static void Label(string label, GUIStyle style = null, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField(label, style != null ? style : Fstyle.label, options);
        }

        public static bool Foldout(bool folded, string title, GUIStyle style = null)
        {
            return EditorGUILayout.Foldout(folded, title, style != null ? style : EditorStyles.foldout);
        }

        public static int IntField(int value, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(value, style != null ? style : Fstyle.textField, options);
        }

        public static int IntField(string label, int value, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntField(label, value, style != null ? style : Fstyle.textField, options);
        }

        public static float FloatField(float value, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(value, style != null ? style : Fstyle.textField, options);
        }

        public static float FloatField(string label, float value, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.FloatField(label, value, style != null ? style : Fstyle.textField, options);
        }

        public static string TextField(string text, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(text, style != null ? style : Fstyle.textField, options);
        }

        public static string TextField(string label, string text, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.TextField(label, text, style != null ? style : Fstyle.textField, options);
        }

        public static int Popup(int selected, string[] values, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selected, values, style != null ? style : Fstyle.popup, options);
        }

        public static Enum PopupEnum(Enum selected, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return EditorGUILayout.EnumPopup(selected, style != null ? style : Fstyle.popup, options);
        }

        public static bool Button(string text, GUIStyle style = null, params GUILayoutOption[] options)
        {
            return GUILayout.Button(text, style != null ? style : Fstyle.button, options);
        }

        public static float HorizontalSlider(ref float value, float leftValue, float rightValue, GUIStyle sliderStyle, GUIStyle thumbStyle, params GUILayoutOption[] options)
        {
            return value = GUILayout.HorizontalSlider(value, leftValue, rightValue, sliderStyle, thumbStyle, options);
        }

        public static float HorizontalSlider(ref float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            return value = GUILayout.HorizontalSlider(value, leftValue, rightValue, options);
        }

        public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
        {
            if (style != null) return GUILayout.Toggle(value, text, style, options);
            return Toggle(value, text, options);
        }

        public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, text, Fstyle.toggle, options);
        }

        public static bool Toggle(bool value, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, "", Fstyle.toggle, options);
        }
    }
}
