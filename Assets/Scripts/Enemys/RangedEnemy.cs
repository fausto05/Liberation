using UnityEngine;

public class RangedEnemy : EnemyBase
{
    
    
    [SerializeField] private Transform firePoint;

    protected override void HandleBehavior()
    {
        float distance = DistanceToPlayer();

        if (distance > stats.attackRange)
        {
            MoveToPlayer();
        }
        else
        {
            Attack();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * stats.moveSpeed;
    }

    private void Attack()
    {
        rb.linearVelocity = Vector2.zero;
        Debug.Log("ATTACKING");

        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            Shoot();

            lastAttackTime = Time.time;
        }
    }

    private void Shoot()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        ProjectilePool.Instance.Get(firePoint.position, direction);
    }
}
