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
        Debug.Log($"SUSCRIBIENDO {gameObject.name}");

        GameEvents.OnPlayerLeftRoom -= HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled -= HandleEnemyKilled;


        GameEvents.OnPlayerLeftRoom += HandlePlayerLeftRoom;
        GameEvents.OnEnemyKilled += HandleEnemyKilled;

        currentKills = 0;
        missionCompleted = false;

        GameEvents.OnMissionStarted?.Invoke(this);

        
    }

    private void HandlePlayerLeftRoom()
    {
        Debug.Log($"HANDLE PLAYER LEFT ROOM EN {gameObject.name}");

        if (MissionManager.Instance.CurrentMission != this)
        {
            Debug.Log("NO SOY LA MISION ACTUAL");
            return;
        }

        Debug.Log("ACTIVANDO SPAWNER");

        enemySpawner.ActivateSpawner();
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
