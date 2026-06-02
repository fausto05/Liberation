using UnityEngine;

public class FirstMission : MissionBase
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int requiredKills = 1;

    private int currentKills;
    private bool missionCompleted;

    public override string MissionName => "Eliminar enemigos";

    public override string GetProgressText()
    {
        return $"{currentKills}/{requiredKills}";
    }

    public override void StartMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;


        GameEvents.OnPlayerLeftRoom += HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled += HandleEnemyKilled;

        currentKills = 0;
        missionCompleted = false;

        GameEvents.OnMissionStarted?.Invoke(this);

        Debug.Log("Mision iniciada: eliminar enemigos");
    }

    private void HandlePlayerLeftRoom()
    {
        enemySpawner.ActivateSpawner();
    }

    private void HandleEnemyKilled()
    {
        if (missionCompleted)
            return;

        currentKills++;

        GameEvents.OnMissionUpdated?.Invoke(this);

        if (currentKills >= requiredKills)
        {
            missionCompleted = true;

            enemySpawner.DeactivateSpawner();

            CompleteMission();
        }
    }

    public override void CompleteMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;

        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;
    }
}
