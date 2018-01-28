using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DZ.Core.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        #region Singleton

        static SceneLoader _instance;
        static SceneLoader instance
        {
            get
            {
                if (_instance == null)
                {
                    var instanceGameObject = new GameObject();
                    instanceGameObject.name = "SceneLoader";
                    DontDestroyOnLoad(instanceGameObject);
                    _instance = instanceGameObject.AddComponent<SceneLoader>();
                }

                return _instance;
            }
        }

        #endregion

        #region API

        public static void Load(string[] sceneNames, float delay = 0f)
        {
            foreach (var sceneName in sceneNames)
            {
                instance._Load(sceneName, delay);
            }
        }

        public static void Load(string sceneName, float delay = 0f)
        {
            instance._Load(sceneName, delay);
        }

        #endregion

        void _Load(string sceneName, float delay = 0f)
        {
            var entity = Contexts.state.CreateEntity();
            entity.sceneName = sceneName;
            StartCoroutine(_LoadSceneAndMarkAsLoaded(entity, delay));
        }

        IEnumerator _LoadSceneAndMarkAsLoaded(StateEntity entity, float delay)
        {
            if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(entity.sceneName).IsValid())
            {
                var loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(entity.sceneName, LoadSceneMode.Additive);
                yield return new WaitUntil(() => loadOperation.isDone);
                yield return new WaitForSeconds(delay);
            }

            entity.loaded = true;
        }
    }
}
