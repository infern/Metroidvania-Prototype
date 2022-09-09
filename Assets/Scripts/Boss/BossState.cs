using System.Collections;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossState
    {
        #region Variables
        [Header("Data")]
        protected bool isExitingState;
        protected float startTime;
        private string animationName;



        [Header("Components")]
        protected BossController boss;
        protected BossStateMachine stateMachine;

        public BossState(BossController boss, BossStateMachine stateMachine, string animationName)
        {
            this.boss = boss;
            this.stateMachine = stateMachine;
            this.animationName = animationName;
        }

        #endregion

        #region Virtual methods
        public virtual void Enter()
        {
            DoChecks();
            startTime = Time.time;
            isExitingState = false;
            boss.AnimationTrigger = false;
            boss.AnimationDamageFrames = false;
            boss.AnimationExit = false;
            boss.Anim.Play(animationName);
        }

        public virtual void Exit()
        {
            isExitingState = true;
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        public virtual void DoChecks() { }

        #endregion


    }
}
