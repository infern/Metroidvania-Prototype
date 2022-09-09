using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Interfaces;
using PlatformerPrototype.InfernKP.Events;
using UnityEngine.InputSystem;


namespace PlatformerPrototype.InfernKP.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [Header("Player Scripts")]//***********
        [SerializeField] PlayerStatus status;
        public PlayerStatus StatusScript => status;

        [SerializeField] PlayerMove move;
        public PlayerMove MoveScript => move;

        [SerializeField] PlayerJump jump;
        public PlayerJump JumpScript => jump;

        [SerializeField] PlayerAttack attack;
        public PlayerAttack AttackScript => attack;

        [SerializeField] PlayerAnimator animator;
        public PlayerAnimator Animator => animator;

        [SerializeField] PlayerInput playerInput;
        public PlayerInput PlayerInput => playerInput;

        [Header("General Scripts")]//***********
        [SerializeField] CollisionChecks collisionChecks;
        public CollisionChecks CollisionChecks => collisionChecks;

        [Header("Components")]//***********
        [SerializeField] Rigidbody2D rigidBody;
        public Rigidbody2D Rigidbody => rigidBody;






        #endregion

        void OnEnable()
        {
            GameProgressEvents.BossDiedTrigger += VictoryPose;
        }

         void OnDisable()
        {
            GameProgressEvents.BossDiedTrigger -= VictoryPose;

        }

        #region Unique Methods

        void VictoryPose()
        {
            animator.ChangeAnimationState(animator.Idle, false);
            playerInput.enabled = false;
            StartCoroutine(DelayBeforePose());
        }

        IEnumerator DelayBeforePose()
        {
            yield return new WaitForSeconds(1.9f);
            status.IsActive = false;
            animator.ForceAnimationState(animator.VictoryPose);
        }


        #endregion

    }
}
