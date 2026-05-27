using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Collider2D doorCollider;

    private bool isUnlocked;

    private void OnEnable()
    {
        GameEvents.OnKeyCollected += UnlockDoor;
    }

    private void OnDisable()
    {
        GameEvents.OnKeyCollected -= UnlockDoor;
    }

    private void UnlockDoor()
    {
        if (isUnlocked)
            return;

        isUnlocked = true;

        doorCollider.enabled = false;

        Debug.Log("Puerta desbloqueada");
    }
}
