using UnityEngine;

public class MeleeAttack : EnemyAttack
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;

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
