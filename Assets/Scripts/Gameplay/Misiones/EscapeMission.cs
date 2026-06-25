using UnityEngine;

public class EscapeMission : MissionBase
{
    [SerializeField] private EnemySpawner enemySpawner;

    public override string MissionName => "Escapar";

    public override string GetProgressText()
    {
        return "Llega a la salida";
    }

    public override void StartMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandleEscapeStarted;
        GameEvents.OnPlayerLeftRoom += HandleEscapeStarted;

        GameEvents.OnMissionStarted?.Invoke(this);
    }

    private void HandleEscapeStarted()
    {
        if (MissionManager.Instance.CurrentMission != this)
            return;

        if (enemySpawner != null)
        {
            Debug.Log("INICIANDO ESCAPE");
            enemySpawner.ActivateSpawner();
        }
    }

    public override void CompleteMission()
    {
        GameEvents.OnPlayerLeftRoom -= HandleEscapeStarted;

        if (enemySpawner != null)
            enemySpawner.DeactivateSpawner();

        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerLeftRoom -= HandleEscapeStarted;
    }
}
