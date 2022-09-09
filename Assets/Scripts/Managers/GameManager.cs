using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Events;

namespace PlatformerPrototype.InfernKP
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        //[Header("Settings")]//***********

        [Header("Data")]//***********
        bool gameActive;
        bool gamePaused;

        [Header("Components")]//***********
        [SerializeField] GameObject camFollowPlayer;
        [SerializeField] GameObject camBossFight;
        [SerializeField] GameObject invisibleWalls;


        #endregion

        #region Unity Methods

        private void Awake()
        {
            Time.timeScale = 1f;
        }

        private void OnEnable()
        {
            GameProgressEvents.PlayerDiedTrigger += GameOver;
            GameProgressEvents.BossDiedTrigger += Victory;
            GameProgressEvents.PauseToggleTrigger += PauseToggle;
            GameProgressEvents.StartBossFightTrigger += EnableBossFight;

        }

        private void OnDisable()
        {
            GameProgressEvents.PlayerDiedTrigger -= GameOver;
            GameProgressEvents.BossDiedTrigger -= Victory;
            GameProgressEvents.PauseToggleTrigger -= PauseToggle;
            GameProgressEvents.StartBossFightTrigger -= EnableBossFight;


        }


        #endregion

        #region Unique Methods

        void Victory()
        {
            UI_events.ShowEndBanner(true);
            gameActive = false;

        }

        void GameOver()
        {
            UI_events.ShowEndBanner(false);
            gameActive = false;

        }

        void PauseToggle()
        {
            if (gamePaused)
            {
                UI_events.ToggleMenu(false);
                Time.timeScale = 1f;
            }
            else
            {
                UI_events.ToggleMenu(true);
                Time.timeScale = 0f;
            }

            gamePaused = !gamePaused;
        }

        void EnableBossFight()
        {
            camFollowPlayer.SetActive(false);
            camBossFight.SetActive(true);
            invisibleWalls.SetActive(true);

        }

        #endregion
    }
}
