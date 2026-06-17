using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void TryAttack()
    {
        if (player == null) return;
        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    protected override void PerformAttack()
    {
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(stats.damage);
        }
    }

    public void OnMeleeHit()
    {
        PerformAttack();
    }
}
