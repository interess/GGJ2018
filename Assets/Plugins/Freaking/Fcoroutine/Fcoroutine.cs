using System.Collections;
using UnityEngine;

namespace Freaking
{
    public class Fcoroutine : MonoBehaviour
    {
        #region Singleton

        private static Fcoroutine _instance;
        private static Fcoroutine instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<Fcoroutine>();
                    _instance.gameObject.name = "Freaking.Coroutine.Manager";
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        #endregion

        public static UnityEngine.Coroutine Start(IEnumerator routine)
        {
            return routine == null ? null : instance.StartCoroutine(routine);
        }

        public static void Stop(IEnumerator routine)
        {
            instance.StopCoroutine(routine);
        }

        public static void Stop(UnityEngine.Coroutine routine)
        {
            instance.StopCoroutine(routine);
        }
    }
}
