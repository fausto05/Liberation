using UnityEngine;

public class BossMeleeState : BossState
{
    private float nextAttackTime;

    public BossMeleeState(BossController boss)
        : base(boss)
    {
    }

    public override void Enter()
    {
        boss.agent.ResetPath();

        nextAttackTime = 0f;
    }

    public override void Update()
    {
        float distance =
            Vector2.Distance(
                boss.transform.position,
                boss.player.position);

        if (distance > boss.stats.attackRange)
        {
            boss.ChangeState(
                new BossChaseState(boss));

            return;
        }

        if (Time.time >= nextAttackTime)
        {
            Attack();

            nextAttackTime =
                Time.time +
                boss.stats.attackCooldown;
        }
    }

    private void Attack()
    {
        IDamageable damageable =
            boss.player.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(
                boss.stats.damage);
        }
    }
}