using UnityEngine;
using System;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.7f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage = 25;
    [SerializeField] private float attackCooldown = 0.5f;

    public event Action OnHitConnected;

    private PlayerMovement playerMovement;
    private Animator animator;
    private float nextAttackTime;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void TryAttack()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + attackCooldown;

        animator.SetTrigger("MeleeAttack");
    }

    
    public void Attack()
    {
        Vector2 direction = playerMovement.LastMoveDirection.normalized;

        if (direction == Vector2.zero)
            direction = Vector2.down;

        attackPoint.localPosition = direction * 0.7f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        bool hitConnected = false;

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                hitConnected = true;
            }
        }

        if (hitConnected)
            OnHitConnected?.Invoke();
    }

    public bool HasEnemyInRange()
    {
        Vector2 direction = playerMovement.LastMoveDirection.normalized;

        if (direction == Vector2.zero)
            direction = Vector2.down;

        attackPoint.localPosition = direction * 0.7f;

        Collider2D hit = Physics2D.OverlapCircle(
            attackPoint.position,
            attackRadius,
            enemyLayer
        );

        return hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
