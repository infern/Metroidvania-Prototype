using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        #region Variables

        //[Header("Settings")]//***********

        [Header("Data")]//***********
        private string currentState;
        const string idleAnimation = "idle";
        const string runAnimation = "run";
        const string jumpAnimation = "jump";
        const string fallAnimation = "fall";
        const string landAnimation = "land";
        const string takeDamageAnimation = "hurt";
        const string deathAnimation = "death";
        const string attackOnGroundAnimation = "attackGrounded";
        const string attackInAirAnimation = "attackInAir";
        const string recoveryAnimation = "recovery";
        const string victoryPoseAnimation = "victoryPoseBegin";


        bool animationTrigger;
        public bool AnimationTrigger
        {
            get => animationTrigger;
            set => animationTrigger = value;
        }

        bool animationDamageFrames;
        public bool AnimationDamageFrames
        {
            get => animationDamageFrames;
            set => animationDamageFrames = value;
        }

        bool animationExit;
        public bool AnimationExit
        {
            get => animationExit;
            set => animationExit = value;
        }



        public string Idle => idleAnimation;
        public string Run => runAnimation;
        public string Jump => jumpAnimation;
        public string Fall => fallAnimation;

        public string Land => landAnimation;
        public string Hurt => takeDamageAnimation;
        public string Death => deathAnimation;
        public string AttackGround => attackOnGroundAnimation;
        public string AttackAir => attackInAirAnimation;
        public string Recovery => recoveryAnimation;
        public string VictoryPose => victoryPoseAnimation;








        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;
        [SerializeField] Animator animator;
        [SerializeField] Animator colorAnimator;


        #endregion

        #region Unity Methods

        #endregion

        #region Unique Methods

        public void ChangeAnimationState(string newState, bool restartIfSame)
        {
            if ((!restartIfSame && currentState == newState) || !Controller.StatusScript.CanCast) return;

            animator.Play(newState);
            currentState = newState;

        }

        public void ForceAnimationState(string newState)
        {
            animator.Play(newState);
            currentState = newState;
        }

        public void PlayColorAnimation(string name)
        {
            colorAnimator.Play(name);
        }

        public void SetBool(string name, bool value)
        {
            colorAnimator.SetBool(name, value);
        }

        #endregion
    }
}