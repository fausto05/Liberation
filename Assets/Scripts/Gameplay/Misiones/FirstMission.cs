using UnityEngine;

public class FirstMission : MissionBase
{
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private int requiredKills = 1;

    private int currentKills;

    private bool missionCompleted;

    public override void StartMission()
    {
        GameEvents.OnPlayerLeftRoom += HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled += HandleEnemyKilled;

        currentKills = 0;

        Debug.Log("Mision iniciada: eliminar enemigos");
    }

    private void HandlePlayerLeftRoom()
    {
        Debug.Log("Jugador salio de la sala");

        enemySpawner.ActivateSpawner();
    }

    private void HandleEnemyKilled()
    {
        if (missionCompleted)
            return;

        currentKills++;

        Debug.Log($"Kills: {currentKills}/{requiredKills}");

        if (currentKills >= requiredKills)
        {
            missionCompleted = true;

            enemySpawner.DeactivateSpawner();

            CompleteMission();
        }
    }

    public override void CompleteMission()
    {
        Debug.Log("Mision completada");

        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;

        MissionManager.Instance.StartNextMission();
    }
}
