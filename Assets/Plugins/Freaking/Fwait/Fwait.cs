using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freaking
{
    public class Fwait : MonoBehaviour
    {
        #region Singleton

        private static Fwait _instance;
        private static Fwait instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<Fwait>();
                    _instance.gameObject.name = "Freaking.Fwait";
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        #endregion

        private Dictionary<int, FwaitPromise> promises = new Dictionary<int, FwaitPromise>();
        private Dictionary<int, System.Func<bool>> conditions = new Dictionary<int, System.Func<bool>>();

        private HashSet<int> timerIndicies = new HashSet<int>();
        private HashSet<int> timerUnscaledIndicies = new HashSet<int>();
        private Dictionary<int, float> timers = new Dictionary<int, float>();
        private Dictionary<int, float> timersUnscaled = new Dictionary<int, float>();

        private List<int> keysToDelete = new List<int>();

        private int index;

        private FwaitPromise _Until(System.Func<bool> condition)
        {
            var promise = new FwaitPromise();

            promises.Add(index, promise);
            conditions.Add(index, condition);

            index++;

            return promise;
        }

        private FwaitPromise _ForSeconds(float seconds)
        {
            var promise = new FwaitPromise();

            promises.Add(index, promise);
            timers.Add(index, seconds);
            timerIndicies.Add(index);

            index++;

            return promise;
        }

        private FwaitPromise _ForSecondsUnscaled(float seconds)
        {
            var promise = new FwaitPromise();

            promises.Add(index, promise);
            timersUnscaled.Add(index, seconds);
            timerUnscaledIndicies.Add(index);

            index++;

            return promise;
        }

        private void Update()
        {
            foreach (KeyValuePair<int, System.Func<bool>> kvp in conditions)
            {
                if (kvp.Value())
                {
                    promises[kvp.Key].Resolve();
                    keysToDelete.Add(kvp.Key);
                }
            }

            foreach (var i in timerIndicies)
            {
                timers[i] -= Time.deltaTime;

                if (timers[i] < 0)
                {
                    promises[i].Resolve();
                    keysToDelete.Add(i);
                }
            }

            foreach (var i in timerUnscaledIndicies)
            {
                timersUnscaled[i] -= Time.unscaledDeltaTime;

                if (timersUnscaled[i] < 0)
                {
                    promises[i].Resolve();
                    keysToDelete.Add(i);
                }
            }

            if (keysToDelete.Count > 0)
            {
                foreach (var key in keysToDelete)
                {
                    promises.Remove(key);
                    if (conditions.ContainsKey(key)) conditions.Remove(key);
                    else if (timerIndicies.Contains(key))
                    {
                        timers.Remove(key);
                        timerIndicies.Remove(key);
                    }
                    else if (timerUnscaledIndicies.Contains(key))
                    {
                        timersUnscaled.Remove(key);
                        timerUnscaledIndicies.Remove(key);
                    }
                }

                keysToDelete.Clear();
            }
        }

        public static FwaitPromise Until(System.Func<bool> condition)
        {
            return instance._Until(condition);
        }

        public static FwaitPromise ForSeconds(float seconds)
        {
            return instance._ForSeconds(seconds);
        }

        public static FwaitPromise ForSecondsUnscaled(float seconds)
        {
            return instance._ForSecondsUnscaled(seconds);
        }
    }
}
