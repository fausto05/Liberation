using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
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
        GameObject projectile =
        Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Vector2 direction =
            (player.position - firePoint.position).normalized;

        projectile.GetComponent<Projectile>().Initialize(direction);
    }
}
