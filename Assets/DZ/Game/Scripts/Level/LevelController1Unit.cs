using UnityEngine;
using UnityEngine.Assertions;

namespace DZ.Game.Scripts
{
    public class LevelController1Unit : LevelControllerUnit
    {
        bool firstHelperShown = false;
        bool raportShown;
        bool warningShown;

        public override void OnStart()
        {
            Debug.Log("Level start");

            raportShown = false;
            warningShown = false;
            firstHelperShown = false;

            var eventEntity = Contexts.input.CreateEventEntity();
            eventEntity.modalOpenEvent = true;
            eventEntity.modalId = "DayOne";

            var controlsUnit = GameObject.FindObjectOfType<ControlsUnit>();
            controlsUnit.switchButton.gameObject.SetActive(false);

            Contexts.state.ticketManagerUnit.Init(PlayerPrefs.GetInt("Raports"), PlayerPrefs.GetInt("Warnings"));

            PlayerPrefs.SetInt("Raports", 0);
            PlayerPrefs.SetInt("Warnings", 0);

            Contexts.state.score = -16;

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
                letterEventEntity.modalId = "Newspaper";

                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalCloseEvent = true;
                eventEntity.modalId = "IntroLetter";
            }

            if (entity.HasEventId() && entity.eventId == "Newspaper_Done")
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalOpenEvent = true;
                letterEventEntity.modalId = "FirstLetter";

                var eventEntity = Contexts.input.CreateEventEntity();
                eventEntity.modalCloseEvent = true;
                eventEntity.modalId = "Newspaper";
            }

            if (entity.HasEventId() && entity.eventId == "FirstLetter_Done")
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalCloseEvent = true;
                letterEventEntity.modalId = "FirstLetter";

                var letter2EventEntity = Contexts.input.CreateEventEntity();
                letter2EventEntity.modalOpenEvent = true;
                letter2EventEntity.modalId = "FirstLetter2";
            }

            if (entity.HasEventId() && entity.eventId == "FirstLetter2_Done")
            {
                var letterEventEntity = Contexts.input.CreateEventEntity();
                letterEventEntity.modalCloseEvent = true;
                letterEventEntity.modalId = "FirstLetter2";

                Contexts.state.hudUnit.SetActive(true);
                Contexts.state.levelActiveEntity.levelSubsSpeed = Contexts.state.worldTimeEntity.worldTimeSpeed;
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

            if (Contexts.state.score < -40 && !warningShown)
            {
                warningShown = true;
                Contexts.state.score = 0;

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
            else if (Contexts.state.score < -20 && !raportShown)
            {
                raportShown = true;
                Contexts.state.score = 0;

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

                if (!firstHelperShown)
                {
                    OnFirstHeavyRapport();
                    firstHelperShown = true;
                    raportShown = false;
                }
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

                Contexts.state.ticketManagerUnit.Init(numberOfRaports, PlayerPrefs.GetInt("Warnings"));

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
