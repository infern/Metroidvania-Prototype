using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;

namespace PlatformerPrototype.InfernKP.Boss
{
    public class Projectile : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField] int damage;
        public int Damage
        {
            get => damage;
            set => damage = value;
        }

        [SerializeField] int knockbackStrength;
        public int KnockbackStrength
        {
            get => knockbackStrength;
            set => knockbackStrength = value;
        }

        [Header("Data")]//***********
        float disappearDuration = 0.6f;
        float returnToPoolDuration = 8f;
        bool broken = false;


        [Header("Components")]//***********
        [SerializeField] Animator animator;
        [SerializeField] Rigidbody2D rigidBody;
        public Rigidbody2D RigidBody => rigidBody;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            broken = false;
            StartCoroutine(ReturnToPoolIfUseless());
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            TryToDealDamage(other);
        }



        #endregion

        #region Unique Methods

        IEnumerator ReturnToPoolIfUseless()
        {
            yield return new WaitForSeconds(returnToPoolDuration);
            Disappear();
        }

        private void TryToDealDamage(Collider2D other)
        {
            if (broken) return;
            if (other.gameObject.layer != LayerMasks.PlayerLayer) return;

            bool isDamagable = other.transform.TryGetComponent(out IDamageable statusScript);
            if (isDamagable)
            {
                statusScript.DamageCheck(transform, damage, true, knockbackStrength);
                StartCoroutine(DisappearCoroutine());
                rigidBody.velocity = Vector2.zero;
                broken = true;
            }
        }

        IEnumerator DisappearCoroutine()
        {
            animator.Play("disappear");
            yield return new WaitForSeconds(disappearDuration);
            Disappear();
        }

        private void Disappear()
        {
            this.gameObject.SetActive(false);
        }




        #endregion
    }
}
