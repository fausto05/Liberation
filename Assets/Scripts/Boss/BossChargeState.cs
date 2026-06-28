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
        boss.transform.position += (Vector3)(boss.chargeDirection *
         boss.chargeSpeed *
         Time.deltaTime);
    }

    public override void OnWallHit()
    {
        boss.ChangeState(new BossChaseState(boss)); 
    }

    public override void Exit()
    {
        boss.EndCharge();
    }
}
