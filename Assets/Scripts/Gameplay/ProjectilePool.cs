using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectilePoolEntry
{
    public Projectile prefab;
    public int initialSize = 10;
}

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance { get; private set; }

    [SerializeField] private List<ProjectilePoolEntry> entries;
    private Dictionary<Projectile, Queue<Projectile>> pools = new();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        foreach (ProjectilePoolEntry entry in entries)
        {
            Queue<Projectile> queue = new Queue<Projectile>();
            for (int i = 0; i < entry.initialSize; i++)
            {
                Projectile p = Instantiate(entry.prefab, transform);
                p.gameObject.SetActive(false);
                queue.Enqueue(p);
            }
            pools[entry.prefab] = queue;
        }
    }

    public Projectile Get(Projectile prefab, Vector2 position, Vector2 direction)
    {
        if (!pools.ContainsKey(prefab))
        {
            Debug.LogWarning($"No pool found for prefab {prefab.name}, creating one.");
            pools[prefab] = new Queue<Projectile>();
        }

        Queue<Projectile> queue = pools[prefab];
        Projectile p = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab, transform);
        p.transform.position = position;
        p.gameObject.SetActive(true);
        p.Initialize(direction, prefab);
        return p;
    }

    public void ReturnToPool(Projectile p, Projectile prefabKey)
    {
        p.gameObject.SetActive(false);
        pools[prefabKey].Enqueue(p);
    }
}
