using UnityEngine;

public class BossMeleeState : BossState
{
    private bool isAttacking;

    public BossMeleeState(BossController boss) : base(boss) { }

    public override void Enter()
    {
        boss.StopAgent();
        boss.LookAtPlayer();

        isAttacking = true;
        boss.animator?.SetTrigger("Melee");
    }

    public override void Update()
    {
        boss.StopAgent();
        boss.LookAtPlayer();
    }

    public void FinishAttack()
    {
        isAttacking = false;

        if (boss.GetAttackCounter() == 3)
        {
            boss.ChangeState(new BossChargePrepareState(boss));
            return;
        }

        if (boss.GetAttackCounter() == 6)
        {
            boss.ChangeState(new BossSlamPrepareState(boss));
            return;
        }

        float distance = Vector2.Distance(
            boss.transform.position,
            boss.player.position
        );

        if (distance <= boss.stats.attackRange)
            boss.ChangeState(new BossMeleeState(boss));
        else
            boss.ChangeState(new BossChaseState(boss));
    }
}