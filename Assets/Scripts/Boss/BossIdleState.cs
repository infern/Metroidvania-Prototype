using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossIdleState : BossState
    {
        public BossIdleState(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (boss.ChaseTarget != null)
            {
                stateMachine.ChangeState(boss.ChaseState);
               
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    } 
}
