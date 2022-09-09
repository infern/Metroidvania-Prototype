using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossAnimationEvents : MonoBehaviour
    {
        [SerializeField] BossController boss;

        public void AnimationTrigger()
        {

            boss.AnimationTrigger = true;
        }

        public void AnimationDamageFramesOn()
        {

            boss.AnimationDamageFrames = true;
        }

        public void AnimationDamageFramesOff()
        {

            boss.AnimationDamageFrames = false;
        }


        public void AnimationExit()
        {
            boss.AnimationExit = true;
        }

    }
}
