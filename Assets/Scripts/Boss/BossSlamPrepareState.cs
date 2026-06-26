using UnityEngine;

public class BossSlamPrepareState : BossState
{
    private float timer;

    public BossSlamPrepareState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.LookAtPlayer();

        boss.animator?.SetTrigger("SlamPrepare");

        timer = boss.slamPrepareTime;

        boss.agent.ResetPath();
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            boss.ChangeState(
                new BossSlamState(boss));
        }
    }
}
