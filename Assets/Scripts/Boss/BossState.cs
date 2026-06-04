using UnityEngine;

public abstract class BossState
{
    protected BossController boss;

    public BossState(BossController boss)
    {
        this.boss = boss;
    }

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void Exit() { }
}
