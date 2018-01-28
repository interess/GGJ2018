using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FreakingEditor
{
    public static class Fscenes
    {
        public const string configName = "FreakingEditor.FscenesConfig";

        private const string _sceneClosedScenePathsKey = "FreakingEditor.Fscenes.ClosedScenePaths.";

        private static FscenesConfig _config;
        public static FscenesConfig config
        {
            get
            {
                if (_config == null) { _config = Resources.Load<FscenesConfig>(configName); }
                return _config;
            }
        }

        [InitializeOnLoadMethod]
        public static void Init()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        [UnityEditor.MenuItem("Assets/Create/Freaking/SccenesFscnesConfig")]
        public static void CrceateFscnesConfig()
        {
            Fasset.CreateAsset<FscenesConfig>(configName);
        }

        public static Scene OpenScene(string path)
        {
            Scene scene = default(Scene);

            try
            {
                scene = EditorSceneManager.OpenScene(path + ".unity", OpenSceneMode.Additive);
                Fmarks.RestoreLastState(scene, true);
            }
            catch (System.Exception exception)
            {
                Debug.LogError(exception.Message);
            }

            return scene;
        }

        public static void RefreshMainScene()
        {
            if (config == null) return;

            if (config.mainScene > config.scenePaths.Count - 1) return;

            var activePath = config.scenePaths[config.mainScene];
            var scene = GetScene(activePath);

            if (scene.IsValid())
            {
                var activeScene = SceneManager.GetActiveScene();
                if (!scene.Equals(activeScene)) SceneManager.SetActiveScene(scene);
            }
        }

        public static void CloseScene(string path)
        {
            var name = GetSceneNameFromPath(path);
            var scene = SceneManager.GetSceneByName(name);
            Fmarks.RestoreLastState(scene);

            if (scene.isDirty && !EditorApplication.isPlaying)
            {
                EditorSceneManager.SaveScene(scene);
            }

            if (scene.isLoaded)
            {
                EditorSceneManager.CloseScene(scene, true);
            }
        }

        public static bool HasScene(string path)
        {
            var name = GetSceneNameFromPath(path);
            var scene = GetScene(name);
            var sceneFromBundle = SceneManager.GetSceneByName(name);

            return scene.isLoaded || sceneFromBundle.isLoaded;
        }

        public static Scene GetScene(string path)
        {
            return SceneManager.GetSceneByPath(path + ".unity");
        }

        public static bool SaveScene(string path)
        {
            var name = GetSceneNameFromPath(path);
            var scene = SceneManager.GetSceneByName(name);
            EditorSceneManager.MarkSceneDirty(scene);

            return EditorSceneManager.SaveScene(scene);
        }

        public static void PlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            switch (playModeStateChange)
            {
                case PlayModeStateChange.ExitingEditMode:
                    for (int i = 0; i < config.devOnly.Count; i++)
                    {
                        if (config.devOnly[i])
                        {
                            var scenePath = config.scenePaths[i];
                            if (HasScene(scenePath))
                            {
                                Debug.Log("Closing scene " + scenePath);
                                CloseScene(scenePath);
                                EditorPrefs.SetBool(_sceneClosedScenePathsKey + scenePath, true);
                            }
                        }
                    }
                    break;

                case PlayModeStateChange.EnteredEditMode:
                    for (int i = 0; i < config.scenePaths.Count; i++)
                    {
                        var scenePath = config.scenePaths[i];
                        var wasClosedBeforePlay = EditorPrefs.GetBool(_sceneClosedScenePathsKey + scenePath);
                        if (wasClosedBeforePlay && !HasScene(scenePath)) OpenScene(scenePath);
                        EditorPrefs.DeleteKey(_sceneClosedScenePathsKey + scenePath);
                    }
                    break;

            }
        }

        private static string GetSceneNameFromPath(string path)
        {
            var name = path.Split('/');
            return name[name.Length - 1];
        }
    }
}
