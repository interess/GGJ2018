using UnityEngine;

namespace FreakingEditor
{
    [ExecuteInEditMode]
    [System.Serializable]
    public class FmarkBase : MonoBehaviour
    {
        public bool lastState;
        public bool lastStateBeforeSceneClose;

#if UNITY_EDITOR
        private bool restricted;

        private void Awake()
        {
            restricted = true;
        }

        private void Start()
        {
            restricted = false;
        }

        private void OnEnable()
        {
            if (!restricted && !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) lastState = true;
        }

        private void OnDisable()
        {
            if (!restricted && !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) lastState = false;
        }
#endif
    }

}
