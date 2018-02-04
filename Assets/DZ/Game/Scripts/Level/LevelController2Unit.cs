using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController2Unit : LevelControllerUnit
    {
        string cachedLetterModalName;
        bool switchedToChannel;
        bool raportShown;
        bool warningShown;

        public override void OnStart()
        {
            Debug.Log("Level start 2");

            raportShown = false;
            warningShown = false;

            var eventEntity = Contexts.input.CreateEventEntity();
            eventEntity.modalOpenEvent = true;
            eventEntity.modalId = "DayTwo";

            Freaking.Fwait.ForSecondsUnscaled(3f).Done(() =>
            {
                var rapport = PlayerPrefs.GetInt("Raports");

                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalOpenEvent = true;
                letterEventEntity.modalId = "SecondLetter";

                if (rapport < 2) letterEventEntity.modalId += "Success";
                else letterEventEntity.modalId += "Fail";

                cachedLetterModalName = letterEventEntity.modalId;

                var closeEventEntity = Contexts.input.CreateEventEntity();
                closeEventEntity.modalCloseEvent = true;
                closeEventEntity.modalId = "DayTwo";
            });
        }

        public override void HandleLevelEvent(InputEntity entity)
        {
            if (entity.HasEventId() && entity.eventId == "SecondLetter_Done")
            {
                var closeEventEntity = Contexts.input.CreateEventEntity();
                closeEventEntity.modalCloseEvent = true;
                closeEventEntity.modalId = cachedLetterModalName;

                Contexts.state.hudUnit.SetActive(true);
                Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTimeEntity.worldTimeSpeed;

                Freaking.Fwait.ForSeconds(10f).Done(() =>
                {
                    if (!switchedToChannel)
                    {
                        var helperModal = Contexts.input.CreateEventEntity();
                        helperModal.modalOpenEvent = true;
                        helperModal.modalId = "SwitchingHelper";
                    }
                });
            }

            if (entity.channelSwitchEvent)
            {
                switchedToChannel = true;
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
