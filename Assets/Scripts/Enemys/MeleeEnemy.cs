using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
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

        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            Debug.Log("Melee Attack");

            

            lastAttackTime = Time.time;
        }
    }
}
