using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;


namespace PlatformerPrototype.InfernKP.Player
{
    public class CollisionChecks : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField] [Range(.09f, .5f)] float coyoteBufferDuration = .2f;
        [SerializeField] Vector2 groundCheckSize = new(.49f, .03f);


        [Header("Data")]//***********
        float coyoteBufferTimer = -1f;
        bool grounded;
        bool landSoundReady = false;

        public bool Grounded
        {
            get => grounded;
            set => grounded = value;

        }
        public bool CoyoteTimeActive
        {
            get => !Timer.IsFinished(coyoteBufferTimer);
        }
        public float LastOnGroundTime
        {
            get => coyoteBufferTimer;
            set => coyoteBufferTimer = value;
        }
        bool landed;

        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;
        [SerializeField] Transform groundCheckPoint;
        [SerializeField] AudioClip landSound;
        [SerializeField] AudioSource audioSource;


        #endregion

        #region Unity Methods

        private void Update()
        {
            CheckGround();
        }

        #endregion

        #region Unique Methods

        void CheckGround()
        {
                grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, LayerMasks.GroundLayerMask) && !Controller.JumpScript.IsJumping;
                if (grounded)
                {
                    coyoteBufferTimer = Timer.Start(coyoteBufferDuration);
                    if (!landed)
                    {
                        Land();
                    }

                }
                else
                    landed = false;
     
        }

        void Land()
        {
            Controller.Animator.ChangeAnimationState(Controller.Animator.Land, false);
            landed = true;
            Controller.JumpScript.CanDoubleJump = false;
            if (landSoundReady)
            {
                audioSource.clip = landSound;
                audioSource.Play();
              
            }
            landSoundReady = true;

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        }

        #endregion

    }
}
