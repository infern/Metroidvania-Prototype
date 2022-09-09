using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossBowAttack : BossState
    {
        int shootAmountOfRepeats;
        bool waitingToRepeat;
        bool RepeatsRemaining => shootAmountOfRepeats >= 0;

        public BossBowAttack(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
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
            RandomizeForBonusRepeat();
            boss.Anim.SetFloat("bowAttackChargeSpeed", boss.Data.bowAttackChargeSpeed);
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
            if (waitingToRepeat && boss.FlipScript.PlayerIsBehind())
            {
                boss.FlipScript.TurnToFacePlayerDirection();
            } 
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        void WaitForTrigger()
        {
            if (boss.AnimationTrigger)
            {
                Shoot();
            }

        }

        void WaitForExit()
        {
            if (!RepeatsRemaining && boss.AnimationExit)
            {
                End();
            }
        }

        private void End()
        {
            stateMachine.ChangeState(boss.IdleState);
        }

        void Shoot()
        {
            boss.AnimationTrigger = false;
            boss.Anim.Play("bowAttackCast");
            boss.PlaySound(boss.Data.bowCastSound);
            GameObject projectile = boss.Arrows.GetObjectFromPool();
            projectile.transform.Rotate(boss.transform.rotation.eulerAngles);

            Vector3 spawnOffset = boss.Data.arrowSpawnOffset;
            spawnOffset.x *= boss.transform.right.x;
            projectile.transform.position = boss.transform.position + spawnOffset;

            bool isProjectile = projectile.TryGetComponent(out Projectile projectileScript);
            if (isProjectile)
            {
                projectileScript.Damage = boss.Data.arrowDamage;
                projectileScript.KnockbackStrength = boss.Data.arrowKnockback;
                projectileScript.RigidBody.velocity = boss.Data.arrowSpeed * boss.transform.right;
            }

            TryToRepeat();
        }

        private void TryToRepeat()
        {
            if (RepeatsRemaining)
            {
                shootAmountOfRepeats--;
                boss.AnimationExit = false;
                waitingToRepeat = true;
            }
        }


        private void RandomizeForBonusRepeat()
        {
            shootAmountOfRepeats = boss.Data.amountOfShootRepeats;
            float randomNumber = Random.Range(0f, 100f);
            if (boss.Data.percentegeChanceForAdditionalRepeat >= randomNumber)
                shootAmountOfRepeats++;
 
        }



    }
}