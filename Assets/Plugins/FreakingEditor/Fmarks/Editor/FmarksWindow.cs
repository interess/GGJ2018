using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
    public class FmarksWindow : EditorWindow
    {
        private static FmarksWindow instance;

        private static bool enableFoldout
        {
            get { return EditorPrefs.GetBool("FreakingEditor.Fmarks.EnableFoldout"); }
            set { EditorPrefs.SetBool("FreakingEditor.Fmarks.EnableFoldout", value); }
        }

        private static bool disableFoldout
        {
            get { return EditorPrefs.GetBool("FreakingEditor.Fmarks.DisableFoldout"); }
            set { EditorPrefs.SetBool("FreakingEditor.Fmarks.DisableFoldout", value); }
        }

        private static bool developmentFoldout
        {
            get { return EditorPrefs.GetBool("FreakingEditor.Fmarks.DevelopmentFoldout"); }
            set { EditorPrefs.SetBool("FreakingEditor.Fmarks.DevelopmentFoldout", value); }
        }

        private GameObject selectedGameObject;
        private Vector2 scroll;

        [MenuItem("Tools/FreakingEditor/Fmarks")]
        public static void GetWindow()
        {
            instance = EditorWindow.GetWindow<FmarksWindow>(false, "Fmarks");
        }

        public void InitConfigs()
        {
            Fmarks.RefreshMarks();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            Flayout.BeginChangeCheck();

            scroll = Flayout.BeginScroll(scroll);

            Flayout.Space();

            if (Flayout.Button("Refresh"))
            {
                Fmarks.RefreshMarks();
            }

            selectedGameObject = Selection.activeGameObject;

            Flayout.BeginVerticalBox();
            {
                enableFoldout = Flayout.Foldout(enableFoldout, "Enable Marks");
                if (enableFoldout)
                {

                    Flayout.BeginHorizontal();
                    {
                        if (Flayout.Button("Enable All", Fstyle.buttonLeft))
                        {
                            Fmarks.EnableAll(Fmarks.enableMarks);
                        }

                        if (Flayout.Button("Disable All", Fstyle.buttonRight))
                        {
                            Fmarks.DisableAll(Fmarks.enableMarks);
                        }
                    }
                    Flayout.EndHorizontal();

                    if (selectedGameObject != null)
                    {
                        Flayout.BeginHorizontal();
                        {
                            Flayout.Label((selectedGameObject.transform.parent != null ? (selectedGameObject.transform.parent.parent != null ? "..." : "") + "/" + selectedGameObject.transform.parent.name + "/" : "/") + selectedGameObject.name, Fstyle.labelLeft, Flayout.Width(100f));
                            Flayout.FlexibleSpace();

                            if (selectedGameObject.GetComponent<FmarkEnable>() == null)
                            {
                                if (Flayout.Button("Mark", Fstyle.button, Flayout.Width(50f)))
                                {
                                    var comp = selectedGameObject.AddComponent<FmarkEnable>();
                                    comp.lastState = selectedGameObject.activeSelf;
                                    comp.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
                                    var disableMark = selectedGameObject.GetComponent<FmarkDisable>();
                                    if (disableMark != null) DestroyImmediate(disableMark);
                                    Fmarks.RefreshMarks();
                                }
                            }
                            else
                            {
                                if (Flayout.Button("Unmark", Fstyle.button, Flayout.Width(50f)))
                                {
                                    DestroyImmediate(selectedGameObject.GetComponent<FmarkEnable>());
                                    Fmarks.RefreshMarks();
                                }
                            }
                        }
                        Flayout.EndHorizontal();
                    }

                    Flayout.Space();

                    for (int i = 0; i < Fmarks.enableMarks.Length; i++)
                    {
                        var mark = Fmarks.enableMarks[i];

                        if (mark == null) continue;

                        Flayout.BeginHorizontal();
                        {
                            Flayout.Label((mark.transform.parent != null ? (mark.transform.parent.parent != null ? "..." : "") + "/" + mark.transform.parent.gameObject.name + "/" : "") + mark.gameObject.name, Fstyle.labelLeft, Flayout.Width(100f));
                            Flayout.FlexibleSpace();

                            if (mark.gameObject.activeSelf)
                            {
                                if (Flayout.Button("Disable", Fstyle.buttonLeft, Flayout.Width(50f)))
                                {
                                    mark.gameObject.SetActive(false);
                                    Fmarks.RefreshMarks();
                                }
                            }
                            else
                            {
                                if (Flayout.Button("Enable", Fstyle.buttonLeft, Flayout.Width(50f)))
                                {
                                    mark.gameObject.SetActive(true);
                                    Fmarks.RefreshMarks();
                                }
                            }

                            if (Flayout.Button("Unmark", Fstyle.buttonRight, Flayout.Width(50f)))
                            {
                                mark.gameObject.SetActive(true);
                                DestroyImmediate(mark);
                                Fmarks.RefreshMarks();
                            }
                        }
                        Flayout.EndHorizontal();
                    }
                }
            }
            Flayout.Space();
            Flayout.EndVertical();

            Flayout.BeginVerticalBox();
            {
                disableFoldout = Flayout.Foldout(disableFoldout, "Disable Marks");
                if (disableFoldout)
                {

                    Flayout.BeginHorizontal();
                    {
                        if (Flayout.Button("Enable All", Fstyle.buttonLeft))
                        {
                            Fmarks.EnableAll(Fmarks.disableMarks);
                        }

                        if (Flayout.Button("Disable All", Fstyle.buttonRight))
                        {
                            Fmarks.DisableAll(Fmarks.disableMarks);
                        }
                    }
                    Flayout.EndHorizontal();

                    if (selectedGameObject != null)
                    {
                        Flayout.BeginHorizontal();
                        {
                            Flayout.Label((selectedGameObject.transform.parent != null ? (selectedGameObject.transform.parent.parent != null ? "..." : "") + "/" + selectedGameObject.transform.parent.name + "/" : "/") + selectedGameObject.name, Fstyle.labelLeft, Flayout.Width(100f));
                            Flayout.FlexibleSpace();

                            if (selectedGameObject.GetComponent<FmarkDisable>() == null)
                            {
                                if (Flayout.Button("Mark", Fstyle.button, Flayout.Width(50f)))
                                {
                                    var comp = selectedGameObject.AddComponent<FmarkDisable>();
                                    comp.lastState = selectedGameObject.activeSelf;
                                    comp.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
                                    var enableMark = selectedGameObject.GetComponent<FmarkEnable>();
                                    if (enableMark != null) DestroyImmediate(enableMark);
                                    Fmarks.RefreshMarks();
                                }
                            }
                            else
                            {
                                if (Flayout.Button("Unmark", Fstyle.button, Flayout.Width(50f)))
                                {
                                    DestroyImmediate(selectedGameObject.GetComponent<FmarkDisable>());
                                    Fmarks.RefreshMarks();
                                }
                            }
                        }
                        Flayout.EndHorizontal();
                    }

                    Flayout.Space();

                    for (int i = 0; i < Fmarks.disableMarks.Length; i++)
                    {
                        var mark = Fmarks.disableMarks[i];

                        if (mark == null) continue;

                        Flayout.BeginHorizontal();
                        {
                            Flayout.Label((mark.transform.parent != null ? (mark.transform.parent.parent != null ? "..." : "") + "/" + mark.transform.parent.gameObject.name + "/" : "/") + mark.gameObject.name, Fstyle.labelLeft, Flayout.Width(100f));
                            Flayout.FlexibleSpace();

                            if (mark.gameObject.activeSelf)
                            {
                                if (Flayout.Button("Disable", Fstyle.buttonLeft, Flayout.Width(50f)))
                                {
                                    mark.gameObject.SetActive(false);
                                    Fmarks.RefreshMarks();
                                }
                            }
                            else
                            {
                                if (Flayout.Button("Enable", Fstyle.buttonLeft, Flayout.Width(50f)))
                                {
                                    mark.gameObject.SetActive(true);
                                    Fmarks.RefreshMarks();
                                }
                            }

                            if (Flayout.Button("Unmark", Fstyle.buttonRight, Flayout.Width(50f)))
                            {
                                mark.gameObject.SetActive(true);
                                DestroyImmediate(mark);
                                Fmarks.RefreshMarks();
                            }
                        }
                        Flayout.EndHorizontal();
                    }
                }
            }
            Flayout.Space();
            Flayout.EndVertical();

            Flayout.BeginVerticalBox();
            {
                developmentFoldout = Flayout.Foldout(developmentFoldout, "Development Marks");
                if (developmentFoldout)
                {

                    Flayout.BeginHorizontal();
                    {
                        if (Flayout.Button("Enable All", Fstyle.buttonLeft))
                        {
                            Fmarks.EnableAll(Fmarks.developmentMarks);
                        }

                        if (Flayout.Button("Disable All", Fstyle.buttonRight))
                        {
                            Fmarks.DisableAll(Fmarks.developmentMarks);
                        }
                    }
                    Flayout.EndHorizontal();

                    if (selectedGameObject != null)
                    {
                        Flayout.BeginHorizontal();
                        {
                            Flayout.Label((selectedGameObject.transform.parent != null ? (selectedGameObject.transform.parent.parent != null ? "..." : "") + "/" + selectedGameObject.transform.parent.name + "/" : "/") + selectedGameObject.name, Fstyle.labelLeft, Flayout.Width(100f));
                            Flayout.FlexibleSpace();

                            if (selectedGameObject.GetComponent<FmarkDevelopment>() == null)
                            {
                                if (Flayout.Button("Mark", Fstyle.button, Flayout.Width(50f)))
                                {
                                    var comp = selectedGameObject.AddComponent<FmarkDevelopment>();
                                    comp.lastState = selectedGameObject.activeSelf;
                                    comp.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
                                    Fmarks.RefreshMarks();
                                }
                            }
                            else
                            {
                                if (Flayout.Button("Unmark", Fstyle.button, Flayout.Width(50f)))
                                {
                                    DestroyImmediate(selectedGameObject.GetComponent<FmarkDevelopment>());
                                    Fmarks.RefreshMarks();
                                }
                            }
                        }
                        Flayout.EndHorizontal();
                    }

                    Flayout.Space();

                    for (int i = 0; i < Fmarks.developmentMarks.Length; i++)
                    {
                        var mark = Fmarks.developmentMarks[i];

                        if (mark == null) continue;

                        Flayout.BeginHorizontal();
                        {
                            Flayout.Label((mark.transform.parent != null ? (mark.transform.parent.parent != null ? "..." : "") + "/" + mark.transform.parent.gameObject.name + "/" : "") + mark.gameObject.name, Fstyle.labelLeft, Flayout.Width(100f));
                            Flayout.FlexibleSpace();

                            if (mark.gameObject.activeSelf)
                            {
                                if (Flayout.Button("Disable", Fstyle.buttonLeft, Flayout.Width(50f)))
                                {
                                    mark.gameObject.SetActive(false);
                                    Fmarks.RefreshMarks();
                                }
                            }
                            else
                            {
                                if (Flayout.Button("Enable", Fstyle.buttonLeft, Flayout.Width(50f)))
                                {
                                    mark.gameObject.SetActive(true);
                                    Fmarks.RefreshMarks();
                                }
                            }

                            if (Flayout.Button("Unmark", Fstyle.buttonRight, Flayout.Width(50f)))
                            {
                                mark.gameObject.SetActive(true);
                                DestroyImmediate(mark);
                                Fmarks.RefreshMarks();
                            }
                        }
                        Flayout.EndHorizontal();
                    }
                }
            }
            Flayout.Space();
            Flayout.EndVertical();

            Flayout.Space();

            Flayout.EndScroll();
        }
    }
}
