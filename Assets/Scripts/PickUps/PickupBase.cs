using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        OnPickup(other.gameObject);

        gameObject.SetActive(false);
    }

    protected abstract void OnPickup(GameObject player);
}
