using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public EnemyStats stats;

    [Header("Melee Attack")]
    public float meleeHitRange = 1.5f;
    public Vector2 meleeHitOffset = Vector2.zero;

    [Header("Charge Attack")]
    public float chargeSpeed = 10f;
    public float chargeDuration = 1f;
    public float chargePrepareTime = 1f;

    [Header("Slam Attack")]
    public float slamPrepareTime = 1f;
    public float slamRadius = 3f;
    public int slamDamage = 20;

    [HideInInspector] public Transform player;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Vector2 chargeDirection;
    [HideInInspector] public Animator animator;

    public BossState CurrentState;

    public int currentHealth;

    private int attackCounter = 0;

    private bool isCharging;

    public LayerMask playerLayer;

    private SpriteRenderer spriteRenderer;

    private bool isDead;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = stats.maxHealth;

        ChangeState(new BossChaseState(this));
    }

    private void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(BossState newState)
    {
        Debug.Log("Estado: " + newState.GetType().Name);

        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState.Enter();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            ChangeState(new BossDeathState(this));
        }
    }

    public void RegisterAttack()
    {
        attackCounter++;
    }

    public int GetAttackCounter()
    {
        return attackCounter;
    }

    public void StartCharge()
    {
        isCharging = true;
    }

    public void EndCharge()
    {
        isCharging = false;
    }

    public void OnMeleeHit()
    {
        Vector2 hitCenter = (Vector2)transform.position + meleeHitOffset;

        Collider2D playerHit = Physics2D.OverlapCircle(
            hitCenter,
            meleeHitRange,
            playerLayer
        );

        if (playerHit != null)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.TakeDamage(stats.damage);
        }

        RegisterAttack();
    }

    public void OnMeleeAnimationFinished()
    {
        if (CurrentState is BossMeleeState meleeState)
        {
            meleeState.FinishAttack();
        }
    }
    public void OnSlamHit()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(
        transform.position,
        slamRadius,
        playerLayer
    );

        if (playerHit != null)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.TakeDamage(slamDamage);
        }
    }

    public void OnSlamAnimationFinished()
    {
        ResetAttackCounter();
        ChangeState(new BossChaseState(this));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCharging)
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable damageable =
                collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(stats.damage);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            CurrentState?.OnWallHit();
        }
    }

    public void ResetAttackCounter()
    {
        attackCounter = 0;
    }

    public void BossKilled()
    {
        GameEvents.OnBossKilled?.Invoke();
    }

    public void LookAtPlayer()
    {
        if (player == null)
            return;

        spriteRenderer.flipX =
            player.position.x < transform.position.x;
    }

    public void StopAgent()
    {
        if (agent == null || !agent.enabled)
            return;

        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
    }

    public void ResumeAgent()
    {
        if (agent == null || !agent.enabled)
            return;

        agent.isStopped = false;
    }

    public void OnDeathAnimationFinished()
    {
        BossKilled();
    }

    public void OnStunAnimationFinished()
    {
        
        ChangeState(new BossChaseState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 hitCenter = (Vector2)transform.position + meleeHitOffset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitCenter, meleeHitRange);

        if (stats != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        }
    }
}
