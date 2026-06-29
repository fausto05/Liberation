using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] spawnPoints;

    private void Awake()
    {
        SaveData data = SaveSystem.Load();

        int checkpoint = data.checkpointID;

        if (checkpoint >= spawnPoints.Length)
            checkpoint = 0;

        player.position =
            spawnPoints[checkpoint].position;
    }
}
