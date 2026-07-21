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
    protected SpriteRenderer spriteRenderer;

    protected int currentHealth;
    protected float lastAttackTime;
    public event System.Action OnDied;
    private GameObject prefabKey;
    private DamageFlash damageFlash;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageFlash = GetComponent<DamageFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        damageFlash?.ResetFlash();
    }

    protected virtual void Update()
    {
        if (player == null) return;

        FlipTowardsPlayer();

        HandleBehavior();
    }

    protected void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected abstract void HandleBehavior();

    public virtual void TakeDamage(int damage)
    {
        damageFlash?.Flash();

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
