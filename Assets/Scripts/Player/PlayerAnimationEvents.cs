using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] PlayerController Controller;

        public void AnimationTrigger()
        {

            Controller.Animator.AnimationTrigger = true;
        }

        public void AnimationDamageFramesOn()
        {

            Controller.Animator.AnimationDamageFrames = true;
        }

        public void AnimationDamageFramesOff()
        {

            Controller.Animator.AnimationDamageFrames = false;
        }


        public void AnimationExit()
        {
            Controller.Animator.AnimationExit = true;
        }

    }
}

