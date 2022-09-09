using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP
{
    public class Spikes : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField, Range(10, 40)] int damage;

        //[Header("Data")]//***********

        //[Header("Components")]//***********


        #endregion

        #region Unity Methods

        void OnCollisionEnter2D(Collision2D other)
        {
            bool isDamagable = other.gameObject.TryGetComponent(out IDamageable statusScript);
            if (isDamagable)
            {
                statusScript.DamageCheck(this.transform, damage, true, 10f);
            }
        }

        #endregion

        #region Unique Methods

        #endregion
    }
}
