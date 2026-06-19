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

        bool pickedUp = OnPickup(other.gameObject);

        if (!pickedUp)
            return;

        collected = true;

        gameObject.SetActive(false);
    }

    protected abstract bool OnPickup(GameObject player);
}
