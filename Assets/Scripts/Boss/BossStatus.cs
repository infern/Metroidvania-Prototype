using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Events;
using PlatformerPrototype.InfernKP.Statics;



namespace PlatformerPrototype.InfernKP.Boss
{
    public class BossStatus : MonoBehaviour, IDamageable
    {

        #region Variables

        //[Header("Settings")]//***********
        [SerializeField, Range(50, 200)] float maxHealth = 100;
        [SerializeField] Vector2 knockbackOnDeathMultiplier;
        [SerializeField, Range(0.1f, 1f)] float recoveryDuration;


        [Header("Data")]//***********
        float recoveryTimer;
        [SerializeField] float currentHealth = 100;
        bool alive = true;
        public bool IsAlive => alive;
        public float Health
        {
            get => currentHealth;
            set => currentHealth = Mathf.Clamp(value, 0f, maxHealth);
        }
        bool enraged;


        [Header("Components")]//***********
        [SerializeField] BossController Controller;

        #endregion

        #region Unity Methods

        void OnValidate()
        {
            //Play mode not active = Changing maxHealth in the inspector, will always adjust currentHealth to that value.
            //Play mode active = Changing maxHealth in the inspector, will adjust currentHealth only if the value is lower then its current value
            Health = Application.isPlaying ? currentHealth : maxHealth;
        }


        #endregion

        #region Unique Methods

        public void DamageCheck(Transform source, int value, bool triggerRecovery, float knockbackStrength)
        {
            if (!Timer.IsFinished(recoveryTimer)) return;

            if (alive)
            {
                TakeDamage(value, source);

                if (triggerRecovery)
                    recoveryTimer = Timer.Start(recoveryDuration);
            }
        }


        void TakeDamage(int value, Transform source)
        {
            Controller.ColorAnim.Play("hurt");
            currentHealth -= value;
            float healthPercentage = currentHealth / maxHealth;
            UI_events.BossLostHp(healthPercentage);

            bool belowEnrageTreshold = healthPercentage < Controller.Data.healthPercentageToEnterEnragedPhase * 0.01f;
            if (!enraged && belowEnrageTreshold)
            {
                enraged = true;
                Controller.Enrage();
            }
            else if (currentHealth <= 0) Death(source);

        }

        void ApplyKnockback(Transform source)
        {
            Vector2 direction = (this.transform.position - source.transform.position).normalized;
            direction.x *= knockbackOnDeathMultiplier.x;
            direction.y = knockbackOnDeathMultiplier.y;
            Controller.Rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }

        void Death(Transform source)
        {
            Controller.PlaySound(Controller.Data.bossDeathSound);

            Controller.Rigidbody.velocity = Vector2.zero;
            ApplyKnockback(source);
            alive = false;
            GameProgressEvents.BossDied();
            Controller.StateMachine.ChangeState(Controller.BossEndingState);
        }

        #endregion
    }
}
