using UnityEngine;

public class BossChargeState : BossState
{
    private float timer;

    public BossChargeState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.animator?.SetTrigger("Charge");

        timer = boss.chargeDuration;

        boss.agent.ResetPath();

        boss.StartCharge();
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        boss.transform.position +=
            (Vector3)(boss.chargeDirection *
            boss.chargeSpeed *
            Time.deltaTime);

        if (timer <= 0f)
        {
            boss.ChangeState(
                new BossChaseState(boss));
        }
    }

    public override void Exit()
    {
        boss.EndCharge();
    }
}
