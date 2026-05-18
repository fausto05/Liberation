using UnityEngine;

public class RangedEnemy : EnemyBase
{
    private RangedAttack attack;

    protected override void Awake()
    {
        base.Awake();

        attack = GetComponent<RangedAttack>();
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
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * stats.moveSpeed;
    }


}
