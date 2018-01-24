using System;
using Freaking;
using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public class FscenesWindow : EditorWindow
    {
        private static FscenesWindow _instance;
        private Vector2 _scroll;
        private static GUIStyle _customStyle;
        private const string _sceneOpenedKey = "FreakingEditor.Fscenes.Opened.";
        private const string _sceneNewScenePathKey = "FreakingEditor.Fscenes.NewScenePath";

        [MenuItem("Tools/FreakingEditor/Fscenes")]
        public static void Init()
        {
            _instance = EditorWindow.GetWindow<FscenesWindow>(false, "Fscenes");
        }

        private void InitCustomStyle()
        {
            _customStyle = new GUIStyle(Fstyle.buttonCenter);
            _customStyle.alignment = TextAnchor.MiddleLeft;

        }

        private void OnGUI()
        {
            if (_customStyle == null) { InitCustomStyle(); }

            if (Fscenes.config == null)
            {
                Fconfig.DrawNoConfig<FscenesConfig>(Fscenes.configName);
                return;
            }

            Flayout.BeginChangeCheck();

            _scroll = Flayout.BeginScroll(_scroll);

            Flayout.Space();

            Flayout.BeginHorizontal();
            Flayout.Label("Scene:", Fstyle.boldLabelLeft, Flayout.Width(60f));
            var oldNewScenePath = EditorPrefs.GetString(_sceneNewScenePathKey);
            var newScenePath = Flayout.TextField(oldNewScenePath, Fstyle.textFieldLeft);
            if (newScenePath != oldNewScenePath) { EditorPrefs.SetString(_sceneNewScenePathKey, newScenePath); }

            if (Flayout.Button("Add", Fstyle.buttonRight, Flayout.Width(30f)))
            {
                var exists = true;
                if (newScenePath.Contains("/"))
                {
                    try
                    {
                        Fscenes.OpenScene(newScenePath);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogError(exception.Message);
                        exists = false;
                    }
                }

                if (exists)
                {
                    Fscenes.config.scenePaths.Add(newScenePath);
                    Fscenes.config.devOnly.Add(false);
                    EditorPrefs.SetBool(_sceneOpenedKey + newScenePath, false);
                    EditorPrefs.SetString(_sceneNewScenePathKey, "");
                    UnityEngine.GUI.FocusControl(null);
                }
            }
            Flayout.EndHorizontal();

            for (int i = 0; i < Fscenes.config.scenePaths.Count; i++)
            {
                try
                {
                    var scenePath = Fscenes.config.scenePaths[i];
                    var sceneNameArr = scenePath.Split('/');
                    var sceneName = sceneNameArr[sceneNameArr.Length - 1];
                    var opened = Fscenes.HasScene(scenePath);
                    var needToBeOpened = EditorPrefs.GetBool(_sceneOpenedKey + scenePath) != opened;
                    var isScene = scenePath.Contains("/");

                    Flayout.BeginHorizontal();

                    if (Flayout.Button("↑", Fstyle.buttonLeft, Flayout.Width(15f)))
                    {
                        if (i - 1 >= 0)
                        {
                            var spath = Fscenes.config.scenePaths[i - 1];
                            var sdevOnly = Fscenes.config.devOnly[i - 1];
                            Fscenes.config.scenePaths[i - 1] = Fscenes.config.scenePaths[i];
                            Fscenes.config.devOnly[i - 1] = Fscenes.config.devOnly[i];

                            Fscenes.config.scenePaths[i] = spath;
                            Fscenes.config.devOnly[i] = sdevOnly;

                            if (Fscenes.config.mainScene == i) Fscenes.config.mainScene--;
                        }
                    }

                    if (Flayout.Button("↓", isScene ? Fstyle.buttonCenter : Fstyle.buttonRight, Flayout.Width(15f)))
                    {
                        if (i + 1 < Fscenes.config.scenePaths.Count)
                        {
                            var spath = Fscenes.config.scenePaths[i + 1];
                            var sdevOnly = Fscenes.config.devOnly[i + 1];
                            Fscenes.config.scenePaths[i + 1] = Fscenes.config.scenePaths[i];
                            Fscenes.config.devOnly[i + 1] = Fscenes.config.devOnly[i];

                            Fscenes.config.scenePaths[i] = spath;
                            Fscenes.config.devOnly[i] = sdevOnly;

                            if (Fscenes.config.mainScene == i) Fscenes.config.mainScene++;
                        }
                    }

                    if (isScene)
                    {
                        needToBeOpened = Flayout.Toggle(needToBeOpened, " " + sceneName, _customStyle);

                        if (!opened && needToBeOpened)
                        {
                            Fscenes.OpenScene(scenePath);
                        }
                        else if (opened && !needToBeOpened)
                        {
                            Fscenes.CloseScene(scenePath);
                        }

                        Fscenes.config.devOnly[i] = Flayout.Toggle(Fscenes.config.devOnly[i], "D", Fstyle.buttonCenter, Flayout.Width(20f));
                        var isMainScene = Flayout.Toggle(Fscenes.config.mainScene == i, "M", Fstyle.buttonCenter, Flayout.Width(20f));
                        if (isMainScene) Fscenes.config.mainScene = i;
                    }
                    else
                    {
                        Flayout.Label(scenePath);
                    }

                    if (Flayout.Button("x", isScene ? Fstyle.buttonRight : Fstyle.button, Flayout.Width(20f)))
                    {
                        EditorPrefs.DeleteKey(_sceneOpenedKey + Fscenes.config.scenePaths[i]);
                        Fscenes.config.scenePaths.RemoveAt(i);
                        Fscenes.config.devOnly.RemoveAt(i);
                        if (Fscenes.config.mainScene == i)
                        {
                            if (i == 0) Fscenes.config.mainScene++;
                            else Fscenes.config.mainScene--;
                        }
                    }

                    Fscenes.RefreshMainScene();

                    Flayout.EndHorizontal();
                }
                catch (System.Exception e)
                {
                    EditorGUILayout.HelpBox(e.Message, MessageType.Error);
                }
            }

            Flayout.Space();

            Flayout.EndScroll();

            if (Flayout.EndChangeCheck())
            {
                EditorUtility.SetDirty(Fscenes.config);
            }
        }
    }
}
