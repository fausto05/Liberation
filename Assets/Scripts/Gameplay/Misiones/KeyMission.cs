using UnityEngine;

public class KeyMission : MissionBase
{
    [SerializeField] private GameObject keyObject;

    private bool missionCompleted;

    public override string MissionName => "Recoger la llave";

    public override string GetProgressText()
    {
        return missionCompleted ? "1/1" : "0/1";
    }

    public override void StartMission()
    {
        GameEvents.OnKeyCollected += HandleKeyCollected;

        missionCompleted = false;

        keyObject.SetActive(true);

        GameEvents.OnMissionStarted?.Invoke(this);

        
    }

    private void HandleKeyCollected(int keyID)
    {
        if (missionCompleted)
            return;

        missionCompleted = true;

        GameEvents.OnMissionUpdated?.Invoke(this);

        CompleteMission();
    }

    public override void CompleteMission()
    {
        GameEvents.OnKeyCollected -= HandleKeyCollected;

        MissionManager.Instance.StartNextMission();
    }
}
