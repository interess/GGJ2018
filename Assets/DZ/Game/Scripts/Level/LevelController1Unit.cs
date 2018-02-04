using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController1Unit : LevelControllerUnit
    {
        bool firstHelperShown = false;

        public override void OnStart()
        {
            Debug.Log("Level start");

            firstHelperShown = false;

            var eventEntity = Contexts.input.CreateEventEntity();
            eventEntity.modalOpenEvent = true;
            eventEntity.modalId = "DayOne";

            Freaking.Fwait.ForSecondsUnscaled(3f).Done(() =>
            {
                var introEventEntity = Contexts.input.CreateEventEntity();
                introEventEntity.modalOpenEvent = true;
                introEventEntity.modalId = "IntroLetter";

                var closeEventEntity = Contexts.input.CreateEventEntity();
                closeEventEntity.modalCloseEvent = true;
                closeEventEntity.modalId = "DayOne";
            });

            // TODO: Start subs flow by events from here

            // Contexts.state.hudUnit.SetActive(true);
        }

        public override void HandleLevelEvent(InputEntity entity)
        {
            if (entity.HasEventId() && entity.eventId == "IntroLetter_Done")
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalOpenEvent = true;
                letterEventEntity.modalId = "FirstLetter";

                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalCloseEvent = true;
                eventEntity.modalId = "IntroLetter";
            }

            if (entity.HasEventId() && entity.eventId == "FirstLetter_Done")
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalCloseEvent = true;
                letterEventEntity.modalId = "FirstLetter";

                Contexts.state.hudUnit.SetActive(true);
                Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTimeEntity.worldTimeSpeed;
            }

            var baseScore = 1;

            if (!Contexts.state.HasScore())
            {
                Contexts.state.score = baseScore;
            }

            if (entity.HasScoreHeavyEvent())
            {
                Contexts.state.score += 30 + entity.wordLength * 2;
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

            if (Contexts.state.score < -15)
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

                if (!firstHelperShown)
                {
                    OnFirstHeavyRapport();
                    firstHelperShown = true;
                }

                Contexts.state.ticketManagerUnit.AddRaport(finalRaport);
            }
            else if (Contexts.state.score < -5)
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

        }

        void OnFirstHeavyRapport()
        {
            Freaking.Fwait.ForSecondsUnscaled(1f).Done(() =>
            {
                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalOpenEvent = true;
                eventEntity.modalId = "FirstHelper";
            });

            Freaking.Fwait.ForSecondsUnscaled(3f).Done(() =>
            {
                var numberOfRaports = PlayerPrefs.GetInt("Raports");
                numberOfRaports--;
                if (numberOfRaports < 0) numberOfRaports = 0;
                PlayerPrefs.SetInt("Raports", numberOfRaports);

                Contexts.state.ticketManagerUnit.Init(0, PlayerPrefs.GetInt("Warnings"));

                // Contexts.state.ticketManagerUnit.RemoveRaport();
            });
        }

        void GameOver()
        {
            var entity = Contexts.input.CreateEventEntity();
            entity.gameOverEvent = true;
        }
    }
}
