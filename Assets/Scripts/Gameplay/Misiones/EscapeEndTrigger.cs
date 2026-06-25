using UnityEngine;

public class EscapeEndTrigger : MonoBehaviour
{
    [SerializeField] private EscapeMission mission;
    [SerializeField] private GameObject closingDoor;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (MissionManager.Instance.CurrentMission != mission)
            return;

        triggered = true;

        if (closingDoor != null)
            closingDoor.SetActive(true);

        mission.CompleteMission();
    }
}
