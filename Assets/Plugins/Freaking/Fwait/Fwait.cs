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
                    _instance.gameObject.name = "Freaking.Wait.Manager";
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }

        #endregion

        private Dictionary<int, Fpromise> promises = new Dictionary<int, Fpromise>();
        private Dictionary<int, System.Func<bool>> conditions = new Dictionary<int, System.Func<bool>>();

        private HashSet<int> timerIndicies = new HashSet<int>();
        private Dictionary<int, float> timers = new Dictionary<int, float>();

        private List<int> keysToDelete = new List<int>();

        private int index;

        private Fpromise _Until(System.Func<bool> condition)
        {
            var promise = new Fpromise();

            promises.Add(index, promise);
            conditions.Add(index, condition);

            index++;

            return promise;
        }

        private Fpromise _ForSeconds(float seconds)
        {
            var promise = new Fpromise();

            promises.Add(index, promise);
            timers.Add(index, seconds);
            timerIndicies.Add(index);

            index++;

            return promise;
        }

        private Fpromise _ForRealSeconds(float seconds)
        {
            var promise = new Fpromise();

            promises.Add(index, promise);
            timers.Add(index, seconds);

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
                }

                keysToDelete.Clear();
            }
        }

        public static Fpromise Until(System.Func<bool> condition)
        {
            return instance._Until(condition);
        }

        public static Fpromise ForSeconds(float seconds)
        {
            return instance._ForSeconds(seconds);
        }
    }
}
