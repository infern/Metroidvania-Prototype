using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerPrototype.InfernKP.Statics;

public class Boulder : MonoBehaviour
{
    #region Variables

    [Header("Settings")]//***********
    [SerializeField] float disappearDuration;

    [Header("Data")]//***********
    float damage;
    public float Damage
    {
        set => damage = value;
    }
    bool shattered = false;
    Vector2 startingPosition;

    [Header("Components")]//***********
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    public AudioSource AudioSource
    {
        set => audioSource = value;
    }


    #endregion

    #region Unity Methods

    private void Awake()
    {
        startingPosition = this.transform.position;
    }

    private void OnEnable()
    {
        shattered = false;
        transform.position = startingPosition;
        gameObject.layer = LayerMasks.BoulderLayer;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (shattered) return;

        Crush(other);
    }

    private void Crush(Collision2D other)
    {
        audioSource.Play();
        CollisionDamage(other);
        shattered = true;
        gameObject.layer = LayerMasks.CollideWithGroundOnlyLayer;
        animator.Play("crush");
        StartCoroutine(DisappearCoroutine());
    }

    private void CollisionDamage(Collision2D other)
    {
        if (other.gameObject.layer != LayerMasks.PlayerLayer) return;

        bool isDamagable = other.transform.TryGetComponent(out IDamageable statusScript);
        if (isDamagable)
        {
            statusScript.DamageCheck(transform, 40, true, 12f);
        }
    }

    #endregion

    #region Unique Methods

    IEnumerator DisappearCoroutine()
    {
        yield return new WaitForSeconds(disappearDuration);
        this.transform.parent.gameObject.SetActive(false);
    }

    #endregion
}
