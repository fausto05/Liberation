using UnityEngine;

public class KillEnemiesMission : MissionBase
{
    [SerializeField] private string missionName = "Eliminar enemigos";
    [SerializeField] private int requiredKills = 1;
    [SerializeField] private EnemySpawner enemySpawner;
    

    private int currentKills;
    private bool missionCompleted;

    public override string MissionName => missionName;

    public override string GetProgressText()
    {
        return $"{currentKills}/{requiredKills}";
    }

    public override void StartMission()
    {
        currentKills = 0;
        missionCompleted = false;

        if (enemySpawner != null)
        {
            Debug.Log($"Activando spawner: {enemySpawner.name}");
            GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
            GameEvents.OnPlayerLeftRoom += HandlePlayerLeftRoom;
        }
        else
        {
            Debug.LogError($"No hay EnemySpawner asignado en {name}");
        }

        GameEvents.OnEnemyKilled += HandleEnemyKilled;

        GameEvents.OnMissionStarted?.Invoke(this);
    }

    private void HandleEnemyKilled()
    {
        if (MissionManager.Instance.CurrentMission != this)
            return;

        if (missionCompleted)
            return;

        currentKills++;

        GameEvents.OnMissionUpdated?.Invoke(this);

        if (currentKills >= requiredKills)
        {
            missionCompleted = true;
            CompleteMission();
        }
    }

    public override void CompleteMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;

        if (enemySpawner != null)
            enemySpawner.DeactivateSpawner();

        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void HandlePlayerLeftRoom()
    {
        if (MissionManager.Instance.CurrentMission != this)
            return;

        if (enemySpawner != null)
        {
            Debug.Log($"Activando spawner: {enemySpawner.name}");
            enemySpawner.ActivateSpawner();
        }
    }
}
