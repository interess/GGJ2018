using System.Collections.Generic;
using UnityEngine;

namespace FreakingEditor
{
    public class FscenesConfig : ScriptableObject
    {
        public List<string> scenePaths;
        public List<bool> devOnly;
        public int mainScene;

        public FscenesConfig()
        {
            scenePaths = new List<string>();
            devOnly = new List<bool>();
        }
    }
}
