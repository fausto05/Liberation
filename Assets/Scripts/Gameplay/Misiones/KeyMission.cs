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
        GameEvents.OnKeyCollected -= HandleKeyCollected;
        GameEvents.OnKeyCollected += HandleKeyCollected;

        missionCompleted = false;

        keyObject.SetActive(true);

        GameEvents.OnMissionStarted?.Invoke(this);
    }

    private void HandleKeyCollected(int keyID)
    {
        Debug.Log(
        $"KEY MISSION: {name} | Current = {MissionManager.Instance.CurrentMission.name}");

        if (MissionManager.Instance.CurrentMission != this)
            return;

        if (missionCompleted)
            return;

        missionCompleted = true;

        Debug.Log($"COMPLETANDO {name}");

        GameEvents.OnMissionUpdated?.Invoke(this);

        CompleteMission();
    }

    public override void CompleteMission()
    {
        GameEvents.OnKeyCollected -= HandleKeyCollected;

        MissionManager.Instance.StartNextMission();
    }

    private void OnDestroy()
    {
        GameEvents.OnKeyCollected -= HandleKeyCollected;
    }
}
