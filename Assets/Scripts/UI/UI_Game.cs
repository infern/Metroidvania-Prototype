using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Events;
using UnityEngine.SceneManagement;


namespace PlatformerPrototype.InfernKP.UI
{
    public class UI_Game : MonoBehaviour
    {
        #region Variables

        [Header("Components")]//***********
        [SerializeField] GameObject gameMenu;
        [SerializeField] GameObject victoryBanner;
        [SerializeField] GameObject gameOverBanner;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip victorySound;
        [SerializeField] AudioClip gameOverSound;



        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            UI_events.ShowEndBannerTrigger += ShowEndBanner;
            UI_events.ToggleMenuTrigger += ToggleGameMenu;


        }
        private void OnDisable()
        {
            UI_events.ShowEndBannerTrigger -= ShowEndBanner;
            UI_events.ToggleMenuTrigger -= ToggleGameMenu;

        }

        #endregion

        #region Unique Methods


        public void QuitButton() => Application.Quit();
        public void RestartButton() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        public void ToggleGameMenu(bool value) => gameMenu.SetActive(value);


        public void ShowEndBanner(bool playerWon)
        {
            if (playerWon) StartCoroutine(VictoryCoroutine());
            else StartCoroutine(GameOverCoroutine());

        }

        IEnumerator VictoryCoroutine()
        {
            yield return new WaitForSeconds(3.2f);
            victoryBanner.SetActive(true);
            audioSource.clip = victorySound;
            audioSource.Play();
            yield return new WaitForSeconds(1.8f);
            UI_events.ToggleMenu(true);
        }

        IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSeconds(2.94f);
            gameOverBanner.SetActive(true);
            audioSource.clip = gameOverSound;
            audioSource.Play();
            yield return new WaitForSeconds(1.8f);
            UI_events.ToggleMenu(true);
        }

        #endregion
    }
}