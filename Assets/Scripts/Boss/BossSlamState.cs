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

        if (attacked)
            return;

        attacked = true;

        Collider2D playerHit =
            Physics2D.OverlapCircle(
                boss.transform.position,
                boss.slamRadius,
                boss.playerLayer);

        if (playerHit != null)
        {
            IDamageable damageable =
                playerHit.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(
                    boss.slamDamage);
            }
        }

        boss.ResetAttackCounter();

        boss.ChangeState(
            new BossChaseState(boss));
    }
}
