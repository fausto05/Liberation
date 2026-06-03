using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    [SerializeField] private int hitsRequired = 5;
    [SerializeField] private float specialRadius = 3f;   
    [SerializeField] private int specialDamage = 50;     
    [SerializeField] private LayerMask enemyLayer;       

    private int hitCounter = 0;
    public bool IsReady => hitCounter >= hitsRequired;

    
    public void RegisterHit()
    {
        if (IsReady) return; 

        hitCounter++;
        Debug.Log($"Hits hacia especial: {hitCounter}/{hitsRequired}");
    }

    public void TryUseSpecial()
    {
        if (!IsReady)
        {
            Debug.Log("Especial no cargado aún.");
            return;
        }

        ExecuteSpecial();
        hitCounter = 0;
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
