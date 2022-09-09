using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Interfaces;
using PlatformerPrototype.InfernKP.Statics;


namespace PlatformerPrototype.InfernKP.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CollisionChecks))]
    public class PlayerMove : MonoBehaviour, IButtonPress
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField] [Range(2f, 226f)] float moveSpeed;
        [SerializeField] float acceleration;
        [SerializeField] float deceleration;
        [SerializeField] float velocityPower;
        [SerializeField] float frictionAmount;
        [SerializeField] float speedWhileCastingMultiplier;
        [SerializeField] float footstepSoundDelayDuration;

        [Header("Data")]//***********
        bool isFacingRight;
        Vector2 direction;
        int footstepsSoundsIndex;
        float footstepSoundDelayTimer = -1f;
        float lerpAmount = 1;


        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;
        [SerializeField] List<AudioClip> footStepsSounds;
        [SerializeField] AudioSource audioSource;


        #endregion

        #region Unity Methods
        void Awake()
        {
            SetStartingFacingDirection();
        }
        void Update()
        {
            CheckForTurning();
        }
        void FixedUpdate()
        {
            Execute();
            Friction();
        }

        #endregion

        #region Unique Methods
        public void ButtonDown(Vector2 direction)
        {
            this.direction = direction;
        }

        public void ButtonUp()
        {

        }



        void Friction()
        {
            if (Controller.CollisionChecks.Grounded && Mathf.Abs(direction.x) < .01f)
            {
                float amount = Mathf.Min(Mathf.Abs(Controller.Rigidbody.velocity.x), Mathf.Abs(frictionAmount));
                amount *= Mathf.Sign(Controller.Rigidbody.velocity.x);
                Controller.Rigidbody.AddForce(-amount * Vector2.right, ForceMode2D.Impulse);


            }
        }



        public void Execute()
        {
            if (!Controller.StatusScript.CanCast) return;
            if (direction.x == 0)
            {
                if (Controller.CollisionChecks.Grounded)
                    Controller.Animator.ChangeAnimationState(Controller.Animator.Idle, false);
                return;
            }

            if (Controller.CollisionChecks.Grounded)
                Controller.Animator.ChangeAnimationState(Controller.Animator.Run, false);

            float targetSpeed = direction.x * moveSpeed;
            targetSpeed = Mathf.Lerp(Controller.Rigidbody.velocity.x, targetSpeed, lerpAmount);
            float speedDifference = targetSpeed - Controller.Rigidbody.velocity.x;
            float accelerationRate = (Mathf.Abs(targetSpeed) > .01f) ? acceleration : deceleration;
            float movement = speedDifference * accelerationRate;
            Controller.Rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
            PlayRandomSoundFromList();
        }


        public void CheckForTurning()
        {
            if (direction.x != 0 && Controller.StatusScript.CanCast)
            {
                bool isMovingRight = direction.x > 0;
                if (isMovingRight != isFacingRight)
                    Turn();
            }
        }

        void Turn()
        {
            float angle = isFacingRight ? 180f : 0f;
            transform.rotation = Quaternion.Euler(0, angle, 0);
            isFacingRight = !isFacingRight;
        }


        void SetStartingFacingDirection()
        {
            isFacingRight = transform.eulerAngles.y == 0f;
        }

        void PlayRandomSoundFromList()
        {
            if (Timer.IsFinished(footstepSoundDelayTimer) && Controller.CollisionChecks.Grounded)
            {
                footstepSoundDelayTimer = Timer.Start(footstepSoundDelayDuration);
                footstepsSoundsIndex = (footstepsSoundsIndex + Random.Range(1, footStepsSounds.Count - 1)) % footStepsSounds.Count;
                audioSource.clip = footStepsSounds[footstepsSoundsIndex];
                audioSource.Play();
            }

        }

        #endregion
    }
}
