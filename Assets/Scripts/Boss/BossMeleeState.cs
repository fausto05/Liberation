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

        boss.animator?.SetTrigger("Melee");
    }

    public override void Update()
    {
        float distance = Vector2.Distance(boss.transform.position, boss.player.position);
        if (distance > boss.stats.attackRange)
        {
            boss.ChangeState(new BossChaseState(boss));
        }
    }

    
}