using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Events;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossController : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField] BossData bossData;
        [SerializeField] BossData enragedBossData;

        public BossData Data => bossData;


        [Header("Data")]//***********
        Transform chaseTarget;
        public Transform ChaseTarget
        {
            get => chaseTarget;
            set => chaseTarget = value;
        }

        bool animationTrigger;
        public bool AnimationTrigger
        {
            get => animationTrigger;
            set => animationTrigger = value;
        }

        bool animationDamageFrames;
        public bool AnimationDamageFrames
        {
            get => animationDamageFrames;
            set => animationDamageFrames = value;
        }

        bool animationExit;
        public bool AnimationExit
        {
            get => animationExit;
            set => animationExit = value;
        }



        //  [Header("States")]//***********
        public BossStateMachine StateMachine { get; private set; }

        public BossIdleState IdleState { get; private set; }
        public BossChaseState ChaseState { get; private set; }
        public BossSwordAttack SwordAttackState { get; private set; }
        public BossBowAttack BowAttackState { get; private set; }
        public BossSpearAttack SpearAttackState { get; private set; }
        public BossOrderCatapultShotState OrderCatapultShotState { get; private set; }
        public BossEndingState BossEndingState { get; private set; }







        [Header("Components")]//***********
        [SerializeField] BossFlip flipScript;
        public BossFlip FlipScript => flipScript;
        [SerializeField] BossStatus statusScript;
        public BossStatus StatusScript => statusScript;
        [SerializeField] ObjectPool arrows;
        public ObjectPool Arrows => arrows;
        [SerializeField] ObjectPool dangerZones;
        public ObjectPool DangerZones => dangerZones;

        [SerializeField] Animator anim;
        [SerializeField] Animator colorAnim;
        [SerializeField] Rigidbody2D rigidBody;
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource audioSource2;
        public AudioSource AudioSource2 => audioSource2;


        public Animator Anim { get => anim; }
        public Animator ColorAnim { get => colorAnim; }

        public Rigidbody2D Rigidbody { get => rigidBody; }

        #endregion


        #region Unity Methods

        private void OnEnable()
        {
            GameProgressEvents.PlayerDiedTrigger += VictoryPose;
        }

        private void OnDisable()
        {
            GameProgressEvents.PlayerDiedTrigger -= VictoryPose;

        }

        private void Awake()
        {
            StateMachine = new BossStateMachine();

            IdleState = new BossIdleState(this, StateMachine, "idle");
            ChaseState = new BossChaseState(this, StateMachine, "move");
            SwordAttackState = new BossSwordAttack(this, StateMachine, "swordAttackBegin");
            BowAttackState = new BossBowAttack(this, StateMachine, "bowAttackBegin");
            SpearAttackState = new BossSpearAttack(this, StateMachine, "spearAttackBegin");
            BossEndingState = new BossEndingState(this, StateMachine, "death");
            OrderCatapultShotState = new BossOrderCatapultShotState(this, StateMachine, "orderCatapultShotBegin");
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionDamage(other);
        }




        #endregion

        #region Unique Methods
        private void CollisionDamage(Collision2D other)
        {
            if (bossData.collisionDamage <= 0) return;

            bool isDamagable = other.transform.TryGetComponent(out IDamageable statusScript);
            if (isDamagable)
            {
                statusScript.DamageCheck(transform, bossData.collisionDamage, false, bossData.collisionKnockback);
            }
        }

        void VictoryPose()
        {
            StartCoroutine(DelayBeforePose());
        }

        IEnumerator DelayBeforePose()
        {
            yield return new WaitForSeconds(1f);
            StateMachine.ChangeState(BossEndingState);

        }

        public void Enrage()
        {
            bossData = enragedBossData;
            UI_events.EnragedTextToggle();
            ChaseState.ForcerOrderCatapultShotCooldown();
            StateMachine.ChangeState(OrderCatapultShotState);

        }

        public void PlaySound(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        #endregion
    }
}