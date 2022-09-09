using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;
namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossChaseState : BossState
    {
        float specialMoveCooldownTimer = -1f;
        float distanceFromTarget;
        float bowAttackCooldownTimer = -1f;
        float spearAttackCooldownTimer = -1f;
        float orderCatapultShotCooldownTimer = -1f;
        public void ForcerOrderCatapultShotCooldown() => orderCatapultShotCooldownTimer = boss.Data.orderCatapultShotCooldownDuration;


        bool startingDisableTimersSet = false;
        int swordAttacksCasted = 0;

        bool targetIsClose;
        bool targetIsFar;

        public BossChaseState(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            StartingCooldowns();
            specialMoveCooldownTimer = Timer.Start(boss.Data.specialGeneralCooldownDuration);

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            DistanceFromTarget();
            if (boss.FlipScript.PlayerIsBehind())
                boss.FlipScript.TurnToFacePlayerDirection();

            SpecialMoves();
            CheckIfTargetIsInRangeForSwordAttack();

        }

        private void SpecialMoves()
        {
            if (Timer.IsFinished(specialMoveCooldownTimer))
            {
                CheckIfOrderCatapultShotIsReady();
                CheckIfSpearAttackIsReady();
                CheckIfTargetIsInRangeForBowAttack();
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            ApplySpeed();
        }

        private void ApplySpeed()
        {
            if (boss.Rigidbody.velocity.magnitude < boss.Data.maxChaseSpeed)
                boss.Rigidbody.AddForce(boss.Data.chaseAccelerationRate * boss.transform.right, ForceMode2D.Force);
        }

        private void StartingCooldowns()
        {
            if (!startingDisableTimersSet)
            {
                startingDisableTimersSet = true;
                spearAttackCooldownTimer = Timer.Start(boss.Data.spearStartingDisableDuration);
                bowAttackCooldownTimer = Timer.Start(boss.Data.bowStartingDisableDuration);
                orderCatapultShotCooldownTimer = Timer.Start(boss.Data.orderCatapultShotStartingDisableDuration);

            }
        }
        private void DistanceFromTarget()
        {
            distanceFromTarget = Vector2.Distance(boss.transform.position, boss.ChaseTarget.position);
            targetIsClose = distanceFromTarget < boss.Data.distanceToTriggerAttack;
            targetIsFar = distanceFromTarget > boss.Data.distanceToTriggerBow;

        }

        private void CheckIfTargetIsInRangeForSwordAttack()
        {
            if (targetIsClose)
            {
                if (swordAttacksCasted < boss.Data.numberOfSwordStatesToForceSpearState)
                {
                    ChangeToSwordState();
                }
                else ChangeToSpearState();

            }

        }
        private void ChangeToSwordState()
        {
            stateMachine.ChangeState(boss.SwordAttackState);
            swordAttacksCasted++;
        }

        private void CheckIfTargetIsInRangeForBowAttack()
        {
            if (targetIsFar && Timer.IsFinished(bowAttackCooldownTimer))
            {
                ChangeToBowState();

            }
        }

        private void ChangeToBowState()
        {
            bowAttackCooldownTimer = Timer.Start(boss.Data.bowCooldownDuration);
            stateMachine.ChangeState(boss.BowAttackState);
        }

        private void CheckIfSpearAttackIsReady()
        {
            if (Timer.IsFinished(spearAttackCooldownTimer))
            {
                ChangeToSpearState();

            }
        }

        private void ChangeToSpearState()
        {
            spearAttackCooldownTimer = Timer.Start(boss.Data.spearCooldownDuration);
            stateMachine.ChangeState(boss.SpearAttackState);
            swordAttacksCasted = 0;
        }


        private void CheckIfOrderCatapultShotIsReady()
        {
            if (Timer.IsFinished(orderCatapultShotCooldownTimer))
            {
                ChangeToOrderCatapultShot();

            }
        }

        private void ChangeToOrderCatapultShot()
        {
            orderCatapultShotCooldownTimer = Timer.Start(boss.Data.orderCatapultShotCooldownDuration);
            stateMachine.ChangeState(boss.OrderCatapultShotState);
        }
    }

}