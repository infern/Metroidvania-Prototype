using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossFlip : MonoBehaviour
    {
        #region Variables

        //[Header("Settings")]//***********

        [Header("Data")]//***********
        bool isFacingRight;
        public bool IsFacingRight
        {
            get => isFacingRight;
            set => isFacingRight = value;
        }

        [Header("Components")]//***********
        [SerializeField] BossController Controller;

        #endregion

        #region Unity Methods

     void Awake()
        {
            SetStartingFacingDirection();
        }

        #endregion

        #region Unique Methods

        public void SetStartingFacingDirection()
        {
            isFacingRight = transform.eulerAngles.y == 0f;
        }

        public bool PlayerIsBehind()
        {
            Vector3 pos1 = isFacingRight ? transform.position : Controller.ChaseTarget.position;
            Vector3 pos2 = isFacingRight ? Controller.ChaseTarget.position : transform.position;

            bool notFacingPlayer = (pos1 - pos2).normalized.x > 0;
            if (notFacingPlayer) return true;
            else return false;

        }


        public void TurnToFacePlayerDirection()
        {
            float angle = isFacingRight ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            isFacingRight = !isFacingRight;
        }

        #endregion
    }
}