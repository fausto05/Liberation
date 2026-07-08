using UnityEngine;

public class BossStunState : BossState
{
    public BossStunState(BossController boss) : base(boss)
    {
    }

    public override void Enter()
    {
        boss.animator?.SetTrigger("Stun");
        boss.StopAgent();
    }

   

    public override void Exit()
    {
        boss.ResumeAgent();
    }
}
