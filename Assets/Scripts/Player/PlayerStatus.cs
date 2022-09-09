using UnityEngine;
using PlatformerPrototype.InfernKP.Events;
using PlatformerPrototype.InfernKP.Statics;
using System.Collections;
using System.Collections.Generic;

namespace PlatformerPrototype.InfernKP.Player
{
    public class PlayerStatus : MonoBehaviour, IDamageable
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField, Range(50, 200)] float maxHealth = 100;
        [SerializeField, Range(0.1f, 2f)] float recoveryDuration = 0.3f;
        [SerializeField] float onDeathKnockbackForce;



        public float Health
        {
            get => currentHealth;
            set => currentHealth = Mathf.Clamp(value, 0f, maxHealth);
        }


        [Header("Data")]//***********
        [SerializeField] float currentHealth = 100;
        bool alive = true;
        float knockbackOnYMultiplier = 0.3f;
        bool isRecovering;
        int hurtSoundIndex;
        bool isCasting;
        bool isActive=true;
        public bool IsCasting
        {
            get => isCasting;
            set => isCasting = value;
        }
        public bool IsActive
        {
            set => isActive = value;
        }
        public bool CanCast => alive && !isCasting && isActive;

        Coroutine Co_Recovery;


        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;
        [SerializeField] List<AudioClip> hurtSounds;
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioSource audioSource;


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
            if (alive && !isRecovering)
            {
                TakeDamage(value, source);

                if (triggerRecovery)
                    Co_Recovery = StartCoroutine(RecoveryCoroutine());

                if (knockbackStrength > 0)
                    ApplyKnockback(source, knockbackStrength);
            }
        }

        void TakeDamage(int value, Transform source)
        {
            //Refactor this later, create CurrentAction.Stop() script instead of checking AttackScript.
            Controller.AttackScript.StopSwinging();
            //
            currentHealth -= value;
            float healthPercentage = currentHealth / maxHealth;
            UI_events.PlayerLostHp(healthPercentage);
            if (currentHealth <= 0) Death(source);
            else PlayRandomHurtSound();
        }

        void ApplyKnockback(Transform source, float strength)
        {
            Vector2 direction = (this.transform.position - source.transform.position).normalized;
            direction.x *= strength;
            direction.y =  strength * knockbackOnYMultiplier;
            Controller.Rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }

        IEnumerator RecoveryCoroutine()
        {
            StartRecovery();
            yield return new WaitForSeconds(recoveryDuration);
            EndRecovery();
        }

        private void StartRecovery()
        {
            isRecovering = true;
            Controller.Animator.SetBool(Controller.Animator.Recovery, isRecovering);
            Controller.Animator.PlayColorAnimation(Controller.Animator.Recovery);
            gameObject.layer = LayerMasks.RecoveringPlayerLayer;
        }

        private void EndRecovery()
        {
            isRecovering = false;
            Controller.Animator.SetBool(Controller.Animator.Recovery, isRecovering);
            gameObject.layer = LayerMasks.PlayerLayer;
        }

        public void CancelRecoveryEarly()
        {
            if (Co_Recovery != null)
            {
                StopCoroutine(Co_Recovery);
                EndRecovery();

            }
        }

        void Death(Transform source)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
            Controller.Animator.PlayColorAnimation(Controller.Animator.Hurt);
            Controller.Rigidbody.velocity = Vector2.zero;
            ApplyKnockback(source, onDeathKnockbackForce);
            Controller.Animator.ChangeAnimationState(Controller.Animator.Death, false);
            alive = false;
            isActive = false;
            Controller.PlayerInput.enabled = false;

            GameProgressEvents.PlayerDied();
        }


        void PlayRandomHurtSound()
        {
            hurtSoundIndex = (hurtSoundIndex + Random.Range(1, hurtSounds.Count - 1)) % hurtSounds.Count;
            audioSource.clip = hurtSounds[hurtSoundIndex];
            audioSource.Play();
        }

        #endregion
    }
}
