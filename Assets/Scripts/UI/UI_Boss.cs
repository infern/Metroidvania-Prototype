using UnityEngine;
using UnityEngine.UI;
using PlatformerPrototype.InfernKP.Events;
using System.Collections;

namespace PlatformerPrototype.InfernKP.UI
{
    public class UI_Boss : UI_Health
    {
        #region Variables

        //[Header("Settings")]//***********


        //[Header("Data")]//***********

        [Header("Components")]//***********
        [SerializeField] GameObject bossHealthBar;
        [SerializeField] Animator animator;
        [SerializeField] GameObject enragedText;
        [SerializeField] AudioSource audioSource;




        #endregion

        #region Unity Methods

        void OnEnable()
        {
            UI_events.BossLostHpTrigger += AdjustHealthBar;
            GameProgressEvents.BossDiedTrigger += DisableHealthBar;
            GameProgressEvents.PlayerDiedTrigger += DisableHealthBar;
            GameProgressEvents.StartBossFightTrigger += EnableHealthBar;
            GameProgressEvents.BossDiedTrigger += EnragedTextToggle;
            UI_events.EnragedTextToggleTrigger += EnragedTextToggle;
        }

        void OnDisable()
        {
            UI_events.BossLostHpTrigger -= AdjustHealthBar;
            GameProgressEvents.BossDiedTrigger -= DisableHealthBar;
            GameProgressEvents.PlayerDiedTrigger -= DisableHealthBar;
            GameProgressEvents.StartBossFightTrigger -= EnableHealthBar;
            GameProgressEvents.BossDiedTrigger -= EnragedTextToggle;
            UI_events.EnragedTextToggleTrigger -= EnragedTextToggle;
        }

        void EnableHealthBar()
        {
            bossHealthBar.SetActive(true);

        }
        void DisableHealthBar() => animator.Play("disappear");

        void EnragedTextToggle()
        {
            bool value = enragedText.activeInHierarchy ? false : true;
            if (value) audioSource.Play();
            enragedText.SetActive(value);

        }

        #endregion



    }
}
