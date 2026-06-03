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
            agent.ResetPath();
            attack.TryAttack();
        }
    }

    private void MoveToPlayer()
    {
        agent.SetDestination(player.position);
    }
}