using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController2Unit : LevelControllerUnit
    {
        string cachedLetterModalName;
        bool switchedToChannel;

        public override void OnStart()
        {
            Debug.Log("Level start 2");

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
            if (entity.HasEventId() && entity.eventId == "FirstLetter_Done")
            {
                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalCloseEvent = true;
                eventEntity.modalId = cachedLetterModalName;

                Contexts.state.hudUnit.SetActive(true);
                Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTimeEntity.worldTimeSpeed;

                switchedToChannel = false;

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

            var baseScore = 10;

            if (!Contexts.state.HasScore())
            {
                Contexts.state.score = baseScore;
            }

            if (entity.HasScoreHeavyEvent())
            {
                Contexts.state.score += 31;
            }
            else if (entity.HasMistakeHeavyEvent())
            {
                Contexts.state.score -= 30;
            }
            else if (entity.HasMistakeLightEvent())
            {
                Contexts.state.score -= 1;
            }

            var finalWarning = false;
            var finalRaport = false;

            if (Contexts.state.score < -5)
            {
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
            else if (Contexts.state.score < -15)
            {
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
        }

        void GameOver()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.gameOverEvent = true;
        }
    }
}
