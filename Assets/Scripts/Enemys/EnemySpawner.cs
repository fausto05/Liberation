using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnEntry
{
    public GameObject prefab;
    public float spawnInterval = 3f;
    public int maxActive = 5;
    [HideInInspector] public int activeCount = 0;
    [HideInInspector] public float timer = 0f;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnEntry> spawnEntries;
    [SerializeField] private List<Transform> spawnPoints;

    private int spawnPointIndex = 0;

    private void Update()
    {
        foreach (SpawnEntry entry in spawnEntries)
        {
            if (entry.activeCount >= entry.maxActive) continue;

            entry.timer -= Time.deltaTime;
            if (entry.timer > 0f) continue;

            entry.timer = entry.spawnInterval;
            SpawnEnemy(entry);
        }
    }

    private void SpawnEnemy(SpawnEntry entry)
    {
        Vector2 pos = spawnPoints[spawnPointIndex].position;
        spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Count;

        EnemyBase enemy = EnemyPool.Instance.Get(entry.prefab, pos);
        enemy.OnDied += () => OnEnemyDied(entry, enemy);
        entry.activeCount++;
    }

    private void OnEnemyDied(SpawnEntry entry, EnemyBase enemy)
    {
        entry.activeCount--;
    }
}
