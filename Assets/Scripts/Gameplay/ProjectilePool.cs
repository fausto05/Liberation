using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [SerializeField] private Projectile prefab;
    [SerializeField] private int initialSize = 20;

    private Queue<Projectile> pool = new Queue<Projectile>();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        for (int i = 0; i < initialSize; i++)
            CreateProjectile();
    }

    private Projectile CreateProjectile()
    {
        Projectile p = Instantiate(prefab, transform);
        p.gameObject.SetActive(false);
        pool.Enqueue(p);
        return p;
    }

    public Projectile Get(Vector2 position, Vector2 direction)
    {
        Projectile p = pool.Count > 0 ? pool.Dequeue() : CreateProjectile();
        p.transform.position = position;
        p.gameObject.SetActive(true);
        p.Initialize(direction);
        return p;
    }

    public void ReturnToPool(Projectile p)
    {
        p.gameObject.SetActive(false);
        pool.Enqueue(p);
    }
}
