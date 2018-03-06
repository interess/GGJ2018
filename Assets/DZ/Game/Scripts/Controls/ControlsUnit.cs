using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DZ.Game.Scripts
{
    public class ControlsUnit : MonoBehaviour
    {
        private float __secondsSinceLastPress;
        private float __secondsSincePress;
        private int __pressCounter;

        private bool __recordStartEventCreated;

        public HUDButtonUnit markerButton;
        public HUDButtonUnit switchButton;

        void Start()
        {
            markerButton.onPointerDown += () =>
            {
                CreateStartRecordEvent();
            };

            markerButton.onPointerUp += () =>
            {
                CreateStopRecordEvent();
            };

            switchButton.onPointerClick += () =>
            {
                CreateChannelSwitchEvent();
            };
        }

        public void Update()
        {
            __secondsSinceLastPress += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                __secondsSinceLastPress = 0f;
                __secondsSincePress = 0f;
            }

            if (__secondsSinceLastPress > 0.2f)
            {
                __pressCounter = 0;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                __secondsSincePress += Time.deltaTime;

                if (__secondsSincePress > 0.18f && !__recordStartEventCreated)
                {
                    __pressCounter = 0;

                    __recordStartEventCreated = true;
                    CreateStartRecordEvent();
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                __secondsSincePress = 0f;
                __pressCounter++;
                __recordStartEventCreated = false;
                CreateStopRecordEvent();
            }

            if (__pressCounter >= 2)
            {
                __pressCounter = 0;
                CreateChannelSwitchEvent();
            }
        }

        private void CreateChannelSwitchEvent()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.channelSwitchEvent = true;
        }

        private void CreateStartRecordEvent()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.subsRecordStartEvent = true;
        }

        private void CreateStopRecordEvent()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.subsRecordStopEvent = true;
        }
    }
}
