using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFramework
{
    public static class FUidGenerator
    {
        private static int _nextFuid = 0;
        public static int nextFuid
        {
            get
            {
                if (freeUids.Count > 0)
                {
                    return freeUids.Dequeue();
                }

                _nextFuid++;
                return _nextFuid;
            }
        }

        private static Queue<int> freeUids = new Queue<int>();
        public static void FreeUid(int uid)
        {
            freeUids.Enqueue(uid);
        }
    }
}
