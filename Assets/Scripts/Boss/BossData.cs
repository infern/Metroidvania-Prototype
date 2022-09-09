using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlatformerPrototype.InfernKP.Boss
{
    [CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/BossData", order = 1)]
    public class BossData : ScriptableObject
    {
        [Header("General")]
        public int collisionDamage = 5;
        public float collisionKnockback;
        [Range(15f, 70f)] public float healthPercentageToEnterEnragedPhase;
        public AudioClip bossDeathSound;
        public AudioClip bossVictorySound;




        [Header("Chase State")]
        public float chaseAccelerationRate;
        public float maxChaseSpeed;


        [Header("Attack State")]
        public float distanceToTriggerAttack = 5f;
        public float swordAttackChargeSpeed = 1f;
        public int swordAttackDamage;
        public Vector2 swordAttackPoint = new(.49f, .03f);
        public Vector2 swordAttackSize = new(.49f, .03f);
        public int swordAttackKnockback;
        public float afterSwordSwingSlideSpeed;
        public AudioClip swordCastSound;


        [Header("Special Moves")]
        public float specialGeneralCooldownDuration;

        [Header("Bow State")]
        public float distanceToTriggerBow = 5f;
        public float bowAttackChargeSpeed = 1f;
        public int arrowDamage;
        public int arrowKnockback;
        public int arrowSpeed;
        public Vector2 arrowSpawnOffset;
        public float bowCooldownDuration;
        public float bowStartingDisableDuration;
        public int amountOfShootRepeats;
        [Range(0f, 100f)] public float percentegeChanceForAdditionalRepeat;
        public AudioClip bowChargeSound;
        public AudioClip bowCastSound;



        [Header("Spear State")]
        public float spearAttackChargeSpeed = 1f;
        public int spearAttackDamage;
        public float spearAttackDashSpeed = 5f;
        public int spearAttackKnockback;
        public float spearCooldownDuration;
        public float spearStartingDisableDuration;
        public float afterSpearDashSlideSpeed;
        public int numberOfSwordStatesToForceSpearState;
        public Vector2 spearAttackPoint = new(.49f, .03f);
        public Vector2 spearAttackSize = new(.49f, .03f);
        public AudioClip spearCastSound;


        [Header("Order Catapult Shot State")]
        public float orderCatapultShotChargeSpeed = 1f;
        public float delayBeforeRockFalls = 4f;
        public int extraDangerZoneForEachSide;
        public float offsetBetweenDangerZones;
        public float boulderDamage;
        public float orderCatapultShotCooldownDuration;
        public float orderCatapultShotStartingDisableDuration;
        public AudioClip orderCatapultSound;
        public AudioClip boulderCrushedSound;





    }
}