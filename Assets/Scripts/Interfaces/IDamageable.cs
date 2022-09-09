using UnityEngine;
public interface IDamageable
{
    public void DamageCheck(Transform source, int damage, bool triggerRecovery, float knockbackStrength);
}
