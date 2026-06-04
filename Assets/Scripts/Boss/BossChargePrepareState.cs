using UnityEngine;

public class BossChargePrepareState : BossState
{
    float timer;

    public BossChargePrepareState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.animator?.SetTrigger("ChargePrepare");

        timer = boss.chargePrepareTime;

        boss.agent.ResetPath();

        boss.chargeDirection =
            (boss.player.position - boss.transform.position)
            .normalized;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            boss.ChangeState(
                new BossChargeState(boss));
        }
    }
}
