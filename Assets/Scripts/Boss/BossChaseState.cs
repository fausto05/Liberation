using UnityEngine;

public class BossChaseState : BossState
{
    public BossChaseState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.animator?.SetBool("IsMoving", true);
    }

    public override void Exit()
    {
        boss.animator?.SetBool("IsMoving", false);
    }

    public override void Update()
    {
        float distance =
            Vector2.Distance(
                boss.transform.position,
                boss.player.position);

        boss.agent.SetDestination(boss.player.position);

        if (distance <= boss.stats.attackRange)
        {
            boss.ChangeState(
                new BossMeleeState(boss));
        }
    }
}
