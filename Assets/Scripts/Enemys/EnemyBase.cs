using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] protected EnemyStats stats;

    [Header("References")]
    protected Transform player;
    protected Rigidbody2D rb;
    protected NavMeshAgent agent;
    protected Animator animator;

    protected int currentHealth;
    protected float lastAttackTime;
    public event System.Action OnDied;
    private GameObject prefabKey;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    protected virtual void Start()
    {
        
    }
    public virtual void OnSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = stats.maxHealth;

        if (agent != null)
        {
            agent.speed = stats.moveSpeed;
        }
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
    public void SetPrefabKey(GameObject key)
    {
        prefabKey = key;
    }

    protected virtual void Die()
    {
        OnDied?.Invoke();

        GameEvents.OnEnemyKilled?.Invoke();

        OnDied = null;

        EnemyPool.Instance.ReturnToPool(this, prefabKey);
    }

    protected float DistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }
}
