using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public static class Fconfig
    {
        public static bool Ensure<T>(ref T config, string nameToLoad) where T : Object
        {
            if (config == null)
            {
                var loadedConfig = Resources.Load<T>(nameToLoad);
                if (loadedConfig == null) return false;

                config = loadedConfig;
            }

            return true;
        }

        public static void DrawNoConfig<T>(string nameToCreate) where T : ScriptableObject
        {

            GUILayout.FlexibleSpace();

            GUILayout.Label("Config \"" + nameToCreate + "\" not found in Resources", Fstyle.labelCenter);

            if (GUILayout.Button("Create"))
            {
                Fasset.CreateAssetAtChosenLocation<T>(nameToCreate);
            };

            GUILayout.FlexibleSpace();
        }
    }
}
