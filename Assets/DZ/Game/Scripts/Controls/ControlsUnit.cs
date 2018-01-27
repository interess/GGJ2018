using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class ControlsUnit : MonoBehaviour
    {
        // TODO: Add double space recognition
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateChannelSwitchEvent();
            }
        }

        private void CreateChannelSwitchEvent()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.channelSwitchEvent = true;
        }
    }
}
