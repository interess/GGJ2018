using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DevTools
{
    public class ReplaceGameObjects : EditorWindow
    {
        public GameObject Prefab;
        public GameObject[] ObjectsToReplace;
        public List<GameObject> TempObjects = new List<GameObject>();
        public bool KeepOriginalNames = true;
        public bool EditMode = false;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/ReplaceGameObjects")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            ReplaceGameObjects window = (ReplaceGameObjects) EditorWindow.GetWindow(typeof(ReplaceGameObjects));

            window.Show();

        }

        void OnSelectionChange()
        {

            GetSelection();
            Repaint();
        }

        void OnGUI()
        {
            EditMode = GUILayout.Toggle(EditMode, "Edit");
            if (GUI.changed)
            {
                if (EditMode)
                    GetSelection();
                else
                    ResetPreview();
            }
            KeepOriginalNames = GUILayout.Toggle(KeepOriginalNames, "Keep names");
            GUILayout.Space(5);
            if (EditMode)
            {
                ResetPreview();

                GUI.color = Color.yellow;
                if (Prefab != null)
                {
                    GUILayout.Label("Prefab: ");
                    GUILayout.Label(Prefab.name);
                }
                else
                {
                    GUILayout.Label("No prefab selected");
                }
                GUI.color = Color.white;

                GUILayout.Space(5);
                GUILayout.BeginScrollView(new Vector2());
                foreach (GameObject go in ObjectsToReplace)
                {
                    GUILayout.Label(go.name);

                    if (Prefab != null)
                    {
                        GameObject newObject;
                        newObject = (GameObject) PrefabUtility.InstantiatePrefab(Prefab);
                        newObject.transform.SetParent(go.transform.parent, true);
                        newObject.transform.localPosition = go.transform.localPosition;
                        newObject.transform.localRotation = go.transform.localRotation;
                        newObject.transform.localScale = go.transform.localScale;
                        TempObjects.Add(newObject);
                        if (KeepOriginalNames)
                            newObject.transform.name = go.transform.name;
                        go.SetActive(false);
                    }
                }
                GUILayout.EndScrollView();

                GUILayout.Space(5);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Apply"))
                {
                    foreach (GameObject go in ObjectsToReplace)
                    {
                        DestroyImmediate(go);
                    }
                    EditMode = false;
                };

                if (GUILayout.Button("Cancel"))
                {
                    ResetPreview();
                    EditMode = false;
                };
                GUILayout.EndHorizontal();
            }
            else
            {
                ObjectsToReplace = new GameObject[0];
                TempObjects.Clear();
                Prefab = null;
            }

        }

        void OnDestroy()
        {
            ResetPreview();
        }

        void GetSelection()
        {
            if (EditMode && Selection.activeGameObject != null)
            {
                PrefabType t = PrefabUtility.GetPrefabType(Selection.activeGameObject);
                if (t == PrefabType.None || t == PrefabType.PrefabInstance)
                {
                    ResetPreview();
                    ObjectsToReplace = Selection.gameObjects;
                }
                else if (t == PrefabType.Prefab)
                {
                    Prefab = Selection.activeGameObject;
                }

            }
        }

        void ResetPreview()
        {
            if (TempObjects != null)
            {
                foreach (GameObject go in TempObjects)
                {
                    DestroyImmediate(go);
                }
            }

            foreach (GameObject go in ObjectsToReplace)
            {
                go.SetActive(true);
            }

            TempObjects.Clear();
        }
    }
}
