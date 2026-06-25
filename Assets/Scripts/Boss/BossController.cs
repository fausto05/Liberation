using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public EnemyStats stats;

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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
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
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
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
        IDamageable damageable = player.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.TakeDamage(stats.damage);

        RegisterAttack();
        if (GetAttackCounter() == 3)
        {
            ChangeState(new BossChargePrepareState(this));
            return;
        }
        if (GetAttackCounter() == 6)
        {
            ChangeState(new BossSlamPrepareState(this));
            return;
        }
    }
    public void OnSlamHit()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(
            transform.position, slamRadius, playerLayer);
        if (playerHit != null)
        {
            IDamageable damageable = playerHit.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.TakeDamage(slamDamage);
        }
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
    }

    public void ResetAttackCounter()
    {
        attackCounter = 0;
    }

    public void BossKilled()
    {
        GameEvents.OnBossKilled?.Invoke();
    }
}
