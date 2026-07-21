using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        
    }

    public void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
            audioSource.PlayOneShot(attackSound);
    }

    public override void TryAttack()
    {
        if (player == null)
            return;

        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            animator.SetTrigger("Attack");

            

            lastAttackTime = Time.time;
        }
    }

    protected override void PerformAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(
        attackPoint.position,
        attackRadius,
        playerLayer);

        if (hit != null)
        {
            hit.GetComponent<IDamageable>()?.TakeDamage(stats.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


    public void OnMeleeHit()
    {
        PerformAttack();
    }
}
