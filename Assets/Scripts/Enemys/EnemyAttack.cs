using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour

{
    [Header("Attack Settings")]
    [SerializeField] protected EnemyStats stats;

    protected Transform player;
    protected float lastAttackTime;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void TryAttack()
    {
        if (player == null) return;

        if (Time.time >= lastAttackTime + stats.attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    protected abstract void PerformAttack();
}
