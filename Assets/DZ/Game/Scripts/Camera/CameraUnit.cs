using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class CameraUnit : MonoBehaviour
    {
        public string id;

        private Camera __camera;
        new public Camera camera { get { if (__camera == null) { __camera = GetComponent<Camera>(); } return __camera; } }
    }
}
