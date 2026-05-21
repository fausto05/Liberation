using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPoolEntry
{
    public GameObject prefab;
    public int initialSize = 5;
}
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] private List<EnemyPoolEntry> entries;
    private Dictionary<GameObject, Queue<EnemyBase>> pools = new();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        foreach (EnemyPoolEntry entry in entries)
        {
            Queue<EnemyBase> queue = new Queue<EnemyBase>();
            for (int i = 0; i < entry.initialSize; i++)
            {
                EnemyBase e = Instantiate(entry.prefab, transform).GetComponent<EnemyBase>();
                e.gameObject.SetActive(false);
                queue.Enqueue(e);
            }
            pools[entry.prefab] = queue;
        }
    }

    public EnemyBase Get(GameObject prefab, Vector2 position)
    {
        if (!pools.ContainsKey(prefab))
        {
            Debug.LogWarning($"No pool for {prefab.name}, creating one.");
            pools[prefab] = new Queue<EnemyBase>();
        }

        Queue<EnemyBase> queue = pools[prefab];
        EnemyBase e = queue.Count > 0 ? queue.Dequeue() : Instantiate(prefab, transform).GetComponent<EnemyBase>();

        e.transform.position = position;
        e.gameObject.SetActive(true);
        e.SetPrefabKey(prefab);
        e.OnSpawn();
        return e;
    }

    public void ReturnToPool(EnemyBase enemy, GameObject prefabKey)
    {
        enemy.gameObject.SetActive(false);
        pools[prefabKey].Enqueue(enemy);
    }
}
