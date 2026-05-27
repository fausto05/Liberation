using UnityEngine;

public class KeyMission : MissionBase
{
    [SerializeField] private GameObject keyObject;

    private bool missionCompleted;

    public override void StartMission()
    {
        GameEvents.OnKeyCollected += HandleKeyCollected;

        keyObject.SetActive(true);

        Debug.Log("Mision iniciada: recoger la llave");
    }

    private void HandleKeyCollected()
    {
        if (missionCompleted)
            return;

        missionCompleted = true;

        CompleteMission();
    }

    public override void CompleteMission()
    {
        Debug.Log("Mision completada: llave recogida");

        GameEvents.OnKeyCollected -= HandleKeyCollected;

        MissionManager.Instance.StartNextMission();
    }
}
