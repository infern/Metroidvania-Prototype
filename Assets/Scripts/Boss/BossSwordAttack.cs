using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossSwordAttack : BossState
    {

        public BossSwordAttack(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();

        }

        public override void Enter()
        {
            base.Enter();
            boss.Rigidbody.velocity = Vector2.zero;
            boss.Anim.SetFloat("swordAttackChargeSpeed", boss.Data.swordAttackChargeSpeed);
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
            DamageFrames();
            Gizmo.DrawBoxCast2D((Vector2)boss.transform.position + (boss.Data.swordAttackPoint * boss.transform.right.x), boss.Data.swordAttackSize, 0, boss.transform.right, 0, Color.blue);

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        void WaitForTrigger()
        {
            if (boss.AnimationTrigger)
            {
                SwingSword();
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



        private void SwingSword()
        {
            boss.AnimationTrigger = false;
            boss.Rigidbody.AddForce(boss.Data.afterSwordSwingSlideSpeed * boss.transform.right, ForceMode2D.Impulse);
            boss.Anim.Play("swordAttackCast");
            boss.PlaySound(boss.Data.swordCastSound);
        }



        private void DamageFrames()
        {
            if (boss.AnimationDamageFrames)
            {
                Vector3 attackPosition = (Vector2)boss.transform.position + (boss.Data.swordAttackPoint * boss.transform.right.x);
                RaycastHit2D[] results = Physics2D.BoxCastAll(attackPosition, boss.Data.swordAttackSize, 0, boss.transform.right, 0f, LayerMasks.PlayerLayerMask);
                foreach (var x in results)
                {
                    bool isDamagable = x.transform.TryGetComponent(out IDamageable statusScript);
                    if (isDamagable)
                    {
                        statusScript.DamageCheck(boss.transform, boss.Data.swordAttackDamage, true, boss.Data.swordAttackKnockback);
                    }
                }
            }
          
        }

    }


}