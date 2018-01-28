using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController1Unit : LevelControllerUnit
    {
        public override void OnStart()
        {
            Debug.Log("Level start");
            Contexts.state.hudUnit.SetActive(true);
        }

        public override void HandleLevelEvent(InputEntity entity)
        {
            Debug.Log("Handle level event");
        }
    }
}
