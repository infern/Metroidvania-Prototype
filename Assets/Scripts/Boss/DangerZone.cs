using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PlatformerPrototype.InfernKP.Boss
{
    public class DangerZone : MonoBehaviour
    {
        #region Variables

        [Header("Data")]//***********
        float delayBeforeArrowsFall;
        public float DelayBeforeRockFalls
        {
            get => delayBeforeArrowsFall;
            set => delayBeforeArrowsFall = value;
        }
        public Boulder Boulder => boulder;

        [Header("Components")]//***********
        [SerializeField] Animator animator;
        [SerializeField] Boulder boulder;

        #endregion

        #region Default Methods

        private void OnDisable()
        {
            boulder.gameObject.SetActive(false);
        }

        #endregion

        #region Unique Methods

        public void Begin()
        {
            StartCoroutine(FallingArrows());
        }
       IEnumerator FallingArrows()
        {
            yield return new WaitForSeconds(delayBeforeArrowsFall);
            animator.Play("disappear");
            boulder.gameObject.SetActive(true);
        }

        #endregion
    }


}