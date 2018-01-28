using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Freaking.Extensions.GameObject
{
    public static class GameObjectExtensions
    {
        public static string GetPath(this UnityEngine.GameObject obj)
        {
            var path = "/" + obj.name;

            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }

            return path;
        }
    }
}
