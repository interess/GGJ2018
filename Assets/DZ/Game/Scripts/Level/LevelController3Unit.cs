using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController3Unit : LevelControllerUnit
    {
        bool raportShown;
        bool warningShown;

        public override void OnStart()
        {
            Debug.Log("Level start");

            var closeEventEntity2 = Contexts.input.CreateEventEntity();
            closeEventEntity2.modalOpenEvent = true;
            closeEventEntity2.modalId = "DayThree";

            raportShown = false;
            warningShown = false;

            var controlsUnit = GameObject.FindObjectOfType<ControlsUnit>();
            controlsUnit.switchButton.gameObject.SetActive(true);

            Freaking.Fwait.ForSecondsUnscaled(3f).Done(() =>
            {
                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalOpenEvent = true;
                eventEntity.modalId = "DayRadioOne";

                var closeEventEntity = Contexts.input.CreateEventEntity();
                closeEventEntity.modalCloseEvent = true;
                closeEventEntity.modalId = "DayThree";
            });
        }

        public override void HandleLevelEvent(InputEntity entity)
        {
            if (entity.HasEventId() && entity.eventId == "Radio_Done")
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalCloseEvent = true;
                letterEventEntity.modalId = "DayRadioOne";

                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalOpenEvent = true;
                eventEntity.modalId = "ThirdLetter";
            }

            if (entity.HasEventId() && entity.eventId == "ThirdLetter_Done")
            {
                var closeEventEntity = Contexts.input.CreateEventEntity();
                closeEventEntity.modalCloseEvent = true;
                closeEventEntity.modalId = "ThirdLetter";

                Contexts.state.hudUnit.SetActive(true);
                Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTimeEntity.worldTimeSpeed;
            }

            var baseScore = 0;

            if (!Contexts.state.HasScore())
            {
                Contexts.state.score = baseScore;
            }

            if (entity.HasScoreHeavyEvent())
            {
                Contexts.state.score += entity.scoreHeavy;
            }
            else if (entity.HasMistakeHeavyEvent())
            {
                Contexts.state.score -= entity.mistakeHeavy;
            }
            else if (entity.HasMistakeLightEvent())
            {
                Contexts.state.score -= 1;
            }

            var finalWarning = false;
            var finalRaport = false;

            if (Contexts.state.score < -40 && !raportShown)
            {
                raportShown = true;
                Contexts.state.score = baseScore;

                var numberOfRaports = PlayerPrefs.GetInt("Raports");
                numberOfRaports++;
                PlayerPrefs.SetInt("Raports", numberOfRaports);

                if (numberOfRaports >= 3)
                {
                    finalRaport = true;
                    PlayerPrefs.SetInt("Raports", 0);
                    GameOver();
                }

                Contexts.state.ticketManagerUnit.AddRaport(finalRaport);
            }
            else if (Contexts.state.score < -20 && !warningShown)
            {
                warningShown = true;
                Contexts.state.score = baseScore;

                var numberOfWarnings = PlayerPrefs.GetInt("Warnings");
                numberOfWarnings++;
                PlayerPrefs.SetInt("Warnings", numberOfWarnings);

                if (numberOfWarnings >= 3)
                {
                    finalWarning = true;
                    PlayerPrefs.SetInt("Warnings", 0);

                    var numberOfRaports = PlayerPrefs.GetInt("Raports");
                    numberOfRaports++;
                    PlayerPrefs.SetInt("Raports", numberOfRaports);

                    if (numberOfRaports >= 3)
                    {
                        finalRaport = true;
                        PlayerPrefs.SetInt("Raports", 0);
                        GameOver();
                    }

                    Contexts.state.ticketManagerUnit.AddRaport(finalRaport);
                }

                Contexts.state.ticketManagerUnit.AddWarning(finalWarning);

            }
        }

        void GameOver()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.gameOverEvent = true;
        }
    }
}
