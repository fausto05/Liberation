using UnityEngine;

public class BossSlamState : BossState
{
    public BossSlamState(BossController boss) : base(boss) { }

    public override void Enter()
    {
        boss.StopAgent();
        boss.LookAtPlayer();
        boss.animator?.SetTrigger("Slam");
    }

    public override void Update()
    {
        boss.StopAgent();
    }
}
