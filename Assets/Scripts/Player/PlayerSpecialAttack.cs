using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    [SerializeField] private int hitsRequired = 3;
    [SerializeField] private float specialRadius = 3f;   
    [SerializeField] private int specialDamage = 50;     
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private SpecialButtonUI specialButtonUI;

    private int hitCounter = 0;
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    public bool IsReady => hitCounter >= hitsRequired;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void Start()
    {
        specialButtonUI.UpdateCharge(0f);
    }


    public void RegisterHit()
    {
        if (IsReady)
            return;

        hitCounter++;

        float progress =
            (float)hitCounter / hitsRequired;

        specialButtonUI.UpdateCharge(progress);

        Debug.Log($"Hits hacia especial: {hitCounter}/{hitsRequired}");
    }

    public void TryUseSpecial()
    {
        if (playerHealth.IsDead)
            return;

        if (!IsReady) return;
        playerMovement.CanMove = false;
        animator.SetTrigger("SpecialAttack");
        hitCounter = 0;
        specialButtonUI.UpdateCharge(0f);
    }

    public void OnSpecialHit()
    {
        ExecuteSpecial();
    }

    private void ExecuteSpecial()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
             transform.position,
             specialRadius,
             enemyLayer
         );

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.TakeDamage(specialDamage);
        }

        Debug.Log($"Especial ejecutado: {hits.Length} enemigos golpeados.");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, specialRadius);
    }

    
}
