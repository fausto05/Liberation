using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class RangedAttack : EnemyAttack
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Projectile projectilePrefab;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void TryAttack()
    {
        if (player == null) return;
        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
            
        }
    }

    public void OnFireProjectile()
    {
        PerformAttack();
    }

    protected override void PerformAttack()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        Projectile projectile = ProjectilePool.Instance.Get(projectilePrefab, firePoint.position, direction);
        projectile.SetDamage(stats.damage);
    }
}
