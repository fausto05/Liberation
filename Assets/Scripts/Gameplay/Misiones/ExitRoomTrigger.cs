using UnityEngine;

public class ExitRoomTrigger : MonoBehaviour
{
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        GameEvents.OnPlayerLeftRoom?.Invoke();
    }
}
