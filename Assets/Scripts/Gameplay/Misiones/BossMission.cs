using UnityEngine;

public class BossMission : MissionBase
{
    [SerializeField] private EnemySpawner bossSpawner;
    [SerializeField] private EnemySpawner supportSpawner;

    public override string MissionName => "Derrotar al jefe";

    public override string GetProgressText()
    {
        return "0/1";
    }

    public override void StartMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandleBossStarted;
        GameEvents.OnPlayerLeftRoom += HandleBossStarted;

        GameEvents.OnBossKilled += HandleBossKilled;

        GameEvents.OnMissionStarted?.Invoke(this);
    }

    private void HandleBossStarted()
    {
        if (MissionManager.Instance.CurrentMission != this)
            return;

        Debug.Log("COMIENZA JEFE");

        bossSpawner.ActivateSpawner();
        supportSpawner.ActivateSpawner();
    }

    private void HandleBossKilled()
    {
        CompleteMission();
    }

    public override void CompleteMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandleBossStarted;
        GameEvents.OnBossKilled -= HandleBossKilled;

        bossSpawner.DeactivateSpawner();
        supportSpawner.DeactivateSpawner();

        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerLeftRoom -= HandleBossStarted;
        GameEvents.OnBossKilled -= HandleBossKilled;
    }
}
