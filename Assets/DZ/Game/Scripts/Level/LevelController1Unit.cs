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

            var baseScore = -3;

            if (!Contexts.state.HasScore())
            {
                Contexts.state.score = baseScore;
            }

            if (entity.HasScoreHeavyEvent())
            {
                Contexts.state.score += 100 + entity.wordLength * 2;
            }
            else if (entity.HasMistakeHeavyEvent())
            {
                Contexts.state.score -= (10 + entity.wordLength);
            }
            else if (entity.HasMistakeLightEvent())
            {
                Contexts.state.score -= 1;
            }

            Debug.Log(Contexts.state.score);

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
            Debug.Log("GameOver - ");
            // Go to menu, clean player prefs
        }
    }
}
