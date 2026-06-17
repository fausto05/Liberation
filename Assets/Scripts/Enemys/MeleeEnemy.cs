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
            animator.SetBool("isMoving", true);
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("isMoving", false);
            attack.TryAttack();
        }
    }

    private void MoveToPlayer()
    {
        agent.SetDestination(player.position);
    }
}
