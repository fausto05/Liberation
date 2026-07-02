using UnityEngine;

public class BossMeleeState : BossState
{
    private float nextAttackTime;

    public BossMeleeState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.agent.ResetPath();
        boss.LookAtPlayer();

        Attack();
    }

    public override void Update()
    {
        float distance = Vector2.Distance(boss.transform.position, boss.player.position);

        if (distance > boss.stats.attackRange)
        {
            boss.ChangeState(new BossChaseState(boss));
            return;
        }

        boss.LookAtPlayer();

        if (Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    private void Attack()
    {
        boss.animator?.SetTrigger("Melee");
        nextAttackTime = Time.time + boss.stats.attackCooldown;
    }
}