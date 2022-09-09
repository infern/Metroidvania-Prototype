using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Boss;
using PlatformerPrototype.InfernKP.Player;
using PlatformerPrototype.InfernKP.Events;



namespace PlatformerPrototype.InfernKP
{
    public class BossFightTrigger : MonoBehaviour
    {
        #region Variables

        [Header("Data")]//***********
        bool bossFightStarted=false;

        [Header("Components")]//***********
        [SerializeField] BossController boss;

        #endregion

        #region Unity Methods
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (bossFightStarted) return;
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null) StartFight(player);

        }

        #endregion

        #region Unique Methods



        void StartFight(PlayerController player)
        {
            bossFightStarted = true;
            boss.ChaseTarget = player.transform;
            GameProgressEvents.StartBossFight();
        }

        #endregion
    }
}
