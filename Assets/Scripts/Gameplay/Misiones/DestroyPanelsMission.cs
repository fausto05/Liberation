using UnityEngine;

public class DestroyPanelsMission : MissionBase
{
    [SerializeField] private int requiredPanels = 2;
    [SerializeField] private GameObject door;

    private int currentPanels;
    private bool missionCompleted;

    public override string MissionName => "Destruir paneles";

    public override string GetProgressText()
    {
        return $"{currentPanels}/{requiredPanels}";
    }

    public override void StartMission()
    {
        currentPanels = 0;
        missionCompleted = false;

        GameEvents.OnPanelDestroyed += HandlePanelDestroyed;

        GameEvents.OnMissionStarted?.Invoke(this);
    }

    private void HandlePanelDestroyed()
    {
        if (MissionManager.Instance.CurrentMission != this)
            return;

        if (missionCompleted)
            return;

        currentPanels++;

        GameEvents.OnMissionUpdated?.Invoke(this);

        if (currentPanels >= requiredPanels)
        {
            missionCompleted = true;
            CompleteMission();
        }
    }

    public override void CompleteMission()
    {
        GameEvents.OnPanelDestroyed -= HandlePanelDestroyed;

        door.SetActive(false);
        
        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnPanelDestroyed -= HandlePanelDestroyed;
    }
}
