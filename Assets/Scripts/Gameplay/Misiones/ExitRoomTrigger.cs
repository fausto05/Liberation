using UnityEngine;

public class ExitRoomTrigger : MonoBehaviour
{
    [SerializeField] private MissionBase missionToActivate;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (!other.CompareTag("Player"))
            return;

        

        if (MissionManager.Instance.CurrentMission != missionToActivate)
        {
            
            return;
        }

        

        if (triggered)
            return;

        triggered = true;

        
        GameEvents.OnPlayerLeftRoom?.Invoke();
    }
}