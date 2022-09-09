using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossEndingState : BossState
    {

        public BossEndingState(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();

        }

        public override void Enter()
        {
            base.Enter();
            if (boss.StatusScript.IsAlive)
            {
                boss.Anim.Play("VictoryPoseBegin");
                boss.PlaySound(boss.Data.bossVictorySound);

            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }



    }

}