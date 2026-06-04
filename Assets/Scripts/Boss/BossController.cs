using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public EnemyStats stats;

    [HideInInspector] public Transform player;
    [HideInInspector] public NavMeshAgent agent;

    public BossState CurrentState;

    public int currentHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
}
