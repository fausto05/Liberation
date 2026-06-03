using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifetime = 3f;
    private Vector2 direction;
    private float timer;
    private int damage;
    private Projectile prefabKey;

    public void Initialize(Vector2 dir, Projectile key)
    {
        direction = dir.normalized;
        timer = 0f;
        prefabKey = key;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifetime)
            ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            ReturnToPool();
            return;
        }

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        ProjectilePool.Instance.ReturnToPool(this, prefabKey);
    }
}
