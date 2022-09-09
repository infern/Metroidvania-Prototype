using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Player;
using PlatformerPrototype.InfernKP.Statics;


namespace PlatformerPrototype.InfernKP
{
    public class TutorialTrigger : MonoBehaviour
    {
        #region Variables

        [Header("Data")]//***********
        bool isOn = true;
        float triggerCooldownTimer = -1;

        [Header("Components")]//***********
        [SerializeField] Animator tutorialAnimator;

        #endregion

        #region Unity Methods
        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && Timer.IsFinished(triggerCooldownTimer)) TutorialToggle();
        }

        #endregion

        #region Unique Methods



        void TutorialToggle()
        {
            triggerCooldownTimer = Timer.Start(0.5f);
            string animation = isOn ? "disappear" : "appear";
            tutorialAnimator.Play(animation);
            isOn=!isOn;
        }

        #endregion
    }

}
