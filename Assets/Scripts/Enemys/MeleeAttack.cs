using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    protected override void PerformAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(stats.damage);
        }

       
    }
}
