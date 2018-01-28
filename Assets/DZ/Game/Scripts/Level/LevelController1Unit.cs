using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController1Unit : LevelControllerUnit
    {
        public override void OnStart()
        {
            Debug.Log("Level start");

            var eventEntity = Contexts.input.CreateEventEntity();
            eventEntity.modalOpenEvent = true;
            eventEntity.modalId = "DayOne";

            Freaking.Fwait.ForSecondsUnscaled(3f).Done(() =>
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalOpenEvent = true;
                // letterEventEntity.modalOpenBackgroundEvent = true;
                letterEventEntity.modalId = "FirstLetter";

                var closeEventEntity = Contexts.input.CreateEventEntity();
                closeEventEntity.modalCloseEvent = true;
                closeEventEntity.modalId = "DayOne";
            });

            // TODO: Start subs flow by events from here

            // Contexts.state.hudUnit.SetActive(true);
        }

        public override void HandleLevelEvent(InputEntity entity)
        {
            if (entity.HasEventId() && entity.eventId == "FirstLetter_Done")
            {
                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalCloseEvent = true;
                eventEntity.modalId = "FirstLetter";

                Contexts.state.hudUnit.SetActive(true);
                Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTimeEntity.worldTimeSpeed;
            }
        }
    }
}
