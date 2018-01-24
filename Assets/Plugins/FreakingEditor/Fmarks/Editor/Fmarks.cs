using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public static class Fmarks
    {
        public static FmarkDisable[] disableMarks;
        public static FmarkEnable[] enableMarks;
        public static FmarkDevelopment[] developmentMarks;

        [InitializeOnLoadMethod]
        public static void Init()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
            RefreshMarks();
        }

        public static void MarkEnable(GameObject go)
        {
            if (go.GetComponent<FmarkEnable>() == null)
            {
                var comp = go.AddComponent<FmarkEnable>();
                comp.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
            }
        }

        public static void MarkDisable(GameObject go)
        {
            if (go.GetComponent<FmarkDisable>() == null)
            {
                var comp = go.AddComponent<FmarkDisable>();
                comp.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
            }
        }

        public static void MarkDevelopment(GameObject go)
        {
            if (go.GetComponent<FmarkDevelopment>() == null)
            {
                var comp = go.AddComponent<FmarkDevelopment>();
                comp.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
            }
        }

        public static void RefreshMarks()
        {
            enableMarks = Resources.FindObjectsOfTypeAll<FmarkEnable>();
            disableMarks = Resources.FindObjectsOfTypeAll<FmarkDisable>();
            developmentMarks = Resources.FindObjectsOfTypeAll<FmarkDevelopment>();
        }

        public static void RestoreLastState(UnityEngine.SceneManagement.Scene scene, bool useLastState = false)
        {
            RefreshMarks();

            if (disableMarks != null)
            {
                for (int i = 0; i < disableMarks.Length; i++)
                {
                    if (disableMarks[i] == null) continue;
                    if (disableMarks[i].gameObject.scene == scene)
                    {
                        if (!useLastState) disableMarks[i].lastStateBeforeSceneClose = disableMarks[i].gameObject.activeSelf;
                        else disableMarks[i].lastState = disableMarks[i].lastStateBeforeSceneClose;
                        disableMarks[i].gameObject.SetActive((useLastState ? disableMarks[i].lastStateBeforeSceneClose : false));
                        if (!useLastState) UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
                    }
                }
            }

            if (enableMarks != null)
            {
                for (int i = 0; i < enableMarks.Length; i++)
                {
                    if (enableMarks[i] == null) continue;
                    if (enableMarks[i].gameObject.scene == scene)
                    {
                        if (!useLastState) enableMarks[i].lastStateBeforeSceneClose = enableMarks[i].gameObject.activeSelf;
                        else enableMarks[i].lastState = enableMarks[i].lastStateBeforeSceneClose;
                        enableMarks[i].gameObject.SetActive((useLastState ? enableMarks[i].lastStateBeforeSceneClose : true));
                        if (!useLastState) UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
                    }
                }
            }

            if (developmentMarks != null)
            {
                for (int i = 0; i < developmentMarks.Length; i++)
                {
                    if (developmentMarks[i] == null) continue;
                    if (developmentMarks[i].gameObject.scene == scene)
                    {
                        if (!useLastState) developmentMarks[i].lastStateBeforeSceneClose = developmentMarks[i].gameObject.activeSelf;
                        else developmentMarks[i].lastState = developmentMarks[i].lastStateBeforeSceneClose;
                        developmentMarks[i].gameObject.SetActive((useLastState ? developmentMarks[i].lastStateBeforeSceneClose : false));
                        if (!useLastState) UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
                    }
                }
            }
        }

        public static void EnableAll(FmarkBase[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i].gameObject.SetActive(true);
            }
        }

        public static void DisableAll(FmarkBase[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i].gameObject.SetActive(false);
            }
        }

        public static void PlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            switch (playModeStateChange)
            {
                case PlayModeStateChange.ExitingEditMode:
                    RefreshMarks();

                    for (int i = 0; i < disableMarks.Length; i++)
                    {
                        disableMarks[i].lastState = disableMarks[i].gameObject.activeSelf;
                        disableMarks[i].gameObject.SetActive(false);
                    }

                    for (int i = 0; i < enableMarks.Length; i++)
                    {
                        enableMarks[i].lastState = enableMarks[i].gameObject.activeSelf;
                        enableMarks[i].gameObject.SetActive(true);
                    }
                    break;
                case PlayModeStateChange.EnteredEditMode:
                    RefreshMarks();

                    for (int i = 0; i < disableMarks.Length; i++)
                    {
                        disableMarks[i].gameObject.SetActive(disableMarks[i].lastState);
                    }

                    for (int i = 0; i < enableMarks.Length; i++)
                    {
                        enableMarks[i].gameObject.SetActive(enableMarks[i].lastState);
                    }
                    break;
            }
        }
    }
}