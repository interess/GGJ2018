using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public static class Fasset
    {
        public static void CreateAssetAtChosenLocation<T>(string name) where T : ScriptableObject
        {
            var folderPath = EditorUtility.OpenFolderPanel("Choose Location for Asset", Application.dataPath, "Resources");

            if (string.IsNullOrEmpty(folderPath)) { return; }

            folderPath = System.Text.RegularExpressions.Regex.Replace(folderPath, ".*Assets/", "");
            folderPath = System.IO.Path.Combine("Assets", folderPath);

            T asset = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(asset, folderPath + "/" + name + ".asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        public static void CreateAsset<T>(string name) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(asset, AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + name + ".asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
        }

        public static void CreateAsset<T>() where T : ScriptableObject
        {
            CreateAsset<T>("UntitledAsset");
        }
    }
}
