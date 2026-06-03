using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private float autoAimRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage = 10;
    [SerializeField] private float minRange = 1f;

    private PlayerMovement playerMovement;

    private float nextFireTime;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public bool HasTargetInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, autoAimRadius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist >= minRange) 
                return true;
        }

        return false;
    }

    public void TryFire()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireRate;

        Fire();
    }

    private void Fire()
    {
        Vector2 direction = GetAimDirection();

        Projectile projectile =
            ProjectilePool.Instance.Get(projectilePrefab, firePoint.position, direction);

        projectile.SetDamage(damage);
    }

    private Vector2 GetAimDirection()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, autoAimRadius, enemyLayer);

        if (hits.Length == 0)
            return playerMovement.LastMoveDirection;

        Collider2D closest = null;
        float minDist = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            float dist =
                Vector2.Distance(transform.position, hit.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = hit;
            }
        }

        return (closest.transform.position - firePoint.position).normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, autoAimRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, minRange);
    }
}
