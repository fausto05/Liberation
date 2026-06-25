using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredKeyID;
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

    private void UnlockDoor(int keyID)
    {
        if (isUnlocked)
            return;

        if (keyID != requiredKeyID)
            return;

        isUnlocked = true;

        gameObject.SetActive(false);

        Debug.Log($"Puerta {requiredKeyID} desbloqueada");
    }
}
