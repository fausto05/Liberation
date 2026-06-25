using UnityEngine;

public class BossSlamState : BossState
{
    private bool attacked;

    public BossSlamState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.animator?.SetTrigger("Slam");
        boss.agent.ResetPath();
    }
}
