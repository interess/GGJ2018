using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelControllerUnit : MonoBehaviour
    {
        public int index { get { return int.Parse(gameObject.name); } }
    }
}
