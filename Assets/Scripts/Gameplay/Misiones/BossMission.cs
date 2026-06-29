using UnityEngine;

public class BossMission : MissionBase
{
    [SerializeField] private BossSpawner bossSpawner;
    [SerializeField] private EnemySpawner supportSpawner;

    public override string MissionName => "Derrotar al jefe";

    private bool bossSpawned;

    public override string GetProgressText()
    {
        return "0/1";
    }

    public override void StartMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandleBossStart;
        GameEvents.OnPlayerLeftRoom += HandleBossStart;

        GameEvents.OnBossKilled -= HandleBossKilled;
        GameEvents.OnBossKilled += HandleBossKilled;

        GameEvents.OnMissionStarted?.Invoke(this);
    }

    private void HandleBossStart()
    {
        if (bossSpawned)
            return;

        bossSpawned = true;

        if (MissionManager.Instance.CurrentMission != this)
            return;

        Debug.Log("BOSS FIGHT START");

        if (bossSpawner != null)
            bossSpawner.SpawnBoss();

        if (supportSpawner != null)
            supportSpawner.ActivateSpawner();
    }

    private void HandleBossKilled()
    {
        CompleteMission();
    }

    public override void CompleteMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandleBossStart;
        GameEvents.OnBossKilled -= HandleBossKilled;

        if (supportSpawner != null)
            supportSpawner.DeactivateSpawner();

        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerLeftRoom -= HandleBossStart;
        GameEvents.OnBossKilled -= HandleBossKilled;
    }

    public void ForceStartBoss()
    {
        HandleBossStart();
    }
}
