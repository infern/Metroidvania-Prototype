using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;
namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossSpearAttack : BossState
    {
        bool isDashing;
        bool exiting;
        public BossSpearAttack(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            isDashing = false;
            exiting = false;
            boss.Rigidbody.velocity = Vector2.zero;
            boss.Anim.SetFloat("spearAttackChargeSpeed", boss.Data.spearAttackChargeSpeed);


        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            WaitForTrigger();
            WaitForExit();

            if (isDashing)
                WhileDashingMethods();
            else
                WhileNotDashingMethods();


        }

        void WaitForTrigger()
        {
            if (boss.AnimationTrigger)
            {
                Dash();
            }

        }

        void WaitForExit()
        {
            if (boss.AnimationExit)
            {
                End();
            }
        }

        private void End()
        {
            stateMachine.ChangeState(boss.IdleState);
        }


        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        private void WhileDashingMethods()
        {
            if (boss.FlipScript.PlayerIsBehind())
                StopDashing();
            DamageFrames();
        }

        private void WhileNotDashingMethods()
        {
            if (!exiting)
            {
                if (boss.FlipScript.PlayerIsBehind())
                    boss.FlipScript.TurnToFacePlayerDirection();
            }
        }

        void Dash()
        {
            boss.PlaySound(boss.Data.spearCastSound);
            boss.AnimationTrigger = false;
            boss.Rigidbody.velocity = new Vector2((boss.Data.spearAttackDashSpeed) * boss.transform.right.x, 0f);
            boss.Anim.Play("spearAttackCast");
            isDashing = true;
        }

        private void DamageFrames()
        {
            Vector3 attackPosition = (Vector2)boss.transform.position + (boss.Data.spearAttackPoint * boss.transform.right.x);
            RaycastHit2D[] results = Physics2D.BoxCastAll(attackPosition, boss.Data.spearAttackSize, 0, boss.transform.right, 0f, LayerMasks.PlayerLayerMask);
            foreach (var x in results)
            {
                bool isDamagable = x.transform.TryGetComponent(out IDamageable statusScript);
                if (isDamagable)
                {
                    statusScript.DamageCheck(boss.transform, boss.Data.spearAttackDamage, true, boss.Data.spearAttackKnockback);
                    StopDashing();
                    break;
                }
            }
        }
        private void StopDashing()
        {
            exiting = true;
            isDashing = false;
            boss.Anim.Play("spearAttackStopDashing");
            boss.Rigidbody.velocity = Vector2.zero;
            boss.Rigidbody.AddForce(boss.Data.afterSpearDashSlideSpeed * boss.transform.right, ForceMode2D.Impulse);

        }



    }

}