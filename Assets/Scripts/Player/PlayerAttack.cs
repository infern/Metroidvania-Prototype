using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Interfaces;
using PlatformerPrototype.InfernKP.Statics;

namespace PlatformerPrototype.InfernKP.Player
{
    public class PlayerAttack : MonoBehaviour, IButtonPress
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField] int swordAttackDamage;
        [SerializeField] Vector2 swordAttackPoint = new(.49f, .03f);
        [SerializeField] Vector2 swordAttackSize = new(.49f, .03f);
        [SerializeField] int swordAttackKnockback;
        [SerializeField] float afterSwingSlideSpeed;
        [SerializeField] float swingVelocityMultiplier;




        [Header("Data")]//***********
        Coroutine Co_Attack;
        int attackSoundIndex;

        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;
        [SerializeField] List<AudioClip> attackSounds;
        [SerializeField] AudioSource audioSource;


        #endregion

        #region Unity Methods

        void Update()
        {
            Vector3 attackPosition = (Vector2)transform.position + (swordAttackPoint * transform.right.x);
            Gizmo.DrawBoxCast2D(attackPosition, swordAttackSize, 0, transform.right, 0, Color.blue);

        }

        #endregion

        #region Unique Methods

        public void ButtonDown()
        {
            Attempt();
        }

        public void ButtonUp()
        {

        }

        void Attempt()
        {
            if (Controller.StatusScript.CanCast)
            {
                Execute();
            }

        }

        public void Execute()
        {
            string animation = Controller.CollisionChecks.Grounded ? Controller.Animator.AttackGround : Controller.Animator.AttackAir;
            Controller.Animator.ChangeAnimationState(animation, true);
            Co_Attack = StartCoroutine(Swing());
            Controller.StatusScript.IsCasting = true;
        }


        IEnumerator Swing()
        {
            if (Controller.CollisionChecks.Grounded)
                Controller.Rigidbody.velocity = new Vector2(Controller.Rigidbody.velocity.x * swingVelocityMultiplier, Controller.Rigidbody.velocity.y);
            PlayRandomSoundFromList();
            Controller.Animator.AnimationTrigger = false;
            Controller.Animator.AnimationExit = false;
            yield return new WaitUntil(() => Controller.Animator.AnimationTrigger == true);
            if (Controller.CollisionChecks.Grounded)
                Controller.Rigidbody.AddForce(afterSwingSlideSpeed * transform.right, ForceMode2D.Impulse);
            do
            {
                DamageFrames();
                yield return null;
            }
            while (Controller.Animator.AnimationDamageFrames);
            yield return new WaitUntil(() => Controller.Animator.AnimationExit == true);
            StopSwinging();
        }

        public void StopSwinging()
        {
            if (Co_Attack != null) StopCoroutine(Co_Attack);
            Controller.StatusScript.IsCasting = false;
        }

        private void DamageFrames()
        {

            Vector3 attackPosition = (Vector2)transform.position + (swordAttackPoint * transform.right.x);
            RaycastHit2D[] results = Physics2D.BoxCastAll(attackPosition, swordAttackSize, 0, transform.right, 0f, LayerMasks.EnemyLayerMask);
            foreach (var x in results)
            {
                bool isDamagable = x.transform.TryGetComponent(out IDamageable statusScript);
                if (isDamagable)
                {
                    statusScript.DamageCheck(transform, swordAttackDamage, true, swordAttackKnockback);
                }
            }

        }

        void PlayRandomSoundFromList()
        {
            attackSoundIndex = (attackSoundIndex + Random.Range(1, attackSounds.Count - 1)) % attackSounds.Count;
            audioSource.clip = attackSounds[attackSoundIndex];
            audioSource.Play();

        }

        #endregion
    }
}
