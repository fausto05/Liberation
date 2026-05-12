using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] protected EnemyStats stats;

    [Header("References")]
    protected Transform player;
    protected Rigidbody2D rb;

    protected int currentHealth;
    protected float lastAttackTime;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = stats.maxHealth;
    }

    protected virtual void Update()
    {
        if (player == null) return;

        HandleBehavior();
    }

    protected abstract void HandleBehavior();

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }
}
