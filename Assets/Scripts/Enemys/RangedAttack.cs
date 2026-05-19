using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class RangedAttack : EnemyAttack
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;

    protected override void PerformAttack()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        Projectile projectile = ProjectilePool.Instance.Get(projectilePrefab, firePoint.position, direction);
        projectile.SetDamage(stats.damage);
    }
}
