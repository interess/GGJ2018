using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelControllerUnit : MonoBehaviour
    {
        public int index { get { return int.Parse(gameObject.name); } }

        public virtual void OnStart()
        {

        }

        public virtual void HandleLevelEvent(InputEntity entity)
        {

        }
    }
}
