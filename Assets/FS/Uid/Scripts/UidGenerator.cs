using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FS.Uid.Scripts
{
    public static class UidGenerator
    {
        private static int __nextUid = 0;
        public static int nextUid
        {
            get
            {
                if (__freeUids.Count > 0)
                {
                    return __freeUids.Dequeue();
                }

                __nextUid++;
                return __nextUid;
            }
        }

        private static Queue<int> __freeUids = new Queue<int>();
        public static void FreeUid(int uid)
        {
            __freeUids.Enqueue(uid);
        }
    }
}
