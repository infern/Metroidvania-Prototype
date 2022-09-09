using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Interfaces;
using PlatformerPrototype.InfernKP.Statics;



namespace PlatformerPrototype.InfernKP.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CollisionChecks))]
    public class PlayerJump : MonoBehaviour, IButtonPress
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField] [Range(1f, 6f)] float lowJumpForce;
        [SerializeField] [Range(20f, 30f)] float highJumpForce;
        [SerializeField] [Range(0.1f, 16f)] float highJumpMaxDuration;
        [SerializeField] [Range(4f, 10f)] float doubleJumpForce;

        [SerializeField] [Range(.09f, .5f)] float buttonBufferDuration = .2f;
        [SerializeField] float gravityScale = 1.5f;
        [SerializeField] float fallGravityMultiplier = 2f;

        [Header("Data")]//***********
        bool buttonHeld;
        float buttonBufferTimer = -1f;
        bool CanJump => !isJumping && Controller.CollisionChecks.CoyoteTimeActive;
        bool isJumping;
        public bool IsJumping => isJumping;
        bool doubleJumpPossible;
        public bool CanDoubleJump
        {
            get => doubleJumpPossible;
            set => doubleJumpPossible = value;
        }
        float highJumpMaxTimer;
        int jumpSoundIndex;
        bool highJumpPossible;


        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;
        [SerializeField] List<AudioClip> jumpSounds;
        [SerializeField] AudioSource audioSource;


        #endregion

        #region Unity Methods

        void Update()
        {
            Attempt();
            Fall();
        }

        void FixedUpdate()
        {
            HighJump();
        }

        #endregion

        #region Unique Methods

        public void ButtonDown()
        {
            buttonHeld = true;
            buttonBufferTimer = Timer.Start(buttonBufferDuration);
        }

        public void ButtonUp()
        {
            buttonHeld = false;
        }


        void Attempt()
        {

            bool ButtonBufferActive = !Timer.IsFinished(buttonBufferTimer);
            if (ButtonBufferActive && Controller.StatusScript.CanCast)
                Execute();
        }

        public void Execute()
        {
            if (CanJump)
                LowJump();
            else if (CanDoubleJump)
                DoubleJump();
        }

        private void LowJump()
        {
            Jump(lowJumpForce);
            EnableAlternateJumps();
        }

        void DoubleJump()
        {
            float velocityX = Controller.Rigidbody.velocity.x;
            Controller.Rigidbody.velocity = new Vector2(velocityX, 0f);
            Jump(doubleJumpForce);
            doubleJumpPossible = false;
        }


        private void Jump(float force)
        {
            Controller.Animator.ChangeAnimationState(Controller.Animator.Jump, true);

            isJumping = true;
            buttonBufferTimer = 0;
            Controller.CollisionChecks.LastOnGroundTime = 0;

            float appliedForce = force;
            if (Controller.Rigidbody.velocity.y < 0)
                appliedForce -= Controller.Rigidbody.velocity.y;


            Controller.Rigidbody.AddForce(appliedForce * Vector2.up, ForceMode2D.Impulse);
            PlayRandomSoundFromList();
        }

        void PlayRandomSoundFromList()
        {
            jumpSoundIndex = (jumpSoundIndex + Random.Range(1, jumpSounds.Count - 1)) % jumpSounds.Count;
            audioSource.clip = jumpSounds[jumpSoundIndex];
            audioSource.Play();
        }

        private void EnableAlternateJumps()
        {
            highJumpMaxTimer = highJumpMaxDuration;
            highJumpPossible = true;
            doubleJumpPossible = true;
        }


        void Fall()
        {
            bool isFalling = Controller.Rigidbody.velocity.y < -0.4f;
            if (isFalling)
            {
                Controller.Animator.ChangeAnimationState(Controller.Animator.Fall, false);
                if (isJumping)
                    isJumping = false;
            }
            Controller.Rigidbody.gravityScale = isFalling ? gravityScale * fallGravityMultiplier : gravityScale;
        }

        void HighJump()
        {
            if (!highJumpPossible) return;

            bool reachedMaxDistance = highJumpMaxTimer <= 0;
            if (IsJumping && !reachedMaxDistance)
            {
                if (!buttonHeld)
                {
                    highJumpPossible = false;
                    return;
                }
                highJumpMaxTimer -= Time.deltaTime;
                Controller.Rigidbody.AddForce(highJumpForce * Vector2.up, ForceMode2D.Force);
            }
        }




        #endregion
    }
}
