using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    private MeleeAttack attack;

    protected override void Awake()
    {
        base.Awake();

        attack = GetComponent<MeleeAttack>();
    }

    protected override void HandleBehavior()
    {
        float distance = DistanceToPlayer();

        if (distance > stats.attackRange)
        {
            MoveToPlayer();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            attack.TryAttack();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 direction =
            (player.position - transform.position).normalized;

        rb.linearVelocity = direction * stats.moveSpeed;
    }
}
