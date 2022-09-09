using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;


namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossOrderCatapultShotState : BossState
    {
        public BossOrderCatapultShotState(BossController boss, BossStateMachine stateMachine, string animName) : base(boss, stateMachine, animName)
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
            boss.Anim.SetFloat("orderCatapultShotChargeSpeed", boss.Data.orderCatapultShotChargeSpeed);
            boss.PlaySound(boss.Data.orderCatapultSound);


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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


        void WaitForTrigger()
        {
            if (boss.AnimationTrigger)
            {
                CreateDangerZones();
            }

        }

        void WaitForExit()
        {
            if (boss.AnimationExit)
            {
                stateMachine.ChangeState(boss.IdleState);
            }
        }


        void CreateDangerZones()
        {
            boss.AnimationTrigger = false;
            float startingPosition = boss.ChaseTarget.transform.position.x;
            startingPosition -= Random.Range(-5f, 5f);
            CreateDangerZone(startingPosition);
            float leftOffset= startingPosition;
            float rightOffset= startingPosition;
            for (int i = 0; i < boss.Data.extraDangerZoneForEachSide; i++)
            {
                leftOffset -=  boss.Data.offsetBetweenDangerZones;
                rightOffset +=  boss.Data.offsetBetweenDangerZones;
                CreateDangerZone(leftOffset);
                CreateDangerZone(rightOffset);

            }
        }

        private void CreateDangerZone(float position)
        {
            GameObject dangerZone = boss.DangerZones.GetObjectFromPool();


            dangerZone.transform.position = new Vector2(position, boss.transform.position.y);


            bool isDangerZone = dangerZone.TryGetComponent(out DangerZone dangerZoneScript);
            if (isDangerZone)
            {
                boss.AudioSource2.clip = boss.Data.boulderCrushedSound;
                dangerZoneScript.Boulder.AudioSource = boss.AudioSource2;
                dangerZoneScript.Boulder.Damage = boss.Data.boulderDamage;
                dangerZoneScript.DelayBeforeRockFalls = boss.Data.delayBeforeRockFalls;
                dangerZoneScript.Begin();
            }
        }
    }
}